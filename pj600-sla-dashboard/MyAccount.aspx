<%@ Page Title="My Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="no.nith.pj600.dashboard.MyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
   <h1>My Account</h1>

   <p>
      <asp:HyperLink ID="ChangePasswordLink" runat="server" NavigateUrl="Account/ChangePassword.aspx">Change password</asp:HyperLink>
   </p>
   
</asp:Content>
