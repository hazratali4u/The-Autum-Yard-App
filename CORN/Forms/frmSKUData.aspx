<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" Title="CORN :: Item Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfSkuId" runat="server" />
                <table>
                    <tr>
                        <td>
                            <strong>
                                <label>
                                    Category Type</label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                Width="200px">
                                <asp:ListItem Text="Finish" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Raw" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 50px;"></td>
                        <td>
                            <strong>
                                <label>
                                    Item Section</label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="True" CssClass="DropList"
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <label>
                                    Item Category</label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddskucategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged"
                                CssClass="DropList" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>
                            <strong>
                                <label class="label-box1">
                                    Unit Of Measure</label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUnitOfMeasure" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <label>
                                    Item Name</label></strong>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtskuname" runat="server" CssClass="txtBox" Width="100%"></asp:TextBox>
                            <asp:TextBox ID="txtskucode" runat="server" CssClass="txtBox" Width="195px" Style="display: none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <label class="label-box1">
                                    Unit In Case</label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtunitincase" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                            <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                TargetControlID="txtunitincase">
                            </cc1:FilteredTextBoxExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <label>
                                    Item Description</label></strong>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtDescription" runat="server" Rows="3" TextMode="MultiLine" Width="100%"></asp:TextBox>

                        </td>
                    </tr>
                    <tr runat="server" visible="false" id="trItemType">
                        <td>
                            <strong>
                                <label>
                                    Item Type</label></strong>
                        </td>
                        <td colspan="4">
                            <asp:DropDownList ID="drpProductType" runat="server" 
                                Width="200px">
                                <asp:ListItem Text="Quantity" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Value" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td></td>
                        <td colspan="4">
                            <asp:CheckBox ID="chkIsDesc" runat="server" Width="93px" Text="Print Desc" Checked="false" Font-Bold="true"></asp:CheckBox>
                            <asp:CheckBox ID="chkIsInventory" runat="server" Text="Is Inventory Item" Checked="false" Font-Bold="true"></asp:CheckBox>
                             
                        </td>
                    </tr>
                </table>
                <div style="margin: 10px 0px 10px 100px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnSave_Click"
                        Text="Save" Width="86px" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>&nbsp; Loading....
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table class="tblhead" width="100%">
                    <tbody>
                        <tr>
                            <td style="color: White; font-weight: bold; width: 12%">
                                <asp:Label ID="Label10" runat="server" Text="Searching Type"></asp:Label>
                            </td>
                            <td style="width: 22%; height: 22px" align="left">
                                <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                    <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                    <asp:ListItem Value="Category">Category</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 22%; height: 22px" align="left">
                                <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                            </td>
                            <td style="width: 48%; height: 22px" align="left">
                                <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="9pt" Text="Filter"
                                    OnClick="btnFilter_Click"></asp:Button>
                                <asp:Button ID="btnViewAll" runat="server" Width="85px" Font-Size="9pt" Text="View All"
                                    OnClick="btnViewAll_Click"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:Panel ID="Panel1" runat="server" Width="100%" Height="200px" ScrollBars="Auto">

                    <asp:GridView ID="grdSKUData" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                        HorizontalAlign="Center" BorderColor="White" AutoGenerateEditButton="False" AutoGenerateDeleteButton="False"
                        BackColor="White" AutoGenerateColumns="False" OnRowEditing="grdSKUData_RowEditing"
                        OnRowDeleting="grdSKUData_RowDeleting">

                        <Columns>
                            <asp:BoundField DataField="Principal_Id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Division_Id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Category_Id" HeaderText="Category_Id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Brand_Id" HeaderText="Brand_Id" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Principal" HeaderText="Supplier" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Division" HeaderText="Division" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true">
                                <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Brand" HeaderText="Bran" ReadOnly="true">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_CODE" HeaderText="Code" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_NAME" HeaderText="Name" ReadOnly="true">
                                <ItemStyle CssClass="grdDetail" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" ReadOnly="true">
                                <ItemStyle CssClass="grdDetail" Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PACKSIZE" HeaderText="Pack Size" ReadOnly="true">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UNITS_IN_CASE" HeaderText="UIC" ReadOnly="true">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GST_ON" HeaderText="GST" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SECTION_ID" ReadOnly="true">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="true" DataField="IS_DESC">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="true" DataField="ISEXEMPTED">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                        <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="7.5%" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle CssClass="grdDetail" Width="7.5%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                        ToolTip="Delete">
                                        <img id="imgDel" alt="" src="~/images/delete.gif" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="grdHead" BorderColor="SteelBlue"></HeaderStyle>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
