﻿<%@ Page Title="ASP.NET Web Forms MVP" Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsMvp.ProjectSite.Default" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/Script/jquery.fancybox/jquery.fancybox.css") %>" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="content" runat="server">
    <div id="home-page">
        <p>
            ASP.NET MVC might be the new kid on the block, but there are still
            a host of compelling advantages to ASP.NET Web Forms.
        </p>
        <p>
            The ASP.NET Web Forms MVP project is about bringing the love back
            to Web Forms through a renewed approach to using it - an approach
            that facilitates <strong>separation of concerns</strong> and
            <strong>testability</strong> whilst maintaining the
            <strong>rapid development</strong> that Web Forms was built to
            deliver.
        </p>
        <ul>
            <li class="first"><a id="whyVideoLink" href="http://wiki.webformsmvp.com/index.php?title=Why_ASP.NET_Web_Forms_MVP%3F">Why MVP? Find out in 3 mins.</a></li>
            <li class="second"><a href="http://webformsmvp.codeplex.com/Release/ProjectReleases.aspx">Download today.</a></li>
            <li class="third"><a href="http://wiki.webformsmvp.com">Learn more.</a></li>
            <li class="fourth"><a href="http://wiki.webformsmvp.com/index.php?title=Spread_the_Word">Spread the word.</a></li>
        </ul>
        <div id="whyVideoEmbedded"></div>
    </div>
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Script/jquery.fancybox/jquery.fancybox-1.2.1.pack.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Script/site.js") %>" type="text/javascript"></script>
</asp:Content>