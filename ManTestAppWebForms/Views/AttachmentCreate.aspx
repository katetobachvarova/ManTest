<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttachmentCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.AttechmentCreate" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div id="labelAttachment">
        <asp:Label ID="Label1" runat="server" Text="Attachment" ></asp:Label>
        </div>
            <div class="form-group">
                <br />
                <asp:FileUpload ID="FileUploadControl" runat="server" />
                <br />
                <asp:Button ID="btn_Upload" runat="server" Text="Upload" OnClick="btn_Upload_Click"  class="btn btn-default"/>
                <asp:Label ID="StatusLabel" runat="server" Text="" ></asp:Label>
            </div>

</asp:Content>
