<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMSRegisterSystem._Default" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Home</h2>
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3">
                <p>
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td style="width: 215px" valign="top">
                <h2>Log In:</h2>
                <table style="width: 100%">
                    <tr>
                        <td>Username
                        </td>
                        <td style="width: 105px;">
                            <asp:TextBox ID="txtUsername" runat="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Password
                        </td>
                        <td style="width: 105px;">
                            <asp:TextBox ID="txtPassword" runat="server" Width="100px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click" Text="Log In" Width="100px" />
                            &nbsp;<asp:Button ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" Text="Log Out"
                                Width="100px" />
                        </td>
                    </tr>
                </table>
                <p>To start, log in above.</p>
                <h3>Test Users</h3>
                <table style="width: 100%;" border="1" cellspacing="0">
                    <tr>
                        <td><b>Type</b></td>
                        <td><b>Username</b></td>
                        <td><b>Password</b></td>
                    </tr>
                    <tr>
                        <td>Student</td>
                        <td>tm112</td>
                        <td>test</td>
                    </tr>
                    <tr>
                        <td>Course Coodinator</td>
                        <td>jd41</td>
                        <td>5</td>
                    </tr>
                    <tr>
                        <td>Tutor</td>
                        <td>ef33&nbsp;</td>
                        <td>3</td>
                    </tr>
                    <tr>
                        <td>Manager</td>
                        <td>hs26</td>
                        <td>6</td>
                    </tr>
                    <tr>
                        <td>Admin Staff</td>
                        <td>ph22</td>
                        <td>2</td>
                    </tr>
                </table>
                <p>&nbsp;</p>
            </td>
            <td style="width: 10px;"></td>
            <td valign="top">
                <h2>Welcome!</h2>
                <p align="center"
                    style="font-family: Ravie; text-decoration: underline; font-size: x-large; color: #0000FF">
                    CMS Student News!
                </p>
                <p align="center"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #000066">
                    25/04/2013 - New CMS Register Launched!
                </p>
                <p align="center"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #000066">
                    20/04/2013 - SDP Coursework due this Sunday. Don&#39;t forget to Upload!
                </p>
                <p align="center"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #000066">
                    15/04/2013 - Welcome back to Term 3! We hope you had a great Easter Break.
                </p>
                <p align="center"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #000066">
                    1/04/2013 - You need to start planning for your SDP Presentation. Watch the 
                    Space for dates and times for these.
                </p>
                <p align="center"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #000066">
                    23/03/2013 - Have a great Easter Break!
                </p>
                <p align="center"
                    style="font-family: Arial, Helvetica, sans-serif; font-size: medium; color: #000066">
                    &nbsp;
                </p>
            </td>
        </tr>
    </table>
</asp:Content>