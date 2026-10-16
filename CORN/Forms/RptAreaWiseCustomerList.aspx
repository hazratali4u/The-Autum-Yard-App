<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptAreaWiseCustomerList.aspx.cs" Inherits="Forms_RptAreaWiseCustomerList"
    Title="CORN :: Customer List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

    </script>
    <div id="right_data">
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>

                                <tr>
                                    <td align="left"></td>
                                    <td align="left" width="70px">
                                        <strong>Location</strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="180px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left" width="70px">
                                        <strong>Customer</strong>
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddl_customer" runat="server" Width="180px" CssClass="DropList">
                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="InActive" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" OnClick="btnViewPDF_Click"
                        CssClass="Button" Width="80" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" OnClick="btnViewExcel_Click"
                        CssClass="Button" Width="80" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>

</asp:Content>
