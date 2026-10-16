<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en-us">
<head>
    <title>Login :: Welcome</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="login-page">
            <asp:UpdatePanel ID="upLogin" runat="server">
                <ContentTemplate>
                    <div class="container">
                        <div class="row login-contents">
                            <div class="col-md-12">
                                <div class="logo">
                                    &nbsp;&nbsp;&nbsp;
                                <img src="images/logo.png" class="img-responsive" alt="" />
                                    <h1>Welcome</h1>
                                    <p>

                                        <div runat="server" id="dvError" visible="false" style='padding: 10px; margin: 5px; background-color: #fbefef; border: Solid 1px #bb0000; color: #bb0000; font-size: 12px; font-family: Verdana; width: 100%'>
                                            <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>

                                        </div>
                                    </p>
                                    <p>
                                        <div runat="server" id="dvLicense" visible="false" style='padding: 10px; margin: 5px; background-color: #fbefef; border: Solid 1px #bb0000; color: #bb0000; font-size: 12px; font-family: Verdana; width: 100%'>
                                            <asp:Label ID="lblLicenseMsg" runat="server" Text=""></asp:Label>
                                        </div>
                                    </p>
                                    <div class="from-group">
                                        <asp:TextBox runat="server" placeholder="User Name" ID="txtLogin" class="form-control" />
                                        <asp:TextBox TextMode="Password" runat="server" placeholder="*******" ID="txtPassword" class="form-control" />
                                        <asp:Button CssClass="btn btn-success" ID="btnSignIn" OnClick="btnSignIn_Click" runat="server"
                                            Text="Login"></asp:Button>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
    <script src="AjaxLibrary/jquery-1.10.2.js" type="text/javascript" language="javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="AjaxLibrary/bootstrap.min.js" type="text/javascript" language="javascript"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
    </script>
</body>
</html>
