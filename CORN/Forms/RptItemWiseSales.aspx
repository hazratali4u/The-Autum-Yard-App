<%@ Page Title="CORN :: Items Sale Report" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="RptItemWiseSales.aspx.cs" Inherits="Forms_RptItemWiseSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Report Type" Width="78px"
                                        Visible="False"></asp:Label>
                                    <asp:DropDownList ID="DrpReportType" runat="server" Width="200px" CssClass="DropList"
                                        Visible="False">
                                        <asp:ListItem>Sales</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Value Type" Width="78px"
                                        Visible="False"></asp:Label>
                                    <asp:DropDownList ID="DrpUnitType" runat="server" Width="200px" CssClass="DropList"
                                        Visible="False">
                                        <asp:ListItem>Pieces</asp:ListItem>
                                        <asp:ListItem>Value</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Location Type" Width="101px"
                                        Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddDistributorType" runat="server" Width="200px" CssClass="DropList"
                                        OnSelectedIndexChanged="ddDistributorType_SelectedIndexChanged" AutoPostBack="True"
                                        Visible="False">
                                    </asp:DropDownList>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="width:70px;">
                                            <strong>
                                                Location</strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 35px">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                From Date</strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 35px">
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                                onkeyup="BlockStartDateKeyPress()" Width="175px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                               To Date</strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 35px">
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                                onkeyup="BlockEndDateKeyPress()" Width="175px"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        &nbsp;
                                        </td>
                                    </tr>
                                    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                        TargetControlID="txtStartDate">
                                    </cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                        TargetControlID="txtEndDate">
                                    </cc1:CalendarExtender>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="85" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Width="85" Text="View Excel"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
