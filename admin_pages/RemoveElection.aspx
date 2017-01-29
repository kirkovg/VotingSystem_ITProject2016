<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemoveElection.aspx.cs" Inherits="admin_pages_UpdateRemoveElection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Remove Election</title>
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
        <asp:GridView ID="gvElections" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvElections_SelectedIndexChanged" class="table table-bordered table-hover">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Election ID" />
                <asp:BoundField DataField="DateFrom" HeaderText="Date From">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DateTo" HeaderText="Date To">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ElectionName" HeaderText="Election Name">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ElectionDescription" HeaderText="Election Description">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ActiveElection" HeaderText="Is Active">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField SelectText="Delete Election" ShowSelectButton="True" ControlStyle-CssClass="btn btn-danger"/>
            </Columns>
        </asp:GridView>

        <div class="container col-lg-offset-4" style="width: 600px;">
            <div class="form-group col-lg-8">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
