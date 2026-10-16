using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Adjust Stock
/// </summary>
public partial class Forms_frmOpeningStock : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly DataControl _dc = new DataControl();
    private static int _rowNo;
    DataTable _purchaseSku;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreatTable();
            GetDocumentNo();
            LoadDistributor();
            LoadSkuDetail();
        }
    }

    private void CreatTable()
    {
        _purchaseSku = new DataTable();
        _purchaseSku.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        _purchaseSku.Columns.Add("SKU_ID", typeof(int));
        _purchaseSku.Columns.Add("SKU_Code", typeof(string));
        _purchaseSku.Columns.Add("SKU_Name", typeof(string));
       
        _purchaseSku.Columns.Add("PRICE", typeof(decimal));
        _purchaseSku.Columns.Add("Quantity", typeof(decimal));
 
        _purchaseSku.Columns.Add("AMOUNT", typeof(decimal));
        
        Session.Add("PurchaseSKU", _purchaseSku);
    }

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
    }

    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            CreatTable();
            LoadGird();
            drpDistributor.Enabled = true;
            
            DrpDocumentType.Enabled = true;
            ClearAll();
            txtDocumentNo.Text = "";
            DisAbaleOption(false);
        }
        else
        {
            LoadDocumentDetail();
            LoadSkuDetail();
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
    }    
    
    private void LoadSkuDetail()
    {
            //DataTable Dtsku_Price = _pController.SelectDataPrice2(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 2, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            //clsWebFormUtil.FillListBox(lstCode, Dtsku_Price, 9, 9, true);
           
            SkuController SKUCtl = new SkuController();
            DataTable dtskuPrice = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSkus, dtskuPrice, "SKU_ID", "SKU_NAME", true);

          
            Session.Add("Dtsku_Price", dtskuPrice);
        
    }
       
    private void LoadGird()
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = _purchaseSku;
        GrdPurchase.DataBind();
    }


    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _rowNo = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
       
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
      

        ddlSkus.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update";

    }

    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        if (_purchaseSku.Rows.Count > 0)
        {
            _purchaseSku.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
    }
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
            //drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            txtDocumentNo.Text = dt.Rows[0][2].ToString();
            _purchaseSku = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
    }

    private bool CheckDublicateSku()
    {
        
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = _purchaseSku.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }    
      
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
       
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            _purchaseSku = (DataTable)Session["PurchaseSKU"];

            if (btnSave.Text == "Add")
            {
                if (CheckDublicateSku())
                {
                    DataRow dr = _purchaseSku.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                    dr["PRICE"] = 0; //foundRows[0]["DISTRIBUTOR_PRICE"];
                    dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                    dr["AMOUNT"] = 0;// decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                    _purchaseSku.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.SelectedItem + " Already Exists ');", true);
                    
                    return;
                }
            }
            else if (btnSave.Text == "Update")
            {
                    DataRow dr = _purchaseSku.Rows[_rowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                    dr["PRICE"] = 0;// foundRows[0]["DISTRIBUTOR_PRICE"];
                    dr["Quantity"] = decimal.Parse(txtQuantity.Text);

                    dr["AMOUNT"] = 0;// decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
            }
            Session.Add("PurchaseSKU", _purchaseSku);
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong item please check in list');", true); 
        }
    }    
     
    /// <summary>
    /// Saves Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        var mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            DateTime mWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            PurchaseController mController = new PurchaseController();
            DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];
            decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));

            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                      , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), 0);
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                   , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()),0
                   , mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), 0);
            }

            lblErrorMsg.Text = "Record Upated";
            _purchaseSku = (DataTable)Session["PurchaseSKU"];
            _purchaseSku.Rows.Clear();
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
            GetDocumentNo();
            drpDistributor.Enabled = true;
            
            DrpDocumentType.Enabled = true;
            ClearAll();
            txtDocumentNo.Text = "";
            DisAbaleOption(false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location or Unassigne');", true);
        }
    }
   
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        drpDistributor.Enabled = true;
        
        DrpDocumentType.Enabled = true;
        ClearAll();
        txtDocumentNo.Text = "";
        DisAbaleOption(false);
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
           
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;

        }
        else
        {

            DrpDocumentType.Enabled = true;
          
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
       
        txtQuantity.Text = "";
      
        ddlSkus.Enabled = true;
        btnSave.Text = "Add";
        lblErrorMsg.Text = "";

    }
}