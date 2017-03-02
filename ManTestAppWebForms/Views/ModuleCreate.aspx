<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.ModuleCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ValidationSummary ShowModelStateErrors="true" runat="server" />
        <asp:FormView runat="server"  CssClass="FormView"
          ItemType="ManTestAppWebForms.Models.Module"
          InsertMethod="InsertItem_Module"
          DefaultMode="Insert"
          OnItemInserted="ItemInserted_Module" Height="130px" Width="279px"
          EnableModelValidation="true">
            <InsertItemTemplate>
              <table>
                <tr>
                  <td class="FormViewHeader">
                    Title:
                  </td>
                  <td>
                    <asp:TextBox ID="ModuleTitle" runat="server" 
                         Text='<%# Bind("Title") %>'></asp:TextBox>
                      <asp:DynamicValidator runat="server" ID="DynamicValidator2" 
                        CssClass="DDControl DDValidator" ControlToValidate="ModuleTitle" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                        CssClass="DDControl DDValidator" ControlToValidate="ModuleTitle" Display="Static" Enabled="false" />
                  </td>
                </tr>
                <tr>
                  <td class="FormViewHeader">
                    Description:
                  </td>
                  <td>
                    <asp:TextBox ID="ModuleDescription" runat="server" 
                           Text='<%# Bind("Description") %>'></asp:TextBox>
                    <asp:DynamicValidator runat="server" ID="DynamicValidator1" 
                        CssClass="DDControl DDValidator" ControlToValidate="ModuleDescription" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                        CssClass="DDControl DDValidator" ControlToValidate="ModuleDescription" Display="Static" Enabled="false" />
                  </td>
                </tr>
              </table>
                <asp:Button runat="server" Text="Insert" CommandName="Insert" class="btn btn-default"  CausesValidation="true"/>
                <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="Cancel_Click"  class="btn btn-default" />
            </InsertItemTemplate>
        </asp:FormView>
</asp:Content>
