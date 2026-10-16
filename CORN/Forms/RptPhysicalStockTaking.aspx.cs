using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For Physical Stock Report
/// </summary>
public partial class Forms_RptPhysicalStockTaking : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        
            LoadLocation();
            this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");           
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
  

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DrpLocation.Items.Clear();
   
            if (RblType.SelectedIndex == 0)
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
            }
            else
            {
                DistributorController DController = new DistributorController();
                DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
                this.DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Physical Stock Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        try
        {
            if (RblType.SelectedIndex == 0)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();

                string FromDate = null;

                CORNBusinessLayer.Reports.CrpPhysicalStockTaking CrpReport = new CORNBusinessLayer.Reports.CrpPhysicalStockTaking();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();

                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                ds = RptInventoryCtl.PhysicalStockTaking(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());


                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                string FromDate = null;

                CORNBusinessLayer.Reports.RptPhysicalStockSummary CrpReport = new CORNBusinessLayer.Reports.RptPhysicalStockSummary();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();


                ds = RptInventoryCtl.PhysicalStockTakingValueWise(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(this.Session["UserId"].ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();


                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Physical Stock Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        try
        {
            if (RblType.SelectedIndex == 0)
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();

                string FromDate = null;

                CORNBusinessLayer.Reports.CrpPhysicalStockTaking CrpReport = new CORNBusinessLayer.Reports.CrpPhysicalStockTaking();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();

                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                ds = RptInventoryCtl.PhysicalStockTaking(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", "");
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());


                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                CORNBusinessLayer.Classes.DocumentPrintController DPrint = new CORNBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
                string FromDate = null;

                CORNBusinessLayer.Reports.RptPhysicalStockSummary CrpReport = new CORNBusinessLayer.Reports.RptPhysicalStockSummary();
                DataSet ds = null;

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);

                FromDate = parsed_date_fromdate.ToShortDateString();


                ds = RptInventoryCtl.PhysicalStockTakingValueWise(Constants.IntNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), int.Parse(this.Session["UserId"].ToString()));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();


                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Principals And Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RblType_SelectedIndexChanged(object sender, EventArgs e)
    {
       LoadLocation();
    }
}
