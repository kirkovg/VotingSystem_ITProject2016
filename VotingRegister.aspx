<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VotingRegister.aspx.cs" Inherits="VotingRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Voting Register</title>
    <link href="styles/bootstrap/css/bootstrap.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
         <!--Navbar-->
        <nav class="navbar navbar-inverse">
          <div class="container-fluid">
            <div class="navbar-header">
              <a class="navbar-brand" href="#" style="color: white">VoteX</a>
            </div>
            <ul class="nav navbar-nav">
              <li>
                  <asp:LinkButton ID="btnLinkLogin" runat="server" CausesValidation="false" OnClick="btnLinkLogin_Click">Log in</asp:LinkButton>
              </li>
              <li>
                  <asp:LinkButton ID="btnLinkSignUp" runat="server" CausesValidation="false" OnClick="btnLinkSignUp_Click">Sign up</asp:LinkButton>
              </li>
              <li>
                   <asp:LinkButton ID="btnLinkRegister" runat="server" CausesValidation="false" OnClick="btnLinkRegister_Click">Voting Register</asp:LinkButton> 
              </li>
             </ul>
          </div>
        </nav>
     
    <div class="container col-lg-offset-4" style="width: 600px;">
        <div class="form-group col-lg-8">
            <label>
                Select a property from the list to search the voting register
            </label>
        </div>

        <div class="form-group col-lg-8">
            <asp:DropDownList ID="ddlOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged" CssClass="btn btn-default dropdown-toggle">
                <asp:ListItem Selected="True" Value="0">Name and Surname</asp:ListItem>
                <asp:ListItem Value="1">SSN</asp:ListItem>
                <asp:ListItem Value="2">Address</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Panel ID="byNameSurname" runat="server">
            <div class="form-group col-lg-8">
                <label>Name</label>
                <asp:TextBox ID="txtSearch1" runat="server" class="form-control"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtSearch1" ErrorMessage="Cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>           
           </div>

            <div class="form-group col-lg-8">
                <label>Surname</label>
                <asp:TextBox ID="txtSearch2" runat="server" class="form-control"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="reqSurname" runat="server" ControlToValidate="txtSearch2" ErrorMessage="Cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

        </asp:Panel>



        <asp:Panel ID="byAddressOrSSN" runat="server">
            <div class="form-group col-lg-8">
                <asp:Label ID="lblSearch" runat="server" style="font-weight: bold"></asp:Label>
                <asp:TextBox ID="txtSearch3" runat="server" class="form-control"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="reqSSN_Address" runat="server" ControlToValidate="txtSearch3" ErrorMessage="Cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </asp:Panel>

        <div class="form-group col-lg-8">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="btn btn-primary"/>
        </div>
        
    </div>
        <asp:Table ID="tblVoters" runat="server" class="table table-bordered table-hover"></asp:Table>
    </form>
</body>
</html>
