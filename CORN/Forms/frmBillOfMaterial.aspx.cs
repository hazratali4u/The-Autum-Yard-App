using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmBillOfMaterial : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    SkuController SKUCtl = new SkuController();
    private static int RowId;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSKUFinished();
            LoadSKU();
            CreateSKUDataTable();
            GetFinishedDetail();
        }
    }

    private void LoadSKUFinished()
    {
        
        DataTable Dtsku_Price = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 1, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSKUFinished, Dtsku_Price, 0, 2, true);
    }

    private void LoadSKU()
    {        
       DataTable Dtsku_Price = SKUCtl.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 2, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSKU, Dtsku_Price, 0, 2, true);

        Session.Add("Dtsku_Price", Dtsku_Price);
    }

  
    protected void gvSKU_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlSKU.SelectedValue = gvSKU.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = gvSKU.Rows[e.NewEditIndex].Cells[2].Text;
        btnAdd.Text = "Update";
    }

   
    protected void gvSKU_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtSKU = (DataTable)this.Session["dtSKU"];
        dtSKU.Rows.RemoveAt(e.RowIndex);

        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }

  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        if (txtQuantity.Text.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtSKU.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedValue);
                    dr["QUANTITY"] = decimal.Parse(txtQuantity.Text);
                    dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                    dtSKU.Rows.Add(dr);
                    txtQuantity.Text = string.Empty;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
                }                
            }
            else
            {
                DataRow dr = dtSKU.Rows[RowId];
                dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedValue);
                dr["QUANTITY"] = decimal.Parse(txtQuantity.Text);
                dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;

                txtQuantity.Text = string.Empty;
                btnAdd.Text = "Add";
                RowId = 0;
            }

            ScriptManager.GetCurrent(Page).SetFocus(ddlSKU);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Enter Quantity');", true);            
            return;
        }

        LoadGrid();
    }

    private void CreateSKUDataTable()
    {
        DataTable dtSKU = new DataTable();
        dtSKU.Columns.Add("SKU_ID", typeof(int));
        dtSKU.Columns.Add("QUANTITY", typeof(decimal));
        dtSKU.Columns.Add("SKU_NAME",typeof(string));
        Session.Add("dtSKU", dtSKU);
    }
    private bool CheckDublicateSKU()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        DataRow[] foundRows = dtSKU.Select("SKU_ID  = '" + ddlSKU.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        if (dtSKU.Rows.Count > 0)
        {
            if (SKUCtl.InsertFinishedSKU(Convert.ToInt32(ddlSKUFinished.SelectedValue), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), Convert.ToInt32(Session["UserID"]), dtSKU))
            {
                this.CreateSKUDataTable();
                this.GetFinishedDetail();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Bill of material added succesfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No SKU found.')", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.CreateSKUDataTable();
        this.GetFinishedDetail();
    }

    private void LoadGrid()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }

    private void GetFinishedDetail()
    {
        if (ddlSKUFinished.Items.Count > 0)
        {
            DataTable dtSKU = SKUCtl.GetFinshedDetail(Convert.ToInt32(ddlSKUFinished.SelectedValue), Constants.IntNullValue);
            
                Session.Add("dtSKU", dtSKU);
            
            LoadGrid();

        }
    }

    protected void ddlSKUFinished_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CreateSKUDataTable();
        this.GetFinishedDetail();
    }
}