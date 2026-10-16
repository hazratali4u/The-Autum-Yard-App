<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUCategory.aspx.cs" Inherits="frmSKUCategory" Title="CORN :: Item Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <div id="right_data">

                <table class="tbl-product-section">
                    <tr>
                        <td style="width: 20px;"></td>
                        <td>
                            <strong>Category Type:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="200px">
                                <asp:ListItem Text="Finish" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Raw" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>Category Code:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategoryCode" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>Category Name :  </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategoryName" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td colspan="2"></td>
                        <td>
                            <asp:Button ID="btnSaveCategory" OnClick="btnSaveCategory_Click" runat="server" Text="New" CssClass="Button" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" Width="100%" BorderWidth="1px">
                                <asp:GridView ID="GrdCategory" runat="server" Width="100%" ForeColor="SteelBlue"
                                    Font-Size="9pt" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnRowEditing="GrdCategory_RowEditing" OnRowDeleting="GrdCategory_RowDeleting"
                                    OnPageIndexChanging="GrdCategory_PageIndexChanging">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <Columns>
                                        <asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                            <ItemStyle CssClass="grdDetail" Width="80%"></ItemStyle>
                                            <HeaderStyle CssClass="grdHead" />
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
                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel21">
        <ProgressTemplate>
            <asp:Image ID="Image3" runat="server" Width="30px" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:Image>&nbsp; Loading .........
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
