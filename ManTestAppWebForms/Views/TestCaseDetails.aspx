<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TreeView ID="TreeViewTestCase" runat="server" OnSelectedNodeChanged="TreeViewTestCase_SelectedNodeChanged" ></asp:TreeView>
</asp:Content>

