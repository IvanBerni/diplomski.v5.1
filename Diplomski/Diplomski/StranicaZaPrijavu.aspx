<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StranicaZaPrijavu.aspx.cs" Inherits="Diplomski.Prijava" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table style="width: 100%; border: 1px solid #FF00FF; background-color: #C0C0C0">
        <tr>
            <td style="width: 173px">q
                <asp:Label ID="Name" runat="server" CssClass="lbl" Text="UserName:" />
            </td>
            <td style="width: 428px">
                <asp:TextBox ID="txtUserName" runat="server" Height="22px" OnTextChanged="txtUserName_TextChanged" Width="200px" />
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="" />
            </td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 173px">
                <asp:Label ID="lblPwd" runat="server" CssClass="lbl" Text="Password:" />
            </td>
            <td style="width: 428px">
                <asp:TextBox ID="txtPwd" runat="server" CssClass="pwd" Height="22px" OnTextChanged="txtPwd_TextChanged" TextMode="Password" Width="200px" />
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 173px">
                <asp:Button ID="btnLogIn" runat="server" onclick="btnLogIn_Click" Text="Sign In" />
            </td>
            <td style="width: 428px">
                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 173px">&nbsp;</td>
            <td style="width: 428px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 173px">&nbsp;</td>
            <td style="width: 428px">
                <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" visible="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>


</asp:Content>

