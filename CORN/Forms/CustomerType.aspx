<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerType.aspx.cs" Inherits="Forms_CustomerType" Title="Customer Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <style type="text/css">
        .cmp input {
            width: 90%;
        }

        .cmp select {
            width: 90%;
        }

        .list input {
            width: 20%;
        }

        .list {
            width: 90%;
        }

        .tblheading {
            background: #006699;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

            .tblheading td {
                color: #ffffff;
                padding: 5px 5px 5px 5px;
            }
    </style>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td style="width: 100px">
                    <cc1:TabContainer ID="TabContainer1" runat="server" Height="375px" Width="650px"
                        ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabPanel1" runat="server">
                            <HeaderTemplate>
                                Designation
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>

                                                    <fieldset>
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 100px"></td>
                                                                    <td style="width: 49px"></td>
                                                                    <td style="width: 100px"></td>
                                                                    <td style="width: 100px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px"></td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True" AccessKey="C"></asp:Label><br />
                                                                    </td>
                                                                    <td style="width: 100px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 28px"></td>
                                                                    <td style="width: 49px; height: 28px">
                                                                        <strong>
                                                                            <asp:Label ID="Label1" runat="server" Width="62px" Text="Code"></asp:Label></strong>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtChannelCode" runat="server" Width="100px" CssClass="txtBox "
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 100px; height: 28px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 29px"></td>
                                                                    <td style="width: 49px; height: 29px">
                                                                        <strong>
                                                                            <asp:Label ID="Label2" runat="server" Width="65px" Text="Name"></asp:Label></strong>
                                                                    </td>
                                                                    <td style="width: 100px; height: 29px">
                                                                        <asp:TextBox ID="txtChannelName" runat="server" Width="200px" CssClass="txtBox "
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 100px; height: 29px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 37px"></td>
                                                                    <td align="right">&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSaveChannelType" OnClick="btnSaveChannelType_Click" runat="server"
                                                                            Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        <asp:Button ID="btnCancel" runat="server" Width="85px" Font-Size="8pt" OnClick="btnCancel_Click"
                                                                            Text="Cancel" CssClass="Button" />
                                                                    </td>
                                                                    <td style="width: 100px; height: 37px"></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </fieldset>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Height="190px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdChannelData" runat="server" Width="100%" ForeColor="SteelBlue"
                                                            CssClass="gridRow2" BorderColor="SteelBlue" BackColor="White" HorizontalAlign="Center"
                                                            AutoGenerateColumns="False" OnRowEditing="grdChannelData_RowEditing" OnRowDeleting="grdChannelData_RowDeleting"
                                                           >
                                                            
                                                            <Columns>
                                                                <asp:BoundField DataField="REF_ID" HeaderText="Id">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SLASH_CODE" HeaderText="Code">
                                                                    <ItemStyle  CssClass="grdDetail" Width="20%"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SLASH_DESC" HeaderText="Name">
                                                                    <ItemStyle  CssClass="grdDetail" Width="60%"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                                                  <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            ToolTip="Delete">
                                                                            <img id="imgDel" alt="" src="~/images/delete.gif" runat="server" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server">
                            <HeaderTemplate>
                                Department
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px"></td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>

                                                    <fieldset>
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 100px; height: 8px"></td>
                                                                    <td style="height: 8px" align="left" colspan="2">
                                                                        <strong>
                                                                            <asp:Label ID="lblErrorMsgDivsion" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></strong><br />
                                                                    </td>
                                                                    <td style="width: 100px; height: 8px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 8px"></td>
                                                                    <td style="width: 159px; height: 8px"></td>
                                                                    <td style="width: 100px; height: 8px"></td>
                                                                    <td style="width: 100px; height: 8px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 28px"></td>
                                                                    <td align="left">
                                                                        <strong>
                                                                            <asp:Label ID="Label12" runat="server" Width="52px" Text="Code"></asp:Label></strong>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtbustypeCode" runat="server" Width="100px" CssClass="txtBox "
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 100px; height: 28px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 29px"></td>
                                                                    <td align="left">
                                                                        <strong>
                                                                            <asp:Label ID="Label21" runat="server" Width="53px" Text="Name"></asp:Label></strong>
                                                                    </td>
                                                                    <td style="width: 100px; height: 29px">
                                                                        <asp:TextBox ID="txtbustypeName" runat="server" Width="194px" CssClass="txtBox "
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 100px; height: 29px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 37px"></td>
                                                                    <td align="right">&nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Button ID="btnSaveBusType" OnClick="btnSaveBusType_Click" runat="server" Width="85px"
                                                                            Font-Size="8pt" Text="New" CssClass="Button" />
                                                                        <asp:Button ID="btncancelDestype" runat="server" OnClick="btncancelDestype_Click"
                                                                            Width="85px" Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                                                    </td>
                                                                    <td style="width: 100px; height: 37px"></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </fieldset>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel2" runat="server" Height="190px" ScrollBars="Vertical" Width="100%">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdBusType" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                                            BorderColor="SteelBlue" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False"
                                                            OnRowEditing="GrdBusType_RowEditing" OnRowDeleting="GrdBusType_RowDeleting" OnPageIndexChanging="GrdBusType_PageIndexChanging">
                                                           
                                                            <Columns>
                                                                <asp:BoundField DataField="REF_ID" HeaderText="Id">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SLASH_CODE" HeaderText="Code">

                                                                    <ItemStyle CssClass="grdDetail" Width="20%"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SLASH_DESC" HeaderText="Name">
                                                                    <ItemStyle  CssClass="grdDetail" Width="58%"></ItemStyle>
                                                                </asp:BoundField>
                                                                 <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                                            <img id="imgEdit2" alt="" src="~/images/edit.gif" runat="server" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            ToolTip="Delete">
                                                                            <img id="imgDel2" alt="" src="~/images/delete.gif" runat="server" />
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="grdHead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                &nbsp;
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="Allowances" TabIndex="2" ID="TabPanel3">
                            <HeaderTemplate>
                                Allowances
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%" class="cmp">
                                    <tbody>
                                        <tr>
                                            <td style="width: 5%; height: 250px"></td>
                                            <td style="width: 90%" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlAllowancesContent" runat="server">
                                                            <fieldset>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%"></td>
                                                                            <td width="26%" valign="bottom">
                                                                                <label>
                                                                                    <strong>Allowance Description</strong>
                                                                                    <asp:HiddenField ID="hdnAllowanceID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceDescription" ErrorMessage="Enter Allowance Description"
                                                                                    ControlToValidate="txtAllowanceDescription" ValidationGroup="grpAllowance" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtAllowanceDescription" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="30%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%"></td>
                                                                            <td width="20%"></td>
                                                                            <td width="30%">
                                                                                <asp:RadioButton Visible="false" ID="rdbValue" Width="25px" GroupName="RatioType" runat="server" />
                                                                                <label>
                                                                                </label>
                                                                                <asp:RadioButton Visible="false" ID="rdbPercentage" Width="25px" GroupName="RatioType" runat="server" />
                                                                                <label>
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%"></td>
                                                                            <td width="20%" valign="bottom">
                                                                                <label>
                                                                                    <strong>Ratio</strong>
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAllowanceRatio" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="30%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td colspan="2" align="right">
                                                                                <asp:LinkButton ID="btnSaveAllowance" ValidationGroup="grpAllowance" runat="server"
                                                                                    OnClick="btnSaveAllowance_Click" CssClass="Button" Text="Save" />

                                                                                <asp:LinkButton ID="btnDiscardAllowance" runat="server" OnClick="btnDiscardAllowance_Click">Cancel</asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </fieldset>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlAllowancesGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                            <asp:LinkButton ID="btnShowAllowancesContent" OnClick="btnShowAllowancesContent_Click"
                                                                runat="server">Add New Allowance</asp:LinkButton>
                                                            <asp:Repeater ID="rptAllowances" runat="server" OnItemCommand="rptAllowances_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table style="width: 100%; margin-top: 0px">
                                                                        <tr class="tblheading">
                                                                            <td>
                                                                                <strong>Allowance Description</strong>
                                                                            </td>
                                                                            <td>
                                                                                <strong>Ratio </strong>
                                                                            </td>
                                                                        </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="AllowanceID" runat="server">
                                                                                     <%# Eval("AllowanceDescription")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="AllowanceID"
                                                                                runat="server">
                                                                        <%# Eval("AllowanceRatio")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr style="background-color: #E4E4E4">
                                                                        <td>
                                                                            <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="AllowanceID"
                                                                                runat="server">
                                                                                     <%# Eval("AllowanceDescription")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="AllowanceID"
                                                                                runat="server">
                                                                        <%# Eval("AllowanceRatio")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 5%; height: 250px"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="Deduction" TabIndex="3" ID="TabPanel4">
                            <HeaderTemplate>
                                Deductions
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%" class="cmp">
                                    <tbody>
                                        <tr>
                                            <td style="width: 5%; height: 250px"></td>
                                            <td style="width: 90%" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlDeductionContent" runat="server">
                                                            <fieldset>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%"></td>
                                                                            <td width="26%" valign="bottom">
                                                                                <label>
                                                                                    <strong>Deduction Description </strong>
                                                                                    <asp:HiddenField ID="hdnDeductionID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtDeductionDescription" ErrorMessage="Enter Deduction Description"
                                                                                    ControlToValidate="txtDeductionDescription" ValidationGroup="grpDeduction" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtDeductionDescription" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="30%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%"></td>
                                                                            <td width="20%"></td>
                                                                            <td width="30%">
                                                                                <asp:RadioButton Visible="false" ID="rdbDeductionValue" Width="25px" GroupName="DeductionRatioType"
                                                                                    runat="server" />
                                                                                <label>
                                                                                </label>
                                                                                <asp:RadioButton Visible="false" ID="rdbDeductionPercentage" Width="25px" GroupName="DeductionRatioType"
                                                                                    runat="server" />
                                                                                <label>
                                                                                </label>
                                                                            </td>

                                                                            <td width="30%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%"></td>
                                                                            <td width="20%" valign="bottom">
                                                                                <label>
                                                                                    <strong>Ratio</strong>
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:TextBox ID="txtDeductionRatio" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="30%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td colspan="2" align="right">&nbsp;
                                                                                <asp:LinkButton ID="btnSaveDeduction" ValidationGroup="grpDeduction" runat="server"
                                                                                    OnClick="btnSaveDeduction_Click" Text="Save" CssClass="Button"></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardDeduction" runat="server" OnClick="btnDiscardDeduction_Click">
                                                                             Cancel</asp:LinkButton>
                                                                            </td>

                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </fieldset>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlDeductionGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                            <asp:LinkButton ID="btnShowDeductionContent" OnClick="btnShowDeductionContent_Click"
                                                                runat="server">Add New Deduction</asp:LinkButton>
                                                            <asp:Repeater ID="rptDeductions" runat="server" OnItemCommand="rptDeductions_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table style="width: 100%; margin-top: 0px">
                                                                        <tr class="tblheading">
                                                                            <td>Deduction Description
                                                                            </td>
                                                                            <td>Ratio
                                                                            </td>
                                                                        </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DeductionID")%>' CommandName="DeductionID"
                                                                                runat="server">
                                                                                     <%# Eval("DeductionDescription")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("DeductionID")%>' CommandName="DeductionID"
                                                                                runat="server">
                                                                        <%# Eval("DeductionRatio")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr style="background-color: #E4E4E4">
                                                                        <td>
                                                                            <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DeductionID")%>' CommandName="DeductionID"
                                                                                runat="server">
                                                                                     <%# Eval("DeductionDescription")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("DeductionID")%>' CommandName="DeductionID"
                                                                                runat="server">
                                                                        <%# Eval("DeductionRatio")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 5%; height: 250px"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
