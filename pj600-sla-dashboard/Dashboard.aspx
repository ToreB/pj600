<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="no.nith.pj600.dashboard.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
      
   <asp:TabContainer ID="TabContainer" runat="server" 
                     ActiveTabIndex="0" ScrollBars="Auto" UseVerticalStripPlacement="true"
                     VerticalStripWidth="130px"
                     CssClass="dashboardTabContainer">
      <asp:TabPanel ID="OverviewTab" runat="server" HeaderText="Overview" ScrollBars="Auto">
         <ContentTemplate>       
            <!-- Content OverviewTab goes here -->
               <h1>Overview</h1>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="SLATab" runat="server" HeaderText="SLA Agreements" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content SLATab goes here -->
            <h1>SLA Agreements</h1>

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="AddServicesTab" runat="server" HeaderText="Additional Services" ScrollBars="Auto">
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
