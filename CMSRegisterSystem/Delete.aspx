<%@ Page Title="Delete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="CMSRegisterSystem.Delete" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Delete</h2>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click"
                    Text="Delete a Student" Width="280px" />
                <br />
                <br />
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click"
                    Text="Delete a Member of Staff" Width="280px" />
                <br />
                <br />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click"
                    Text="Delete a Course" Width="280px" />
                <br />
                <br />
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click"
                    Text="Delete a Student from a Course" Width="280px" />
                <br />
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
    </p>
</asp:Content>