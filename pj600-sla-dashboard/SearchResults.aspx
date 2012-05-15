<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="no.nith.pj600.dashboard.SearchResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h1>Search Results</h1>

   <br />

   <div style="overflow: auto;">
   <div class="Filter">
      <asp:CheckBoxList ID="Filter" runat="server" AutoPostBack="true"
         RepeatDirection="Horizontal" RepeatLayout="flow" Width="1000" OnSelectedIndexChanged="Filter_SelectedChanged" Visible="false">
         <asp:ListItem Text="Project No." Value="ProjectNo" Selected="true"/>
         <asp:ListItem Text="Project Name" Value="ProjectName" Selected="true"/>
         <asp:ListItem Text="Customer Name" Value="CustomerName" Selected="true"/>
         <asp:ListItem Text="Project Manager" Value="ProjectManager" Selected="true"/>
         <asp:ListItem Text="Project Start Time" Value="ProjectStartTime" Selected="true"/>
         <asp:ListItem Text="Project Stop Time" Value="ProjectStopTime" Selected="true"/>
         <asp:ListItem Text="Hours Spent" Value="HoursSpent" Selected="true"/>
         <asp:ListItem Text="Total Sales Amount" Value="TotalSalesAmount" Selected="true"/>
         <asp:ListItem Text="Balance Amount" Value="BalanceAmount" Selected="true"/>
      </asp:CheckBoxList>
   </div>

   <asp:Panel ID="MessagePanel" runat="server" Visible="false">
      <asp:Literal ID="Message" runat="server" Text="No search results were found." />
   </asp:Panel>

   <input runat="server" id="SearchInputHidden" type="hidden" value="" />
   <asp:GridView ID="Results" runat="server" HeaderStyle-Wrap="false" AutoGenerateColumns="false">
      <Columns>
         <asp:BoundField DataField="ProjectNo" HeaderText="Project No." SortExpression="ProjectNo" />
         <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName" />
         <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
         <asp:BoundField DataField="ProjectManager" HeaderText="Project Manager" SortExpression="ProjectManager" />
         <asp:BoundField DataField="ProjectStartTime" HeaderText="Project Start Time" SortExpression="ProjectStartTime" />
         <asp:BoundField DataField="ProjectStopTime" HeaderText="Project Stop Time" SortExpression="ProjectStopTime" />
         <asp:BoundField DataField="HoursSpent" HeaderText="Hours Spent" SortExpression="HoursSpent" DataFormatString="{0:N}" />
         <asp:BoundField DataField="TotalSalesAmount" HeaderText="Total Sales Amount" SortExpression="TotalSalesAmount" DataFormatString="{0:N}" />
         <asp:BoundField DataField="BalanceAmount" HeaderText="Balance Amount" SortExpression="BalanceAmount" DataFormatString="{0:N}" />
      </Columns>
   </asp:GridView>
   </div>

</asp:Content>
