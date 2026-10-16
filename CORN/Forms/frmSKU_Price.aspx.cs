using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit SKU Price
/// </summary>
public partial class SKU_Price : System.Web.UI.Page
{
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    readonly SKUPriceDetailController mSKUController = new SKUPriceDetailController();
    private DataTable _mDt;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //LoadSKUS();
            LoadDistributor();
            txtFromdate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            Populate_drpSKUCategory();
            FillddlSkus();
            if (ddlSkus.Items.Count > 0)
            {
                LoadGrid(int.Parse(ddlSkus.SelectedValue));
            }
        }
    }

    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSkus()
    {
        DataTable dtSKU;
        SKUPriceDetailController pController = new SKUPriceDetailController();
        dtSKU = pController.SelectDataPrice2(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 4, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        hfSKU.Value = GetJson(dtSKU);
    }

    private void LoadAllItems()
    {

        DataTable dtsku = mSKUController.SelectSKuCurrentPrice(Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

        DataView dv = dtsku.DefaultView;
        dv.Sort = "sku_name asc";
        DataTable sortedDt = dv.ToTable();

        Grid_pricedetails.DataSource = sortedDt;
        Grid_pricedetails.DataBind();
    }

    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    private void LoadGrid(int skuid)
    {

        DataTable dtsku = mSKUController.SelectSKuCurrentPrice(Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, skuid);
        Grid_pricedetails.DataSource = dtsku.DefaultView;
        Grid_pricedetails.DataBind();
    }
    private void FillddlSkus()
    {
        SkuController mSKUController = new SkuController();
        DataTable dt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(ddskucategory.SelectedValue.ToString()), Convert.ToInt32(ddlType.SelectedValue),
            int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlSkus, dt, "SKU_ID", "SKU_NAME", true);
    }

    /// <summary>
    /// Loads Locations To Location ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);
    }

    /// <summary>
    /// Save Or Updates an SKU Price
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();

            int count = 0;

            int SKUID = 0;
            if (ddlSkus.Items.Count > 0)
            {
                SKUID = Convert.ToInt32(dc.chkNull_0(ddlSkus.SelectedValue));
            }


            for (int i = 0; i < ChbDistributorList.Items.Count; i++)
            {
                if (ChbDistributorList.Items[i].Selected == true)
                {
                    count++;
                    mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), SKUID, 1, 0, 0, 0, decimal.Parse(dc.chkNull_0(txtTradePrice.Text)), 0, DateTime.Parse(txtFromdate.Text), 0);
                    LoadGrid(SKUID);
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select any Location');", true);
            }

            txtTradePrice.Text = "";

        }
    }

    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    protected void btnLoadGrid_Click(object sender, EventArgs e)
    {

        if (hfSKUID.Value != "")
        {
            LoadGrid(Convert.ToInt32(hfSKUID.Value));
        }
        else
        {
            hfSKUID.Value = "";
            Grid_pricedetails.DataSource = null;
            Grid_pricedetails.DataBind();
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUCategory();
        FillddlSkus();
    }
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlSkus();
        if (ddlSkus.Items.Count > 0)
        {
            LoadGrid(int.Parse(ddlSkus.SelectedValue));
        }
    }

    private void Populate_drpSKUCategory()
    {
        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedValue), null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskucategory, _mDt, 0, 3, true);
    }

    protected void ddlSkus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSkus.Items.Count > 0)
        {
            LoadGrid(int.Parse(ddlSkus.SelectedValue));
        }

    }

    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        LoadAllItems();

    }
}
