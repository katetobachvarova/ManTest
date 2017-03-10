﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.ModuleDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text='<%# "Module " + currentModule.Title %>'  CssClass="legend titlemantest"></asp:Label>
      <asp:Label ID="Label2" runat="server" Text='<%# "Project " + currentModule.Project.Title %>'  CssClass="legend titlemantest"></asp:Label>
    <br/>
    <asp:GridView ID="GridViewTestCases" runat="server"
                     ItemType="ManTestAppWebForms.Models.TestCase"
         DataKeyNames="Id"
         SelectMethod="GridViewTestCases_GetData"
         UpdateMethod="GridViewTestCases_UpdateItem"
         DeleteMethod="GridViewTestCases_DeleteItem"
         AutoGenerateColumns="false"
         AutoGenerateDeleteButton="false"
         AutoGenerateEditButton="false"
        CssClass="table tablegridview">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true"></asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="TITLE"></asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="DESCRIPTION"></asp:BoundField>
            <asp:hyperlinkfield 
                            datanavigateurlfields="Id" 
                            datanavigateurlformatstring="TestCaseDetails.aspx?testCaseId={0}"
                            Text="Details"/>
            <asp:CommandField ShowEditButton="True"/>
            <asp:CommandField ShowDeleteButton="True"/>
        </Columns>

    </asp:GridView>
</asp:Content>
