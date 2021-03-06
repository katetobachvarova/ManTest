﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="PlaceHolderForLinks" runat="server"></asp:PlaceHolder>
    <div >
     <br/>
    <asp:Label ID="Label3" runat="server" Text="Test Case" CssClass="titlemantest" style="margin-bottom:25px;" ></asp:Label>
    <br/>
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummaryTestCase" runat="server"  ShowModelStateErrors="true"/>
    </div>
    <asp:FormView ID="FormViewCurrentTestCase" runat="server"
         ItemType="ManTestAppWebForms.Models.TestCase"
         DataKeyNames="Id"
         EnableModelValidation="true"
         SelectMethod="FormViewCurrentTestCase_GetItem"
         UpdateMethod="FormViewCurrentTestCase_UpdateItem"
         OnDataBound="FormViewCurrentTestCase_DataBound"
         DeleteMethod="FormViewCurrentTestCase_DeleteItem"
         CssClass="tabletestcasedetails">
            <ItemTemplate>
                <div >
                    <table class="tabletestcasedetails">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Title : "  ></asp:Label>
                            </td>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Title") %>'></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Label ID="Label4" runat="server" Text="Description : "  ></asp:Label>
                            </td>
                            <td class="literalw">
                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("Description") %>' ></asp:Literal>
                            </td>
                        </tr>
                        <tr class="marginbuttons">
                            <td>
                                 <asp:Button ID="ButtonEdit" runat="server" Text="Edit"  CommandName="Edit" class="btn btn-default btnmargintopandbottom"  CausesValidation="true" />
                                 <asp:Button ID="ButtonDelete" runat="server" Text="Delete"  CommandName="Delete" class="btn btn-default btnmargintopandbottom"  OnClientClick="return confirm('Are you sure you want to delete this TestCase?');" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Button ID="ButtonAddStep" runat="server" Text="Add Step"  OnClick="AddStepToTestCase" class="btn btn-default"  CausesValidation="true" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
            <EditItemTemplate>
                 <div >
                    <table class="tabletestcasedetails">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Project : "  ></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListProjectsEdit" runat="server"   CssClass="form-control" DataTextField="Title" DataValueField="Id" OnSelectedIndexChanged="DropDownListProjects_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Label ID="Label4" runat="server" Text="Module : "  ></asp:Label>
                            </td>
                            <td class="literalw">
                                <asp:DropDownList ID="DropDownListModulesEdit" runat="server"   CssClass="form-control" DataTextField="Title" DataValueField="Id" OnSelectedIndexChanged="DropDownListModules_SelectedIndexChanged" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <asp:Label ID="Label5" runat="server" Text="Title : "  ></asp:Label>
                            </td>
                             <td>
                                 <asp:TextBox ID="TextBoxTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                                 CssClass="DDControl DDValidator" ControlToValidate="TextBoxTitle" Display="Static" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <asp:Label ID="Label6" runat="server" Text="Description : "  ></asp:Label>
                            </td>
                             <td>
                                <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control"  TextMode="MultiLine" Rows="3" Wrap="true"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                                    CssClass="DDControl DDValidator" ControlToValidate="TextBoxDescription" Display="Static" Enabled="false" />
                            </td>
                        </tr>
                        <tr class="marginbuttons">
                            <td>
                                <asp:Button ID="ButtonUpdate" runat="server" Text="Update"  CommandName="Update" class="btn btn-default" CausesValidation="true" />
                                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel"  OnClick="btn_TestCaseCancel_Click" class="btn btn-default"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </EditItemTemplate>
    </asp:FormView>
    <br/>
    <div class="col-lg-12">
    <asp:Label ID="LabelRelatedSteps" runat="server" CssClass="titlemantest"></asp:Label>
    </div>

    <asp:ListView ID="ListViewSteps" runat="server"
                  ItemType="ManTestAppWebForms.Models.Step"
                  SelectMethod="ListViewSteps_GetData"
                  DataKeyNames="Id"
                  OnItemDataBound="ListViewSteps_ItemDataBound"
         >
        <ItemTemplate>
            <div class="col-lg-12">
                <table class="tabletestcasedetails">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Step Order : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("StepOrder") %>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Title : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("Title") %>'></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="Label4" runat="server" Text="Description : "  style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                        </td>
                        <td class="literalw">
                            <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("Description") %>' ></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
             <div class="col-lg-12">
                <asp:PlaceHolder ID="PlaceHolderForImages" runat="server"></asp:PlaceHolder>
            </div>
            <br/>
            <div class="col-lg-12">
                <asp:GridView ID="GridViewAttachments_GetData" runat="server"
                              ItemType="ManTestAppWebForms.Models.Attachment"
                              DataKeyNames="Id"
                              AutoGenerateColumns="false"
                              CssClass="table tablegridview table-hover"
                     >
                     <%--<EmptyDataTemplate>
                        No data found
                    </EmptyDataTemplate>--%>
                    <Columns>
                        <asp:BoundField DataField="Id" ReadOnly="true" HeaderText="ID">
                            </asp:BoundField>
                        <asp:BoundField DataField="FileName" HeaderText="FileName">
                            </asp:BoundField>
                        <asp:hyperlinkfield 
                                        datanavigateurlfields="Id" 
                                        datanavigateurlformatstring="AttachmentOpen.aspx?attachmentId={0}"
                                        Text="Open"
                                         />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-lg-12">
            <asp:Button ID="btn_StepDetails" runat="server" Text="Step Details"  OnClick="btn_StepDetails_Click" CommandArgument='<%# Eval("Id") %>' class="btn btn-default" />
            </div>
            <br/>
            <div class="col-lg-12">
            <hr class="hr">
            </div>
        </ItemTemplate>
    </asp:ListView>

    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/jquery.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/eye.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/utils.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/zoomimage.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/layout.js") %>'></script>
</asp:Content>

