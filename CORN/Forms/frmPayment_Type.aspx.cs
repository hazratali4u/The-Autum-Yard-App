using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmPayment_Type : System.Web.UI.Page
{
    readonly GeoHierarchyController _gCtl = new GeoHierarchyController();
    DataTable _dt;
    PagedDataSource _pageResult;
    int _count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
       
            _dt = _gCtl.GetPAYMENT_TYPE(Constants.IntNullValue);

            Session.Add("dt", _dt);
            FillRepeater();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            Maxid();
        }
      
    }
    public void Maxid()
    {
        _dt = _gCtl.GetMaxPAYMENT_TYPE_ID();
        string maxid = _dt.Rows[0][0].ToString();
        txtCodeNo.Text = maxid;
    }
    public void Clear()
    {
        txtCodeNo.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        btnSave.Text = "Save";
        txtCodeNo.Focus();
        Maxid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ptCode = txtCodeNo.Text;
        string ptDescription = txtDescription.Text;
        string ptRemarks = txtRemarks.Text;
    

        if (btnSave.Text == "Save")
        {
            _gCtl.InsertPAYMENT_TYPE("N/A", ptDescription,ptRemarks, System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()),true);
            FillRepeater();
            Maxid();
        }
        else if (btnSave.Text == "Update")
        {
            int ptid = Convert.ToInt32(txtCodeNo.Text);
            _gCtl.UpdatePAYMENT_TYPE(ptid, "N/A", ptDescription, ptRemarks, System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()));
        }
        Clear();
        FillRepeater();
        Maxid();
        
    }
    private int FillRepeater()
    {
        _dt = new DataTable();
        rTableDefination.DataSource = null;
        rTableDefination.DataBind();
        lblTotalNoOfRecords.Text = "";
      
        _dt = _gCtl.GetPAYMENT_TYPE(Constants.IntNullValue);
        // dt = (DataTable)Session["dt"];

        if (_dt.Rows.Count > 0)
        {
            _pageResult = new PagedDataSource();
            DataView dv = new DataView(_dt);
            _pageResult.DataSource = dv;
            _pageResult.AllowPaging = true;
            _pageResult.PageSize = 6;
            _count = _pageResult.PageCount;

            _pageResult.CurrentPageIndex = CurrentPage;
            rTableDefination.DataSource = _pageResult;
            rTableDefination.DataBind();
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
           
            lblTotalNoOfRecords.Text = Convert.ToString(rTableDefination.Items.Count);
         //   lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["TotalRows"]) / Convert.ToDecimal(20)));

           
        }
        else if (_dt.Rows.Count == 0)
        {
           
            lblTotalNoOfRecords.Visible = false;
           
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
        else
        {
          

            rTableDefination.DataSource = _pageResult;
            rTableDefination.DataBind();

            // return count;
        }
        return _count;
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
    protected void rTableDefination_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = _gCtl.GetPAYMENT_TYPE(Convert.ToInt32(e.CommandArgument));
            if (dt.Rows.Count > 0)
            {
                txtCodeNo.Text = dt.Rows[0]["PT_ID"].ToString();
                //txtCodeNo.Text = dt.Rows[0]["PT_CODE"].ToString();
                txtDescription.Text = dt.Rows[0]["PT_DESCRIPTION"].ToString();
                txtRemarks.Text = dt.Rows[0]["PT_REMARKS"].ToString();
               // hf_ptISACTIVE.Value = dt.Rows[0]["IS_ACTIVE"].ToString();

                Session.Add("PT_ID", Convert.ToInt32(e.CommandArgument));

                btnSave.Text = "Update";
            }
        }
        else if (e.CommandName == "delete")
        {
            DataTable dt = _gCtl.GetPAYMENT_TYPE(Convert.ToInt32(e.CommandArgument));
            if (dt.Rows.Count > 0)
            {
                hf_tbldefID.Value = dt.Rows[0]["PT_ID"].ToString();
                Session.Add("PT_ID", Convert.ToInt32(e.CommandArgument));

                int ptid = Convert.ToInt32(hf_tbldefID.Value);
                _gCtl.DeletePAYMENT_TYPE(ptid,false);
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