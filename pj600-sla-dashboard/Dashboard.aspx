<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="no.nith.pj600.dashboard.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
      
   <asp:TabContainer ID="TabContainer" runat="server" 
                     ActiveTabIndex="0" ScrollBars="Auto" UseVerticalStripPlacement="true"
                     VerticalStripWidth="130px">
      <asp:TabPanel ID="OverviewTab" runat="server" HeaderText="Overview" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content OverviewTab goes here -->

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="SLATab" runat="server" HeaderText="SLA Agreements" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content SLATab goes here -->

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="AddServicesTab" runat="server" HeaderText="Additional Services" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content AddServicesTab goes here -->

         </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="GraphsTab" runat="server" HeaderText="Graphs" ScrollBars="Auto">
         <ContentTemplate>
            
            <!-- Content GraphsTab goes here -->

         </ContentTemplate>
      </asp:TabPanel>
   </asp:TabContainer>

</asp:Content>
