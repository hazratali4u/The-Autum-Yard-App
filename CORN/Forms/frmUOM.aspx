<%@ Page Title="CORN :: Add UOM" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmUOM.aspx.cs" Inherits="Forms_frmUOM2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCodeNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Table No');
                return false;
            }
            str = document.getElementById('<%=txtDescription.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Description');
                return false;
            }
            return true;
        }
    </script>
    <asp:UpdatePanel ID="upUOM" runat="server">
        <ContentTemplate>
            <div id="right_data">
                <table class="tbl-product-section">
                    <tr style="display: none;">
                        <td style="width: 20px">
                        </td>
                        <td>
                            <strong>Code #:</strong>
                        </td>
                        <td>
                            <asp:Label ID="txtCodeNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr> 
                        <td>
                        </td>
                        <td>
                            <strong>Unit of Measure:<strong>
                        </td>
                        <td>
                           <asp:RadioButtonList ID="ddlType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                Width="200px">
                                <asp:ListItem Text="Serving" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inventory" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <strong>Description:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <strong>Remarks:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="2" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td colspan="2">
                        </td>
                        <td>
                            <asp:HiddenField ID="hf_RefID" runat="server" Visible="false" />
                            <asp:LinkButton ID="btnSave" runat="server" class="btn-1" OnClick="btnSave_Click"
                                CssClass="Button">Save</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Button">Exit</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                 <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">

                <asp:Repeater ID="rDepartment" runat="server" OnItemCommand="rDepartment_ItemCommand">
                    <HeaderTemplate>
                        <table style="width: 100%;" class="table-border">
                            <tr>
                                <td class="HidePanel">
                                    <strong><span>Code</span></strong> 
                                </td>
                                <td class="tblhead">
                                  <strong>   <span>Description</span></strong> 
                                </td>
                                <td class="tblhead">
                                   <strong>  <span>Remarks</span></strong> 
                                </td>
                                <td  class="tblhead">
                                    
                                </td>
                                 <td  class="tblhead">
                                    
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="HidePanel">
                                <%# Eval("UOM_CODE")%>
                            </td>
                            <td class="grdDetail">
                                <%# Eval("UOM_DESC")%>
                            </td>
                            <td class="grdDetail">
                                <%# Eval("UOM_REMARKS")%>
                            </td>
                            <td align="center" class="grdDetail">
                                <span class="edit2">
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" ToolTip="Edit" CommandArgument='<%# Eval("UOM_ID")%>'><image src="../images/edit.gif" ></asp:LinkButton></span>
                            </td>
                            <td align="center" class="grdDetail">
                                <span class="delete">
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete" ToolTip="Delete" CommandArgument='<%# Eval("UOM_ID")%>'><image src="../images/delete.gif"></asp:LinkButton></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                     
                        </asp:Panel>
                <div class="clr">
                </div>
                <table width="100%">
                    <tr>
                        <td align="right" width="56%">
                           
                        </td>
                        <td align="right" width="44%">
                         
                            <asp:Label ID="lblDummy" runat="server" Text="Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                            <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
