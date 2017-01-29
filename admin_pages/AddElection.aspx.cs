using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_AddElection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"].ToString() == "admin@votex.com")
        {
            lblUserID.Text = Session["userID"].ToString();
        }
        else if (Session["userID"] != null && Session["userID"].ToString() != "admin@votex.com")
        {
            Response.Redirect("~/UserVote.aspx");
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }


        if (!IsPostBack)
        {
            populateDDLs();
            if (checkActiveElections() == true)
            {
                pnlElections.Visible = false;
                lblMessage.Text = "There is an ongoing election, you cannot add a new one.<br/> "
                    + "To add a new one without waiting, you can end the ongoing election from the Admin Panel.";
            }
            else
            {
                pnlElections.Visible = true;

            }
        }

        pnlPartiesToParticipate.Visible = false;
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
            while(reader.Read())
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
    /// Log out the user and kill the active session
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Login.aspx");
    }


    /// <summary>
    /// populates drop down lists to choose hours and minutes from
    /// </summary>
    protected void populateDDLs()
    {
        for (int i = 0;i < 24; i++)
        {
            if (i < 10)
            {
                ddlHoursFrom.Items.Add("0" + i);
            }
            else
            {
                ddlHoursFrom.Items.Add(i.ToString());
            }
        }


        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlHoursTo.Items.Add("0" + i);
            }
            else
            {
                ddlHoursTo.Items.Add(i.ToString());
            }
        }


        for (int i = 0; i <= 59; i++)
        {
            if (i < 10)
            {
                ddlMinutesFrom.Items.Add("0" + i);
            }
            else
            {
                ddlMinutesFrom.Items.Add(i.ToString());
            }
        }

        for (int i = 0; i <= 59; i++)
        {
            if (i < 10)
            {
                ddlMinutesTo.Items.Add("0" + i);
            }
            else
            {
                ddlMinutesTo.Items.Add(i.ToString());
            }
        }
    }

    /// <summary>
    /// insert an election into the database
    /// </summary>
    /// <returns>true if the insert is succesful</returns>
    protected bool insertElection()
    {
        
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "INSERT INTO Elections VALUES(@DateFrom,@DateTo,@ElectionName,@ElectionDescription,@ActiveElection)";
        SqlCommand command = new SqlCommand(sqlString, conn);
        int result = 0;

        string selectedTime = ddlHoursFrom.SelectedItem.Value +
                ":" + ddlMinutesFrom.SelectedItem.Value + ":00.00";

        if (clndFrom.SelectedDate <= clndTo.SelectedDate && 
            clndFrom.SelectedDate == DateTime.Today && 
            clndTo.SelectedDate >= DateTime.Today &&
            DateTime.Now <= Convert.ToDateTime(selectedTime))
        {
            // "01/08/2008 14:50:50.42" datetime example
            string dateTimeFrom = clndFrom.SelectedDate.ToShortDateString() +
                " " + ddlHoursFrom.SelectedItem.Value +
                ":" + ddlMinutesFrom.SelectedItem.Value + ":00.00";

            string dateTimeTo = clndTo.SelectedDate.ToShortDateString() +
                " " + ddlHoursTo.SelectedItem.Value +
                ": " + ddlMinutesTo.SelectedItem.Value + ":00.00";


            command.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(dateTimeFrom));
            command.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(dateTimeTo));
            command.Parameters.AddWithValue("@ElectionName", txtElecName.Text);
            command.Parameters.AddWithValue("@ElectionDescription", txtElecDescr.Text);
            command.Parameters.AddWithValue("@ActiveElection", 1);
            try
            {
                conn.Open();
                result = command.ExecuteNonQuery();
                //lblMessageError.ForeColor = System.Drawing.Color.Green;
                //lblMessageError.Text = "Election added and started succesfully";
            }
            catch (Exception err)
            {
                //lblMessage.Text = err.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            lblMessageError.ForeColor = System.Drawing.Color.Red;
            lblMessageError.Text = "The starting date is after the ending date or you are selecting a date before today.<br/>"
                + "The starting date must be today.<br/>"
                + "The starting time cannot be before the exact time right now.<br/>"
                + "The starting time cannot be after the ending time.";
        }

        if (result != 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// inserting an election, then selecting parties, then inserting their corresponding IDs to
    /// Participations table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        // repeated try to add an election after already submitting one  
        if (checkActiveElections() == true)
        {
            pnlElections.Visible = false;
            lblMessage.Text = "There is an ongoing election, you cannot add a new one.<br/> "
                + "To add a new one without waiting for it to pass, "+
                "you can end the ongoing election from the Admin Panel.";
        }
        else
        {
            pnlElections.Visible = true;
        }

        // call to function to insert an election
        if (insertElection() == true)
        {
            // after inserting the election select the parties to participate
            pnlElections.Visible = false;
            getParties();
            pnlPartiesToParticipate.Visible = true;
        }
   
    }

    /// <summary>
    /// gets all the parties from the database to insert them into the checkbox list for selection
    /// </summary>
    protected void getParties()
    {
        
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ID,PartyName FROM Parties";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;
        
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListItem lt = new ListItem();
                lt.Text = reader["PartyName"].ToString();
                lt.Value = reader["ID"].ToString();

                chkLstParties.Items.Add(lt);
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
    /// select the active election ID to populate the participations table with the corresponding party IDs
    /// </summary>
    /// <returns>Election ID if exists, else zero</returns>
    protected int selectIdFromActiveElection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ID FROM Elections WHERE ActiveElection=@ActiveElection";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;
        int electionId = 0;


        command.Parameters.AddWithValue("@ActiveElection", 1);
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                electionId = Convert.ToInt32(reader["ID"]);
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


        if (electionId != 0)
        {
            return electionId;
        }
        return 0;
    }

    /// <summary>
    /// inserts party ID with the corresponding election ID into Participations table
    /// </summary>
    /// <param name="partyId"></param>
    protected void insertIntoParticipations(int partyId)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "INSERT INTO Participations VALUES(@PartyID,@ElectionID)";
        SqlCommand command = new SqlCommand(sqlString, conn);


        int electionId = selectIdFromActiveElection();
        command.Parameters.AddWithValue("@PartyID", partyId);
        command.Parameters.AddWithValue("@ElectionID", electionId);


        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            //lblMessageError2.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // populate Participations Table in the database with every party
        
        foreach (ListItem tmpParty in chkLstParties.Items)
        {
            if (tmpParty.Selected)
            {
                insertIntoParticipations(Convert.ToInt32(tmpParty.Value));
            }
        }

        lblMessage.Text = "Corresponding participations saved";
        lblMessage.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnAdminPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminPanel.aspx");
    }

    protected void btnAddParty_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddParty.aspx");
    }

    protected void btnAddElection_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddElection.aspx");
    }

    protected void btnUpdateParty_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateRemoveParty.aspx");
    }

    protected void btnRemoveElection_Click(object sender, EventArgs e)
    {
        Response.Redirect("RemoveElection.aspx");
    }

    protected void btnElectionStatistics_Click(object sender, EventArgs e)
    {
        Response.Redirect("ElectionStatistics.aspx");
    }
}