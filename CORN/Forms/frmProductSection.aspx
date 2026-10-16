<%@ Page Title="CORN :: Item Section" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmProductSection.aspx.cs" Inherits="Forms_frmProductSection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/style2.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCodeNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                // alert('Must enter Code No');
                //  return true;
            }
            str = document.getElementById('<%=txtSectionName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Section Name');
                return false;
            }
            str = document.getElementById('<%=txtPrinterName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Printer Name');
                return false;
            }
            return true;
        }
    </script>
    <div id="right_data">

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>


                <table class="tbl-product-section">
                    <tr style="display: none">
                        <td style="width: 20px;"></td>
                        <td>
                            <strong>
                                <label>Section Code : </label>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodeNo" runat="server" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>
                                <label>Section Name : </label>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSectionName" runat="server" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>
                                <label>Printer Name : </label>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrinterName" runat="server" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="4">
                            <asp:CheckBox ID="chkIsPrint" runat="server" Width="93px" Text=" Is Print" Checked="true" Font-Bold="true"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td colspan="2"></td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server"
                                OnClick="btnSave_Click" CssClass="Button">Save</asp:LinkButton>
                            <asp:LinkButton ID="btnExit" runat="server"
                                OnClick="btnExit_Click" CssClass="Button">Exit</asp:LinkButton>
                        </td>
                    </tr>

                </table>

                <div>
                    <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">


                        <asp:Repeater ID="rServiceDefination" runat="server"
                            OnItemCommand="rServiceDefination_ItemCommand">
                            <HeaderTemplate>
                                <table style="width: 100%;" class="table-border">
                                    <tr>

                                        <td class="grdHead" style="width: 30%">
                                            <strong>Section Name</strong>
                                        </td>
                                        <td class="grdHead" style="width: 50%">
                                            <strong>Printer Name</strong>
                                        </td>
                                        <td class="grdHead" style="width: 10%"></td>
                                        <td class="grdHead" style="width: 10%"></td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="GridAlternateRowStyle">

                                    <td class="grdDetail">
                                        <%#Eval("SECTION_NAME")%>
                                    </td>
                                    <td class="grdDetail">
                                        <%#Eval("PRINTER_NAME")%>
                                    </td>
                                    <td align="center" class="grdDetail">
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="edit" CommandArgument='<%# Eval("SECTION_ID")%>'><image src="../images/edit.gif" ></asp:LinkButton>
                                    </td>

                                    <td align="center" class="grdDetail">
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="delete" CommandArgument='<%# Eval("SECTION_ID")%>'><image src="../images/delete.gif"></asp:LinkButton>
                                    </td>
                                </tr>

                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <div class="clr"></div>
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" width="56%"></td>
                            <td align="right" width="44%">

                                <asp:Label ID="lblDummy" runat="server" Text="Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <asp:HiddenField ID="hf_tbldefID" runat="server" />

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>

