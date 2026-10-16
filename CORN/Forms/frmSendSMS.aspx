<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSendSMS.aspx.cs" Inherits="Forms_frmSendSMS" Title="CORN :: Send SMS" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">

    <script language="JavaScript" type="text/javascript">

       
       Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }


        function SelectAll() {
            var chkBoxList = document.getElementById('<%= LstCustomer.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllCustomer.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }

        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAllCustomer.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstCustomer.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width:2%">

                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>
                            <asp:CheckBox ID="ChbAllCustomer" onclick="SelectAll()" runat="server" Text="All Customers" Font-Bold="true"></asp:CheckBox>
                            <asp:Panel ID="Panel1" runat="server" Width="400px" Height="350px" ScrollBars="Vertical"
                                BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                <asp:CheckBoxList ID="LstCustomer" onclick="UnCheckSelectAll()" runat="server">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td style="width:2%">

                        </td>
                        <td style="vertical-align: top; padding-top: 20px !important;">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblError" runat="server" Width="312px" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Phone #</strong>  </td>
                                    <td>
                                        <asp:TextBox ID="txtPhoneNo" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><strong>SMS Message </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSMS" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="22" Columns="40" Width="100%" style="resize:none;"></asp:TextBox>
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                    <tr>
                                    <td colspan="4" align="right">
                                        <asp:Button ID="btnSendSMS" OnClick="btnSendSMS_Click" runat="server" Text="Send SMS" CssClass="Button" />
                                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="Button" />
                                    </td>
                                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfNumber" runat="server" ></asp:HiddenField>

    </div>
</asp:Content>
