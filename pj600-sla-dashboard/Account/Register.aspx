<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="no.nith.pj600.dashboard.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:SqlDataSource ID="RolesDataSource" runat="server" 
      ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
      SelectCommand="SELECT [RoleName] FROM [vw_aspnet_Roles] ORDER BY [RoleName]"></asp:SqlDataSource>
   
   <asp:panel ID="RegisterPanel" runat="server">
      <asp:Label ID="Message" runat="server" Text="" EnableViewState="false" />
      
      <h2>
         Create a New Account
      </h2>
      <p>
         Use the form below to create a new account.<br /><br />
         Password will be generated automatically and an e-mail with account info<br />
         will be sent to the registered specified e-mail.
      </p>
      
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="errorMessage" 
            ValidationGroup="RegisterUserValidationGroup"/>
      <div id="RegisterLeft" class="floatLeft accountInfo">
         <fieldset class="register">
               <legend>Account Information</legend>
               <p>
                  <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                  <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" />
                  <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
               </p>
               <p>
                  <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                  <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                        CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
               </p>
         </fieldset>
         <p class="submitButton">
               <asp:Button ID="CreateUserButton" runat="server" Text="Create User" 
                  ValidationGroup="RegisterUserValidationGroup" OnClick="CreateUser"/>
         </p>
      </div>
      <div id="RegisterRight" class="floatLeft">
         <fieldset>
            <legend>Roles</legend>

            <asp:CheckBoxList ID="RolesList" runat="server" DataSourceId="RolesDataSource" DataTextField="RoleName" AutoPostBack="false">
            </asp:CheckBoxList>

         </fieldset>
      </div>
   </asp:panel>
</asp:Content>
