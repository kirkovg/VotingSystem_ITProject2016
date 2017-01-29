<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPanel.aspx.cs" Inherits="admin_pages_AdminPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel</title>
    <link href="~/styles/bootstrap/css/bootstrap.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <!--Navbar-->
        <nav class="navbar navbar-inverse">
          <div class="container-fluid">
            <div class="navbar-header">
              <a class="navbar-brand" href="#" style="color:white;">VoteX</a>
            </div>
            <ul class="nav navbar-nav">
              <li>
                  <asp:LinkButton ID="btnAdminPanel" runat="server" CausesValidation="false" OnClick="btnAdminPanel_Click">Admin Panel</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="btnAddParty" runat="server" CausesValidation="false" OnClick="btnAddParty_Click">Add Party</asp:LinkButton>
              </li>
              <li>
                   <asp:LinkButton ID="btnAddElection" runat="server" CausesValidation="false" OnClick="btnAddElection_Click">Add Election</asp:LinkButton> 
              </li>
                <li>
                   <asp:LinkButton ID="btnUpdateParty" runat="server" CausesValidation="false" OnClick="btnUpdateParty_Click">Edit Party</asp:LinkButton> 
              </li>
              <li>
                   <asp:LinkButton ID="btnRemoveElection" runat="server" CausesValidation="false" OnClick="btnRemoveElection_Click">Remove Election</asp:LinkButton> 
              </li>
               <li>
                   <asp:LinkButton ID="btnElectionStatistics" runat="server" CausesValidation="false" OnClick="btnElectionStatistics_Click">Statistics</asp:LinkButton> 
              </li>
             </ul>

              <ul class="nav navbar-nav navbar-right">
                  <li style="margin-top: 15px; color:white">
                      <span class="glyphicon glyphicon-user"></span>
                      <asp:Label ID="lblUserID" runat="server"></asp:Label>
                  </li>
                  
                  <li><asp:LinkButton ID="linkLogOut" runat="server" CausesValidation="False" OnClick="linkLogOut_Click">Log Out</asp:LinkButton></li>
              </ul>

          </div>
        </nav>

    
    <div>
        <!--Active Election-->
        <asp:Panel ID="pnlActiveElection" runat="server">
            <div class="container col-lg-offset-4" style="width: 600px;">
                <div class="form-group col-lg-8">
                    <label>Starting date and time:</label>
                    <asp:Label ID="lblDateTimeFrom" runat="server" style="font-weight: bold;"></asp:Label>
                </div>
            
                <div class="form-group col-lg-8">
                    <label>Ending date and time:</label>
                    <asp:Label ID="lblDateTimeTo" runat="server" style="font-weight: bold;"></asp:Label>
                </div>

                 <div class="form-group col-lg-8">
                    <label>Election Name:</label>
                    <asp:Label ID="lblElectionName" runat="server" style="font-weight: bold;"></asp:Label>
                </div>

                <div class="form-group col-lg-8">
                    <label>Election Description:</label>
                    <br />
                    <asp:Label ID="lblElectionDescription" runat="server" style="font-weight: bold;"></asp:Label>
                </div>

                <div class="form-group col-lg-8">
                    <label>Number of Parties participating in this election: </label>
                    <asp:Label ID="lblNumParties" runat="server" style="font-weight: bold;"></asp:Label>
                </div>

                <div class="form-group col-lg-8" style="width: 600px;">
                    <label>Parties participating in this election: </label>
                    <asp:Table ID="tblParties" runat="server" class="table table-bordered table-hover" style="margin-left: -90px;"></asp:Table>
                </div>
            </div>

            <div class="container col-lg-offset-4" style="width: 600px;">
                <div class="form-group col-lg-8">
                    <asp:Button ID="btnEnd" runat="server" Text="Stop Election" OnClick="btnEnd_Click" class="btn btn-danger"/>
                </div>
            </div>
            
        </asp:Panel>

        <div class="container col-lg-offset-4" style="width: 600px;">
            <div class="form-group col-lg-8">
                <asp:Label ID="lblMessage" runat="server" style="font-weight: bold;"></asp:Label>
            </div>
        </div>
        
    </div>
    </form>
</body>
</html>
