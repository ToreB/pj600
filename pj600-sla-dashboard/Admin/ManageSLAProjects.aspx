<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSLAProjects.aspx.cs" Inherits="no.nith.pj600.dashboard.Admin.ManageSLAProjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <asp:SqlDataSource ID="SlaProjectsDataSource" runat="server" 
      ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString %>" 
      SelectCommand="SELECT * FROM [SLAProjects] ORDER BY [ProjectNo]" 
      DeleteCommand="DELETE FROM [SLAProjects] WHERE [Id] = @Id" 
      InsertCommand="INSERT INTO [SLAProjects] ([ProjectNo]) VALUES (@ProjectNo)" 
      UpdateCommand="UPDATE [SLAProjects] SET [ProjectNo] = @ProjectNo WHERE [Id] = @Id">
      <DeleteParameters>
         <asp:Parameter Name="Id" Type="Int32" />
      </DeleteParameters>
      <InsertParameters>
         <asp:Parameter Name="ProjectNo" Type="Int32" />
      </InsertParameters>
      <UpdateParameters>
         <asp:Parameter Name="ProjectNo" Type="Int32" />
         <asp:Parameter Name="Id" Type="Int32" />
      </UpdateParameters>
   </asp:SqlDataSource>
   
   <asp:ValidationSummary ID="InputValidationSummary" runat="server" CssClass="errorMessage" 
                    ValidationGroup="InputValidationGroup" />
   <asp:ValidationSummary ID="EditValidationSummary" runat="server" CssClass="errorMessage" 
                    ValidationGroup="EditValidationGroup" />

   <asp:ListView ID="SLAProjectsList" runat="server" 
      DataSourceID="SlaProjectsDataSource" DataKeyNames="Id" 
      InsertItemPosition="LastItem">
      <AlternatingItemTemplate>
         <tr style="">            

            <td>
               <asp:Label ID="ProjectNoLabel" runat="server" Text='<%# Eval("ProjectNo") %>' />
            </td>
            <td>
               
               <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
               <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" 
                  Text="Delete" />
            </td>
         </tr>
      </AlternatingItemTemplate>
      <EditItemTemplate>
         <tr style="">           

            <td>
               <asp:TextBox ID="ProjectNoEditBox" runat="server" 
                  Text='<%# Bind("ProjectNo") %>' />
               <asp:RequiredFieldValidator ID="ProjectNoEditRequired" runat="server" ControlToValidate="ProjectNoEditBox"
                  CssClass="failureNotification" ErrorMessage="ProjectNo is required." ToolTip="ProjectNo is required." Text="*" 
                  ValidationGroup="EditValidationGroup" />
               <asp:CompareValidator ID="ProjectNoEditCompare" runat="server" ControlToValidate="ProjectNoEditBox" 
                  ErrorMessage="ProjectNo has to be an integer" CssClass="failureNotification" ToolTip="ProjectNo has to be an integer"
                  ValidationGroup="EditValidationGroup" Operator="DataTypeCheck" Type="Integer" Text="*"/>
            </td>
            <td>
               <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" 
                  Text="Update" ValidationGroup="EditValidationGroup"/>
               <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" 
                  Text="Cancel" />
            </td>
         </tr>
      </EditItemTemplate>
      <EmptyDataTemplate>
         <table runat="server" style="">
            <tr>
               <td>
                  No data was returned.</td>
            </tr>
         </table>
      </EmptyDataTemplate>
      <InsertItemTemplate>
         <tr style="">

            <td>
               <asp:TextBox ID="ProjectNoInputBox" runat="server" 
                  Text='<%# Bind("ProjectNo") %>' />
               <asp:RequiredFieldValidator ID="ProjectNoInputRequired" runat="server" ControlToValidate="ProjectNoInputBox" 
                  ErrorMessage="ProjectNo is required" CssClass="failureNotification" ToolTip="This field requires an integer"
                  ValidationGroup="InputValidationGroup" Text="*"/>
               <asp:CompareValidator ID="ProjectNoInputCompare" runat="server" ControlToValidate="ProjectNoInputBox" 
                  ErrorMessage="ProjectNo has to be an integer" CssClass="failureNotification" ToolTip="ProjectNo has to be an integer"
                  ValidationGroup="InputValidationGroup" Operator="DataTypeCheck" Type="Integer" Text="*"/>
            </td>
            <td>
               <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" 
                  Text="Insert" ValidationGroup="InputValidationGroup"/>
               <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" 
                  Text="Clear" />
            </td>
         </tr>
      </InsertItemTemplate>
      <ItemTemplate>
         <tr style="">           

            <td>
               <asp:Label ID="ProjectNoLabel" runat="server" Text='<%# Eval("ProjectNo") %>' />
            </td>
            <td>
               
               <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
               <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" 
                  Text="Delete" />
            </td>
         </tr>
      </ItemTemplate>
      <LayoutTemplate>
         <table runat="server" border="1" style="">
            <tr runat="server">
               <td runat="server" style="border-style: none">
                  <table ID="itemPlaceholderContainer" runat="server" border="0" style="">
                     <tr runat="server" style="">                       
                     
                        <th runat="server">ProjectNo</th>
                        <th runat="server">Options</th>
                     </tr>
                     <tr ID="itemPlaceholder" runat="server">
                     </tr>
                  </table>
               </td>
            </tr>
            <tr runat="server">
               <td runat="server" style="">
                  <asp:DataPager ID="DataPager1" runat="server">
                     <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" 
                           ShowNextPageButton="False" ShowPreviousPageButton="False" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" 
                           ShowNextPageButton="False" ShowPreviousPageButton="False" />
                     </Fields>
                  </asp:DataPager>
               </td>
            </tr>
         </table>
      </LayoutTemplate>
      <SelectedItemTemplate>
         <tr style="">
            <td>
               <asp:LinkButton  ID="DeleteButton" runat="server" CommandName="Delete" 
                  Text="Delete" />
               <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td>
               <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
            </td>
            <td>
               <asp:Label ID="ProjectNoLabel" runat="server" Text='<%# Eval("ProjectNo") %>' />
            </td>
         </tr>
      </SelectedItemTemplate>
   </asp:ListView>

</asp:Content>
