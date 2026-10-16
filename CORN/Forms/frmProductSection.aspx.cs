using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmProductSection : System.Web.UI.Page
{
    GeoHierarchyController GCtl = new GeoHierarchyController();
    private SkuController _skuController = new SkuController();
    DataTable dt;
    PagedDataSource _pageResult;
    int _count;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dt = GCtl.GetSERVICE_TYPE(Constants.IntNullValue);
            Session.Add("dt", dt);
            FillRepeater();

            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    public void Clear()
    {
        txtCodeNo.Text = string.Empty;
        txtSectionName.Text = string.Empty;
        txtPrinterName.Text = string.Empty;
        btnSave.Text = "Save";
        txtCodeNo.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            _skuController.InsertProductSection(txtCodeNo.Text, txtSectionName.Text, txtPrinterName.Text, chkIsPrint.Checked);
        }
        else if (btnSave.Text == "Update")
        {
            int Stid = Convert.ToInt32(hf_tbldefID.Value);
            _skuController.UpdateProductSection(Stid, txtCodeNo.Text, txtSectionName.Text, txtPrinterName.Text, true, chkIsPrint.Checked);
        }
        Clear();
        FillRepeater();
    }
    private int FillRepeater()
    {
        dt = new DataTable();
        rServiceDefination.DataSource = null;
        rServiceDefination.DataBind();
        lblTotalNoOfRecords.Text = "";

        dt = _skuController.SelectProductSection(Constants.IntNullValue, null, null);
        // dt = (DataTable)Session["dt"];
        if (dt.Rows.Count > 0)
        {

            DataView dv = new DataView(dt);


            rServiceDefination.DataSource = dv;
            rServiceDefination.DataBind();


            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;

            lblTotalNoOfRecords.Text = Convert.ToString(rServiceDefination.Items.Count);
            //   lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["TotalRows"]) / Convert.ToDecimal(20)));


        }
        else if (dt.Rows.Count == 0)
        {


            lblTotalNoOfRecords.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
        else
        {


            rServiceDefination.DataSource = _pageResult;
            rServiceDefination.DataBind();


        }
        return _count;
    }

    protected void rServiceDefination_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = new DataTable();
            dt = _skuController.SelectProductSection(Convert.ToInt32(e.CommandArgument), null, null);
            if (dt.Rows.Count > 0)
            {
                hf_tbldefID.Value = dt.Rows[0]["SECTION_ID"].ToString();
                txtCodeNo.Text = dt.Rows[0]["SECTION_CODE"].ToString();
                txtSectionName.Text = dt.Rows[0]["SECTION_NAME"].ToString();
                txtPrinterName.Text = dt.Rows[0]["PRINTER_NAME"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IS_PRINT"]))
                {
                    chkIsPrint.Checked = true;
                }
                else
                {
                    chkIsPrint.Checked = false;
                }
                // hf_ptISACTIVE.Value = dt.Rows[0]["IS_ACTIVE"].ToString();

                this.Session.Add("SECTION_ID", Convert.ToInt32(e.CommandArgument));

                btnSave.Text = "Update";
            }
        }
        else if (e.CommandName == "delete")
        {
            DataTable dt = new DataTable();
            dt = _skuController.SelectProductSection(Convert.ToInt32(e.CommandArgument), null, null);
            if (dt.Rows.Count > 0)
            {
                hf_tbldefID.Value = dt.Rows[0]["SECTION_ID"].ToString();
                this.Session.Add("SECTION_ID", Convert.ToInt32(e.CommandArgument));

                int Stid = Convert.ToInt32(hf_tbldefID.Value);
                _skuController.UpdateProductSection(Stid, null, null, null, false, false);
                FillRepeater();
                Clear();
            }
        }
    }


    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}