using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for VotingRegisterService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VotingRegisterService : System.Web.Services.WebService
{

    public VotingRegisterService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    /// <summary>
    /// Class to represent a whole record of the Voters table
    /// </summary>
    public class Voter
    {
        public string ssn;
        public string name;
        public string surname;
        public string dateOfBirth;
        public string address;
        public string municipality;

        public override string ToString()
        {
            return string.Format("SSN: {0}, FirstName: {1}, LastName:{2}," +
                                "DateOfBirth:{3}, Address: {4}, Municipality{5}",
                                ssn, name, surname, dateOfBirth, address, municipality);
        }
    }


    [WebMethod(Description = "search the Voters table by SSN and return a Voter Object")]
    public Voter getBySSN(long voterSSN)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT SSN, FirstName, LastName, DateOfBirth, Address, Municipality " +
                           "FROM Voters WHERE SSN=@SSN";
        SqlCommand command = new SqlCommand(sqlString, conn);
        command.Parameters.AddWithValue("@SSN", voterSSN);
        SqlDataReader reader;
        Voter voter = new Voter();

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                voter.ssn = reader["SSN"].ToString();
                voter.name = reader["FirstName"].ToString();
                voter.surname = reader["LastName"].ToString();
                DateTime date = (DateTime)reader["DateOfBirth"];
                voter.dateOfBirth = date.ToShortDateString();
                voter.address = reader["Address"].ToString();
                voter.municipality = reader["Municipality"].ToString();
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


        return voter;
    }

    [WebMethod(Description = "search the Voters table by Address and returns a list of Voter objects")]
    public List<Voter> getByAddress(string voterAdress)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT SSN, FirstName, LastName, DateOfBirth, Address, Municipality " +
                           "FROM Voters WHERE Address=@Address";
        SqlCommand command = new SqlCommand(sqlString, conn);
        command.Parameters.AddWithValue("@Address", voterAdress);
        SqlDataReader reader;
        
        List<Voter> voters = new List<Voter>();

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Voter voter = new Voter();
                voter.ssn = reader["SSN"].ToString();
                voter.name = reader["FirstName"].ToString();
                voter.surname = reader["LastName"].ToString();
                DateTime date = (DateTime)reader["DateOfBirth"];
                voter.dateOfBirth = date.ToShortDateString();
                voter.address = reader["Address"].ToString();
                voter.municipality = reader["Municipality"].ToString();
                //Console.WriteLine(voter);
                voters.Add(voter);
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


        return voters;
    }

    [WebMethod(Description = "search the Voters table by Name and Surname and returns a list of Voter objects")]
    public List<Voter> getByNameSurname(string voterName,string voterSurname)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBVoteXConn"].ConnectionString;
        string sqlString = "SELECT SSN, FirstName, LastName, DateOfBirth, Address, Municipality " +
                           "FROM Voters WHERE FirstName=@FirstName AND LastName=@LastName";
        SqlCommand command = new SqlCommand(sqlString, conn);
        command.Parameters.AddWithValue("@FirstName", voterName);
        command.Parameters.AddWithValue("@LastName", voterSurname);
        SqlDataReader reader;

        List<Voter> voters = new List<Voter>();

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Voter voter = new Voter();
                voter.ssn = reader["SSN"].ToString();
                voter.name = reader["FirstName"].ToString();
                voter.surname = reader["LastName"].ToString();
                DateTime date = (DateTime)reader["DateOfBirth"];
                voter.dateOfBirth = date.ToShortDateString();
                voter.address = reader["Address"].ToString();
                voter.municipality = reader["Municipality"].ToString();
                //Console.WriteLine(voter);
                voters.Add(voter);
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


        return voters;
    }

}
