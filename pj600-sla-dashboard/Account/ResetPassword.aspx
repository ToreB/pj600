<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="no.nith.pj600.dashboard.Account.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <asp:LoginView ID="ResetPasswordView" runat="server" EnableViewState="false">
         <AnonymousTemplate>

            <h1>Reset Password</h1>

            <br />

            <asp:ValidationSummary ID="EmailValidationSummary" runat="server" CssClass="errorMessage" 
                    ValidationGroup="EmailValidationGroup" />
            <p>
               <asp:Label ID="Message" runat="server" Text=""></asp:Label>
            </p>

            <div style="width:400px;">
               <fieldset>
                  <legend>Email</legend>
                  <p>
                     <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="EmailInput">Email:</asp:Label>       
                     <asp:TextBox ID="EmailInput" runat="server" CssClass="textEntry" />
                     <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailInput"
                        CssClass="failureNotification" ErrorMessage="Email is required." ToolTip="Email is required." Text="*" 
                        ValidationGroup="EmailValidationGroup" />
                  </p>
            
            </fieldset>
            <p class="submitButton">
               <asp:Button ID="ResetButton" runat="server" Text="Reset password" OnClick="ResetButton_Click" ValidationGroup="EmailValidationGroup" />
            </p>
            </div>
            


         </AnonymousTemplate>
      </asp:LoginView>

</asp:Content>
