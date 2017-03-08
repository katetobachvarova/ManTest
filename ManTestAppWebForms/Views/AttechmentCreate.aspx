<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttechmentCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.AttechmentCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FileUpload ID="FileUploadControl" runat="server" />
    <asp:Button ID="btn_Upload" runat="server" Text="Upload"  OnClick="btn_Upload_Click"/>
    <br /><br />
    <asp:Label ID="StatusLabel" runat="server" Text="Upload Status : "></asp:Label>
</asp:Content>
