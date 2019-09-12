<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterSearchCRUD.aspx.cs" Inherits="WebApp.SamplePages.FilterSearchCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Review Basic CRUD</h1>
    <div class="row">
            <div class="col-sm-offset-1">
                <asp:Label Id="label1" runat="server" text="Select an artist to view albums">&nbsp&nbsp&nbsp
                <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>
                </asp:Label>
            </div>
    </div>
</asp:Content>
