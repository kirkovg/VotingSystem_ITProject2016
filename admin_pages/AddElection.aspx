<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddElection.aspx.cs" Inherits="admin_pages_AddElection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Election</title>
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

    <!--Message Panel-->
    <div class="container col-lg-offset-4" style="width: 600px;">
        <div class="form-group col-lg-8">
            <asp:Label ID="lblMessage" runat="server" style="font-weight: bold;"></asp:Label>
            <asp:Label ID="lblMessageError2" runat="server" style="font-weight: bold;"></asp:Label>
        </div>
    </div>

    
    <div class="container col-lg-offset-4" style="width: 600px;">
        <!--Elections Panel-->
        <asp:Panel ID="pnlElections" runat="server">
            <div class="form-group col-lg-8">
                <label>Select a starting date and time:</label>
                <asp:Calendar ID="clndFrom" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" ToolTip="DateFrom">
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
                <br />
                <label>Hours:&nbsp</label>
                <asp:DropDownList ID="ddlHoursFrom" runat="server" class="btn btn-default"></asp:DropDownList>
                <label>Minutes:&nbsp</label>
                <asp:DropDownList ID="ddlMinutesFrom" runat="server" class="btn btn-default"></asp:DropDownList>
            </div>
            
            <div class="form-group col-lg-8">
                <label>Select an ending date and time:</label>
                <asp:Calendar ID="clndTo" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" ToolTip="DateTo">
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
                <br />
                <label>Hours:&nbsp</label>
                <asp:DropDownList ID="ddlHoursTo" runat="server" class="btn btn-default"></asp:DropDownList>
                <label>Minutes:&nbsp</label>
                <asp:DropDownList ID="ddlMinutesTo" runat="server" class="btn btn-default"></asp:DropDownList>
            </div>


            <div class="form-group col-lg-8">
                <label>Election Name:</label>
                <asp:TextBox ID="txtElecName" runat="server" ToolTip="Election Name" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqElecName" runat="server" ErrorMessage="Enter an Election Name" ForeColor="Red" ControlToValidate="txtElecName"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-lg-8">
                <label>Election Description:</label>
                <asp:TextBox ID="txtElecDescr" runat="server" TextMode="MultiLine" class="form-control" ToolTip="Election Description"></asp:TextBox>
            </div>

            <div class="form-group col-lg-8">
                <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                <br />
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit Election" class="btn btn-primary"/>
            </div>
            
        </asp:Panel>


        <!--Participating parties panel-->
        
        <asp:Panel ID="pnlPartiesToParticipate" runat="server">
            <div class="form-group col-lg-8">
                <label>Select from the list of parties to participate:</label>
                <asp:CheckBoxList ID="chkLstParties" runat="server"></asp:CheckBoxList>
                <br />
                <asp:Label ID="lblSelectedParties" runat="server" style="font-weight: bold;"></asp:Label>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary"/>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
