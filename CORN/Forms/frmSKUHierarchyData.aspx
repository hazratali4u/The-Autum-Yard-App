<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUHierarchyData.aspx.cs" Inherits="frmSKUHierarchyData" Title="CORN :: Add Supplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <cc1:TabContainer ID="TabContainer1" runat="server" Height="400px" Width="650px"
                        ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabPanel1" runat="server">
                            <HeaderTemplate>
                                Supplier
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 16%"></td>
                                                                <td colspan="3" style="width: 86%;">
                                                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 29px">
                                                                    <strong>Supplier Name</strong>
                                                                </td>
                                                                <td colspan="3" style="width: 86%; height: 29px">
                                                                    <asp:TextBox ID="txtPrincipalName" runat="server" Width="100%" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 29px">
                                                                    <strong>Address</strong>
                                                                </td>
                                                                <td colspan="3" style="width: 86%; height: 29px">
                                                                    <asp:TextBox ID="txtAddress" runat="server" Width="100%" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 29px">
                                                                    <strong>Contact Person</strong>
                                                                </td>
                                                                <td style="width: 38%; height: 29px">
                                                                    <asp:TextBox ID="txtContactPerson" runat="server" Width="84%" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td align="left" style="width: 15%; height: 29px;">
                                                                    <strong>Email Address</strong>
                                                                </td>
                                                                <td align="left" style="width: 36%; height: 29px;">
                                                                    <asp:TextBox ID="txtEmail" runat="server" Width="100%" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 29px">
                                                                    <strong>Phone </strong>
                                                                </td>
                                                                <td style="width: 38%; height: 29px">
                                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="84%" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td align="left" style="width: 12%; height: 29px;">
                                                                    <strong>Fax Number</strong>
                                                                </td>
                                                                <td align="left" style="width: 38%; height: 29px;">
                                                                    <asp:TextBox ID="txtFaxNumber" runat="server" Width="100%" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Button ID="btnSavePrincipal" OnClick="btnSavePrincipal_Click" runat="server"
                                                                        Width="35%" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="ChIsMunalDiscount" runat="server" Width="190px" Visible="false"
                                                                        Text="Is Manual Discount" Checked="true"></asp:CheckBox>

                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 10px; height: 90px">
                                                &nbsp;<asp:Panel ID="Panel21" runat="server">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="31px" />
                                                            Wait Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Height="160px" ScrollBars="Vertical" BorderColor="Silver"
                                                Width="100%" BorderWidth="1px">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdPrincipal" runat="server" Width="100%" ForeColor="SteelBlue"
                                                            BorderColor="SteelBlue" BackColor="White" AutoGenerateColumns="False" OnRowEditing="GrdPrincipal_RowEditing"
                                                            OnRowCommand="GrdPrincipal_OnRowCommand">
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle CssClass="grdDetail"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" Width="50%" CssClass="grdHead"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IS_MANUALDISCOUNT">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ADDRESS">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                                                    <ItemStyle CssClass="grdDetail" Width="34%"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EMAIL">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PHONE">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FAX">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("SKU_HIE_ID" )%>' ToolTip="Edit">
                                                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="8%" />
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemStyle CssClass="grdDetail" Width="8%"
                                                                        HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName='<%# Eval("IS_ACTIVE").ToString().Equals("True") ? "Active" : "Inactive" %>'
                                                                            CommandArgument='<%# Eval("SKU_HIE_ID" )%>' Text='<%# Eval("IS_ACTIVE").ToString().Equals("True") ? "Active" : "Inactive " %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                                            <AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server" Visible="False">
                            <HeaderTemplate>
                                Category
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 100px; height: 16px"></td>
                                                                <td style="height: 16px" align="left" colspan="2">
                                                                    <br />
                                                                    <asp:Label ID="lblErrorMsgCategory" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; height: 16px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 8px"></td>
                                                                <td style="width: 69px; height: 8px"></td>
                                                                <td style="width: 100px; height: 8px"></td>
                                                                <td style="width: 100px; height: 8px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 28px"></td>
                                                                <td style="width: 69px; height: 28px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label121" runat="server" Width="90px" Text="Category Code"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 100px; height: 28px">
                                                                    <asp:TextBox ID="txtCategoryCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 100px; height: 28px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 29px"></td>
                                                                <td style="width: 69px; height: 29px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label211" runat="server" Width="90px" Text="Category Name" __designer:wfdid="w121"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 100px; height: 29px">
                                                                    <asp:TextBox ID="txtCategoryName" runat="server" Width="194px" __designer:wfdid="w122"
                                                                        Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 100px; height: 29px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 37px"></td>
                                                                <td style="width: 69px; height: 37px" align="right">&nbsp;
                                                                </td>
                                                                <td style="width: 100px; height: 37px" align="left">
                                                                    <asp:Button ID="btnSaveCategory" OnClick="btnSaveCategory_Click" runat="server" Width="85px"
                                                                        Font-Size="8pt" Text="New" CssClass="Button" />
                                                                </td>
                                                                <td style="width: 100px; height: 37px"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="z-index: 101; left: 80px; width: 100px; position: absolute; top: 150px; height: 90px">
                                                &nbsp;<asp:Panel ID="Panel2" runat="server">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel21">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="31px" />
                                                            Wait Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Height="245px" ScrollBars="Vertical" BorderColor="Silver"
                                                Width="100%" BorderWidth="1px">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdCategory" runat="server" Width="100%" ForeColor="SteelBlue"
                                                            Font-Size="9pt" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                            AutoGenerateColumns="False" OnRowEditing="GrdCategory_RowEditing" OnRowDeleting="GrdCategory_RowDeleting"
                                                            OnPageIndexChanging="GrdCategory_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" CssClass="grdDetail"
                                                                        Width="60%"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
