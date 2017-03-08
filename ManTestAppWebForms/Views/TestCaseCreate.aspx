﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary ShowModelStateErrors="true" runat="server" />
    
    <asp:Label ID="Label1" runat="server" Text="Project : "></asp:Label>
    <asp:DropDownList ID="DropDownListProjects" runat="server"  CssClass="form-control" 
        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"
        ItemType="ManTestAppWebForms.Models.Project" >
    </asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="DDControl DDValidator" ControlToValidate="DropDownListProjects" Display="Static" Enabled="false" />

    <asp:Label ID="Label2" runat="server" Text="Module : "></asp:Label>
     <asp:DropDownList runat="server"
          ID="DropDownListModules"
          CssClass="form-control"
         Enabled="false"
          OnSelectedIndexChanged="DropDownListModules_SelectedIndexChanged" AutoPostBack="true">
     </asp:DropDownList>
        <asp:FormView runat="server"  CssClass="FormView" ID="Form"
          ItemType="ManTestAppWebForms.Models.TestCase"
          InsertMethod="InsertItem_TestCase"
          DefaultMode="Insert"
          OnItemInserted="ItemInserted_TestCase" Height="130px" Width="279px"
          EnableModelValidation="true" >
            <InsertItemTemplate>
              <table>
                <tr>
                  <td class="FormViewHeader">
                    Title:
                  </td>
                  <td>
                    <asp:TextBox ID="TestCaseTitle" runat="server"  CssClass="form-control"
                         Text='<%# Bind("Title") %>'></asp:TextBox>
                      <asp:DynamicValidator runat="server" ID="DynamicValidator2" 
                        CssClass="DDControl DDValidator" ControlToValidate="TestCaseTitle" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                        CssClass="DDControl DDValidator" ControlToValidate="TestCaseTitle" Display="Static" Enabled="false" />
                  </td>
                </tr>
                <tr>
                  <td class="FormViewHeader">
                    Description:
                  </td>
                  <td>
                    <asp:TextBox ID="TestCaseDescription" runat="server"  CssClass="form-control"
                           Text='<%# Bind("Description") %>'></asp:TextBox>
                    <asp:DynamicValidator runat="server" ID="DynamicValidator1" 
                        CssClass="DDControl DDValidator" ControlToValidate="TestCaseDescription" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                        CssClass="DDControl DDValidator" ControlToValidate="TestCaseDescription" Display="Static" Enabled="false" />
                  </td>
                </tr>
              </table>
                <asp:Button runat="server" Text="Insert" CommandName="Insert" class="btn btn-default"  CausesValidation="true"/>
                <asp:Button runat="server" Text="Cancel" CausesValidation="false"  OnClick="Cancel_Click"  class="btn btn-default" />
            </InsertItemTemplate>

        </asp:FormView>

</asp:Content>