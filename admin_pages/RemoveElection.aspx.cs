using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class admin_pages_UpdateRemoveElection : System.Web.UI.Page
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
            getElections();
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
        Response.Redirect("~/Login.aspx");
    }

    /// <summary>
    /// Fill the gridview with data from Elections table
    /// </summary>
    protected void getElections()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT * FROM Elections";
        SqlCommand command = new SqlCommand(sqlString,conn);

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds, "Elections");
            gvElections.DataSource = ds;
            gvElections.DataBind();

            gvElections.Visible = true;
            ViewState["ElectionsData"] = ds;
        }
        catch(Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

    }


    /// <summary>
    /// get the Election ID to delete the corresponding row in the Elections table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvElections_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblMessage.Text = "deleted row with id: " + gvElections.Rows[gvElections.SelectedIndex].Cells[0].Text;
        int electionId = Convert.ToInt32(gvElections.Rows[gvElections.SelectedIndex].Cells[0].Text);

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "DELETE FROM Elections WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        // call to delete all foreign keys in Participations
        deleteFromParticipations(electionId);
        deleteFromVotingInfo(electionId);

        command.Parameters.AddWithValue("@ID", electionId);
        int status = 0;

        try
        {
            conn.Open();
            status = command.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }

        if (status != 0)
        {
            //lblMessage.Text = "Succesfully deleted election with id: " + gvElections.Rows[gvElections.SelectedIndex].Cells[0].Text;
            Response.Redirect(Request.RawUrl);
        }


    }

    /// <summary>
    /// deletes first the foreign keys of ElectionID in Participations because of FK constraints
    /// </summary>
    /// <param name="electionId"></param>
    protected void deleteFromParticipations(int electionId)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "DELETE FROM Participations WHERE ElectionID=@ElectionID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@ElectionID", electionId);

        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    ///  deletes first the foreign keys of ElectionID in VotingInfo because of FK constraints
    /// </summary>
    /// <param name="electionId"></param>
    protected void deleteFromVotingInfo(int electionId)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "DELETE FROM VotingInfo WHERE ElectionID=@ElectionID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@ElectionID", electionId);

        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
        }
        finally
        {
            conn.Close();
        }
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
