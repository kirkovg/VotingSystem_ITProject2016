using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userID"] != null && Session["userID"].ToString() == "admin@votex.com")
        {
            Response.Redirect("~/admin_pages/AdminPanel.aspx");
        }

        // initial dates
        if (!IsPostBack)
        {
            // years
            for (int i = 1900; i <= 1999; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }

            // months
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(i.ToString());
            }

            // days
            for (int i = 1; i <= 31; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }

        

    }

    /// <summary>
    /// Gets all SSNs from the Voters table
    /// </summary>
    /// <param name="userSSN"></param>
    /// <returns> true if there is a duplicate SSN, else false </returns>
    protected bool checkDuplicateSSN(string userSSN)
    {
        List<string> lstPasswords = new List<string>();
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT SSN From Voters";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;


        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                lstPasswords.Add(reader["SSN"].ToString());
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


        if(lstPasswords.Contains(userSSN))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Selects the count from Voters table then creates distinct ID
    /// </summary>
    /// <returns> composite distinct id from name,surname and count </returns>
    protected string generateVoterID()
    {

        string firstName = txtFirstName.Text.ToLower().Substring(0,3);
        string lastName = txtLastName.Text.ToLower().Substring(0,2);
        string votersCount = null;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT COUNT(*) From Voters";
        SqlCommand command = new SqlCommand(sqlString, conn);
        SqlDataReader reader;
        
        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                votersCount = reader[0].ToString();
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


        if (votersCount != null)
        {
            return firstName + lastName + votersCount;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Inserts a new Voter into the Voters table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "INSERT INTO Voters VALUES(@ID,@SSN,@Email,@Password,@FirstName,@LastName,@DateOfBirth,@Address,@Municipality)";
        SqlCommand command = new SqlCommand(sqlString, conn);


        // Get distinct voter ID  
        string id = generateVoterID();


        // check duplicate SSN from database
        string userSSN = txtSSN.Text;
        bool flagSSN = checkDuplicateSSN(userSSN);

        // create Date of Birth from selected DDLs
        string birthYear = ddlYear.SelectedItem.Value;
        string birthMonth = ddlMonth.SelectedItem.Value;
        string birthDay = ddlDay.SelectedItem.Value;

        string VoterDateOfBirth = birthDay + "/" + birthMonth + "/" + birthYear;


        if (userSSN.Length != 13)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The SSN needs to be exactly 13 numbers long";
        }
        else if (flagSSN == false)
        {
            // add command parameters
            if (id != null)
            {
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@SSN", txtSSN.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text);
                command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                command.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(VoterDateOfBirth));
                command.Parameters.AddWithValue("@Address", txtAdress.Text);
                command.Parameters.AddWithValue("@Municipality", txtMunicipality.Text);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Account created successfully. Your User ID is : " + id + 
                        "<br/>You can now log in to your account.";
                    pnlSignup.Visible = false;
                }
                catch (Exception err)
                {
                    //lblMessage.Text = err.Message;  // change after completing
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "There is already a Voter with that SSN";
        }


    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }


    /// <summary>
    /// change number of days depending of selected month or year
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDay.Items.Clear();
        int year = Convert.ToInt32(ddlYear.SelectedItem.Value);

        if (ddlMonth.SelectedItem.Value == "4" ||
            ddlMonth.SelectedItem.Value == "6" ||
            ddlMonth.SelectedItem.Value == "9" ||
            ddlMonth.SelectedItem.Value == "11")
        {

            for (int i = 1; i <= 30; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }
        else if (ddlMonth.SelectedItem.Value == "1" ||
            ddlMonth.SelectedItem.Value == "3" ||
            ddlMonth.SelectedItem.Value == "5" ||
            ddlMonth.SelectedItem.Value == "7" ||
            ddlMonth.SelectedItem.Value == "8" ||
            ddlMonth.SelectedItem.Value == "10" ||
            ddlMonth.SelectedItem.Value == "12")
        {
            for (int i = 1; i <= 31; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }

        else if (ddlMonth.SelectedItem.Value == "2" && DateTime.IsLeapYear(year))
        {
            for (int i = 1; i <= 29; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
        }
        else
        {
            for (int i = 1; i <= 28; i++)
            {
                ddlDay.Items.Add(i.ToString());
            }
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