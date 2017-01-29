<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElectionStatistics.aspx.cs" Inherits="admin_pages_ElectionStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Election Statistics</title>
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
    
        <!--Content-->
        <div class="container col-lg-offset-4">
            <div class="form-group col-lg-8" style="margin-left: -120px;">
                <label>Here you can browse through different types of statistics about the elections</label>
            </div>

            <div class="form-group col-lg-8">
                <asp:ListBox ID="lbOptions" runat="server" class="list-group-item" AutoPostBack="True" OnSelectedIndexChanged="lbOptions_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">Winner Parties</asp:ListItem>
                    <asp:ListItem Value="1">Number of Parties by Election</asp:ListItem>
                    <asp:ListItem Value="2">Number of Votes by Election</asp:ListItem>
                    <asp:ListItem Value="3">Number of Votes by Party of Election</asp:ListItem>
                </asp:ListBox>
            </div>
            
        </div>

        <div class="container col-lg-offset-4" >
            <div class="form-group col-lg-8" style="margin-left: -210px;">
                <asp:GridView ID="gvElectionStats" runat="server" class="table table-bordered table-hover"></asp:GridView>
            </div>
        </div>
        

        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </form>
</body>
</html>
