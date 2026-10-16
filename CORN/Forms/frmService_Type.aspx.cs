using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmService_Type : System.Web.UI.Page
{
    readonly GeoHierarchyController GCtl = new GeoHierarchyController();
    DataTable dt;
    PagedDataSource pageResult;
    int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dt = GCtl.GetSERVICE_TYPE(Constants.IntNullValue);
            this.Session.Add("dt", dt);
            FillRepeater();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    public void Clear()
    {
        txtCodeNo.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtService.Text = string.Empty;
        btnSave.Text = "Save";
        txtCodeNo.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string StCode = txtCodeNo.Text;
        string StDescription = txtDescription.Text;
        string StServices = txtService.Text;


        if (btnSave.Text == "Save")
        {
            GCtl.InsertSERVICE_TYPE(StCode, StDescription, int.Parse(StServices), System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()), true);
            FillRepeater();
        }
        else if (btnSave.Text == "Update")
        {
            int Stid = Convert.ToInt32(hf_tbldefID.Value);
            GCtl.UpdateSERVICE_TYPE(Stid, StCode, StDescription, int.Parse(StServices), System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()));


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
        dt = GCtl.GetSERVICE_TYPE(Constants.IntNullValue);
        // dt = (DataTable)Session["dt"];

        if (dt.Rows.Count > 0)
        {
            pageResult = new PagedDataSource();
            DataView dv = new DataView(dt);
            pageResult.DataSource = dv;
            pageResult.AllowPaging = true;
            pageResult.PageSize = 6;
            count = pageResult.PageCount;

            pageResult.CurrentPageIndex = CurrentPage;
            rServiceDefination.DataSource = pageResult;
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
          

            rServiceDefination.DataSource = pageResult;
            rServiceDefination.DataBind();

            
        }
        return count;
    }
    public int CurrentPage
    {
        get
        {
            object objview = this.ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }
    protected void rServiceDefination_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = new DataTable();
            dt = GCtl.GetSERVICE_TYPE(Convert.ToInt32(e.CommandArgument));
            if (dt.Rows.Count > 0)
            {
                hf_tbldefID.Value = dt.Rows[0]["ST_ID"].ToString();
                txtCodeNo.Text = dt.Rows[0]["ST_CODE"].ToString();
                txtDescription.Text = dt.Rows[0]["ST_DESCRIPTION"].ToString();
                txtService.Text = dt.Rows[0]["ST_SERVICE"].ToString();
                // hf_ptISACTIVE.Value = dt.Rows[0]["IS_ACTIVE"].ToString();

                Session.Add("ST_ID", Convert.ToInt32(e.CommandArgument));

                btnSave.Text = "Update";
            }
        }
        else if (e.CommandName == "delete")
        {
            DataTable dt = new DataTable();
            dt = GCtl.GetSERVICE_TYPE(Convert.ToInt32(e.CommandArgument));
            if (dt.Rows.Count > 0)
            {
                hf_tbldefID.Value = dt.Rows[0]["ST_ID"].ToString();
                this.Session.Add("ST_ID", Convert.ToInt32(e.CommandArgument));

                int Stid = Convert.ToInt32(hf_tbldefID.Value);
                GCtl.DeleteSERVICE_TYPE(Stid, false);
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