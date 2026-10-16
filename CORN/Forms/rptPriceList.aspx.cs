using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using CORNBusinessLayer.Reports;

/// <summary>
/// Form For SKU Price List Report
/// </summary>
public partial class Forms_rptPriceList : System.Web.UI.Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptSaleController _rptSaleCtl = new RptSaleController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        LoadDistributor();
        LoadCatagory();
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads SKU Categories To Category Combo
    /// </summary>
    protected void LoadCatagory()
    {
        var hierarchy = new SkuHierarchyController();
        // DataTable dtCatagory = hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        // DataView dv = new DataView(dtCatagory);
        // dtCatagory = dv.ToTable();

        // clsWebFormUtil.FillDropDownList(this.DrpCatagory, dtCatagory, "Category_Id", "Category_Name");


        DataTable dtCatagory = hierarchy.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, 1, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        this.DrpCatagory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpCatagory, dtCatagory, 0, 3, false);
    }
    /// <summary>
    /// Shows SKU Price List in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue));

            var crpReport = new crpPriceList3();
            DataSet ds = null;
            
            ds = _rptSaleCtl.PriceList(Constants.IntNullValue, int.Parse(DrpCatagory.SelectedValue), int.Parse(DrpDistributor.SelectedValue));

            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            
            crpReport.SetParameterValue("Branch", DrpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows SKU Price List in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue));
            var crpReport = new crpPriceList3();

            DataSet ds = _rptSaleCtl.PriceList(Constants.IntNullValue, int.Parse(DrpCatagory.SelectedValue), int.Parse(DrpDistributor.SelectedValue));

            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            
            crpReport.SetParameterValue("Branch", DrpDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", crpReport);
            this.Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}
