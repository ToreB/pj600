<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="no.nith.pj600.dashboard.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="loginLeft">
       
       <asp:Panel ID="LogoutMessagePanel" runat="server" Visible="false" EnableViewState="false">
          <asp:Label ID="LogoutMessage" runat="server" CssClass="infoMessage" Text="You have successfully been logged out." />
       </asp:Panel>

       <asp:Panel ID="AccountStatusPanel" runat="server" Visible="false" EnableViewState="false">
          <p class="errorMessage"><asp:Label ID="AccountStatusLabel" runat="server" Text="" /></p>
       </asp:Panel>

       <h2>
           Log in
       </h2>
       <p>
           Please enter your username and password.<br />
       </p>

       <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" OnLoggedIn="OnLoggedIn" 
            OnLoginError="OnLoginError">
           <LayoutTemplate>

               <span class="failureNotification">
                   <asp:Literal ID="FailureText" runat="server" />
                   <asp:Literal ID="FailureAttemptsLiteral" runat="server" />
               </span>

               <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="errorMessage" 
                    ValidationGroup="LoginUserValidationGroup"/>

               <div class="accountInfo">
                   <fieldset class="login">
                       <legend>Account information</legend>
                       <p>
                           <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                           <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                       </p>
                       <p>
                           <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                           <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                       </p>
                       <p>
                           <asp:CheckBox ID="RememberMe" runat="server" />
                           <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Remember me</asp:Label>
                       </p>
                       <p>
                          <asp:HyperLink ID="ResetPasswordLink" runat="server" NavigateUrl="~/Account/ResetPassword.aspx">Forgotten your password?</asp:HyperLink>
                       </p>
                   </fieldset>
                   <p class="submitButton">
                       <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                   </p>
               </div>
           </LayoutTemplate>
       </asp:Login>
    </div>

    <div id="loginRight">
      <h2>Welcome to 99X Dashboard</h2>
      <p>
         Information about 99X dashboard goes here.
      </p>
    </div>
</asp:Content>
