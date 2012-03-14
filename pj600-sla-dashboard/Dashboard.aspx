<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="no.nith.pj600.dashboard.Dashboard" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Dashboard
    </h2>
   
   <asp:GridView ID="GridView1" runat="server">
   </asp:GridView>

</asp:Content>
