<%@ Page Title="CORN :: Stock Issuance" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmIssuance.aspx.cs" Inherits="Forms_frmIssuance" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>

    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddlSkus.ClientID %>").searchable();
        }

        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            return true;
        }
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;
            return true;
        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField runat="server" ID="hfId"/>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px">
                                                <strong>Transaction Type<strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="13">Issuance</asp:ListItem>
                                                    <asp:ListItem Value="14">Return</asp:ListItem>

                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Document No</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Location"
                                                        Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Section</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpSection" runat="server" CssClass="DropList" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <strong>Remarks</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="350px" MaxLength="250"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 300px; background-color: #006699; padding: 2px;">
                                                <label class="lblDetail">
                                                    Item Description
                                                </label>
                                            </td>
                                            <td style="width: 70px; background-color: #006699; padding: 2px;">
                                                <label class="lblDetail">
                                                    Quantity
                                                </label>
                                            </td>

                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSkus" runat="server" Width="300px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px"
                                                    onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </td>

                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px"
                                                    Font-Size="8pt" Text="Add" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Width="485px" Height="160px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="100%">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField ReadOnly="true" DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true" DataField="SKU_CODE">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true" DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="300px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true" DataField="FREE_SKU" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="grdButton"
                                                                        ForeColor="Black"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="40px"
                                                                    HorizontalAlign="Center" CssClass="grdButton" ForeColor="Black" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete" CssClass="grdButton" ForeColor="Black"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px"
                                                                    HorizontalAlign="Center" CssClass="grdButton" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <table>
                            <tr>
                                <td>
                                    <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="110px" Font-Size="8pt"
                                        Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                        CssClass="Button" />
                                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="110px" Font-Size="8pt"
                                        Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                </td>
                                <td>
                                    <fieldset>
                                        <table>
                                            <tr>
                                                <td>
                                                    <strong style="font-size: 13px;">Total Quantity&nbsp;&nbsp;</strong>
                                                    <asp:TextBox ID="txtTotalQuantity" runat="server" Width="100px" CssClass="txtBox"
                                                        Enabled="false" Style="padding: 1px; font-size: 13px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

