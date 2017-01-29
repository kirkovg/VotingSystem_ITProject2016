<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddParty.aspx.cs" Inherits="admin_pages_AddParty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add party</title>
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
    
    

    <div class="container col-lg-offset-4" style="width: 600px;">
        <div class="form-group col-lg-8" >
            <label>You can enter a new voting candidate(party) here</label>
        </div>

        <div class="form-group col-lg-8">
            <label>Party Name:</label>
            <asp:TextBox ID="txtPartyName" runat="server" class="form-control"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="reqPartyName" runat="server" ControlToValidate="txtPartyName" ErrorMessage="Enter the Party Name" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group col-lg-8">
            <label>Number of members:</label>
            <asp:TextBox ID="txtNumMembers" runat="server" class="form-control"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="reqNumMembers" runat="server" ControlToValidate="txtNumMembers" Display="Dynamic" ErrorMessage="Enter the number of members" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regexNumMembers" runat="server" ControlToValidate="txtNumMembers" Display="Dynamic" ErrorMessage="Must enter a number value" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>
        
        <div class="form-group col-lg-8">
            <label>Logo:</label>
            <asp:FileUpload ID="fileUplLogo" runat="server" class="form-control"/>
            <br />
            <asp:RequiredFieldValidator ID="reqLogo" runat="server" ControlToValidate="fileUplLogo" ErrorMessage="Must upload a party logo" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group col-lg-8">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary"/>
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" style="font-weight: bold;"></asp:Label>
        </div>
        
    </div>
    </form>
</body>
</html>
