using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VotingRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            byAddressOrSSN.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        VotingRegisterService service = new VotingRegisterService();
        tblVoters.Rows.Clear();

        if (ddlOptions.SelectedItem.Value == "0")
        {
            

            List<VotingRegisterService.Voter> voters = service.getByNameSurname(txtSearch1.Text, txtSearch2.Text);

            // adding a table header
            TableHeaderRow tHeader = new TableHeaderRow();
            TableHeaderCell[] tHeaderCells = new TableHeaderCell[] {
                    new TableHeaderCell
                    {
                        Text = "SSN"
                    },
                    new TableHeaderCell
                    {
                        Text = "Name"
                    },
                    new TableHeaderCell
                    {
                        Text = "Surname"
                    },
                    new TableHeaderCell
                    {
                        Text = "DateOfBirth"
                    },
                     new TableHeaderCell
                    {
                        Text = "Address"
                    },
                      new TableHeaderCell
                    {
                        Text = "Municipality"
                    },
            };

            tHeader.Cells.AddRange(tHeaderCells);
            tblVoters.Rows.Add(tHeader);


            foreach (VotingRegisterService.Voter voter in voters)
            {
                // adding rows to the table
                TableCell[] cells = new TableCell[] {
                      new TableCell
                      {
                          Text = voter.ssn
                      },
                      new TableCell
                      {
                          Text = voter.name
                      },
                      new TableCell
                      {
                          Text = voter.surname
                      },
                      new TableCell
                      {
                          Text = voter.dateOfBirth
                      },
                      new TableCell
                      {
                          Text = voter.address
                      },
                      new TableCell
                      {
                          Text = voter.municipality
                      }
                };

                TableRow tr = new TableRow();
                tr.Cells.AddRange(cells);
                tblVoters.Rows.Add(tr);
            }

        }
        else if (ddlOptions.SelectedItem.Value == "1")  // ssn
        {

            VotingRegisterService.Voter voter = service.getBySSN(Convert.ToInt64(txtSearch3.Text));

            // adding a table header
            TableHeaderRow tHeader = new TableHeaderRow();
            TableHeaderCell[] tHeaderCells = new TableHeaderCell[] {
                    new TableHeaderCell
                    {
                        Text = "SSN"
                    },
                    new TableHeaderCell
                    {
                        Text = "Name"
                    },
                    new TableHeaderCell
                    {
                        Text = "Surname"
                    },
                    new TableHeaderCell
                    {
                        Text = "DateOfBirth"
                    },
                     new TableHeaderCell
                    {
                        Text = "Address"
                    },
                      new TableHeaderCell
                    {
                        Text = "Municipality"
                    },
            };

            tHeader.Cells.AddRange(tHeaderCells);
            tblVoters.Rows.Add(tHeader);

            // adding rows to the table
            TableCell[] cells = new TableCell[] {
                    new TableCell
                    {
                        Text = voter.ssn
                    },
                    new TableCell
                    {
                        Text = voter.name
                    },
                    new TableCell
                    {
                        Text = voter.surname
                    },
                    new TableCell
                    {
                        Text = voter.dateOfBirth
                    },
                    new TableCell
                    {
                        Text = voter.address
                    },
                    new TableCell
                    {
                        Text = voter.municipality
                    }
            };

            TableRow tr = new TableRow();
            tr.Cells.AddRange(cells);
            tblVoters.Rows.Add(tr);


        }
        else if (ddlOptions.SelectedItem.Value == "2")  // address
        {
            List<VotingRegisterService.Voter> voters = service.getByAddress(txtSearch3.Text);

            // adding a table header
            TableHeaderRow tHeader = new TableHeaderRow();
            TableHeaderCell[] tHeaderCells = new TableHeaderCell[] {
                    new TableHeaderCell
                    {
                        Text = "SSN"
                    },
                    new TableHeaderCell
                    {
                        Text = "Name"
                    },
                    new TableHeaderCell
                    {
                        Text = "Surname"
                    },
                    new TableHeaderCell
                    {
                        Text = "DateOfBirth"
                    },
                     new TableHeaderCell
                    {
                        Text = "Address"
                    },
                      new TableHeaderCell
                    {
                        Text = "Municipality"
                    },
            };

            tHeader.Cells.AddRange(tHeaderCells);
            tblVoters.Rows.Add(tHeader);


            foreach (VotingRegisterService.Voter voter in voters)
            {
                // adding rows to the table
                TableCell[] cells = new TableCell[] {
                      new TableCell
                      {
                          Text = voter.ssn
                      },
                      new TableCell
                      {
                          Text = voter.name
                      },
                      new TableCell
                      {
                          Text = voter.surname
                      },
                      new TableCell
                      {
                          Text = voter.dateOfBirth
                      },
                      new TableCell
                      {
                          Text = voter.address
                      },
                      new TableCell
                      {
                          Text = voter.municipality
                      }
                };

                TableRow tr = new TableRow();
                tr.Cells.AddRange(cells);
                tblVoters.Rows.Add(tr);
            }
        }

    }



    protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOptions.SelectedItem.Value == "0")
        {
            byNameSurname.Visible = true;
            byAddressOrSSN.Visible = false;
            txtSearch1.Text = "";
            txtSearch2.Text = "";
        }
        else if (ddlOptions.SelectedItem.Value == "1")
        {
            byNameSurname.Visible = false;
            byAddressOrSSN.Visible = true;
            txtSearch3.Text = "";
            lblSearch.Text = "SSN";
        }
        else if (ddlOptions.SelectedItem.Value == "2")
        {
            byNameSurname.Visible = false;
            byAddressOrSSN.Visible = true;
            txtSearch3.Text = "";
            lblSearch.Text = "Address";
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