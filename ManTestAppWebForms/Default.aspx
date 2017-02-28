<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ManTestAppWebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" >
        <h1>ManTest</h1>
        <p class="lead">ManTest is a web application for managing manual test cases</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Projects</h2>
            <p>
                <a class="btn btn-default" href="http://localhost:49280/Views/ProjectIndex">Project Index &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Modules</h2>
            <p>
                <a class="btn btn-default" href="http://localhost:49280/Views/ModuleIndex">Module Index &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Test Cases</h2>
            <p>
                <a class="btn btn-default" href="http://localhost:49280/Views/TestCaseIndex">Test Case Index &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
