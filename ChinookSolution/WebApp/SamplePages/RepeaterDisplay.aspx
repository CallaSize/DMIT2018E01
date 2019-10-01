<%@ Page Title="Repeater Nested Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterDisplay.aspx.cs" Inherits="WebApp.SamplePages.RepeaterDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Displaying </h1>
    <%--header template is top of list
        foot template is bottom of list
        item template occurs once per DTO record
        Alternating Item Template(every other record)--%>
    <asp:Repeater ID="AlbumTracksList" runat="server" 
        DataSourceID="AlbumTracksListODS" 
        ItemType="ChinookSystem.Data.DTOs.AlbumDTO"> <%--to ease working with the properties in your class on this control, use the item type attribute and assign the class name of your data definition--%>
        <HeaderTemplate>
            <h3>Albums and Tracks</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <h5><strong>Album:<%# Item.AlbumTitle%> </strong></h5>
            <p><strong>Artist:<%# Item.AlbumArtist%> </strong>
                # of Tracks: <%# Item.TrackCount %>
                Play Time: <%# Item.PlayTime %>
            </p>
            <asp:GridView ID="TrackList" runat="server" 
                DataSource="<%# Item.AlbumTracks %>"
                CssClass="table"
                GridLines="Horizontal"
                BorderStyle="None">

            </asp:GridView>
        </ItemTemplate>
        <FooterTemplate>
            &copy; DMIT 2019 NAIT course all rights reserved
        </FooterTemplate>
    </asp:Repeater>
   
    <asp:ObjectDataSource ID="AlbumTracksListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_AlbumandTracks" 
        TypeName="ChinookSystem.BLL.AlbumController">

    </asp:ObjectDataSource>

</asp:Content>
