using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;
using CrystalDecisions.Shared;

public partial class Forms_RptDistributorUser : System.Web.UI.Page
{

    Distributor_UserController mDUController = new Distributor_UserController(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadEmployee();
        }
    }

    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
     
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, false);

    }

    private void LoadEmployee()
    {
        drpEmployee.Items.Clear();
        DataTable dt = null;
        if (drpStatus.SelectedValue == "false")
        {
            dt = mDUController.SelectDistributorUser(0, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
        }
        else
        {
            dt = mDUController.SelectDistributorUser(1, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
        
        }
        drpEmployee.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

        clsWebFormUtil.FillDropDownList(drpEmployee, dt, 0, 6);
    }
    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployee();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployee();
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpUserList CrpReport = new CORNBusinessLayer.Reports.CrpUserList();
        DataSet ds = null;
        ds = mDUController.SelectDistributorUserInfo(int.Parse(drpEmployee.SelectedValue.ToString()), Convert.ToBoolean(drpStatus.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Status", drpStatus.SelectedItem.Text);
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Route Wise Customer List in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        CORNBusinessLayer.Reports.CrpUserList CrpReport = new CORNBusinessLayer.Reports.CrpUserList();
        DataSet ds = null;

        ds = mDUController.SelectDistributorUserInfo(int.Parse(drpEmployee.SelectedValue.ToString()), Convert.ToBoolean(drpStatus.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()));
     
        CrpReport.SetDataSource(ds);

        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Status", drpStatus.SelectedItem.Text);
        string path = CORNCommon.Classes.Configuration.GetAppInstallationPath() + "\\ChartOfAccount.xls";

        CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        CrpReport.ExportToDisk(ExportFormatType.Excel, path);


        System.IO.FileInfo file = new System.IO.FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = "application/octet-stream";

            Response.WriteFile(file.FullName);

            Response.End();

        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }
}