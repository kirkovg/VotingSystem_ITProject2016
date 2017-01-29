using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class UserVote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else if (Session["userID"] != null && Session["userID"].ToString() == "admin@votex.com")
        {
            Response.Redirect("~/admin_pages/AdminPanel.aspx");
        }
        else
        {
            lblUserID.Text = Session["userID"].ToString();
        }

        // display active elections
        pnlElection.Visible = false;
        getActiveElection();
        getParticipatingParties();
        if (DateTime.Now < getStartingDate() || DateTime.Now > getEndingDate())
        {
            pnlElection.Visible = false;
            lblMessage.Text = "There are no ongoing elections.";
        }
        else if (checkActiveElections() == true)
        {
            if (checkVoterParticipation() == true)
            {
                lblMessage.Text = "You have already voted in the last active election.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                pnlElection.Visible = false;
            }
            else
            {
                pnlElection.Visible = true;
            }
        }
        else
        {
            pnlElection.Visible = false;
            lblMessage.Text = "There are no ongoing elections.";
        }
    }

    /// <summary>
    /// Log out the user and kill the active session
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }

    /// <summary>
    /// Checks if a voter has already voted in a certain active election
    /// </summary>
    /// <returns> true if he has already voted, else false</returns>
    protected bool checkVoterParticipation()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT VoterID FROM VotingInfo WHERE VoterID=@VoterID AND ElectionID=@ElectionID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@VoterID", Session["userID"].ToString());
        command.Parameters.AddWithValue("@ElectionID", Convert.ToInt32(ViewState["ElectionID"]));
        SqlDataReader reader;

        string tmpVoterId = null;
        
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                tmpVoterId = reader[0].ToString();
            }
            reader.Close();
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

        if (tmpVoterId != null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets all the Active Elections from the Elections table
    /// </summary>
    /// <returns> true if there is a single active election, else false </returns>
    protected bool checkActiveElections()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ActiveElection FROM Elections";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;
        bool activeElectionFlag = false;
        int result = 0;
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                // if ActiveElection is true, then set flag to true
                result = Convert.ToInt32(reader["ActiveElection"]);
                if (result == 1)
                {
                    activeElectionFlag = true;
                    break;
                }
            }

            reader.Close();
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

        if (activeElectionFlag == true)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// get the active election from the database and attach the information to the page
    /// </summary>
    protected void getActiveElection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT * FROM Elections WHERE ActiveElection=@ActiveElection";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;


        command.Parameters.AddWithValue("@ActiveElection", 1);
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                ViewState["ElectionID"] = reader["ID"];
                lblDateFrom.Text = reader["DateFrom"].ToString();
                lblDateTo.Text = reader["DateTo"].ToString();
                lblElectionName.Text = reader["ElectionName"].ToString();
                lblElectionDescr.Text = reader["ElectionDescription"].ToString();

            }
            reader.Close();
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// get the info on the participating parties in the active election
    /// </summary>
    protected void getParticipatingParties()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ID,PartyName,NumberOfMembers,ImageLogo FROM Participations INNER JOIN Parties ON Participations.PartyID = Parties.ID WHERE ElectionID = @ElectionID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;

        command.Parameters.AddWithValue("@ElectionID", Convert.ToInt32(ViewState["ElectionID"]));

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                //ListItem lt = new ListItem();
                //lt.Text = reader["PartyName"].ToString();
                //lt.Value = reader["ID"].ToString();

                //rblParties.Items.Add(lt);

                byte[] bytes = (byte[])reader["ImageLogo"];
                string partyName = reader["PartyName"].ToString();
                string partyId = reader["ID"].ToString();
                string numMembers = reader["NumberOfMembers"].ToString();
                string strBase64 = Convert.ToBase64String(bytes);
                Image img = new Image();
                img.ImageUrl = "data:Image/png;base64," + strBase64;

                rblParties.Items.Add
                (
                    new ListItem(string.Format("Party Name: {0},<br/> Number of Members: {1}, <br/> <img src='{2}' width='100' height='100'/>",
                                partyName,
                                numMembers,
                                img.ImageUrl)
                    , partyId)
                );
            }
            reader.Close();
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

        rblParties.Visible = true;
    }

    /// <summary>
    /// saving vote of a user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "INSERT INTO VotingInfo VALUES(@VoterID,@PartyID,@ElectionID,@VoteDate)";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@VoterID", Session["userID"].ToString());
        command.Parameters.AddWithValue("@PartyID", Convert.ToInt32(rblParties.SelectedItem.Value));
        command.Parameters.AddWithValue("@ElectionID",Convert.ToInt32(ViewState["ElectionID"]));
        command.Parameters.AddWithValue("@VoteDate",DateTime.Now);
        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            //lblMessage.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }

        pnlElection.Visible = false;
        lblMessage.Visible = true;
        lblMessage.Text = "Your vote has been saved.";
        lblMessage.ForeColor = System.Drawing.Color.Green;
    }

    /// <summary>
    /// Overriding event to display images in radio button list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblParties_DataBound(object sender, EventArgs e)
    {
        getParticipatingParties();
    }


    /// <summary>
    /// gets the starting date of the active election
    /// </summary>
    /// <returns>the date if found, DateTime.MinValue if not</returns>
    protected DateTime getStartingDate()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT DateFrom FROM Elections WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["ElectionID"]));
        SqlDataReader reader;

        DateTime tmp = DateTime.MinValue;

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                tmp = Convert.ToDateTime(reader["DateFrom"]);
            }
            reader.Close();
        }
        catch (Exception err)
        {
            //lblMessage.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }

        return tmp;

    }

    /// <summary>
    /// gets the ending date of the active election
    /// </summary>
    /// <returns>the date if found, DateTime.MinValue if not</returns>
    protected DateTime getEndingDate()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT DateTo FROM Elections WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["ElectionID"]));
        SqlDataReader reader;

        DateTime tmp = DateTime.MinValue;

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                tmp = Convert.ToDateTime(reader["DateTo"]);
            }
            reader.Close();
        }
        catch (Exception err)
        {
            //lblMessage.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }

        return tmp;

    }

    protected void btnLinkPasswordRecovery_Click(object sender, EventArgs e)
    {
        Response.Redirect("PasswordRecovery.aspx");
    }

    protected void btnLinkUserVote_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserVote.aspx");
    }
}