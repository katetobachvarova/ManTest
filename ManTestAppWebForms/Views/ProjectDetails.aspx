<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.ProjectDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
    </div>
    <br/>
    <div style="width:100%;">
    <asp:Label ID="LabelRelatedModules" runat="server"   CssClass="labelmantest"></asp:Label>
        <asp:GridView ID="GridViewModules" runat="server"
            ItemType="ManTestAppWebForms.Models.Module"
            DataKeyNames="Id"
            SelectMethod="GetModules"
            DeleteMethod="GridViewModules_DeleteItem"
            UpdateMethod="GridViewModules_UpdateItem"
            AutoGenerateColumns="false"
            CssClass="table tablegridview"
            OnRowCreated="GridViewModules_RowCreated"
            EditRowStyle-CssClass="SelectedRowStyle">
     <%--       <EmptyDataTemplate>
                No data found
            </EmptyDataTemplate>--%>
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true"></asp:BoundField>
            <asp:TemplateField HeaderText="TITLE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Title") %>' ID="TextBox1" CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="Label1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DESCRIPTION">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Description") %>' ID="TextBox2" CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Label2"></asp:Label>
                    </ItemTemplate>
             </asp:TemplateField>
            
             <asp:HyperLinkField
                            DataNavigateUrlFields="Id"
                            DataNavigateUrlFormatString="AttachTestCase.aspx?moduleId={0}"
                            Text="Add TestCase" />
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
                    <asp:LinkButton runat="server" Text="Delete" CommandName="Delete" CausesValidation="False" ID="LinkButton4"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:Hyperlinkfield 
                            datanavigateurlfields="Id" 
                            datanavigateurlformatstring="ModuleDetails.aspx?moduleId={0}"
                            Text="Details>"/>

        </Columns>
    </asp:GridView>

    <asp:Label ID="LabelRelatedTestCases" runat="server" CssClass="labelmantest"></asp:Label>

        <asp:GridView ID="GridViewTestCases" runat="server"
            ItemType="ManTestAppWebForms.Models.TestCase"
            DataKeyNames="Id"
            SelectMethod="GetTestCases"
            DeleteMethod="GridViewTestCases_DeleteItem"
            UpdateMethod="GridViewTestCases_UpdateItem"
            AutoGenerateColumns="false"
            CssClass="table tablegridview"
            OnRowCreated="GridViewTestCases_RowCreated"
            EditRowStyle-CssClass="SelectedRowStyle">
            <%--   <EmptyDataTemplate>
            No data found
        </EmptyDataTemplate>--%>
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true"></asp:BoundField>
                <asp:TemplateField HeaderText="TITLE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Title") %>' ID="TextBox1" CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="Label1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DESCRIPTION">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Description") %>' ID="TextBox2" CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Label2"></asp:Label>
                    </ItemTemplate>

                    <ItemStyle CssClass="col"></ItemStyle>
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
                            datanavigateurlfields="Id" 
                            datanavigateurlformatstring="TestCaseDetails.aspx?testCaseId={0}"
                            Text="Details>"/>
        </Columns>
    </asp:GridView>
    </div>
</asp:Content>
