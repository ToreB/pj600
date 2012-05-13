<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="no.nith.pj600.dashboard._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<script type="text/javascript">

   /* Makes a postback everytime the active tab changes */
   function ActiveTabChanged(sender, e) {
      __doPostBack('TabContainer', sender.get_activeTab().get_headerText());
   }

</script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
   <asp:ScriptManager ID="ScriptManager" runat="server" />

   <asp:TabContainer ID="TabContainer" runat="server" 
                     ActiveTabIndex="1" ScrollBars="Auto" UseVerticalStripPlacement="true"
                     VerticalStripWidth="130px"
                     CssClass="dashboardTabContainer"
                     OnClientActiveTabChanged="ActiveTabChanged"
                     OnActiveTabChanged="TabContainerTabChange"
                     >
      <asp:TabPanel ID="BugFix" runat="server" Visible="false" TabIndex="0">
         <ContentTemplate>
            <!-- Inserted this TabPanel to fix a bug where the first tab wouldn't behave properly -->
         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="OverviewTab" runat="server" HeaderText="Overview" ScrollBars="Auto" TabIndex="1">
         <ContentTemplate>       
            <!-- Content OverviewTab goes here -->

            <div class="dashboardTab">
                <asp:GridView ID="OverviewTable" runat="server" AllowPaging="true" PageSize="10" 
                  PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="OnPageIndexChanging"
                  AutoGenerateColumns="false" OnRowCreated="RowCreated"
                  AllowSorting="true" OnSorting="OnSorting" HeaderStyle-Wrap="false">
                  <Columns>
                     <asp:BoundField DataField="ProjectNo" HeaderText="Project No." SortExpression="ProjectNo" />
                     <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName" />
                     <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
                     <asp:BoundField DataField="ProjectManager" HeaderText="Project Manager" SortExpression="ProjectManager" />
                     <asp:BoundField DataField="ProjectStartTime" HeaderText="Project Start Time" SortExpression="ProjectStartTime" />
                     <asp:BoundField DataField="ProjectStopTime" HeaderText="Project Stop Time" SortExpression="ProjectStopTime" />
                     <asp:BoundField DataField="ProjectHourEstimate" HeaderText="Project Hour Estimate" SortExpression="ProjectHourEstimate" DataFormatString="{0:N}" />
                     <asp:BoundField DataField="ProjectCostEstimate" HeaderText="Project Cost Estimate" SortExpression="ProjectCostEstimate" DataFormatString="{0:N}" />
                     <asp:BoundField DataField="HoursSpent" HeaderText="Hours Spent" SortExpression="HoursSpent" DataFormatString="{0:N}" />
                     <asp:BoundField DataField="TotalSalesAmount" HeaderText="Total Sales Amount" SortExpression="TotalSalesAmount" DataFormatString="{0:N}" />
                     <asp:BoundField DataField="BalanceAmount" HeaderText="Balance Amount" SortExpression="BalanceAmount" DataFormatString="{0:N}" />
                  </Columns>
               </asp:GridView>
            </div>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="SLATab" runat="server" HeaderText="SLA Agreements" ScrollBars="Auto" TabIndex="2">
         <ContentTemplate>
            
            <!-- Content SLATab goes here -->
            <div class="dashboardTab">
               <asp:GridView ID="SLATable" runat="server" AllowPaging="true" PageSize="10" 
                  PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="OnPageIndexChanging"
                  AutoGenerateColumns="false" OnRowCreated="RowCreated"
                  AllowSorting="true" OnSorting="OnSorting" HeaderStyle-Wrap="false">
                  <Columns>
                     <asp:BoundField DataField="ProjectNo" HeaderText="Project No." SortExpression="ProjectNo"  />                  
                     <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"  />
                     <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName"  />
                     <asp:BoundField DataField="ProjectManager" HeaderText="Project Manager" SortExpression="ProjectManager" />
                     <asp:BoundField DataField="BalanceAmount" HeaderText="Balance Amount" SortExpression="BalanceAmount" DataFormatString="{0:N}" />
                  </Columns>
               </asp:GridView>
            </div>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="AddlServicesTab" runat="server" HeaderText="Additional Services" ScrollBars="Auto" TabIndex="3">
         <ContentTemplate>
            
            <!-- Content AddServicesTab goes here -->
            <div class="dashboardTab">
               <asp:GridView ID="AddlServicesTable" runat="server" AllowPaging="true" PageSize="10" 
                  PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="OnPageIndexChanging"
                  AutoGenerateColumns="false" OnRowCreated="RowCreated"
                  AllowSorting="true" OnSorting="OnSorting" HeaderStyle-Wrap="false">
                  <Columns>
                     <asp:BoundField DataField="ProjectNo" HeaderText="Project No." SortExpression="ProjectNo"  />                  
                     <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"  />
                     <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName"  />
                     <asp:BoundField DataField="ArticleNo" HeaderText="Article No." SortExpression="ArticleNo" />
                     <asp:BoundField DataField="ArticleName" HeaderText="Article Name" SortExpression="ArticleName"  />
                     <asp:BoundField DataField="TotalSalesAmount" HeaderText="Total Sales Amount" SortExpression="TotalSalesAmount" DataFormatString="{0:N}" />
                  </Columns>
               </asp:GridView>
            </div>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="GraphsTab" runat="server" HeaderText="Graphs" ScrollBars="Auto" TabIndex="4">
         <ContentTemplate>
            
            <!-- Content GraphsTab goes here -->
            <h1>Graphs</h1>
            <div class="dashboardTab">
               
            </div>

         </ContentTemplate>
      </asp:TabPanel>
   </asp:TabContainer>

</asp:Content>
