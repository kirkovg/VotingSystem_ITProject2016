using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_pages_UpdateParty : System.Web.UI.Page
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

        if(!IsPostBack)
        {
            getAllParties();
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
    /// gets the info on all parties
    /// </summary>
    protected void getAllParties()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ID,PartyName,NumberOfMembers FROM Parties";
        SqlCommand command = new SqlCommand(sqlString, conn);

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds);
            gvParties.DataSource = ds;
            gvParties.DataBind();

            gvParties.Visible = true;
            ViewState["PartiesData"] = ds;
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
    /// cancel the edit for the corresponding party
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvParties_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["PartiesData"];
        gvParties.EditIndex = -1;
        gvParties.DataSource = ds;
        gvParties.DataBind();
    }

    /// <summary>
    /// show edit for corresponding party
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvParties_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["PartiesData"];
        gvParties.EditIndex = e.NewEditIndex;
        gvParties.DataSource = ds;
        gvParties.DataBind();
    }

    /// <summary>
    /// update the corresponding party
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvParties_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "UPDATE Parties SET PartyName=@PartyName, NumberOfMembers=@NumberOfMembers WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        int status = 0;

        TextBox tb = (TextBox)gvParties.Rows[e.RowIndex].Cells[1].Controls[0];
        command.Parameters.AddWithValue("@PartyName", tb.Text);

        tb = (TextBox)gvParties.Rows[e.RowIndex].Cells[2].Controls[0];
        command.Parameters.AddWithValue("@NumberOfMembers", Convert.ToInt32(tb.Text));

        string partyId = gvParties.Rows[e.RowIndex].Cells[0].Text;
        command.Parameters.AddWithValue("@ID", Convert.ToInt32(partyId));

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
            getAllParties();
            Response.Redirect(Request.RawUrl);
        }
    }

    /// <summary>
    /// on selected index change delete the corresponding party
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvParties_SelectedIndexChanged(object sender, EventArgs e)
    {
        int partyId = Convert.ToInt32(gvParties.Rows[gvParties.SelectedIndex].Cells[0].Text);

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "DELETE FROM Parties WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        // call to delete all foreign keys in Participations
        deleteFromParticipations(partyId);
        // call to delete all foreign keys in VotingInfo
        deleteFromVotingInfo(partyId);

        command.Parameters.AddWithValue("@ID", partyId);
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
            Response.Redirect(Request.RawUrl);
        }
    }

    /// <summary>
    /// deletes first the foreign keys of PartyID in Participations because of FK constraints
    /// </summary>
    /// <param name="partyId"></param>
    protected void deleteFromParticipations(int partyId)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "DELETE FROM Participations WHERE PartyID=@PartyID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@PartyID", partyId);

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
    ///  deletes first the foreign keys of PartyID in VotingInfo because of FK constraints
    /// </summary>
    /// <param name="partyId"></param>
    protected void deleteFromVotingInfo(int partyId)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "DELETE FROM VotingInfo WHERE PartyID=@PartyID";
        SqlCommand command = new SqlCommand(sqlString, conn);

        command.Parameters.AddWithValue("@PartyID", partyId);

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