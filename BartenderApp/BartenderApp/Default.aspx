<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BartenderApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bartender Application</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

    </style>
</head>
<body>
    <table width="100%" cellpadding="50">
        <tr valign="top">
            <td align="center">
                <form id="form1" runat="server">
                    <div runat="server" class="formBox">
                        <div class="title">Cici's Bartender App</div>
                        <div class="content">
                            <asp:Button ID="btnPlaceOrder" CssClass="btn" runat="server" Text="Order A Drink" OnClick="btnPlaceOrder_Click" ToolTip="Servers, click to place a drink order for your table." />&nbsp;
                            <asp:Button ID="btnViewOrders" CssClass="btn" runat="server" Text="View Orders" OnClick="btnViewOrders_Click" ToolTip="Bartenders, click to view a list of drink orders." />

                            <div style="min-height: 50px; vertical-align:middle; text-align: center; font-size: x-small;">
                                <br />
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" style="font-size: small"></asp:Label>
                                <br />
                                &nbsp;</div>
                            <div id="divPlaceOrder" runat="server"><center>
                                <table>
                                    <tr>
                                        <td>Select a drink:</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddDrinkSelection" runat="server" width="207"/></td>
                                    </tr>
                                    <tr>
                                        <td>Table Number:</td>
                                        <td>
                                            <asp:DropDownList ID="ddTableNum" runat="server">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td><asp:Button runat="server" ID="btnSubmit" CssClass="btn2" OnClick="btnSubmit_Click" Text="Place Order" /></td>
                                    </tr>
                                </table></center>
                            </div>
                            <div id="divOrderQ" runat="server" style="text-align: center;">
                                <asp:GridView ID="gvOrderQ" runat="server" CellPadding="4" width="500" HeaderStyle-BackColor="#5A0000"
                                    HeaderStyle-ForeColor="White" RowStyle-BackColor="#E8CCCC" AlternatingRowStyle-BackColor="White"
                                    RowStyle-ForeColor="#5A0000" AutoGenerateColumns="true" AllowPaging="true" PageSize="10" PagerSettings-Mode="NumericFirstLast"
                                    Font-Names="Calibri" Font-Size="Smaller"
                                    OnPageIndexChanging="OnPageIndexChanging">
                                </asp:GridView>
                                <br />
                                <asp:Button CssClass="btn2" ID="btnReinitDb" runat="server" OnClick="btnReinitDb_Click" Text="Re-initialize Db" ToolTip="This removes all orders from the database." />
                            </div>
                        </div>
                    </div>
                </form>

    </td></tr></table>
</body>
</html>
