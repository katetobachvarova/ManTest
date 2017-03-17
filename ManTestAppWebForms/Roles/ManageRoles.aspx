<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRoles.aspx.cs" Inherits="ManTestAppWebForms.Roles.ManageRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Manage Roles</h2>
    <p>
        <b>Create a New Role: </b>
        <asp:TextBox ID="RoleName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RoleNameReqField" runat="server" 
            ControlToValidate="RoleName" Display="Dynamic" 
            ErrorMessage="You must enter a role name."></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="CreateRoleButton" runat="server" Text="Create Role" 
            onclick="CreateRoleButton_Click"/>
    </p>
    <p>
        <asp:GridView ID="RoleList" runat="server" AutoGenerateColumns="False" 
            onrowdeleting="RoleList_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="RoleNameLabel" Text='<%# Bind("Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField DeleteText="Delete Role" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
