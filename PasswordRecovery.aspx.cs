using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class PasswordRecovery : System.Web.UI.Page
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
    /// gets the user password from the logged ID
    /// </summary>
    /// <returns></returns>
    protected string getUserPassword()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT Password FROM Voters WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        command.Parameters.AddWithValue("@ID", Session["userID"].ToString());
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
        catch (Exception err)
        {

        }
        finally
        {
            conn.Close();
        }

        return cmpPassword;
    }

    /// <summary>
    /// makes the change 
    /// </summary>
    protected void changePassword()
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "UPDATE Voters SET Password=@Password WHERE ID=@ID";
        SqlCommand command = new SqlCommand(sqlString, conn);
        command.Parameters.AddWithValue("@Password", txtNewPass.Text);
        command.Parameters.AddWithValue("@ID", Session["userID"].ToString());

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

    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (getUserPassword() == txtOldPass.Text)
        {
            if (txtNewPass.Text.Length >= 5)
            {
                changePassword();
                lblMessage.Text = "Password changed succesfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Your password must have at least 5 characters";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblMessage.Text = "Wrong password";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        
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