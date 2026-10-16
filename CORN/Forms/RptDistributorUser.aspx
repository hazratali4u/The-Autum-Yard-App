<%@ Page Title="CORN :: Employee List" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptDistributorUser.aspx.cs" Inherits="Forms_RptDistributorUser" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    
    <div id="right_data">
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td align="left" colspan="3">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left" width="70px">
                                        <strong>
                                           Location</strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left" width="70px">
                                        <strong>
                                           Status</strong>
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="drpStatus" runat="server" Width="200px" CssClass="DropList"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" >
                                            <asp:ListItem Text="Active" Value="true"></asp:ListItem>
                                            <asp:ListItem Text="InActive" Value="false"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left" width="70px">
                                        <strong>
                                            Employee</strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpEmployee" runat="server" Width="200px" CssClass="DropList"
                                             >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                               
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" OnClick="btnViewPDF_Click"
                        CssClass="Button" Width="85" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" OnClick="btnViewExcel_Click"
                        CssClass="Button" Width="85" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
  
</asp:Content>

