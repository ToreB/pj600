<%@ Page Title="Accounts Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountsManagement.aspx.cs" Inherits="no.nith.pj600.dashboard.Admin.AccountsManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h1>Account Management</h1>

   <h2>Unlock locked accounts</h2>
   <asp:SqlDataSource ID="AccountsDataSource" runat="server" 
      ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
      
      SelectCommand="SELECT MembershipUsers.UserName, MembershipUsers.Email, Roles.RoleName AS Role, MembershipUsers.IsApproved, MembershipUsers.IsLockedOut, MemberShipUsers.CreateDate, MembershipUsers.LastLoginDate, MembershipUsers.LastLockoutDate FROM vw_aspnet_MembershipUsers AS MembershipUsers LEFT JOIN vw_aspnet_UsersInRoles AS UsersInRoles ON MembershipUsers.UserId = UsersInRoles.UserId LEFT JOIN vw_aspnet_Roles AS Roles ON UsersInRoles.RoleId = Roles.RoleId ORDER BY MembershipUsers.UserName">
    </asp:SqlDataSource>

   <asp:GridView ID="AccountsList" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
      DataSourceID="AccountsDataSource" OnRowCommand="RowCommand" ShowHeaderWhenEmpty="true">
      <Columns>
         <asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" />
         <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
         <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
         <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
            SortExpression="IsApproved" />
         <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" 
            SortExpression="IsLockedOut" />
         <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" />
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
