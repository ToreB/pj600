<%@ Page Title="Accounts Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountsManagement.aspx.cs" Inherits="no.nith.pj600.dashboard.Admin.AccountsManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h1>Account Management</h1>

   <h2>Unlock locked accounts</h2>
   <asp:SqlDataSource ID="AccountsDataSource" runat="server" 
      ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
      SelectCommand="SELECT [UserName], [Email], [IsApproved], [IsLockedOut], [LastLoginDate], [LastLockoutDate] FROM [vw_aspnet_MembershipUsers] ORDER BY [UserName]">
    </asp:SqlDataSource>

   <asp:GridView ID="AccountsList" runat="server" AllowPaging="True" 
      AllowSorting="false" AutoGenerateColumns="False" 
      DataSourceID="AccountsDataSource" OnRowCommand="RowCommand">
      <Columns>
         <asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" />
         <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
         <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
            SortExpression="IsApproved" />
         <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" 
            SortExpression="IsLockedOut" />
         <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" 
            SortExpression="LastLoginDate" />
         <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" 
            SortExpression="LastLockoutDate" />
         <asp:TemplateField>
            <ItemTemplate>
               <asp:LinkButton ID="UnlockButton" runat="server" Text="Unlock User" 
                  CommandName="Unlock" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" visible="true" Enabled="false"/>
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
   </asp:GridView>

   <h2>Create a new Account</h2>
   <a href="../Account/Register.aspx">Create a new account</a>

</asp:Content>
