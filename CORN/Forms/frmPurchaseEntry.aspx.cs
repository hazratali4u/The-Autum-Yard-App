using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using System.Globalization;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseEntry : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly PurchaseController _mPurchaseCtrl = new PurchaseController();
    readonly DataControl _dc = new DataControl();
    private static int _rowNo;
    private static decimal _privouseQty;
    DataTable _purchaseSkus;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DrpDocumentType.Focus();

            LoadPrincipal();
            LoadDistributor();
            LoadSkuDetail();
            CreatTable();
            GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
        }
    }
    
    private void CreatTable()
    {
        _purchaseSkus = new DataTable();
        _purchaseSkus.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        _purchaseSkus.Columns.Add("SKU_ID", typeof(int));
        _purchaseSkus.Columns.Add("SKU_Code", typeof(string));
        _purchaseSkus.Columns.Add("SKU_Name", typeof(string));
        _purchaseSkus.Columns.Add("PRICE", typeof(decimal));
        _purchaseSkus.Columns.Add("Quantity", typeof(decimal));
        _purchaseSkus.Columns.Add("AMOUNT", typeof(decimal));
      
        Session.Add("PurchaseSKUS", _purchaseSkus);  
    
    }

    #region Load

    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();   
        
       
        DataTable dt = _mPurchaseCtrl.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue),Constants.IntNullValue,Constants.LongNullValue,int.Parse(Session["UserId"].ToString()),0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));          
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }
   
    private void LoadPrincipal()
    {
        DataTable mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(drpPrincipal, mDt, 0, 1, true);
    }

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2,true);
    }
    
    private void LoadToDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpTransferFor, dt, 0, 2, true);
    }

    private void LoadDocumentDetail()
    {
        
        DataTable dt = _mPurchaseCtrl.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            if (DrpDocumentType.SelectedIndex == 0 || DrpDocumentType.SelectedIndex == 4)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 1)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 2)
            {
                drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else
            {
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            _purchaseSkus = _mPurchaseCtrl.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKUS", _purchaseSkus);
            LoadGird();
        }
    }
        
    private void LoadGird()
    {
        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];

        if (_purchaseSkus != null)
        {
            GrdPurchase.DataSource = _purchaseSkus;
            GrdPurchase.DataBind();

            decimal totalValue = _purchaseSkus.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["Quantity"].ToString()));
            decimal totalAmount = _purchaseSkus.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
            txtTotalQuantity.Text = String.Format(CultureInfo.InvariantCulture, 
                                 "{0:0.00}", totalValue);
            txtTotalAmount.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0.00}", totalAmount);
        }
    }

    private void LoadSkuDetail()
    {
        if (drpPrincipal.Items.Count > 0)
        {

            SkuController SKUCtl = new SkuController();
            DataTable dtskuPrice = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSkus, dtskuPrice, "SKU_ID", "SKU_NAME", true);

            //DataTable dtskuPrice = _pController.SelectDataPrice2(1, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            //clsWebFormUtil.FillDropDownList(ddlSkus, dtskuPrice, 0, 9, true);
            Session.Add("Dtsku_Price", dtskuPrice);
        }
    }
    
    #endregion

    #region IndexChnage

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblInvoice.Text = "INV/DC  No";
        if (DrpDocumentType.SelectedIndex == 0)
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Purchase For";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;

            txtPrice.Text="";
            txtPrice.Enabled = true;
            GetDocumentNo();
            drpPrincipal.Visible = true;
            lbltoLocation.Visible = true;
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            lblfromLocation.Text = "Transfer From";
            drpDistributor.Enabled = true;
            drpPrincipal.Visible = false;
            lbltoLocation.Visible = false;
            LoadToDistributor();
            DrpTransferFor.Visible = true;
            lblInvoice.Text = "Driver Name";
            Label4.Visible = true;
            Label4.Text = "Transfer To";

            txtPrice.Text = "";
            txtPrice.Enabled = false;

            GetDocumentNo();
        }
        else if (DrpDocumentType.SelectedIndex == 2)
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Return From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            DrpTransferFor.Visible = false;
            Label4.Visible = false;

            drpPrincipal.Visible = true;
            lbltoLocation.Visible = true;

            txtPrice.Text = "";
            txtPrice.Enabled = false;
            GetDocumentNo();
        }
        //else if (DrpDocumentType.SelectedIndex == 3)
        //{
        //    lblfromLocation.Text = "Transfer to";
        //    drpDistributor.Enabled = true;
        //    drpPrincipal.Enabled = true;
        //    LoadToDistributor();
        //    DrpTransferFor.Visible = true;
        //    Label4.Visible = true;
        //    Label4.Text = "Transfer From";


        //    txtPrice.Text = "";
        //    txtPrice.Enabled = false;
        //    GetDocumentNo();
        //}
        else
        {
            
            lblfromLocation.Text = "Location";
            drpDistributor.Enabled = true;
            drpPrincipal.Visible = false;
            lbltoLocation.Visible = false;

            DrpTransferFor.Visible = false;
            Label4.Visible = false;


            txtPrice.Text = "";
            txtPrice.Enabled = false;
            GetDocumentNo();
        }
        DrpDocumentType.Focus();
       // LoadSkuDetail();
    }

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            CreatTable();
            Session.Add("PurchaseSKUS", _purchaseSkus);
            LoadGird();
            ClearAll();
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
        }
        else
        {
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            DrpDocumentType.Enabled = false;
            LoadDocumentDetail();
            LoadSkuDetail();
        }
        drpDocumentNo.Focus();
    }


    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
        drpDistributor.Focus();
    }

    #endregion

    #region Grid Operations

    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _rowNo = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
       
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        _privouseQty = decimal.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);
        
        txtPrice.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        
        ddlSkus.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update";

    }
    
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
        if (_purchaseSkus.Rows.Count > 0)
        {
            _purchaseSkus.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKUS", _purchaseSkus);
            LoadGird();
        }
    }

    #endregion

    private bool CheckDublicateSku()
    {
        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
        DataRow[] foundRows = _purchaseSkus.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }

    #region Click OPerations

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (decimal.Parse(_dc.chkNull_0(txtQuantity.Text)) > 0)
            {
                DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
                DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");

                if (foundRows.Length > 0)
                {
                    _purchaseSkus = (DataTable)Session["PurchaseSKUS"];

                    decimal currentStock = CheckStockStatus(int.Parse(_dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));

                    if (btnSave.Text == "Add")
                    {
                        if (CheckDublicateSku())
                        {
                            if (currentStock == -1)
                            {
                                DataRow dr = _purchaseSkus.NewRow();
                                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                                dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));

                                if (DrpDocumentType.SelectedValue == "2")
                                {
                                    dr["PRICE"] = decimal.Parse(_dc.chkNull_0(txtPrice.Text));
                                    dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(txtPrice.Text)) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                                }
                                else
                                {
                                    dr["PRICE"] = 0;// foundRows[0]["DISTRIBUTOR_PRICE"];
                                    dr["AMOUNT"] = 0;// decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                                }


                                _purchaseSkus.Rows.Add(dr);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.SelectedItem.Text + " Current closing Stock is " + currentStock + "');", true);

                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.SelectedItem.Text + " Already Exists ');", true);

                            return;
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        if (currentStock == -1)
                        {
                            DataRow dr = _purchaseSkus.Rows[_rowNo];
                            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                            dr["Quantity"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));

                            if (DrpDocumentType.SelectedValue == "2")
                            {
                                dr["PRICE"] = decimal.Parse(_dc.chkNull_0(txtPrice.Text));
                                dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(txtPrice.Text)) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                            }
                            else
                            {
                                dr["PRICE"] = 0;//foundRows[0]["DISTRIBUTOR_PRICE"];
                                dr["AMOUNT"] = 0;//decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSkus.SelectedItem.Text + "Current closing Stock is " + currentStock.ToString() + "');", true);

                            return;
                        }
                    }
                    Session.Add("PurchaseSKUS", _purchaseSkus);
                    ClearAll();
                    LoadGird();
                    DisAbaleOption(true);
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Item Select');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Quantity');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
        }
    }

    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
        if (DrpDocumentType.SelectedIndex == 2 || DrpDocumentType.SelectedIndex == 3)
        {
            if (drpDistributor.SelectedValue.ToString() == DrpTransferFor.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transfer to Location must be different ');", true);
                return;
            }
        }
        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
        if (_purchaseSkus.Rows.Count > 0)
        {
            var mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    ShowReport();
                    _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
                    _purchaseSkus.Rows.Clear();
                    Session.Add("PurchaseSKUS", _purchaseSkus);

                    LoadGird();
                    GetDocumentNo();
                    drpDistributor.Enabled = true;
                    drpPrincipal.Enabled = true;
                    DrpDocumentType.Enabled = true;
                    ClearAll();
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";
                    txtDocumentNo.Text = "";
                    txtTotalQuantity.Text = "";
                    DisAbaleOption(false);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one Item must enter');", true);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        CreatTable();
        LoadGird();
        ClearAll();
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = ""; 
    }
    
    #endregion

    private bool CalculatePurchase(DateTime mWorkDate)
    {
        var dtPurchaseDetail = (DataTable)Session["PurchaseSKUS"];
        
        decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
       
        bool mResult = false;

        if (DrpDocumentType.SelectedIndex == 0)
        {
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                 mResult = _mPurchaseCtrl.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue.ToString()), mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
            else
            {
                 mResult = _mPurchaseCtrl.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue.ToString()), mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
            //dtPurchaseDetail

        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                mResult = _mPurchaseCtrl.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));

            }
            else
            {
                mResult = _mPurchaseCtrl.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
            }
           
            return mResult;
        }
        else if (DrpDocumentType.SelectedIndex == 2)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                 mResult = _mPurchaseCtrl.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
            else
            {
                 mResult = _mPurchaseCtrl.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
        }
        else if (DrpDocumentType.SelectedIndex == 3)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                 mResult = _mPurchaseCtrl.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
            else
            {
                 mResult = _mPurchaseCtrl.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
        }
        else
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                 mResult = _mPurchaseCtrl.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
            else
            {
                 mResult = _mPurchaseCtrl.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , mWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                return mResult;
            }
        }
    }

    private decimal CheckStockStatus(int skuId)
    {
        if (DrpDocumentType.SelectedIndex == 0 || DrpDocumentType.SelectedIndex == 3)
        {
            return -1;
        }
        else
        {
            var mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock2(int.Parse(drpDistributor.SelectedValue.ToString()), skuId, "N/A", DateTime.Parse(Session["CurrentWorkDate"].ToString()),15);
            if (dt.Rows.Count > 0)
            {
                if (decimal.Parse(dt.Rows[0][0].ToString()) + _privouseQty  >= decimal.Parse(txtQuantity.Text))
                {
                    return -1;
                }
                else
                {
                    return decimal.Parse(dt.Rows[0][0].ToString()) + _privouseQty ;
                }
            }
        }

        return 0;
    }

    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
        else
        {
            DrpDocumentType.Enabled = true;
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }

    private void ClearAll()
    {
       
        txtQuantity.Text = "";
        ddlSkus.Enabled = true;
        txtPrice.Text = "";
        txtAmount.Text = "0";
        btnSave.Text = "Add";
        _privouseQty = 0;
        
      
    }

    private void ShowReport()
    {
        try
        {
            LatestDataSet ds = new LatestDataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("BarCode", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("From", typeof(string));
            dt.Columns.Add("To", typeof(string));
            dt.Columns.Add("DriverName", typeof(string));
            dt.Columns.Add("TransferDate", typeof(DateTime));
            int i = 0;
            DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKUS"];
            foreach (DataRow dr in dtPurchaseDetail.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["BarCode"] = dr["SKU_Code"];
                dt.Rows[i]["Description"] = dr["SKU_Name"];
                dt.Rows[i]["QTY"] = Convert.ToDecimal(dr["Quantity"]);
                dt.Rows[i]["From"] = DrpTransferFor.SelectedItem.Text;
                dt.Rows[i]["To"] = drpDistributor.SelectedItem.Text;
                dt.Rows[i]["DriverName"] = txtDocumentNo.Text;
                dt.Rows[i]["TransferDate"] = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
                i += 1;
            }
            foreach (DataRow dr in dt.Rows)
            {
                ds.Tables["StockRegisterTransferOut"].ImportRow(dr);
            }
            var crpReport = new CRPStockRegisterTransferOut();
            crpReport.SetDataSource(ds);
            crpReport.Refresh();





            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow1", script);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    
}