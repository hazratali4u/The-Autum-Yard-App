<%@ Page Title="CORN :: Payroll Report" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptProcessPayroll.aspx.cs" Inherits="Forms_rptProcessPayroll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="javascript" type="text/javascript">
        function onCalendarShown5() {
            var cal = $find("calendar5");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call5);
                    }
                }
            }
        }

        function onCalendarHidden5() {
            var cal = $find("calendar5");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call5);
                    }
                }
            }
        }

        function call5(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar5");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 102px;">
                                        <strong>Location</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Department</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddDesignation" runat="server" Width="205px" AutoPostBack="True"
                                            CssClass="DropList" OnSelectedIndexChanged="ddDesignation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Employee Name</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrpEmployee" runat="server" Width="205px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Month</strong>
                                    </td>
                                    <td>
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
                                        <asp:TextBox ID="txtSalaryMonth" runat="server" Width="100px"></asp:TextBox>
                                        <asp:ImageButton ID="ibExpDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                        <ajaxToolkit:CalendarExtender ID="ceExpDate" BehaviorID="calendar5" runat="server"
                                            Format="MMM-yyyy" PopupButtonID="ibExpDate" TargetControlID="txtSalaryMonth"
                                            OnClientShown="onCalendarShown5" OnClientHidden="onCalendarHidden5">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table width="100%">
                        <tr>
                            <td style="width: 914px">
                                <asp:Label ID="ltrAdd" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                               &nbsp; &nbsp;<asp:Button AccessKey="S" ID="btnShow" autopostback="true"  runat="server"
                                    Width="90px" Font-Size="8pt" Text="Print PDF" CssClass="Button" OnClick="btnShow_Click1" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
