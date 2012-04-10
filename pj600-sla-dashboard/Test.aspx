<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Test.aspx.cs" Inherits="no.nith.pj600.dashboard.Test" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <h2>Upload Excel file</h2>
    <asp:Panel ID="ExcelUploadPanel" runat="server" CssClass="borderWithPadding" >
      <asp:Label ID="ExcelUploadLabel" runat="server" Text="Upload Excel file: " AssociatedControlID="ExcelUploadPanel" /><asp:FileUpload ID="ExcelFileUpload" runat="server" />
      <asp:Button ID="ExcelUploadButton" runat="server" Text="Upload" OnClick="ExcelUploadButton_Click"/>
      <br />
      <p>
         <asp:Label ID="ExcelUploadStatusLabel" runat="server" Text=""></asp:Label>
      </p>
   </asp:Panel>
    <br />
   <asp:GridView ID="GridView1" runat="server">
   </asp:GridView>

</asp:Content>
