<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCreateUser.aspx.cs" Inherits="Forms_frmCreateUser" Title="CORN :: User Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }


        function ValidateForm() {
            var str;

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
            return true;
        }

    </script>
    
    <div id="right_data">
        <div style="z-index: 101; left: 481px; width: 100px; position: absolute; top: 100px; height: 100px"
            id="DIV1">
            <asp:UpdateProgress ID="UpdateProgress5" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>&nbsp; Loading....
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 90px">
                                                <strong>Base Location</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddDistributorId" runat="server" Width="205px" CssClass="DropList"
                                                    OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Name</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpUser" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Roles</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddRole" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Login Id</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLoginId" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Password</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtpassword" runat="server" Width="200px" TextMode="Password"  MaxLength="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"></asp:CheckBox>
                                                <asp:CheckBox ID="chkright" runat="server" Width="93px" Text="Item Delete"></asp:CheckBox>
                                                <asp:CheckBox ID="chkright2" runat="server" Width="93px" Text="Item Less"></asp:CheckBox>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td align="right" colspan="1"></td>
                                            <td>&nbsp;
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="82px" Font-Size="8pt"
                                                    Text="Save" CssClass="Button" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Width="82px" Font-Size="8pt" Text="Cancel"
                                                    OnClick="btnCancel_Click" CssClass="Button" />
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

                    <table class="tblhead" width="100%">
                        <tbody>
                            <tr>
                                <td style="color: White; font-weight: bold;">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                        <asp:ListItem Value="USER_NAME">User Name</asp:ListItem>
                                        <asp:ListItem Value="LOGIN_ID">Login</asp:ListItem>
                                        <asp:ListItem Value="role_name">Role</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="width: 250px; height: 21px" align="left">
                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnFilter_Click" />
                                </td>
                                <td style="width: 95px; height: 21px"></td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="250px" ScrollBars="Vertical"
                        BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue"
                            AutoGenerateColumns="False" BackColor="White"
                            OnRowEditing="Grid_users_RowEditing">

                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="User Id" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Distributor_ID" HeaderText="Distributor" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel "></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_CODE" HeaderText="Code" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="Name" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" Width="20%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LOGIN_ID" HeaderText="Login" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PASSWORD" HeaderText="Password" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="role_name" HeaderText="Role" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" Width="18%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" ReadOnly="true">
                                    <ItemStyle CssClass="grdDetail" Width="8%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ROLE_ID" HeaderText="ROLE_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="isDelRight" HeaderText="isDelRight" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="isLessRight" HeaderText="isLessRight" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                            <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
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
