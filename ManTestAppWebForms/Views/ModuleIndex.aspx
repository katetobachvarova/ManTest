﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleIndex.aspx.cs" Inherits="ManTestAppWebForms.Views.ModuleIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Modules</h2>
    <asp:HyperLink NavigateUrl="~/Views/ModuleCreate" Text="Add New Module" runat="server" />
    <asp:GridView runat="server" ID="gv_ModuleIndex"
                  DataKeyNames="Id"
                  SelectMethod="gv_ModuleIndex_GetData"
                  DeleteMethod="gv_ModuleIndex_DeleteItem"
                  UpdateMethod="gv_ModuleIndex_UpdateItem"
                  ItemType="ManTestAppWebForms.Models.Module"
                  AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" AutoGenerateSelectButton="false"
                  OnRowDeleted="gv_ModuleIndex_RowDeleted"
                  CssClass="table table-hover table-striped"
                  AllowSorting="true" AllowPaging="true" PageSize="5"
                  AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:GridView runat="server" ID="Kat">  
                    </asp:GridView>
                    </ItemTemplate>
            </asp:TemplateField>

        <asp:BoundField  DataField="Id"  HeaderText="ID"></asp:BoundField>
        <asp:BoundField  DataField="Title"  HeaderText="TITLE"></asp:BoundField>
        <asp:BoundField  DataField="Description"  HeaderText="DESCRIPTION"></asp:BoundField>
        <asp:hyperlinkfield headertext="Add Test Case"
                            datanavigateurlfields="Id" 
                            datanavigateurlformatstring="AttachTestCase.aspx?moduleId={0}"
                            Text="Add Test Case"
              />
        </Columns>
    </asp:GridView>
</asp:Content>
