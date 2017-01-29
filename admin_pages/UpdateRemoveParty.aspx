<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateRemoveParty.aspx.cs" Inherits="admin_pages_UpdateParty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Party</title>
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
        <asp:GridView ID="gvParties" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvParties_RowCancelingEdit" OnRowEditing="gvParties_RowEditing" OnRowUpdating="gvParties_RowUpdating" OnSelectedIndexChanged="gvParties_SelectedIndexChanged" class="table table-bordered table-hover">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Party ID" ReadOnly="True" >
                
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PartyName" HeaderText="Party Name" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NumberOfMembers" HeaderText="Number of Members" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary">
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField SelectText="Delete" ShowSelectButton="True" ControlStyle-CssClass="btn btn-danger">
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>


        <br />
        <br />
        <div class="container col-lg-offset-4" style="width: 600px;">
            <div class="form-group col-lg-8">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
