<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseEntry.aspx.cs" Inherits="Forms_frmPurchaseEntry" Title="CORN :: Stock Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddlSkus.ClientID %>").searchable();
        }

        function CalcualeAmount() {
            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var Rate = document.getElementById('<%=txtPrice.ClientID%>').value;

            document.getElementById("<%= txtAmount.ClientID %>").value = (Qty * Rate).toFixed(2);
        }

        function ValidateForm() {
            var str;
          
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            var lblInvoice = document.getElementById('<%=lblInvoice.ClientID%>').innerHTML;
            if (str == null || str.length == 0) {
            if(lblInvoice=='Driver Name')
            {alert('Must Enter Driver Name');
            }else
            {
                document.getElementById('<%=txtDocumentNo.ClientID%>').focus();
                alert('Must Enter Invoice/DC No');
            }
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
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 24px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblbox" Height="14px" Text="Transaction Type"
                                                        Width="102px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 24px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="2">Purchase</asp:ListItem>
                                                    <asp:ListItem Value="5">Transfer Out</asp:ListItem>
                                                    <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                                    <%-- <asp:ListItem Value="4">Transfer In</asp:ListItem>--%>
                                                    <asp:ListItem Value="6">Damage</asp:ListItem>
                                                    <%-- <asp:ListItem Value="10">Import Damage</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 24px">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="5px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 316px;" align="center" colspan="1" rowspan="8" valign="middle">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Supplier" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" CssClass="DropList" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Purchase For"
                                                        Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Transfer To" Visible="False"
                                                        Width="82px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpTransferFor" runat="server" CssClass="DropList" Visible="False"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lblInvoice" runat="server" CssClass="lblbox" Text="INV/DC  No" Width="94px" MaxLength="100"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Remarks" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="300px" MaxLength="250"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px" valign="top">
                                            </td>
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
                                            <td style="width: 284px; background-color: #006699; padding: 2px;">
                                                <label class="lblDetail">
                                                    Item Description
                                                </label>
                                            </td>
                                            <td style="width: 70px; background-color: #006699; padding: 2px;">
                                                <label class="lblDetail">
                                                    Quantity
                                                </label>
                                            </td>
                                            <td style="width: 70px; background-color: #006699; padding: 2px;">
                                                <label class="lblDetail">
                                                    Price
                                                </label>
                                            </td>
                                            <td style="width: 70px; background-color: #006699; padding: 2px;">
                                                <label class="lblDetail">
                                                    Amount
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSkus" runat="server" Width="284px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px" CssClass="txtBox " onblur="CalcualeAmount();"
                                                    onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPrice" onblur="CalcualeAmount();" runat="server" Width="70px"
                                                    onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" Enabled="false" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px"
                                                    Font-Size="8pt" Text="Add" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" Width="600px" Height="150px" ScrollBars="Vertical"
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
                                                            <asp:BoundField ReadOnly="true" DataField="SKU_CODE" >
                                                               <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true" DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="290px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true" DataField="Quantity" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true" DataField="Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField ReadOnly="true"  DataField="Amount" DataFormatString="{0:f2}">
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
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                            <td style="position: absolute">
                                                <table>
                                                    <tr>
                                                        <td>
                                                         <strong style="font-size:13px;">
                                                   Total Quantity&nbsp;&nbsp;</strong>
                                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="100px" CssClass="txtBox"
                                                    Enabled="false" style="padding:1px; font-size:13px;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <strong style="font-size:13px;">
                                                   Total Amount&nbsp;&nbsp;&nbsp;</strong>
                                                <asp:TextBox ID="txtTotalAmount" runat="server" Width="100px" CssClass="txtBox"
                                                    Enabled="false" style="padding:1px; font-size:13px;"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                            &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="110px" Font-Size="8pt"
                            Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                            CssClass="Button" />
                        <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="110px" Font-Size="8pt"
                            Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
