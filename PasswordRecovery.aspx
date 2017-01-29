<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Password Change</title>
    <link href="~/styles/bootstrap/css/bootstrap.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <!--Navbar-->
        <nav class="navbar navbar-inverse">
          <div class="container-fluid">
            <div class="navbar-header">
              <a class="navbar-brand" href="#" style="color:white">VoteX</a>
            </div>
            <ul class="nav navbar-nav">
              <li>
                  <asp:LinkButton ID="btnLinkUserVote" CausesValidation="false" runat="server" OnClick="btnLinkUserVote_Click">User vote</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="btnLinkPasswordRecovery" CausesValidation="false" runat="server" OnClick="btnLinkPasswordRecovery_Click">Password change</asp:LinkButton>
              </li>
             </ul>

              <ul class="nav navbar-nav navbar-right">
                  <li style="margin-top: 15px; color:white">
                      <span class="glyphicon glyphicon-user"></span>
                      <asp:Label ID="lblUserID" runat="server"></asp:Label>
                  </li>
                  
                  <li>
                      <asp:LinkButton ID="linkLogOut" runat="server" CausesValidation="False" OnClick="linkLogOut_Click">Log Out</asp:LinkButton>

                  </li>
              </ul>

          </div>
        </nav>


        <div class="container col-lg-offset-4" style="width: 600px;">
            <div class="form-group col-lg-8">
                <label>Old Password</label>
                <asp:TextBox ID="txtOldPass" runat="server" placeholder="Old Password" TextMode="Password" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqOldPass" runat="server" ErrorMessage="Must enter the old password" ControlToValidate="txtOldPass" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-8">
                <label>New Password</label>
                <asp:TextBox ID="txtNewPass" runat="server" placeholder="New Password" TextMode="Password" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqNewPass" runat="server" ErrorMessage="Must enter the new password" ControlToValidate="txtNewPass" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
                
            <div class="form-group col-lg-8">
                <asp:Button ID="btnChange" runat="server" Text="Change" OnClick="btnChange_Click" class="btn btn-success" />
                <br />
                <br />
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>    
        </div>
        
        
    </form>
</body>
</html>
