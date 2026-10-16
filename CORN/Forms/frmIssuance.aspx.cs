using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNBusinessLayer.Reports;
using CORNCommon.Classes;
using System.Globalization;

public partial class Forms_frmIssuance : System.Web.UI.Page
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

            LoadDistributor();
            LoadSection();
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
       // _purchaseSkus.Columns.Add("UOM_ID", typeof(int));
        _purchaseSkus.Columns.Add("FREE_SKU", typeof(decimal));

        Session.Add("PurchaseSKUS", _purchaseSkus);

    }

    #region Load

    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
      

        DataTable dt = _mPurchaseCtrl.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue), Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }

    private void LoadSection()
    {
         SkuController _mSkuController = new SkuController();

        DataTable _mDt = _mSkuController.SelectProductSection(Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(drpSection, _mDt, "SECTION_ID", "SECTION_NAME", true);

      
    }

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    
    private void LoadDocumentDetail()
    {

        DataTable dt = _mPurchaseCtrl.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {

            drpSection.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();


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

            decimal totalValue = _purchaseSkus.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["FREE_SKU"].ToString()));
            txtTotalQuantity.Text = String.Format(CultureInfo.InvariantCulture,"{0:0.00}", totalValue);

        }
    }

    private void LoadSkuDetail()
    {
        if (drpSection.Items.Count > 0)
        {

            SkuController SKUCtl = new SkuController();
            DataTable dtskuPrice = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSkus, dtskuPrice, "SKU_ID", "SKU_DETAIL", true);

       
            Session.Add("Dtsku_Price", dtskuPrice);
        }
    }

    #endregion

    #region IndexChnage

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {

       GetDocumentNo();
       DrpDocumentType.Focus();
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            CreatTable();
            Session.Add("PurchaseSKUS", _purchaseSkus);
            LoadGird();
            ClearAll();
            drpSection.Enabled = true;
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
        }
        else
        {
            txtBuiltyNo.Text = "";

            drpSection.Enabled = false;
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
                                dr["FREE_SKU"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                              //  dr["UOM_ID"] = int.Parse(_dc.chkNull_0(txtPrice.Text));
                                
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
                            dr["FREE_SKU"] = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                           // dr["UOM_ID"] = int.Parse(_dc.chkNull_0(txtPrice.Text));

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
        Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];

        _purchaseSkus = (DataTable)Session["PurchaseSKUS"];

        if (_purchaseSkus.Rows.Count > 0)
        {
            var mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    ShowReport(Convert.ToInt64(hfId.Value));
                    _purchaseSkus = (DataTable)Session["PurchaseSKUS"];
                    _purchaseSkus.Rows.Clear();
                    Session.Add("PurchaseSKUS", _purchaseSkus);

                    LoadGird();
                    GetDocumentNo();
                    drpDistributor.Enabled = true;
                    drpSection.Enabled = true;
                    DrpDocumentType.Enabled = true;

                    ClearAll();
                    txtBuiltyNo.Text = "";

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

        txtBuiltyNo.Text = "";
    }

    #endregion

    private bool CalculatePurchase(DateTime mWorkDate)
    {
        var dtPurchaseDetail = (DataTable)Session["PurchaseSKUS"];

        bool mResult = false;

        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            long id = _mPurchaseCtrl.InsertIssuance(int.Parse(drpDistributor.SelectedValue.ToString()), "", int.Parse(DrpDocumentType.SelectedValue.ToString())
           , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(drpSection.SelectedValue.ToString()), 0, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpSection.SelectedValue.ToString()));

            if (id > 0)
            {
                mResult = true;
                hfId.Value = Convert.ToString(id);
            }
        }
        else
        {
            mResult = _mPurchaseCtrl.UpdateIssuance(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), "", int.Parse(DrpDocumentType.SelectedValue.ToString())
           , mWorkDate, int.Parse(drpDistributor.SelectedValue), int.Parse(drpSection.SelectedValue.ToString()), 0, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(Session["UserId"].ToString()), int.Parse(drpSection.SelectedValue.ToString()));

            hfId.Value = drpDocumentNo.SelectedValue.ToString();
        }
        return mResult;
    }
    private decimal CheckStockStatus(int skuId)
    {
        if (DrpDocumentType.SelectedValue == "14")
        {
            return -1;
        }
        else
        {
            var mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock2(int.Parse(drpDistributor.SelectedValue.ToString()), skuId, "N/A", DateTime.Parse(Session["CurrentWorkDate"].ToString()),16);
            if (dt.Rows.Count > 0)
            {
                if (decimal.Parse(dt.Rows[0][0].ToString()) + _privouseQty >= decimal.Parse(txtQuantity.Text))
                {
                    return -1;
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
            drpSection.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
        else
        {
            DrpDocumentType.Enabled = true;
            drpSection.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }

    private void ClearAll()
    {

        txtQuantity.Text = "";
        ddlSkus.Enabled = true;
        

        btnSave.Text = "Add";
        _privouseQty = 0;
    }

    private void ShowReport(long Id)
    {
        try
        {
            DocumentPrintController mController = new DocumentPrintController();
            RptInventoryController RptInventoryCtl = new RptInventoryController();
            CrpIssueDocument CrpReport = new CrpIssueDocument();
            DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            DataSet ds = RptInventoryCtl.SelectIssueDocument(int.Parse(drpDistributor.SelectedValue.ToString()), 
                int.Parse(drpSection.SelectedValue), Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpDocumentType.SelectedValue), Id);

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("DocumentType", "Purchase Document");
            CrpReport.SetParameterValue("Principal", drpSection.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}