<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StepDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.StepDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <div>
    <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
    <asp:TextBox ID="TextBoxTitle" runat="server" Text='<%# Step.Title %>'></asp:TextBox>
    </div>
    <div>
    <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
    <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Step.Description %>' Height="65px"></asp:TextBox>
    </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Attachments"></asp:Label>
    <asp:ListView ID="ListViewAttachments" runat="server" SelectMethod="ListViewAttachments_GetData"
         ItemType="ManTestAppWebForms.Models.Attachment">
        <ItemTemplate>
            <li title='<%# Eval("Url") %>'></li>
            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Url") %>'></asp:Label>
        </ItemTemplate>
    </asp:ListView>
    <asp:Button ID="Button1" runat="server" Text="Add Attachment"   OnClick="btn_AddAttachment"  />
</asp:Content>
