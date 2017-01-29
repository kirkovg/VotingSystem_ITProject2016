using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_ElectionStatistics : System.Web.UI.Page
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
            getWinnerParty();
            getNumberOfPartiesByElection();
            getNumVotesByElection();
            getVotesPerPartyPerElection();
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
    /// gets the winner party and the number of votes for every election
    /// </summary>
    protected void getWinnerParty()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "select ElectionName, PartyName as Winner, NumVotes " + 
                           "from Votes_Party_Election, Elections, Parties " + 
                           "where NumVotes = (select max(NumVotes) from Votes_Party_Election) " + 
	                       "and Votes_Party_Election.ElectionID = Elections.ID " +
                           "and Votes_Party_Election.PartyID = Parties.ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds);
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();

            ViewState["dataset0"] = ds;
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
    /// gets the number of parties by election
    /// </summary>
    protected void getNumberOfPartiesByElection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "select ElectionName, count(PartyID) as NumParties " + 
                           "from Participations, Elections " + 
                           "where Participations.ElectionID = Elections.ID " +
                           "group by ElectionName";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds);
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();

            ViewState["dataset1"] = ds;
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
    /// gets the number of votes of a whole election
    /// </summary>
    protected void getNumVotesByElection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "select ElectionName, count(VoterID) as NumVotes " +
                           "from VotingInfo inner join Elections on VotingInfo.ElectionID = Elections.ID " + 
                           "group by ElectionName";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds);
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();

            ViewState["dataset2"] = ds;
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
    /// gets the number of votes for a party in an election
    /// </summary>
    protected void getVotesPerPartyPerElection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "select NumVotes, PartyName, ElectionName " +
                           "from Parties, Elections, Votes_Party_Election " +
                           "where Votes_Party_Election.ElectionID = Elections.ID and " +
                           "Votes_Party_Election.PartyID = Parties.ID " +
                           "order by ElectionName";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds);
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();

            ViewState["dataset3"] = ds;
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }
    }

    protected void lbOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbOptions.SelectedValue == "0")
        {
            DataSet ds = (DataSet)ViewState["dataset0"];
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();
        }
        else if (lbOptions.SelectedValue == "1")
        {
            DataSet ds = (DataSet)ViewState["dataset1"];
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();
        }
        else if (lbOptions.SelectedValue == "2")
        {
            DataSet ds = (DataSet)ViewState["dataset2"];
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();
        }
        else if (lbOptions.SelectedValue == "3")
        {
            DataSet ds = (DataSet)ViewState["dataset3"];
            gvElectionStats.DataSource = ds;
            gvElectionStats.DataBind();
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