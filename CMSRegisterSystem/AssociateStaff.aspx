<%@ Page Title="Associate Lead Staff with Teaching Session" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssociateStaff.aspx.cs" Inherits="CMSRegisterSystem.AssociateStaff" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Associate Lead Staff with Teaching Session</h2>

    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label></p>

    <p style="font-size: small; color: #333333;">
        Use this form to associate a member of staff with a teaching session
    </p>
    <p style="font-size: small; font-weight: normal; color: #333333;">
        &nbsp;&nbsp;&nbsp;
    Course &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="255px">
    </asp:DropDownList>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
    Occurrence&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:DropDownList ID="DropDownList3" runat="server" Height="20px" Width="255px"
        Style="margin-right: 0px">
    </asp:DropDownList>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
    Staff&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList4" runat="server" Height="20px" Width="255px">
    </asp:DropDownList>
        &nbsp;<br />
        <br />
        &nbsp;&nbsp;&nbsp;
    Room&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList5" runat="server" Height="20px" Width="255px">
    </asp:DropDownList>
    </p>
    <p style="font-size: medium; font-weight: normal; color: #000000;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save"
            Width="123px" Height="23px" />
    </p>
    <p style="font-size: medium; font-weight: normal; color: #000000;">
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
</asp:Content>