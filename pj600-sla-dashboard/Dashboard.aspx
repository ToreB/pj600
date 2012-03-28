<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="no.nith.pj600.dashboard.Dashboard" %>

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
                     ActiveTabIndex="0" ScrollBars="Auto" UseVerticalStripPlacement="true"
                     VerticalStripWidth="130px"
                     CssClass="dashboardTabContainer"
                     OnClientActiveTabChanged="ActiveTabChanged"
                     OnActiveTabChanged="TabContainerTabChange"
                     >
      <asp:TabPanel ID="OverviewTab" runat="server" HeaderText="Overview" ScrollBars="Auto">
         <ContentTemplate>       
            <!-- Content OverviewTab goes here -->
            <h1>Overview</h1>

            <div class="dashboardTab">
               <asp:GridView ID="OverviewTable" runat="server" AllowPaging="true" PageSize="10" 
                  PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="OnPageIndexChanging"
                  OnPageIndexChanged="OnPageIndexChanged">
               </asp:GridView>
            </div>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="SLATab" runat="server" HeaderText="SLA Agreements" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content SLATab goes here -->
            <div class="dashboardTab">
               <asp:GridView ID="SLATable" runat="server" AllowPaging="true" PageSize="10" 
                  PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="OnPageIndexChanging"
                  OnPageIndexChanged="OnPageIndexChanged" AutoGenerateColumns="false" 
                  AllowSorting="true" OnSorting="SLATable_OnSorting">
                  <Columns>
                     <asp:BoundField DataField="ProjectNo" HeaderText="Project No." SortExpression="ProjectNo"/>                  
                     <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"/>
                     <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName"/>
                  </Columns>
               </asp:GridView>
            </div>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="AddlServicesTab" runat="server" HeaderText="Additional Services" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content AddServicesTab goes here -->
            <h1>Additional Services</h1>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="GraphsTab" runat="server" HeaderText="Graphs" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content GraphsTab goes here -->
            <h1>Graphs</h1>

         </ContentTemplate>
      </asp:TabPanel>
   </asp:TabContainer>

</asp:Content>
