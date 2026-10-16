<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKU_Price.aspx.cs" Inherits="SKU_Price" Title="CORN :: Item Price" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20229.26215, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";

        }
        function pageLoad() {
            $("select").searchable();
        }
       
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Price Efftive Date');
                return false;
            }

            str = document.getElementById('<%=txtTradePrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Trade Price');
                return false;
            }
           
            return true;
        }
        function CheckBoxListSelect() {
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
            var chkBoxCount;
            var i;
            if (chkBox.checked == true) {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            } else {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {

                    chkBoxCount[i].checked = false;

                }
            }

        }

//        function ProductSelected(source, eventArgs) {
//            var skuDetail = eventArgs.get_text();
//            document.getElementById("= hfSKUID.ClientID ").value = eventArgs.get_value();
//            document.getElementById("=txtSKUName.ClientID").value = skuDetail;
//            document.getElementById("= btnDummy.ClientID ").click();
//        }


    </script>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <asp:Label runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="1">
                                    <asp:HiddenField ID="hfSKU" runat="server" />
                                    <asp:HiddenField ID="hfSKUID" runat="server" />
                                </td>
                                <td colspan="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px; " align="left">
                                   
                                </td>

                                <td style="width: 298px; " align="left">
                                     <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlType_SelectedIndexChanged" Width="200px" Visible="false">
                                                    <asp:ListItem Text ="Finish" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text ="Raw" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                </td>
                                <td width="300px"  align="left"
                                    rowspan="6">
                                    <table width="100%" style=" border-right: darkgray 1px ridge; border-top: darkgray 1px ridge;
                                    border-left: darkgray 1px ridge; border-bottom: darkgray 1px ridge" >
                                        <tbody>
                                            <tr>
                                                <td class="tblhead" width="300px">
                                                    <strong>
                                                        <asp:Label ID="Label11" runat="server" Width="300px" ForeColor="White" Font-Bold="True"
                                                            Text="Location Name"></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="SelectAll"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="320px">
                                                    <asp:Panel ID="Panel1" runat="server" Width="320px" Height="150px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                                        <asp:CheckBoxList ID="ChbDistributorList" runat="server" Width="300px" Font-Size="8pt">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td >
                                </td>
                            </tr>
                            <tr>
                            <td>
                                              <strong>  <label >Item Category</label></strong>
                                           </td>
                                           <td>
                                               <asp:DropDownList ID="ddskucategory" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged" CssClass="DropList"  Width="200px">
                                                </asp:DropDownList>
                                           </td>
                                           <td style="width: 201px; ">
                                </td>
                                           </tr>
                            <tr>
                                <td style="width: 106px;" align="left">
                                    <strong>
                                        Item Name</strong>
                                </td>
                                <td  align="left">
                                    <asp:DropDownList runat="server" ID="ddlSkus" OnSelectedIndexChanged="ddlSkus_OnSelectedIndexChanged" Width="200px" AutoPostBack="True" onfocus="ddlFocus(this);" onblur="ddlBlur(this);" />
                                  
                                   <%-- <asp:TextBox ID="txtSKUName" runat="server" Width="200px"></asp:TextBox>
                                     <asp:Button ID="btnDummy" runat="server" CssClass="HidePanel"  OnClick="btnLoadGrid_Click" />--%>

                                   <%-- <cc1:AutoCompleteExtender ID="aceProduct" runat="server" TargetControlID="txtSKUName"
                                        ServicePath="wsProductList.asmx" MinimumPrefixLength="1" CompletionInterval="500"
                                        CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetProducts2">
                                    </cc1:AutoCompleteExtender>--%>
                                </td>
                                <td style="width: 201px; ">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px" align="left">
                                    <strong>
                                        <asp:Label ID="Label8" runat="server" Width="100px" Text="Date Effected"></asp:Label></strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox Style="text-align: justify" ID="txtFromdate" TabIndex="2" runat="server"
                                        Width="180px" ValidationGroup="MKE"  Enabled="true"
                                        MaxLength="1"></asp:TextBox>
                                          <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                    TargetControlID="txtFromdate">
                                                </cc1:CalendarExtender>
                                </td>
                                
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Width="101px" Text="Selling Price" 
                                            CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox ID="txtTradePrice" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" Font-Size="8pt"
                                        Text="Save" ValidationGroup="vg" CssClass="Button" />
                                    &nbsp;
                                    
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="z-index: 101; left: 530px; width: 100px; position: absolute; top: 440px;
                        height: 100px">
                        &nbsp;
                        <asp:Panel ID="Panel21" runat="server" >
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"  AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="23px" Height="26px" 
                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnViewAll" runat="server" CssClass="Button"  Text="View All"
                                    OnClick="btnViewAll_Click"></asp:Button>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_pricedetails" runat="server" Width="100%" ForeColor="SteelBlue"
                              BorderColor="White" BackColor="White"
                            AutoGenerateColumns="False" HorizontalAlign="Center"  >
                            <Columns>
                                <asp:BoundField DataField="distributor_name" HeaderText="Location" >
                                    <ItemStyle CssClass="grdDetail" Width="25%"/>
                                      <HeaderStyle  CssClass="grdHead"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                    <HeaderStyle  CssClass="HidePanel"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                    <ItemStyle CssClass="grdDetail" Width="40%"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" HeaderText="GST"  >
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="HidePanel"  />
                                    <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_PRICE" Visible="false" HeaderText="Purchase Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TRADE_PRICE" HeaderText="Selling Price" DataFormatString="{0:F2}">
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                    <HeaderStyle CssClass="grdHead" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RETAIL_PRICE" HeaderText="Retail Price" Visible="false" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TAX_PRICE" HeaderText="GST (%)" Visible="false" DataFormatString="{0:F2}">
                                    <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                    <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="DATE_EFFECTED" HeaderText="Date Effected">
                                    <ItemStyle CssClass="grdDetail" Width="15%">
                                    </ItemStyle>
                                    <HeaderStyle CssClass="grdHead" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="grdHead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
