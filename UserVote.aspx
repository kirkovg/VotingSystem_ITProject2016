<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserVote.aspx.cs" Inherits="UserVote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Vote</title>
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
                  <asp:LinkButton ID="btnLinkUserVote" runat="server" CausesValidation="false" OnClick="btnLinkUserVote_Click">User vote</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="btnLinkPasswordRecovery" runat="server" CausesValidation="false" OnClick="btnLinkPasswordRecovery_Click">Password change</asp:LinkButton>
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
       

        <!--Active Election status-->
        

        <!-- Election description and party selection-->
        <div class="container col-lg-offset-4" style="width: 600px;"> 
            <div class="form-group col-lg-8">
                <asp:Label ID="lblMessage" runat="server" style="font-weight: bold"></asp:Label>
            </div>

            <asp:Panel ID="pnlElection" runat="server">
                

                <div class="form-group col-lg-8">
                    <label>Election Name: </label>
                    <asp:Label ID="lblElectionName" runat="server"></asp:Label>
                </div>
                
                <div class="form-group col-lg-8">
                    <label>Election Description: </label><br />
                    <asp:Label ID="lblElectionDescr" runat="server"></asp:Label>
                </div>

                <div class="form-group col-lg-8">
                    <label>From: </label>
                    <asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                </div>

                <div class="form-group col-lg-8">
                    <label>To:</label>
                    <asp:Label ID="lblDateTo" runat="server"></asp:Label>
                </div>

                <div class="form-group col-lg-8">
                    <asp:RadioButtonList ID="rblParties" runat="server" OnDataBound="rblParties_DataBound">
                    </asp:RadioButtonList>
                </div>
                
                <div class="form-group col-lg-8">
                    <asp:RequiredFieldValidator ID="reqVote" runat="server" ErrorMessage="Must select a party" ControlToValidate="rblParties" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save Vote" OnClick="btnSave_Click" class="btn btn-primary" />
                </div>       
            </asp:Panel>
        </div>
        
    </form>
</body>
</html>
