<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StepCreate.aspx.cs" Inherits="ManTestAppWebForms.Views.StepCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create Step</h2>
     <asp:ValidationSummary ShowModelStateErrors="true" runat="server" />
    <asp:FormView ID="FormViewStep" runat="server"
                  ItemType="ManTestAppWebForms.Models.Step"
                  DefaultMode="Insert"
                  CssClass="FormView"
                  EnableModelValidation="true"
                  InsertMethod="FormViewStep_InsertItem"
                  OnItemInserted="FormViewStep_ItemInserted">
        <InsertItemTemplate>
            <table>
                <tr>
                  <td class="FormViewHeader">
                    Step Order:
                  </td>
                  <td>
                    <asp:TextBox ID="TextBox1" runat="server"  CssClass="form-control"
                         Text='<%# Bind("StepOrder") %>'></asp:TextBox>
                      <asp:DynamicValidator runat="server" ID="DynamicValidator3" 
                        CssClass="DDControl DDValidator" ControlToValidate="StepTitle" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" 
                        CssClass="DDControl DDValidator" ControlToValidate="StepTitle" Display="Static" Enabled="false" />
                  </td>
                </tr>
                <tr>
                  <td class="FormViewHeader">
                    Title:
                  </td>
                  <td>
                    <asp:TextBox ID="StepTitle" runat="server"  CssClass="form-control"
                         Text='<%# Bind("Title") %>'></asp:TextBox>
                      <asp:DynamicValidator runat="server" ID="DynamicValidator2" 
                        CssClass="DDControl DDValidator" ControlToValidate="StepTitle" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                        CssClass="DDControl DDValidator" ControlToValidate="StepTitle" Display="Static" Enabled="false" />
                  </td>
                </tr>
                <tr>
                  <td class="FormViewHeader">
                    Description:
                  </td>
                  <td>
                    <asp:TextBox ID="StepDescription" runat="server"  CssClass="form-control"
                           Text='<%# Bind("Description") %>'></asp:TextBox>
                    <asp:DynamicValidator runat="server" ID="DynamicValidator1" 
                        CssClass="DDControl DDValidator" ControlToValidate="StepDescription" Display="Static" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                        CssClass="DDControl DDValidator" ControlToValidate="StepDescription" Display="Static" Enabled="false" />
                  </td>
                </tr>
              </table>
                <asp:Button runat="server" Text="Insert" CommandName="Insert" class="btn btn-default"  CausesValidation="true"/>
                <asp:Button runat="server" Text="Cancel" CausesValidation="false"  OnClick="Cancel_Click"  class="btn btn-default" />
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
