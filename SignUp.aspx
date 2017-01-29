<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link href="~/styles/bootstrap/css/bootstrap.css" type="text/css" rel="stylesheet"/>
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


    <div class="container col-lg-offset-4" style="width: 620px;">
        <div class="form-group col-lg-8">
           <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>

        <asp:Panel ID="pnlSignup" runat="server">
            <div class="form-group col-lg-8" style="margin-left: 20px;">
                <label>Please sign up to gain access to the voting questionnaire</label>
            </div>

            <div class="form-group col-lg-8">
                <label>Email</label>
                <asp:TextBox ID="txtEmail" placeholder="Enter email" runat="server" ToolTip="Enter Email" class="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Incorrect email address" ForeColor="Red" ValidationExpression="\S+@\S+\.\S+" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Enter your address" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-8">
                <label>First Name</label>
                <asp:TextBox ID="txtFirstName" class="form-control" placeholder="Enter firstname" runat="server" ToolTip="Enter First Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Enter your first name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-8">
                <label>Last Name</label>
                <asp:TextBox ID="txtLastName" class="form-control" placeholder="Enter lastname" runat="server" ToolTip="Enter Last Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Enter your last name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        
            <div class="form-group col-lg-8">
                <label>SSN</label>
                <asp:TextBox ID="txtSSN" placeholder="Enter SSN" class="form-control" runat="server" ToolTip="Enter SSN"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqSSN" runat="server" ControlToValidate="txtSSN" ErrorMessage="Enter your SSN" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
         
            <div class="form-group col-lg-8">
                <label>Address</label>
                <asp:TextBox ID="txtAdress" placeholder="Enter address" class="form-control" runat="server" ToolTip="Enter Address"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqAddress" runat="server" ControlToValidate="txtAdress" ErrorMessage="Enter your address" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-8">
                <label>Municipality</label>
                <asp:TextBox ID="txtMunicipality" class="form-control" placeholder="Enter municipality" runat="server" ToolTip="Enter Municipality"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqMunicipality" runat="server" ControlToValidate="txtMunicipality" ErrorMessage="Enter your municipality" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        
            <div class="form-group col-lg-8">
                <label>Enter your Date of Birth:</label>
                <br />
                <label>Year: </label>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="btn btn-default dropdown-toggle">
                </asp:DropDownList>
      
                &nbsp;<label>Month: </label>
                <asp:DropDownList ID="ddlMonth" CssClass="btn btn-default dropdown-toggle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                </asp:DropDownList>

                &nbsp;<label> Day: </label>
                <asp:DropDownList ID="ddlDay" runat="server" CssClass="btn btn-default dropdown-toggle">
                </asp:DropDownList>
            </div>

            <div class="form-group col-lg-8">
                <label>Password</label>
                <asp:TextBox ID="txtPassword" class="form-control" placeholder="Enter password" runat="server" TextMode="Password" ToolTip="Enter Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Enter your password" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        
            <div class="form-group col-lg-8">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"/>
            </div>
    
        </asp:Panel>

    
    </div>
    </form>
</body>
</html>
