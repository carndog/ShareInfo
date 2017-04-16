<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Default.aspx.cs" Inherits="SharePriceList.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmMain" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="txtSymbol"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_OnClick" />

                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Repeater runat="server" ID="rptHoldings">

                            <HeaderTemplate>
                                <table cellspacing="0" rules="all" border="1">
                                <tr>
                                    <th scope="col" style="width: 80px">
                                        Symbol
                                    </th>
                                    <th scope="col" style="width: 120px">
                                        Name
                                    </th>
                                    <th scope="col" style="width: 100px">
                                        Price
                                    </th>
                                    <th scope="col" style="width: 100px">
                                        Change
                                    </th>
                                    <th scope="col" style="width: 100px">
                                        Change %
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Symbol") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Price") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Change") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "ChangePercentage") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>

                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
