<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="CMSRegisterSystem.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Template</h2>
    
        <table style="width:100%">
            <tr>
                <td>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Use this page as a template for new ones as it will keep the user logged in.</td>
            </tr>
        </table>
</asp:Content>