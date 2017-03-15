﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.ModuleDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
     </div>
    <div>
    <asp:Label ID="LabelProjectTitle" runat="server"  CssClass="legend titlemantest"></asp:Label>
    </div>
    <div>
    <asp:Label ID="LabelModuleTitle" runat="server"  CssClass="legend titlemantest"></asp:Label>
    </div>
    <br/>
    <asp:Label ID="LabelRelatedTestCases" runat="server" Text="Related Test Cases" CssClass="labelmantest"></asp:Label>
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
        <EmptyDataTemplate>
            No data found
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true"></asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="TITLE"></asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="DESCRIPTION"></asp:BoundField>
            <asp:hyperlinkfield 
                            datanavigateurlfields="Id" 
                            datanavigateurlformatstring="TestCaseDetails.aspx?testCaseId={0}"
                            Text="Details >"/>
            <asp:CommandField ShowEditButton="True"/>
            <asp:CommandField ShowDeleteButton="True"/>
        </Columns>

    </asp:GridView>
</asp:Content>