<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.ModuleDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
     </div>
    <br/>
    <asp:Label ID="LabelRelatedTestCases" runat="server" CssClass="labelmantest"></asp:Label>
    <asp:GridView ID="GridViewTestCases" runat="server"
        ItemType="ManTestAppWebForms.Models.TestCase"
        DataKeyNames="Id"
        SelectMethod="GridViewTestCases_GetData"
        UpdateMethod="GridViewTestCases_UpdateItem"
        DeleteMethod="GridViewTestCases_DeleteItem"
        AutoGenerateColumns="false"
        CssClass="table tablegridview table-hover"
        OnRowCreated="GridViewTestCases_RowCreated"
        EditRowStyle-CssClass="SelectedRowStyle"
        AllowSorting="true"
        AllowPaging="true">
        <%--<EmptyDataTemplate>
            No data found
        </EmptyDataTemplate>--%>
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" SortExpression="ID"></asp:BoundField>
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
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton runat="server" Text="Update" CommandName="Update" CausesValidation="True" ID="LinkButton1"></asp:LinkButton>&nbsp;<asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="False" ID="LinkButton2"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton runat="server" Text="Edit" CommandName="Edit" CausesValidation="False" ID="LinkButton1"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton runat="server" Text="Delete" CommandName="Delete" CausesValidation="False" ID="LinkButton2" OnClientClick="return confirm('Are you sure you want to delete this Module?');"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:HyperLinkField
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="TestCaseDetails.aspx?testCaseId={0}"
                Text="Details>" />
           
        </Columns>
    </asp:GridView>
</asp:Content>
