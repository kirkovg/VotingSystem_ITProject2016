using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DateTime electionDateTo = DateTime.MaxValue;
        Application.Lock();
        getActiveElection();
        if (ViewState["ActiveElectionDateTo"] != null)
        {
            electionDateTo = (DateTime)ViewState["ActiveElectionDateTo"];
        }
        if (DateTime.Now > electionDateTo)
        {
            endActiveElecition();
            //lblMessage.Text = "Ended succesfully";
        }
        Application.UnLock();
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="userID"></param>
    /// <returns> password for given User ID </returns>
    /// 
    protected string getLogInData(string userID)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT Password FROM Voters WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        command.Parameters.AddWithValue("@ID", userID);
        SqlDataReader reader;
        string cmpPassword = null;
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read()) 
            {
                cmpPassword = reader[0].ToString();
            }

            reader.Close();
        }
        catch(Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

        return cmpPassword;
    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        string userID = txtUserId.Text;
        string password = txtPassword.Text;

        string cmpPassword = getLogInData(userID);
        if (userID == "admin@votex.com" && password == "adminpass")
        {
            // redirect to admin view
            Session["userID"] = userID;
            Response.Redirect("~/admin_pages/AdminPanel.aspx");
        }
        else if (password == cmpPassword)
        {
            Session["userID"] = userID;
            Response.Redirect("UserVote.aspx");
              
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Wrong User ID or password";
            txtUserId.Text = "";
            txtPassword.Text = "";
        }
    }


    /// <summary>
    /// redirects to SignUp.aspx
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("SignUp.aspx");
    }


    /// <summary>
    /// gets the id and ending date of the active election and sets it to the viewstates
    /// </summary>
    protected void getActiveElection()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT ID,DateTo FROM Elections WHERE ActiveElection=@ActiveElection";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;


        command.Parameters.AddWithValue("@ActiveElection", 1);
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                ViewState["ElectionID"] = reader["ID"].ToString();
                ViewState["ActiveElectionDateTo"] = Convert.ToDateTime(reader["DateTo"]);
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
    /// ends an active election
    /// </summary>
    protected void endActiveElecition()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "UPDATE Elections SET ActiveElection=0 WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;


        command.Parameters.AddWithValue("@ID", ViewState["ElectionID"].ToString());
        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }
    }

    protected void btnLinkRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("VotingRegister.aspx");
    }

    protected void btnLinkSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("SignUp.aspx");
    }

    protected void btnLinkLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}