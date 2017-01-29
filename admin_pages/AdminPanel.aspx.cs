using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_pages_AdminPanel : System.Web.UI.Page
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

        if (checkActiveElections() == false)
        {
            pnlActiveElection.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "There are no ongoing elections at the moment.<br/>" +
                    "To start/add a new one go to the AddElection page.";
        } 
        else
        {
            getActiveElection();
            getNumberOfParticipatingParties();
            getParticipatingParties();
            tblParties.Visible = true;

        }
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
                lblDateTimeFrom.Text = reader["DateFrom"].ToString();
                lblDateTimeTo.Text = reader["DateTo"].ToString();
                lblElectionName.Text = reader["ElectionName"].ToString();
                lblElectionDescription.Text = reader["ElectionDescription"].ToString();
               
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
    /// get the number of all participating parties for the active election
    /// </summary>
    protected void getNumberOfParticipatingParties()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ElectionID, COUNT(Participations.PartyID) AS Broj" +
                            " FROM Participations" +
                            " WHERE ElectionID = @ElectionID" +
                            " GROUP BY ElectionID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;
        

        command.Parameters.AddWithValue("@ElectionID", Convert.ToInt32(ViewState["ElectionID"]));
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                lblNumParties.Text = reader["Broj"].ToString();
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


        // adding a table header
        TableHeaderRow tHeader = new TableHeaderRow();
        TableHeaderCell[] tHeaderCells = new TableHeaderCell[] {
            new TableHeaderCell
            {
                Text = "Party Name"
            },
            new TableHeaderCell
            {
                Text = "Number of Members"
            },
            new TableHeaderCell
            {
                Text = "Party Logo"
            }
        };

        tHeader.Cells.AddRange(tHeaderCells);
        tblParties.Rows.Add(tHeader);

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] bytes = (byte[])reader["ImageLogo"];
                string partyName = reader["PartyName"].ToString();
                string partyId = reader["ID"].ToString();
                string numMembers = reader["NumberOfMembers"].ToString();
                string strBase64 = Convert.ToBase64String(bytes);
                Image img = new Image();
                img.ImageUrl = "data:Image/png;base64," + strBase64;


                // adding rows to the table
                TableCell[] cells = new TableCell[] {
                      new TableCell
                      {
                          Text = partyName
                      },
                      new TableCell
                      {
                          Text = numMembers
                      },
                      new TableCell
                      {
                          Text = string.Format("<img src='{0}' width='100' height='100'/>", img.ImageUrl)
                      }
                };

                TableRow tr = new TableRow();
                tr.Cells.AddRange(cells);
                tblParties.Rows.Add(tr);
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
    /// stop the election forcefully
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnd_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "UPDATE Elections SET ActiveElection=0 WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;

        command.Parameters.AddWithValue("@ID", Convert.ToInt32(ViewState["ElectionID"]));
        int status = 0;

        try
        {
            conn.Open();
            status = command.ExecuteNonQuery();
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

        if (status != 0)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Election stopped succesfully";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            pnlActiveElection.Visible = false;
        }
    }


    /// <summary>
    /// Overriding event to display images in check box list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bultLstParties_DataBound(object sender, EventArgs e)
    {
        getParticipatingParties();
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