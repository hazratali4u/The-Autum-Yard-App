<%@ Page Title="CORN :: Bill Of Material" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
 CodeFile="frmBillOfMaterial.aspx.cs" Inherits="Forms_frmBillOfMaterial" %>

<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">

    function ValidateForm() {
        var str;      
        str = document.getElementById('<%=txtQuantity.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Quantity');
            return false;
        }

        return true;
    }

    function pageLoad() {

        $("select").searchable();

        //find the current popup
        var popUp = $find('ModelPopup');

        //check it exists so the script won't fail
        if (popUp) {
            //Add the function below as the event
            popUp.add_hidden(HidePopupPanel);
        }
    }

    function HidePopupPanel(source, args) {
        //find the panel associated with the extender
        objPanel = document.getElementById(source._PopupControlID);

        //check the panel exists
        if (objPanel) {
            //set the display attribute, so it remains hidden on postback
            objPanel.style.display = 'none';
        }
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
    prm.add_beginRequest(BeginRequestHandler);
    // Raised after an asynchronous postback is finished and control has been returned to the browser.
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.show();
        }
    }

    function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }
</script>
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<div id="right_data">
    <asp:UpdatePanel ID="pnlBillOfMaterial" runat="server">
        <ContentTemplate>
            <div>
                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif" Width="31px" />
                    </ProgressTemplate>
                    </asp:UpdateProgress>
                <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>            
            </div>
            <table width="100%">
            <tr>
                <td style="width:7%">
                    <strong> <asp:Label ID="lblSKUFinished" runat="server" Text="Select Item"></asp:Label> </strong>
                </td>
                <td style="width:1%"></td>
                <td style="width:92%">
                    <asp:DropDownList ID="ddlSKUFinished" runat="server" Width="300px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlSKUFinished_SelectedIndexChanged">                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td style="width:350px; background-color: #006699; padding: 2px;">
                                 <label class="lblDetail">Raw Material Item</label>
                            </td>
                            <td style="width:70px; background-color: #006699; padding: 2px;">
                                <label class="lblDetail">Quantity</label>      
                                                      </td>
                            <td style="width:80px">
                             
                            </td>
                           
                        </tr>
                        <tr>
                            <td >
                                <asp:DropDownList ID="ddlSKU" runat="server" Width="360px">                    
                                </asp:DropDownList>
                            </td>
                            <td >
                                <asp:TextBox ID="txtQuantity" runat="server" Width="70"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeQuantity" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtQuantity">
                                </ajaxToolkit:FilteredTextBoxExtender>                                
                            </td>
                            <td >
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="Button" Width="80px"
                                    onclick="btnAdd_Click" OnClientClick="return ValidateForm();" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="pnlSKU" runat="server" Width="531px" Height="250px" 
                                    ScrollBars="Vertical" BorderColor="Silver" BorderStyle="Groove" 
                                    BorderWidth="1px">
                                    <asp:GridView Width="100%" id="gvSKU" runat="server" ForeColor="SteelBlue" BackColor="White" HorizontalAlign="Center"
                                     AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" OnRowDeleting="gvSKU_RowDeleting" OnRowEditing="gvSKU_RowEditing">
                                       
                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_NAME"  ReadOnly="true">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="72%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QUANTITY"  ReadOnly="true">
                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="13%"></ItemStyle>
                                            </asp:BoundField>      
                                           <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                    <img id="imgEdit2" alt="" src="~/images/edit.gif" runat="server" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="7%" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="grdDetail" Width="7%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                    ToolTip="Delete">
                                                    <img id="imgDel2" alt="" src="~/images/delete.gif" runat="server" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>                            
                            <td style="width:70%" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;</td>
                        </tr>
                         <tr>
                            <td style="width:30%;" colspan="3" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                                    onclick="btnSave_Click" Width="70px" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Button" 
                                    onclick="btnCancel_Click" Width="70px" />
                            </td>
                            <td style="width:70%" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>    
</div>
</asp:Content>

