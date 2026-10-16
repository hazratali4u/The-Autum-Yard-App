using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmTableDefination : System.Web.UI.Page
{
    readonly GeoHierarchyController GCtl = new GeoHierarchyController();
    DataTable dt;
    int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
         //   dt = GCtl.GetTableDefination(Constants.IntNullValue,int.Parse(ddlDistributor.SelectedValue));
           // Session.Add("dt", dt);
            FillRepeater();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlDistributor, dt, 0, 2, true);
    }
    public void Clear()
    {
        txtTableNo.Text = string.Empty;
        txtDescription.Text = string.Empty;

        ddlDistributor.Enabled = true;
        btnSave.Text = "Save";
        txtTableNo.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string tblNo = txtTableNo.Text;
        string tblDescription = txtDescription.Text;
        const string tblCapacity = "N/A";
        const string tblAbbrivation = "N/A";
        
        if (btnSave.Text == "Save")
        {
            GCtl.InsertTableDefination(int.Parse(ddlDistributor.SelectedValue),tblNo,tblDescription,tblCapacity,tblAbbrivation,System.DateTime.Now,int.Parse(this.Session["UserId"].ToString()));
            FillRepeater();
        }
        else if(btnSave.Text=="Update")
        {
            int Tblid = Convert.ToInt32(hf_tbldefID.Value);
            GCtl.UpdateTableDefination(Tblid, tblNo, tblDescription, tblCapacity, tblAbbrivation, System.DateTime.Now, int.Parse(this.Session["UserId"].ToString()));
        
          
        }
        Clear();
        FillRepeater();
    }
    private int FillRepeater()
    {
        dt = new DataTable();
        rTableDefination.DataSource = null;
        rTableDefination.DataBind();
        lblTotalNoOfRecords.Text = "";
        dt = GCtl.GetTableDefination(Constants.IntNullValue, int.Parse(ddlDistributor.SelectedValue));
        // dt = (DataTable)Session["dt"];

        if (dt.Rows.Count > 0)
        {
            rTableDefination.DataSource = dt;
            rTableDefination.DataBind();
          
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblTotalNoOfRecords.Text = Convert.ToString(rTableDefination.Items.Count);
            //lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["TotalRows"]) / Convert.ToDecimal(20)));

           
        }
        else if (dt.Rows.Count == 0)
        {
          
            lblTotalNoOfRecords.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
        else
        {
            rTableDefination.DataSource = dt; 
            rTableDefination.DataBind();

           
        }
        return count;
    }
   
protected void rTableDefination_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = new DataTable();
            dt = GCtl.GetTableDefination(Convert.ToInt32(e.CommandArgument), int.Parse(ddlDistributor.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                hf_tbldefID.Value = dt.Rows[0]["TableDefination_ID"].ToString();
                txtTableNo.Text = dt.Rows[0]["TableDefination_No"].ToString();
                txtDescription.Text = dt.Rows[0]["TableDefination_DESCRIPTION"].ToString();
                ddlDistributor.SelectedValue = dt.Rows[0]["distributorId"].ToString();
                // this.Session.Add("TableDefination_ID", Convert.ToInt32(e.CommandArgument));
                btnSave.Text = "Update";
                ddlDistributor.Enabled = false;
            }
        }
        else if (e.CommandName == "delete")
        {

            GCtl.DeleteTableDefination(Convert.ToInt32(e.CommandArgument), false);
            FillRepeater();
            Clear();
        }
    }
protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
{
    FillRepeater();
}
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}