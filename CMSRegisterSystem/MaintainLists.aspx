<%@ Page Title="Assign Student to Teaching Session" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintainLists.aspx.cs" Inherits="CMSRegisterSystem.MaintainLists" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Maintain Lists of Students - Assign Student to Teaching Session</h2>

    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>

    <p style="font-size: small; color: #333333; height: 18px; width: 421px;">
        Use this form to assign a student to a teaching session&nbsp;
    </p>
    <p style="font-size: medium; color: #333333; font-weight: normal; height: 5px; width: 423px;">
        Assign student to a session
    </p>
    <p style="font-size: medium; color: #333333; font-weight: normal; height: 5px; width: 423px;">
        &nbsp;
    </p>
    <p style="font-size: small; color: #333333; font-weight: normal; height: 144px; width: 449px;">
        &nbsp;&nbsp;&nbsp;&nbsp;Student ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="255px"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp; Course&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
   
        <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="255px"
            Style="margin-left: 1px">
        </asp:DropDownList>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
    Occurance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList4" runat="server" Height="20px" Width="255px">
    </asp:DropDownList>
    </p>
    <p style="font-size: small; color: #333333; font-weight: normal; height: 29px; width: 449px;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Assign"
        Height="23px" Width="123px" />
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
</asp:Content>