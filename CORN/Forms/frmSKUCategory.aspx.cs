using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUCategory : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
    static int CategoryId;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategroy();
        }
    }
    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCategoryPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCategroy();
    }
        
    private void LoadCategroy()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdCategory.DataSource = dt;
        GrdCategory.Columns[0].Visible = true;
        GrdCategory.DataBind();
        GrdCategory.Columns[0].Visible = false;
    }

    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ddCategoryDivision.SelectedValue = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        CategoryId = int.Parse(GrdCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
        txtCategoryName.Enabled = true;
        btnSaveCategory.Text = "Update";
    }

    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CategoryId = int.Parse(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, CategoryId);
        if (dt.Rows.Count > 0)
        {
            //lblErrorMsgCategory.Text = "Wrong Command: first delete associated Brand";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadCategroy();
        }
    }

    /// <summary>
    /// Sets PageIndex Of Category Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCategory.PageIndex = e.NewPageIndex;
        LoadCategroy();
    }

    /// <summary>
    /// Save Or Updates a Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        //lblErrorMsgCategory.Text = "";
        if (btnSaveCategory.Text == "New")
        {
            txtCategoryCode.Text = this.GetAutoCode("CA", 0);
            txtCategoryName.Enabled = true;
            txtCategoryName.Focus();
            btnSaveCategory.Text = "Save";
        }
        else if (btnSaveCategory.Text == "Save")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                //lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            mController.InsertHierarchy(Constants.SKUCategory, Convert.ToInt32(ddlType.SelectedValue), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
            //lblErrorMsgCategory.Text = "";

        }
        else if (btnSaveCategory.Text == "Update")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                //lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Convert.ToInt32(ddlType.SelectedValue), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
        }
    }
    
    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CodeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string PreeFix,int CodeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, Constants.LongNullValue);
    }
    
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CValue">Value</param>
    private void SetAutoCode(string PreeFix, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(PreeFix, 1, CValue);
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategroy();
    }
}
