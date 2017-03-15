<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRoles.aspx.cs" Inherits="ManTestAppWebForms.Roles.UserRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewUsers" runat="server"
         ItemType="ManTestAppWebForms.Models.ApplicationUser"
         SelectMethod="GridViewUsers_GetData"
         AutoGenerateColumns ="false"
         OnRowDataBound="GridViewUsers_RowDataBound"
         UpdateMethod="GridViewUsers_UpdateItem1"
         AutoGenerateEditButton="false"
         DataKeyNames="Email"
         OnRowUpdating="GridViewUsers_RowUpdating"
         >
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="UserName" ></asp:BoundField>
<asp:TemplateField HeaderText="Role">
  <%--  <ItemTemplate>
                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Month") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlMonth" runat="server" DataSource='<%#GetMonths() %>' DataTextField="monthname" DataValueField="month"></asp:DropDownList>
                    </EditItemTemplate>--%>
    <ItemTemplate>
        
        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="DropDownListRoles" runat="server" 
                          OnSelectedIndexChanged="DropDownListRoles_SelectedIndexChanged"
                          DataTextField="Name" DataValueField="Id"  >
            </asp:DropDownList>
        </EditItemTemplate>

</asp:TemplateField>
            <asp:CommandField ShowEditButton="True"/>

        </Columns>


    </asp:GridView>
</asp:Content>
