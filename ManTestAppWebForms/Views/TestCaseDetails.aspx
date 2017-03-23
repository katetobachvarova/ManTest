<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCaseDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.TestCaseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div>
    <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
    </div>
     <br/>
    <asp:Label ID="Label3" runat="server" Text="Test Case" CssClass="titlemantest"></asp:Label>
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
         DeleteMethod="FormViewCurrentTestCase_DeleteItem">
            <ItemTemplate>
                <div>
                    <table style="margin-left: 0px;">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Title : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="form-control" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Label ID="Label4" runat="server" Text="Description : "  style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                            </td>
                            <td>
                                 <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="form-control"  Enabled="false" TextMode="MultiLine"  Wrap="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Button ID="ButtonEdit" runat="server" Text="Edit"  CommandName="Edit" class="btn btn-default"  style="margin-bottom:5px" CausesValidation="true" />
                                 <asp:Button ID="ButtonDelete" runat="server" Text="Delete"  CommandName="Delete" class="btn btn-default" style="margin-bottom:5px" />
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
                    <asp:DropDownList ID="DropDownListProjectsEdit" runat="server"   CssClass="form-control" DataTextField="Title" DataValueField="Id" OnSelectedIndexChanged="DropDownListProjects_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="DDControl DDValidator" ControlToValidate="DropDownListProjectsEdit" Visible="true" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:DropDownList ID="DropDownListModulesEdit" runat="server"   CssClass="form-control" DataTextField="Title" DataValueField="Id" OnSelectedIndexChanged="DropDownListModules_SelectedIndexChanged" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div>
                    <asp:TextBox ID="TextBoxTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="form-control"></asp:TextBox>
                     <asp:DynamicValidator runat="server" ID="DynamicValidator2" 
                                CssClass="DDControl DDValidator" ControlToValidate="TextBoxTitle" Display="Static" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                                CssClass="DDControl DDValidator" ControlToValidate="TextBoxTitle" Display="Static" Enabled="false" />
                </div>
                <div>
                    <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control"  TextMode="MultiLine" Rows="3" Wrap="true"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                                    CssClass="DDControl DDValidator" ControlToValidate="TextBoxDescription" Display="Static" Enabled="false" />
                </div>
            
                    <div>
                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update"  CommandName="Update" class="btn btn-default" CausesValidation="true" />
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel"  OnClick="btn_TestCaseCancel_Click" class="btn btn-default"/>
                    </div>
            </EditItemTemplate>
    </asp:FormView>
    <br/>
    <div class="col-lg-12">
    <asp:Label ID="LabelRelatedSteps" runat="server" CssClass="titlemantest"></asp:Label>
    </div>
    <br/>
    <asp:ListView ID="ListViewSteps" runat="server"
                  ItemType="ManTestAppWebForms.Models.Step"
                  SelectMethod="ListViewSteps_GetData"
                  DataKeyNames="Id"
                  OnItemDataBound="ListViewSteps_ItemDataBound"
         >
        <ItemTemplate>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Step Order : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("StepOrder") %>' CssClass="form-control" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Title : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="form-control" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="Label4" runat="server" Text="Description : "  style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="form-control"  Enabled="false" TextMode="MultiLine"  Wrap="true"></asp:TextBox>
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
            <div>
            <asp:Button ID="btn_StepDetails" runat="server" Text="Step Details"  OnClick="btn_StepDetails_Click" CommandArgument='<%# Eval("Id") %>' class="btn btn-default" style="margin-left: 15px;"/>
            </div>
                <br/>
            <hr class="hr">
        </ItemTemplate>
    </asp:ListView>

    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/jquery.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/eye.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/utils.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/zoomimage.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/layout.js") %>'></script>
</asp:Content>

