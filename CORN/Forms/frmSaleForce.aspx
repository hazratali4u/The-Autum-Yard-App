<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleForce.aspx.cs" Inherits="Forms_frmSaleForce" Title="CORN :: Employee Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtUserName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter User Name');
                return false;
            }
            str = document.getElementById('<%=txtNICNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter NIC No');
                return false;
            }
            str = document.getElementById('<%=txtLoginId.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Login Id');
                return false;
            }
            str = document.getElementById('<%=txtpassword.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Password');
                return false;
            }
            str = document.getElementById('<%=txtMobileNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Mobile No');
                return false;
            }
            str = document.getElementById('<%=txtAddress2.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Address');
                return false;
            }
            return true;
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 143px" align="left"></td>
                                            <td style="width: 175px">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td align="left"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="lbldesignationID" runat="server" Width="94px" Text="Base Location"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 15px">
                                                <asp:DropDownList ID="ddDistributorId" runat="server" Width="205px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 15px">&nbsp; &nbsp;
                                            </td>
                                            <td style="height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="96px" Text="Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 15px">
                                                <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 12px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="79px" Text="Designation" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 12px">
                                                <asp:DropDownList ID="ddDesignation" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 12px"></td>
                                            <td style="height: 12px" align="left">
                                                <strong>N.I.C No</strong>
                                            </td>
                                            <td style="height: 12px">
                                                <asp:TextBox ID="txtNICNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="96px" Text="Department" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpDepartment" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>
                                                <strong>Joining Date</strong>
                                            </td>
                                            <td style="width: 175px; height: 22px">
                                                <asp:TextBox ID="txtJoinDate" runat="server" Width="150px" CssClass="txtBox "></asp:TextBox>
                                                <asp:ImageButton ID="ibtnJoinDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnJoinDate"
                                                    TargetControlID="txtJoinDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 22px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblMobileNo" runat="server" Width="96px" Text="Mobile No:" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 22px">
                                                <asp:TextBox ID="txtMobileNo" runat="server" Width="202px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 22px"></td>
                                            <td style="height: 22px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblPhNo" runat="server" Width="90px" Text="Phone No:" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 22px">
                                                <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" valign="top" align="left">
                                                <strong>Email Address</strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="516px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" valign="top" align="left">
                                                <strong>Permanent Address</strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtAddress2" runat="server" Width="516px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" valign="top" align="left">
                                                <strong>Present Address</strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtAddress1" runat="server" Width="516px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" valign="top" align="left"></td>
                                            <td style="width: 175px">
                                                <asp:TextBox ID="txtLoginId" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 1px"></td>
                                            <td valign="top" align="left"></td>
                                            <td>
                                                <asp:TextBox ID="txtpassword" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 21px" align="left"></td>
                                            <td style="width: 175px; height: 21px">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="82px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                <asp:Button ID="btnCancel" runat="server" Width="73px" Font-Size="8pt" Text="Cancel"
                                                    OnClick="btnCancel_Click" CssClass="Button" />
                                            </td>
                                            <td style="width: 1px; height: 21px"></td>
                                            <td style="height: 21px"></td>
                                            <td style="height: 21px; display: none;">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Height="230px" ScrollBars="Vertical" Width="100%"
                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                            BorderColor="SteelBlue" HorizontalAlign="Center" AutoGenerateColumns="False"
                            BackColor="White" OnRowEditing="Grid_users_RowEditing" OnRowDeleting="GrdPurchase_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="User Id" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_CODE" HeaderText="Code" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="Name" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NIC_NO" HeaderText="NIC No" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PHONE" HeaderText="Phone No" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MOBILE" HeaderText="Mobile" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL" HeaderText="Email" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle  CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS1" HeaderText="Present Address" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS2" HeaderText="Permanent Address" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle  CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SLASH_DESC" HeaderText="Designation" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_TYPE_ID" HeaderText="USERTYPE_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE_JOIN" HeaderText="DATE_JOIN" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Dept_ID" HeaderText="Dept_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle CssClass="grdDetail" Width="40px" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            ToolTip="Delete">
                                            <img id="imgDel" alt="" src="~/images/delete.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="grdHead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
