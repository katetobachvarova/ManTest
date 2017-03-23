<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary ShowModelStateErrors="true" runat="server" />
     <br/>
    <asp:Label ID="Label1" runat="server" Text="Project : " class="col-lg-2 control-label"></asp:Label>
    <div class="col-lg-10">
        <asp:DropDownList ID="DropDownListProjects" runat="server"   class="form-control formcontrol2"
        OnSelectedIndexChanged="DropDownListProjects_SelectedIndexChanged" AutoPostBack="true"
        ItemType="ManTestAppWebForms.Models.Project" >
    </asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="DDControl DDValidator" ControlToValidate="DropDownListProjects" Display="Static" Enabled="false" />
    </div>
    <asp:Label ID="Label2" runat="server" Text="Module : " class="col-lg-2 control-label"></asp:Label>
     <div class="col-lg-10">
        <asp:DropDownList runat="server"
          ID="DropDownListModules"
          CssClass="form-control formcontrol2"
          Enabled="false"
          OnSelectedIndexChanged="DropDownListModules_SelectedIndexChanged" AutoPostBack="true">
     </asp:DropDownList>
     </div>
        <asp:FormView runat="server" ID="Form" CssClass="tableformview"
          ItemType="ManTestAppWebForms.Models.TestCase"
          InsertMethod="InsertItem_TestCase"
          DefaultMode="Insert"
          OnItemInserted="ItemInserted_TestCase" 
          EnableModelValidation="true" >
            <InsertItemTemplate >
                        <asp:Label ID="Label5" runat="server" Text="Title : " class="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="TestCaseTitle" runat="server"  CssClass="form-control formcontrol2" 
                             Text='<%# Bind("Title") %>'></asp:TextBox>
                            <asp:DynamicValidator runat="server" ID="DynamicValidator2" 
                            CssClass="DDControl DDValidator" ControlToValidate="TestCaseTitle" Display="Static" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                            CssClass="DDControl DDValidator" ControlToValidate="TestCaseTitle" Display="Static" Enabled="false" />
                        </div>
                        <asp:Label ID="Label6" runat="server" Text="Description : " class="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="TestCaseDescription" runat="server"  CssClass="form-control formcontrol2" TextMode="MultiLine" Rows="3"
                                         Text='<%# Bind("Description") %>'></asp:TextBox>
                            <asp:DynamicValidator runat="server" ID="DynamicValidator1" 
                                CssClass="DDControl DDValidator" ControlToValidate="TestCaseDescription" Display="Static" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                                CssClass="DDControl DDValidator" ControlToValidate="TestCaseDescription" Display="Static" Enabled="false" />
                        </div>
                    <asp:Button runat="server" Text="Insert" CommandName="Insert" class="btn btn-default"  CausesValidation="true"/>
                    <asp:Button runat="server" Text="Cancel" CausesValidation="false"  OnClick="Cancel_Click"  class="btn btn-default" />
            </InsertItemTemplate>
        </asp:FormView>
</asp:Content>
