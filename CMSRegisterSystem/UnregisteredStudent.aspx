<%@ Page Title="Adding an Unregistered Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnregisteredStudent.aspx.cs" Inherits="CMSRegisterSystem.UnregisteredStudent" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Adding an Unregistered Student</h2>

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
            <td>Please Select a Course:
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="CourseAccessDataSource" DataTextField="CourseCode" DataValueField="CourseCode">
                        <asp:ListItem>COMP1632</asp:ListItem>
                    </asp:DropDownList>
                <asp:AccessDataSource ID="CourseAccessDataSource" runat="server" DataFile="~/App_Data/MainDatabase.accdb" SelectCommand="SELECT [CourseCode] FROM [Course]"></asp:AccessDataSource>
                <br />
                <br />
                First Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <br />
                Surname:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <br />
                <br />
                Student ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <br />
                <br />
                Year of Study:&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                <br />
                <br />
                Programme ID:
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="ProgrammeAccessDataSource" DataTextField="ProgrammeName" DataValueField="ProgrammeName">
                        <asp:ListItem Value="1">Computing</asp:ListItem>
                        <asp:ListItem Value="2">Computer Science</asp:ListItem>
                    </asp:DropDownList>
                <asp:AccessDataSource ID="ProgrammeAccessDataSource" runat="server" DataFile="~/App_Data/MainDatabase.accdb" SelectCommand="SELECT [ProgrammeName] FROM [Programme]"></asp:AccessDataSource>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                    Text="Add Student" />
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>