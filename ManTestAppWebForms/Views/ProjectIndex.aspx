<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectIndex.aspx.cs" Inherits="ManTestAppWebForms.Views.ProjectIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <div>
    <h2>Projects</h2>
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummaryTestCase" runat="server"  ShowModelStateErrors="true"/>
    </div>
    <div>
    <asp:HyperLink NavigateUrl="~/Views/ProjectCreate" Text="Add New Project" runat="server"  ID="AddNewProjectLink"/>
    </div>
    <asp:GridView runat="server" ID="gv_ProjectIndex"
        DataKeyNames="Id"
        SelectMethod="GetData_ProjectIndex"
        DeleteMethod="gv_ProjectIndex_DeleteItem"
        UpdateMethod="gv_ProjectIndex_UpdateItem"
        ItemType="ManTestAppWebForms.Models.Project"
        OnRowDeleted="gv_ProjectIndex_RowDeleted"
        CssClass="table tablegridview table-hover"
        AllowSorting="true" AllowPaging="true" PageSize="5"
        AutoGenerateColumns="false"
        EditRowStyle-CssClass="SelectedRowStyle"
        OnRowCreated="gv_ProjectIndex_RowCreated">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" SortExpression="Id"></asp:BoundField>
            <asp:TemplateField HeaderText="TITLE" SortExpression="TITLE">
                <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("Title") %>' ID="TextBox1" CssClass="form-control"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="Label1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
                <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("Description") %>' ID="TextBox2" CssClass="form-control"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Label2"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="ModuleCreate.aspx?projectId={0}"
                Text="Add Module" />
            <asp:HyperLinkField
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="AttachTestCase.aspx?projectId={0}"
                Text="Add TestCase" />
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton runat="server" Text="Update" CommandName="Update" CausesValidation="True" ID="LinkButtonEdit"></asp:LinkButton>&nbsp;<asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" ID="LinkButton2"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" ID="LinkButtonEdit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton runat="server" Text="Delete" CommandName="Delete" CausesValidation="False" ID="LinkButtonDelete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("ProjectDetails.aspx?projectId={0}", Item.Id) %>'> Details></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
