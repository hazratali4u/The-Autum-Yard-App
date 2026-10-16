using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmUOM2 : System.Web.UI.Page
{
    readonly GeoHierarchyController DptTpe = new GeoHierarchyController();
     DataTable dt = new DataTable();
   
    int count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            FillRepeater();
            maxid();
            txtDescription.Focus();
            ScriptManager.GetCurrent(Page).SetFocus(txtDescription);
        }
    }
    public void maxid()
    {
        dt = DptTpe.GetMaxUOM_ID();
        string maxid = dt.Rows[0][0].ToString();
        txtCodeNo.Text = maxid;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        //string dptRemarks = txtRemarks.Text;
        //  int dptType = Convert.ToInt32(ddlType.SelectedValue);
        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime docDate = Convert.ToDateTime(Session["CurrentWorkDate"]);

        if (btnSave.Text == "Save")
        {
            DptTpe.InsertUOM(txtCodeNo.Text, txtDescription.Text, txtRemarks.Text,int.Parse(ddlType.SelectedValue), DateTime.Now, Convert.ToInt32(Session["USER_ID"]), true);
        }
        else if (btnSave.Text == "Update")
        {
            DptTpe.UpdateUOM(Convert.ToInt32(hf_RefID.Value), txtDescription.Text, txtRemarks.Text, int.Parse(ddlType.SelectedValue), DateTime.Now, Convert.ToInt32(Session["USER_ID"]), true);
        }

        Cleartxt();
        FillRepeater();
       
        txtDescription.Focus();
        ScriptManager.GetCurrent(Page).SetFocus(txtDescription);
        maxid();
    }
    private void Cleartxt()
    {
        txtCodeNo.Text = "";
        txtDescription.Text = "";
        txtRemarks.Text = "";
        //ddlType.SelectedIndex = 0;
        btnSave.Text = "Save";
    }

    
    private int FillRepeater()
    {
        rDepartment.DataSource = null;
        rDepartment.DataBind();

        lblTotalNoOfRecords.Text = "";

        dt = DptTpe.GetUOM(Constants.IntNullValue, int.Parse(ddlType.SelectedValue));

        if (dt.Rows.Count > 0)
        {
            rDepartment.DataSource = dt;
            rDepartment.DataBind();

           
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblTotalNoOfRecords.Text = Convert.ToString(rDepartment.Items.Count);
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
            rDepartment.DataSource = dt;
            rDepartment.DataBind();

            // return count;
        }
        return count;
    }
    
    protected void rDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = new DataTable();
            dt = DptTpe.GetUOM(Convert.ToInt32(e.CommandArgument), Constants.IntNullValue);
            if (dt.Rows.Count > 0)
            {
                hf_RefID.Value = dt.Rows[0]["UOM_ID"].ToString();
                txtCodeNo.Text = dt.Rows[0]["UOM_CODE"].ToString();
                txtRemarks.Text = dt.Rows[0]["UOM_REMARKS"].ToString();
                ddlType.SelectedValue = dt.Rows[0]["UOM_TYPE_ID"].ToString();
                txtDescription.Text = dt.Rows[0]["UOM_DESC"].ToString();
                this.Session.Add("UOM_ID", Convert.ToInt32(e.CommandArgument));
                btnSave.Text = "Update";
            }
        }
        else if (e.CommandName == "delete")
        {
            DataTable dt = new DataTable();
            dt = DptTpe.GetUOM(Convert.ToInt32(e.CommandArgument), Constants.IntNullValue);
            if (dt.Rows.Count > 0)
            {
                hf_RefID.Value = dt.Rows[0]["UOM_ID"].ToString();
                this.Session.Add("UOM_ID", Convert.ToInt32(e.CommandArgument));

                DptTpe.UpdateUOM(Convert.ToInt32(hf_RefID.Value), txtDescription.Text, txtRemarks.Text, int.Parse(ddlType.SelectedValue), DateTime.Now, Convert.ToInt32(Session["USER_ID"]), false);

               
                FillRepeater();
                Cleartxt();
                maxid();
            }
        }
    }
    
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRepeater();
    }
}