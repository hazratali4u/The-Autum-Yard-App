<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptPriceList.aspx.cs" Inherits="Forms_rptPriceList" Title="CORN :: SKU Price List" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table style="width: 273px; height: 68px" id="TABLE1">
                                    <tbody>

                                        <tr>
                                            <td style="width: 1px" align="left"></td>
                                            <td style="width: 70px" align="left">
                                                <strong>Location</strong>
                                            </td>
                                            <td style="width: 1px" align="left"></td>
                                            <td style="width: 203px; height: 25px" align="left">
                                                <asp:DropDownList ID="DrpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td style="width: 1px; height: 25px" align="left"></td>
                                            <td style="width: 29px; height: 25px" align="left">
                                                <strong>Catagory</strong>
                                            </td>
                                            <td style="width: 1px; height: 25px" align="left"></td>
                                            <td style="width: 203px; height: 25px" align="left">
                                                <asp:DropDownList ID="DrpCatagory" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" OnClick="btnViewPDF_Click" Width="90"/>
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="Button"
                        OnClick="btnViewExcel_Click" Width="90"/>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
