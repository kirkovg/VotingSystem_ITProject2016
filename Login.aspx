<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log in</title>
    <link href="styles/bootstrap/css/bootstrap.css" type="text/css" rel="stylesheet"/>
    <link href="styles/main.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">

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

<div id="bkgrDiv">
    <div class="container col-lg-offset-4" style="width: 600px">

        <div class="form-group col-lg-8"  style="margin-left: 25px;">
            <label>Welcome to the VoteX voting system!</label>
        </div>   

        <div>
            <div class="form-group col-lg-8">   
                    <label>User ID</label> 
                    <asp:TextBox ID="txtUserId" placeholder="User ID" runat="server" class="form-control col-lg-8"></asp:TextBox>    
            </div>   
        
            <div class="form-group col-lg-8">
                    <label>Password</label>
                    <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" TextMode="Password" class="form-control"></asp:TextBox>              
            </div>    

            <div class="form-group col-lg-8">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>   
         
            <div class="form-group col-lg-8">     
                <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click" Text="Log in" class="btn btn-primary"/>
            </div>     
        </div>
    </div>
</div>   
    
    </form>
</body>
</html>
