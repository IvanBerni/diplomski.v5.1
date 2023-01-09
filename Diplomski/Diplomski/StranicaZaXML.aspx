<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StranicaZaXML.aspx.cs" Inherits="Diplomski.StranicaZaXML" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <br />

     <asp:Button ID="ExportUXml" runat="server" Text="EXPORT KORISNIKA iz baze u XML" OnClick="ExportUXml_Click" />

    <br />
    <br />
    <br />

    <asp:Button ID="SpremiXMLuBazu" runat="server" Text="SPREMI U BAZU podatke iz XML" OnClick="SpremiXMLuBazu_Click" Width="401px" />
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
    <BR />
   <!-- <td>  
        </td>  -->
    <br />
    <br />
    <asp:Button ID="BtUsporedi" runat="server" Text="Usporedi liste korisnika" OnClick="BtUsporedi_Click" Width="398px" />
    
    <br />
    <br />
    <br />
    <asp:Button ID="UsporediDvaXmla" runat="server" Text="Usporedi dva XML-a" OnClick="UsporediDvaXmla_Click" Width="398px" />
    <br />
    <br />
    <br />
    <br />
    <asp:Button ID="bt_uveziXml" runat="server" Text="uveziXml" OnClick="bt_uveziXml_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:FileUpload runat="server" ID="FileUpload1" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />

   <br /> 
</asp:Content>
