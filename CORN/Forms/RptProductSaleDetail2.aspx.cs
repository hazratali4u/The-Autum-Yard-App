using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CORNBusinessLayer.Classes;
using System.Data;
using CORNCommon.Classes;

public partial class Forms_RptProductSaleDetail2 : System.Web.UI.Page
{
    readonly RptSaleController _rptCustomerCtl = new RptSaleController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            LoadDistributor();
           // LoadPrincipal();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }

    }
    private void LoadPrincipal()
    {
        //SKUPriceDetailController pController = new SKUPriceDetailController();
        //DataTable mDt = pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        //DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpPrincipal, mDt, 0, 1);
    }
    private void LoadDistributor()
    {
        DistributorController dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }



    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController dPrint = new DocumentPrintController();

        DataSet ds = null;
        DataTable dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        ds = _rptCustomerCtl.SelectSKUSaleReport(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue,
           DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1);

        CORNBusinessLayer.Reports.CrpProductSaleDetail2 crpReport = new CORNBusinessLayer.Reports.CrpProductSaleDetail2();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("Principal", "");
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());


        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }


    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController dPrint = new DocumentPrintController();

        DataTable dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = _rptCustomerCtl.SelectSKUSaleReport(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue,
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1);

        CORNBusinessLayer.Reports.CrpProductSaleDetail2 crpReport = new CORNBusinessLayer.Reports.CrpProductSaleDetail2();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("Principal", "");

        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());


        string path = Configuration.GetAppInstallationPath() + "\\Exported.xls";

        crpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        crpReport.ExportToDisk(ExportFormatType.Excel, path);

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