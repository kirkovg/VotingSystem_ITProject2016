using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_pages_AddParty : System.Web.UI.Page
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
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HttpPostedFile postedFile = fileUplLogo.PostedFile;
        string filename = Path.GetFileName(postedFile.FileName);
        string fileExtension = Path.GetExtension(filename);

        string partyName = txtPartyName.Text;
        int numMembers = Convert.ToInt32(txtNumMembers.Text);
        
        if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif" ||
                    fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp")
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
            string sqlString = "INSERT INTO Parties VALUES(@PartyName,@NumberOfMembers,@ImageLogo)";
            SqlCommand command = new SqlCommand(sqlString, conn);

            Stream stream = postedFile.InputStream;
            BinaryReader binaryReader = new BinaryReader(stream);
            Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

            command.Parameters.AddWithValue("@PartyName", partyName);
            command.Parameters.AddWithValue("@NumberOfMembers", numMembers);
            command.Parameters.AddWithValue("@ImageLogo", bytes);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Party added succesfully";
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
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Only images (.jpg, .png, .gif and .bmp) can be uploaded";
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