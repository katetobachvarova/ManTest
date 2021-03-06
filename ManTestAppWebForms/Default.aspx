﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ManTestAppWebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" >
        <h1>ManTest</h1>
        <p class="lead">ManTest is a web application for managing manual test cases</p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Projects</h2>
            <p>
                <a class="btn btn-default" href="Views/ProjectIndex">Project Index &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2>Test Cases</h2>
            <p>
                <a class="btn btn-default" href="Views/TestCaseIndex">Test Case Index &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
