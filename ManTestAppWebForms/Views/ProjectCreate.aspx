<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.ProjectCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary ShowModelStateErrors="true" runat="server" />
    <asp:FormView runat="server"  CssClass="FormView"
          ItemType="ManTestAppWebForms.Models.Project"
          InsertMethod="InsertItem_Project"
          DefaultMode="Insert"
          OnItemInserted="ItemInserted_Project" Height="130px" Width="279px"
          EnableModelValidation="true">
    <InsertItemTemplate>
          <table>
            <tr>
              <td class="FormViewHeader">
                Title:
              </td>
              <td>
                  <div>
                <asp:TextBox ID="ProjectTitle" runat="server"  CssClass="form-control"
                     Text='<%# Bind("Title") %>'></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                    CssClass="DDControl DDValidator" ControlToValidate="ProjectTitle" Display="Dynamic" Enabled="true" Text="*"  ErrorMessage="The Title field is required"/>
              </div>
                      </td>
            </tr>
            <tr>
              <td class="FormViewHeader">
                Description:
              </td>
              <td>
                  <div>
                <asp:TextBox ID="ProjectDescription" runat="server"  CssClass="form-control"
                       Text='<%# Bind("Description") %>'></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                    CssClass="DDControl DDValidator" ControlToValidate="ProjectDescription" Display="Dynamic" Enabled="true" Text="*"  ErrorMessage="The Description field is required"/>
              </div>
                      </td>
            </tr>
          </table>
            <asp:Button runat="server" Text="Insert" CommandName="Insert" class="btn btn-default"  CausesValidation="true"/>
            <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="Cancel_Click"  class="btn btn-default" />
        </InsertItemTemplate>
        </asp:FormView>
</asp:Content>
