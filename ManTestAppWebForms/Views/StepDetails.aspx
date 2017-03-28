<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StepDetails.aspx.cs" Inherits="ManTestAppWebForms.Views.StepDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="PlaceHolderForLinks" runat="server"></asp:PlaceHolder>
    <div>
        <asp:ValidationSummary ID="ValidationSummaryTestCase" runat="server"  ShowModelStateErrors="true"/>
    </div>
    <br/>
    <asp:FormView ID="FormViewStep" runat="server" 
         ItemType="ManTestAppWebForms.Models.Step"
         DataKeyNames="Id"
         CssClass="FormView formviewnomargin"
         EnableModelValidation="true"
         DeleteMethod="FormViewStep_DeleteItem"
         SelectMethod="FormViewStep_GetItem"
         UpdateMethod="FormViewStep_UpdateItem"
         OnDataBound="FormViewStep_DataBound"
         >
        <ItemTemplate>
        <div>
        <table style="margin-left: 0px;">
             <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Step Order : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                </td>
                <td>
                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("StepOrder") %>'></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Title : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                </td>
                <td>
                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("Title") %>'></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="Label4" runat="server" Text="Description : "  style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                </td>
                <td>
                     <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("Description") %>'></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="ButtonEdit" runat="server" Text="Edit"  CommandName="Edit" class="btn btn-default btnmargintopandbottom" CausesValidation="true" />
                </td>
                <td>
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete"  CommandName="Delete" class="btn btn-default btnmargintopandbottom" OnClientClick="return confirm('Are you sure you want to delete this Step?');"/>
                </td>
            </tr>
        </table>
        </div>
        </ItemTemplate>
        <EditItemTemplate>
        <div>
            <table style="margin-left: 0px;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Step Order : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                    </td>
                    <td>
                            <asp:TextBox ID="StepOrder" runat="server" Text='<%# Bind("StepOrder") %>' CssClass="form-control"></asp:TextBox>
                            <asp:DynamicValidator runat="server" ID="DynamicValidator2" 
                            CssClass="DDControl DDValidator" ControlToValidate="StepOrder" Display="Static" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                            CssClass="DDControl DDValidator" ControlToValidate="StepOrder" Display="Static" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Title : "   style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                    </td>
                    <td>
                            <asp:TextBox ID="TextBoxTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="form-control"></asp:TextBox>
                            <asp:DynamicValidator runat="server" ID="DynamicValidator1" 
                            CssClass="DDControl DDValidator" ControlToValidate="TextBoxTitle" Display="Static" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" 
                            CssClass="DDControl DDValidator" ControlToValidate="TextBoxTitle" Display="Static" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:Label ID="Label4" runat="server" Text="Description : "  style="padding-left: 0px; text-wrap:avoid" ></asp:Label>
                    </td>
                    <td>
                          <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control"  TextMode="MultiLine" Rows="3" Wrap="true"></asp:TextBox>
                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" 
                          CssClass="DDControl DDValidator" ControlToValidate="TextBoxDescription" Display="Static" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="Update"  CommandName="Update" class="btn btn-default"  style="margin-bottom:5px" CausesValidation="true" />
                    </td>
                    <td>
                        <asp:Button ID="Button4" runat="server" Text="Cancel"  OnClick="btn_StepCancel_Click"   class="btn btn-default" style="margin-bottom:5px"/>
                    </td>
                </tr>
            </table>
        </div>
        </EditItemTemplate>
    </asp:FormView>
    <div>
    <asp:Button ID="btnAddAttachment" runat="server" Text="Add Attachment"   OnClick="btn_AddAttachment" class="btn btn-default" style="margin-bottom:5px" />
    </div>
    <div>
        <asp:PlaceHolder ID="PlaceHolderForImages" runat="server"></asp:PlaceHolder>
     </div>
    <br/>
    <asp:GridView ID="gvAttachments" runat="server"
        ItemType="ManTestAppWebForms.Models.Attachment"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        SelectMethod="gvAttachments_GetData"
        DeleteMethod="gvAttachments_DeleteItem"
        CssClass="table tablegridview table-hover"
        OnRowCreated="gvAttachments_RowCreated"
        AllowSorting="true">
        <%--   <EmptyDataTemplate>
            No data found
        </EmptyDataTemplate>--%>
        <Columns>
            <asp:BoundField DataField="Id" ReadOnly="true" HeaderText="ID" SortExpression="ID"></asp:BoundField>
            <asp:BoundField DataField="FileName" HeaderText="FileName" SortExpression="FileName"></asp:BoundField>
            <asp:HyperLinkField
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="AttachmentOpen.aspx?attachmentId={0}"
                Text="Open" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton runat="server" Text="Delete" CommandName="Delete" CausesValidation="False" ID="LinkButton1" OnClientClick="return confirm('Are you sure you want to delete this Attachment?');"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/jquery.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/eye.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/utils.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/zoomimage.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/Zoom/layout.js") %>'></script>
</asp:Content>

