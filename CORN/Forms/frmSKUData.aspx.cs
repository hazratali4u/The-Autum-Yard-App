using System;
using System.Data;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{
    readonly SkuController _mSkuController = new SkuController();
    readonly DataControl dc = new DataControl();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
    readonly GeoHierarchyController DptTpe = new GeoHierarchyController();
    private  DataTable _mDt,_mSkuDt;


    /// <summary>
    /// Division Session used for item Type 
    /// </summary>
    /// <param name="sender">CompanyTable</param>
    /// <param name="e">IS_CURRENT COLUMN </param>


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            Populate_drpSKUCategory();
            LoadData();
            LoadGrid();
            FillddlUnitOfMeasure();
            Populate_ddlSection();
            Maxid();
            if (Session["Division"] != null)
            {
                if (Session["Division"].ToString() == "0")
                {
                    trItemType.Visible = true;
                }
            }
        }
    }


    public void Maxid()
    {
        DataTable dt = _mSkuController.GetMaxSku_ID();
        string maxid = dt.Rows[0][0].ToString();
        
        for (int i=maxid.Length; i<7; i++)
        {
            maxid = 0 + maxid;
        }
        maxid = "SKU" + maxid;

        txtskucode.Text = maxid;
    }
    /// <summary>
    /// Loads Divisions To Division Combo, Categories To Category Combo, Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    /// 
    /// ddlSection
    /// 
    /// 
    private void Populate_ddlSection()
    {
        _mDt = _mSkuController.SelectProductSection(Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(ddlSection, _mDt, "SECTION_ID", "SECTION_NAME", true);
    }
    private void Populate_drpSKUCategory()
    {
        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedValue), null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskucategory, _mDt, 0, 3, true);
    }

    /// <summary>
    /// Loads Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        LoadGrid();
    }
    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    
    private void LoadData()
    {
        if (ddskucategory.Items.Count > 0)
        {
            _mSkuDt = _mSkuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddskucategory.SelectedValue.ToString()), Convert.ToInt32(ddlType.SelectedValue), int.Parse(Session["CompanyId"].ToString()));
            Session.Add("m_SKUDt", _mSkuDt);
        }
    }

    private void LoadAllItems()
    {

        _mSkuDt = _mSkuController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));

        DataView dv = _mSkuDt.DefaultView;
        dv.Sort = "Category asc";
        DataTable sortedDt = dv.ToTable();

        grdSKUData.DataSource = sortedDt;
        grdSKUData.DataBind();
    }

    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        _mSkuDt = (DataTable)Session["m_SKUDt"];
        if (_mSkuDt != null)
        {
            switch (ddSearchType.SelectedIndex)
            {

                case 1:
                    _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                    break;
                case 3:
                    _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                    break;
                case 4:
                    _mSkuDt.DefaultView.RowFilter = ddSearchType.SelectedValue + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    _mSkuDt.DefaultView.RowFilter = "SKU_CODE" + " like '%" + "" + "%'";
                    break;
            }
            grdSKUData.DataSource = _mSkuDt.DefaultView;
            grdSKUData.DataBind();
        }
    }

   /// <summary>
   /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
   {
       Populate_drpSKUCategory();
        try
        {
            ddskucategory.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[2].Text;
            hfSkuId.Value = grdSKUData.Rows[e.NewEditIndex].Cells[4].Text;
            txtskucode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", "");
            txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
            txtunitincase.Text = grdSKUData.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", "");
            txtDescription.Text = grdSKUData.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");

            ddlUnitOfMeasure.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[12].Text;

            if (int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[15].Text) > 0)
            {
                ddlSection.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[15].Text;
            }
            drpProductType.SelectedValue= grdSKUData.Rows[e.NewEditIndex].Cells[1].Text;
            chkIsDesc.Checked = Convert.ToBoolean(grdSKUData.Rows[e.NewEditIndex].Cells[16].Text);
            chkIsInventory.Checked = Convert.ToBoolean(grdSKUData.Rows[e.NewEditIndex].Cells[17].Text);
        }
        catch (Exception)
        {

            throw;
        }

        btnSave.Text = "Update";
   }
   
    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {

       //bool IsExemted = bool.Parse(grdSKUData.Rows[e.RowIndex].Cells[14].Text);
       string result = _mSkuController.UpdateSKUS(false,true, false, Constants.CharNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
       Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.ShortNullValue, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[4].Text), null, null, null, null, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()),"",0);
       LoadData();
       LoadGrid();
   }
  

   /// <summary>
   /// Save Or Updates an SKU
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
       //const bool isExemted = true;
        //used as inventory
       
       
       if (txtskucode.Text.Length <= 0)
       {
          // lblErrorMsg.Text = "Must Enter SKU Code";
           return;
       }
       if (txtskuname.Text.Length <= 0)
       {
           //lblErrorMsg.Text = "Must Enter SKU Name";
           return;
       }

        //Used BrandID for UOM

       int sectionId = 0;
       if (ddlSection.Items.Count > 0)
       {
           sectionId = int.Parse(ddlSection.SelectedValue);
       }
       
        if(btnSave.Text == "Save") 
       {
           _mSkuController.InsertSKUS(chkIsDesc.Checked,chkIsInventory.Checked, true, char.Parse("E"), 1, Convert.ToInt32(drpProductType.SelectedValue), int.Parse(ddskucategory.SelectedValue.ToString()),
           Convert.ToInt32(ddlType.SelectedValue), 1, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text)),
           txtskucode.Text.Trim().ToUpper(), txtskuname.Text.Trim(), null, ddlUnitOfMeasure.SelectedValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtDescription.Text.Trim(), sectionId);
           CLearAll();  
       }
       else if (btnSave.Text == "Update")
       {
           _mSkuController.UpdateSKUS(chkIsDesc.Checked, chkIsInventory.Checked, true, char.Parse("E"), 1, Convert.ToInt32(drpProductType.SelectedValue),
           int.Parse(ddskucategory.SelectedValue.ToString()), Convert.ToInt32(ddlType.SelectedValue), 1, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text)), Convert.ToInt32(hfSkuId.Value), txtskucode.Text.Trim().ToUpper(), txtskuname.Text.Trim(), null, ddlUnitOfMeasure.SelectedValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtDescription.Text.Trim(), sectionId);
           CLearAll();
       }
       LoadData(); 
        
    }
  
    /// <summary>
    /// Filters SKU From SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        LoadAllItems();
       
    }
    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void CLearAll()
    {
        txtskucode.Text = "";
        txtunitincase.Text = "";
        txtskuname.Text = "";
        txtDescription.Text = "";
        btnSave.Text = "Save";

        chkIsInventory.Checked = false;

        LoadData();
        LoadGrid();
        FillddlUnitOfMeasure();
        Populate_ddlSection();
        Maxid();
    }
    private void FillddlUnitOfMeasure()
    {

        DataTable dt = DptTpe.GetUOM(Constants.IntNullValue, int.Parse(ddlType.SelectedValue));
        clsWebFormUtil.FillDropDownList(ddlUnitOfMeasure, dt, 0, 2, true);
        //ddlUnitOfMeasure
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "2")
        {
            chkIsInventory.Checked = false;
            chkIsInventory.Visible = false;

            chkIsDesc.Checked = false;
            chkIsDesc.Visible = false;
        }
        else
        {
            chkIsInventory.Checked = false;
            chkIsInventory.Visible = true;

            chkIsDesc.Checked = false;
            chkIsDesc.Visible = true;
        }
            Populate_drpSKUCategory();

        FillddlUnitOfMeasure();
        LoadData();
        LoadGrid();
    }
}
