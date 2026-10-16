using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Threading; 

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmSaleForce : System.Web.UI.Page
{
    Distributor_UserController UController = new Distributor_UserController();
    static int UserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDISTRIBUTOR();
            LoadDesignation();
            LoadDepartment();
            LoadGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");

            CORNCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtJoinDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtJoinDate.Attributes.Add("readonly", "readonly");
        }

        Response.Expires = 0;
        Response.Cache.SetNoStore();
    }

    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        ddDistributorId.DataSource = dtDistributor;
        ddDistributorId.DataTextField = "DISTRIBUTOR_NAME";
        ddDistributorId.DataValueField = "DISTRIBUTOR_ID";
        ddDistributorId.DataBind();
    }

    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.SaleForce, null, Constants.IntNullValue, true);
        ddDesignation.DataSource = m_dt;
        ddDesignation.DataTextField = "SLASH_DESC";
        ddDesignation.DataValueField = "REF_ID";
        ddDesignation.DataBind();
    }
    private void LoadDepartment()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, true);
        drpDepartment.DataSource = m_dt;
        drpDepartment.DataTextField = "SLASH_DESC";
        drpDepartment.DataValueField = "REF_ID";
        drpDepartment.DataBind();
    }
    
    protected void LoadGrid()
    {
        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = UController.SelectDistributorUser(Constants.IntNullValue, int.Parse(ddDistributorId.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
            Grid_users.DataSource = dt;
            Grid_users.DataBind();
        }
    }

    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow gvr = Grid_users.Rows[e.NewEditIndex];
            UserId  = int.Parse(gvr.Cells[0].Text);
            
           txtUserName.Text=gvr.Cells[2].Text.Replace("&nbsp;","");
           txtNICNo.Text=gvr.Cells[3].Text.Replace("&nbsp;","");
           txtPhoneNo.Text=gvr.Cells[4].Text.Replace("&nbsp;","");
           txtMobileNo.Text=gvr.Cells[5].Text.Replace("&nbsp;","");
           txtEmail.Text=gvr.Cells[6].Text.Replace("&nbsp;","");
           txtAddress1.Text=gvr.Cells[7].Text.Replace("&nbsp;","");
           txtAddress2.Text=gvr.Cells[8].Text.Replace("&nbsp;","");
           chkIsActive.Checked = bool.Parse(gvr.Cells[10].Text);
           
            ddDesignation.SelectedValue = gvr.Cells[11].Text;
           
            txtJoinDate.Text = gvr.Cells[13].Text.Replace("&nbsp;", "");
            if (gvr.Cells[15].Text != 0.ToString())
            {
                drpDepartment.SelectedValue = gvr.Cells[15].Text;
            } 
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            btnSave.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
                   
            ex.Message.ToString();
        }

    }
    
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        UserId =Convert.ToInt32(Grid_users.Rows[e.RowIndex].Cells[0].Text);

        UController.UpdateDistributor_User(UserId, int.Parse(Session["CompanyId"].ToString()), null, false, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
                   Constants.IntNullValue, Constants.IntNullValue, null, null, null, null, null
                   , null, null, null, null, Constants.IntNullValue);

        LoadGrid();

    }
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();

                string strcode = "";
                UserId = int.Parse(mAutoCode.GetAutoCustomerCode("EM", 0, Constants.LongNullValue).ToString());

                if (UserId.ToString().Length == 1)
                {
                    strcode = "EM" + "000" + UserId.ToString();
                }
                else if (UserId.ToString().Length == 2)
                {
                    strcode = "EM" + "00" + UserId.ToString();
                }
                else if (UserId.ToString().Length == 3)
                {
                    strcode = "EM" + "0" + UserId.ToString();
                }
                else
                {
                    strcode = "EM" + UserId.ToString();
                }

                string SaleForceId = UController.InsertDistributor_User(int.Parse(Session["CompanyId"].ToString()), txtNICNo.Text, true, Convert.ToDateTime(txtJoinDate.Text), System.DateTime.Now, int.Parse(ddDesignation.SelectedValue.ToString()),
                    int.Parse(ddDistributorId.SelectedValue.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtLoginId.Text, txtpassword.Text
                    , txtMobileNo.Text, strcode, txtUserName.Text, txtPhoneNo.Text, int.Parse(drpDepartment.SelectedValue));

                mAutoCode.GetAutoCustomerCode("EM", 1, long.Parse(UserId.ToString()));
            }
            else if (btnSave.Text == "Update")
            {
                UController.UpdateDistributor_User(UserId, int.Parse(Session["CompanyId"].ToString()), txtNICNo.Text, chkIsActive.Checked, Convert.ToDateTime(txtJoinDate.Text), System.DateTime.Now, int.Parse(ddDesignation.SelectedValue.ToString()),
                    int.Parse(ddDistributorId.SelectedValue.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtLoginId.Text, txtpassword.Text
                    , txtMobileNo.Text, null, txtUserName.Text, txtPhoneNo.Text, int.Parse(drpDepartment.SelectedValue));
            }
            LoadGrid();
            ClearControls();
            btnSave.Text = "Save";
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGrid();
        ClearControls();
        btnSave.Text = "Save";
        btnSave.Enabled = true;
    }

    protected void ClearControls()
    {
        try
        {
            txtUserName.Text = null;
            txtPhoneNo.Text = null;
            txtMobileNo.Text = null;
            txtAddress1.Text = null;
            txtAddress2.Text = null;
            txtEmail.Text = null;
            txtLoginId.Text = null;
            txtpassword.Text = null;
            txtNICNo.Text = null;
            lblErrorMsg.Text = null;
            txtJoinDate.Text = CORNCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
