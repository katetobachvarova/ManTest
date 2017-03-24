<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseIndex.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Test Cases</h2>
    <div>
        <asp:ValidationSummary ID="ValidationSummaryTestCase" runat="server"  ShowModelStateErrors="true"/>
    </div>
    <asp:HyperLink NavigateUrl="~/Views/TestCaseCreate" Text="Add New TestCase" runat="server" ID="AddNewTestCaseLink"/>
    <asp:GridView ID="gvTestCases" runat="server"
        AutoGenerateColumns="false"
        ItemType="ManTestAppWebForms.Models.TestCase"
        DataKeyNames="Id"
        CssClass="table tablegridview table-hover"
        SelectMethod="gvTestCases_GetData"
        DeleteMethod="gvTestCases_DeleteItem"
        UpdateMethod="gvTestCases_UpdateItem"
        AllowSorting="true" AllowPaging="true" PageSize="5"
        EditRowStyle-CssClass="SelectedRowStyle"
        OnRowCreated="gvTestCases_RowCreated"
        OnRowDataBound="gvTestCases_RowDataBound">
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
            <asp:TemplateField HeaderText="Related Project" SortExpression="Project.Title">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListProject" runat="server" CssClass="form-control"  
                         OnSelectedIndexChanged="DropDownListProject_SelectedIndexChanged" 
                         AutoPostBack="true"
                         ItemType="ManTestAppWebForms.Models.Project"
                         DataValueField="Id"
                         DataTextField="Title">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Project.Title") %>' ID="Label3"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Related Module" SortExpression="Module">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListModule" runat="server" CssClass="form-control" 
                        OnSelectedIndexChanged="DropDownListModule_SelectedIndexChanged"
                         AutoPostBack="true"
                         ItemType="ManTestAppWebForms.Models.Module"
                         DataValueField="Id"
                         DataTextField="Title">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Module.Title") %>' ID="Label4"></asp:Label>
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
                    <asp:LinkButton runat="server" Text="Delete" CommandName="Delete" CausesValidation="False" ID="LinkButton3"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:hyperlinkfield  
                                DataNavigateUrlFields="Id"
                                DataNavigateUrlFormatString="StepCreate.aspx?testCaseId={0}"
                                Text="Add Step"/>
          <asp:hyperlinkfield 
                                DataNavigateUrlFields="Id"
                                DataNavigateUrlFormatString="TestCaseDetails.aspx?testCaseId={0}"
                                Text="Details>"/>
        </Columns>
    </asp:GridView>
</asp:Content>
