<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDayCloseStatus.aspx.cs" Inherits="Forms_frmDayCloseStatus" Title="CORN :: Data Update Status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
      <script language="JavaScript" type="text/javascript">
        javascript: window.history.forward(1);                   
    </script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            if (document.getElementById('<%=btnDayClose.ClientID%>') != null) {
                document.getElementById('<%=btnDayClose.ClientID%>').disabled = true;
            }
        }

        function endRequest(sender, e) {
            if (document.getElementById('<%=btnDayClose.ClientID%>') != null) {
                document.getElementById('<%=btnDayClose.ClientID%>').disabled = false;
            }
        }

        function pageLoad() {
            $.tablesorter.addParser({
                id: "datetime",
                is: function (s) {
                    return false;
                },
                format: function (s, table) {
                    s = s.replace(/\-/g, "/");
                    s = s.replace(/(\d{1,2})[\/\-](\d{1,2})[\/\-](\d{4})/, "$3/$2/$1");
                    return $.tablesorter.formatFloat(new Date(s).getTime());
                },
                type: "numeric"
            });


            $('#<%=Grid_Hierarchy.ClientID %>').tablesorter(
        {
            dateFormat: 'dd-mm-yyyy',
            headers:
                {
                    3: { sorter: 'datetime' },
                    2: { sorter: false }
                }
        });
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <div id="right_data">
                <div>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                    Width="31px" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
    </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" colspan="3">
                                <asp:RadioButtonList ID="rblDistributorTypes" runat="server" Visible="False" Width="450px"
                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDistributorTypes_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="2">In Active</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:GridView ID="Grid_Hierarchy" runat="server" Width="100%" ForeColor="SteelBlue"
                                    HorizontalAlign="Center" CssClass="tablesorter" BorderColor="White" BackColor="White"
                                    AutoGenerateColumns="False">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous" />
                                    <Columns>
                                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location Name">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLOSING_DATE" HeaderText="Last Day Close">
                                            <ItemStyle ForeColor="Red" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIME_STAMP" HeaderText="Date/Time of Closing" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="tblhead" />
                                </asp:GridView>                             
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <asp:Button ID="btnDayClose" runat="server" Visible="False" Text="Day Close" OnClick="btnDayClose_Click"
                                    CssClass="Button" Width="90" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                
                                <asp:Button ID="btnSync" runat="server" Visible="False" Text="Sync" OnClick="btnSync_Click"
                                    CssClass="Button" Width="90" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>        
    </div>
</asp:Content>
