<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseIndex.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Test Cases</h2>
    <div>
        <asp:ValidationSummary ID="ValidationSummaryTestCase" runat="server"  ShowModelStateErrors="true"/>
    </div>
    <asp:HyperLink NavigateUrl="~/Views/TestCaseCreate" Text="Add New TestCase" runat="server" />
    <asp:GridView ID="gvTestCases" runat="server"
        AutoGenerateColumns="false"
        ItemType="ManTestAppWebForms.Models.TestCase"
        DataKeyNames="Id"
        CssClass="table tablegridview"
        SelectMethod="gvTestCases_GetData"
        DeleteMethod="gvTestCases_DeleteItem"
        UpdateMethod="gvTestCases_UpdateItem"
        AllowSorting="true" AllowPaging="true" PageSize="5"
        EditRowStyle-CssClass="SelectedRowStyle"
        OnRowCreated="gvTestCases_RowCreated">
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
            <asp:TemplateField HeaderText="Related Project Id">
                <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("ProjectId") %>' ID="TextBox3" CssClass="form-control"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("ProjectId") %>' ID="Label3"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Related Module Id">
                <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("ModuleId") %>' ID="TextBox4" CssClass="form-control"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("ModuleId") %>' ID="Label4"></asp:Label>
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
          
          <%--<asp:TemplateField>
              <ItemTemplate>
                  <asp:HyperLink ID="EditHyperLink" runat="server"  Text="Edit" NavigateUrl="~/Views/ProjectIndex.aspx"></asp:HyperLink>
                  <asp:Literal ID="Literal1" runat="server"> / </asp:Literal>
                  <asp:HyperLink ID="DeleteHyperLink" runat="server" Text="Delete" NavigateUrl="~/Views/ProjectIndex.aspx"></asp:HyperLink>
                  <asp:Literal ID="Literal2" runat="server"> / </asp:Literal>
                  <asp:HyperLink ID="DetailsHyperLink" runat="server" Text="Details" NavigateUrl="~/Views/ProjectIndex.aspx"></asp:HyperLink>
               </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                  </EditItemTemplate>
          </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
</asp:Content>
