<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayArtistAlbums.aspx.cs" Inherits="WebApp.SamplePages.DisplayArtistAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="ArtistAlbums" runat="server" 
        AutoGenerateColumns="False" 
        DataSourceID="ArtistAlbumsListODS" 
        AllowPaging="True"
        PageSize="5">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="ArtistName" HeaderText="ArtistName" SortExpression="ArtistName"></asp:BoundField>
            <asp:BoundField DataField="RYear" HeaderText="RYear" SortExpression="RYear"></asp:BoundField>
            <asp:BoundField DataField="Label" HeaderText="Label" SortExpression="Label"></asp:BoundField>
        </Columns>
    </asp:GridView>
    
    <asp:ObjectDataSource ID="ArtistAlbumsListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_AlbumsOfArtist" 
        TypeName="ChinookSystem.BLL.AlbumController">
    </asp:ObjectDataSource>
</asp:Content>
