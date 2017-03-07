<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.ProjectDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TreeView ID="TreeViewModules" runat="server"  CssClass="TreeView" ></asp:TreeView>
</asp:Content>



<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.ProjectDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="ListViewModules" runat="server"  
        InsertMethod="ListView1_InsertItem"
         SelectMethod="ListViewModules_GetData"
        >
        <LayoutTemplate>
        <table id="t">
            <thead>
                <th>
                    Id
                </th>
                <th>
                    Title
                </th>
                 <th>
                    Description
                </th>
                <th> Project Description
                </th>
            </thead>
           <tr id="itemPlaceholder" runat="server"></tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <%# Eval("Id") %>
            </td>
            <td>
                <%# Eval("Title") %>
            </td>
            <td>
                <%# Eval("Description") %>
            </td>
            <td>
                <%# Eval("Project.Description") %>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <h2>
            No Records!</h2>
    </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>--%>
