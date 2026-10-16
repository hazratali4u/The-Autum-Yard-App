<%@ Page Title="CORN :: Add Payment Type" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmPayment_Type.aspx.cs" Inherits="Forms_frmPayment_Type" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        <%-- function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCodeNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Code No');
                return false;
            }
            str = document.getElementById('<%=txtDescription.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Description');
                return false;
            }
            str = document.getElementById('<%=txtRemarks.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Remarks');
                return false;
            }
            return true;
        }--%> 
    </script>
    <div id="right_data">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table class="tbl-product-section">
                    <tr>
                        <td style="width: 20px;"></td>
                        <td>
                            <strong>Code #:</strong>
                        </td>
                        <td>
                            <asp:Label ID="txtCodeNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>Description:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="195px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="txtDescription"
                                ValidationGroup="rfv" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>Remarks:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td colspan="2"></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                                ValidationGroup="rfv" Text="Save"></asp:Button>
                            <asp:Button ID="btnExit" runat="server" CssClass="Button" OnClick="btnExit_Click"
                                Text="Exit"></asp:Button>
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">

                    <asp:Repeater ID="rTableDefination" runat="server" OnItemCommand="rTableDefination_ItemCommand">
                        <HeaderTemplate>
                            <table style="width: 100%" class="table-border">
                                <tr>
                                    <td class="grdHead" style="width: 10%">Code #
                                    </td>
                                    <td class="grdHead" style="width: 30%">Description
                                    </td>
                                    <td class="grdHead" style="width: 40%">Remarks
                                    </td>
                                    <td class="grdHead" style="width: 10%"></td>
                                    <td class="grdHead" style="width: 10%"></td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="grdDetail">
                                    <%# Eval("PT_ID")%>
                                </td>
                                <td class="grdDetail">
                                    <%# Eval("PT_DESCRIPTION")%>
                                </td>
                                <td class="grdDetail">
                                    <%# Eval("PT_REMARKS")%>
                            </t>
                            <td align="center" class="grdDetail">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("PT_ID")%>'><image src="../images/edit.gif" ></asp:LinkButton>
                            </td>
                                    <td align="center" class="grdDetail">
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("PT_ID")%>'><image src="../images/delete.gif"></asp:LinkButton>
                                    </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <div class="clr">
                </div>
                <table width="100%">
                    <tr>
                        <td align="right" width="56%"></td>
                        <td align="right" width="44%">

                            <asp:Label ID="lblDummy" runat="server" Text="Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                            <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hf_tbldefID" runat="server" />
                <asp:HiddenField ID="hf_ptISACTIVE" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
