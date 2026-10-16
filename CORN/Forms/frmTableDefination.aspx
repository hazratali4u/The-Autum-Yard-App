<%@ Page Title="CORN :: Add Tables" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTableDefination.aspx.cs" Inherits="Forms_frmTableDefination" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtTableNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Table Name');
                document.getElementById('<%=txtTableNo.ClientID%>').focus();
                return false;
            }
            str = document.getElementById('<%=txtDescription.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Description');
                document.getElementById('<%=txtDescription.ClientID%>').focus();
                return false;
            }
            return true;
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 20px">
                        
                        </td>
                        <td>
                        <strong>Location:</strong>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlDistributor" Width="198px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px">
                            
                        </td>
                        <td>
                            <strong>Table Name:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTableNo" runat="server" Width="195px"></asp:TextBox>
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
                    <tr style="height: 50px;">
                        <td colspan="2">
                        </td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" class="Button" OnClick="btnSave_Click">Save</asp:LinkButton>
                            <asp:LinkButton ID="btnExit" runat="server" class="Button" OnClick="btnExit_Click">Exit</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                
                <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px" Width="70%">
                <asp:Repeater ID="rTableDefination" runat="server" OnItemCommand="rTableDefination_ItemCommand">
                    <HeaderTemplate>
                        <table style="width: 100%" class="table-border">
                            <tr>
                                <td  class="grdHead" style="width: 30%">
                                   <strong>Table Name</strong> 
                                </td>
                                <td  class="grdHead" style="width: 50%">
                                  <strong>   Description</strong> 
                                </td>
                                <td  class="grdHead" style="width: 10%">
                                                                    </td>
                                   <td   class="grdHead" style="width: 10%">
                                                                    </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="grdDetail">
                                <%# Eval("TableDefination_No")%>
                            </td>
                            <td class="grdDetail">
                                <%# Eval("TableDefination_Description")%>
                            </td>
                             
                            <td align="center" class="grdDetail">
                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="edit" CommandArgument='<%# Eval("TableDefination_ID")%>'>
                                    <image src="../images/edit.gif" ></asp:LinkButton>
                            </td>
                            <td align="center" class="grdDetail">
                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="delete" CommandArgument='<%# Eval("TableDefination_ID")%>'>
                                    <image src="../images/delete.gif"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                        
                    </asp:Panel>
                </ul>
                <div class="clr">
                </div>
                <table width="70%">
                    <tr>
                        <td align="right" width="56%">
                            
                        </td>
                        <td align="right" width="44%">
                            
                            <asp:Label ID="lblDummy" runat="server" Text="Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                            <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hf_tbldefID" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
