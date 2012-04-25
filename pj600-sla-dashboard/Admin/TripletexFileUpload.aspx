<%@ Page Title="Upload Tripletex Export File" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TripletexFileUpload.aspx.cs" Inherits="no.nith.pj600.dashboard.Admin.TripletexFileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
   <h1>Upload Tripletex Export File</h1>
   <div style="width:400px;">
      <fieldset>
         <legend>Upload file: </legend>
         <asp:FileUpload ID="FileUpload" runat="server" />
      </fieldset>
      <p class="submitButton">
         <asp:Button ID="FileUploadButton" runat="server" Text="Upload" OnClick="FileUploadButton_Click"/>
      </p>
      <p>
         <asp:Label ID="FileUploadStatusLabel" runat="server" Text=""></asp:Label>
      </p>
   </div>

</asp:Content>
