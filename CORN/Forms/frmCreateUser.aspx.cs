using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

/// <summary>
/// Form to Add, Edit Users
/// </summary>
public partial class Forms_frmCreateUser : System.Web.UI.Page
{
    readonly RoleManagementController mController = new RoleManagementController();
    readonly UserController UController = new UserController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDISTRIBUTOR();
            LoadUser();
            LoadRole();
            LoadGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");

        }

      

    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        ddDistributorId.DataSource = dtDistributor;

        ddDistributorId.DataTextField = "DISTRIBUTOR_NAME";
        ddDistributorId.DataValueField = "DISTRIBUTOR_ID";
        ddDistributorId.DataBind();
    }

    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        if (ddDistributorId.Items.Count > 0)
        {
            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(Constants.IntNullValue, int.Parse(ddDistributorId.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpUser, dt, 0, 6, true);
        }
    }

    /// <summary>
    /// Loads Roles To Role Combo
    /// </summary>
    protected void LoadRole()
    {
        DataTable dt = mController.SelectRoleMaster(Constants.IntNullValue, null, Constants.DateNullValue, Constants.DateNullValue, true);
        ddRole.DataSource = dt;
        ddRole.DataTextField = "ROLE_NAME";
        ddRole.DataValueField = "ROLE_ID";
        ddRole.DataBind();
    }

    /// <summary>
    /// Loads Users To User Grid
    /// </summary>
    protected void LoadGrid()
    {
        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = UController.SelectSlashUser(null, null);
            switch (ddSearchType.SelectedIndex)
            {

                case 1:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 3:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dt.DefaultView.RowFilter = "USER_NAME" + " like '%" + "" + "%'";
                    break;
            }
            Grid_users.DataSource = dt;
            Grid_users.DataBind();
        }
    }

    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
    }

    /// <summary>
    /// Sets User Data For Edit. This Function Runs When An Existing User Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow gvr = Grid_users.Rows[e.NewEditIndex];
            ddDistributorId.SelectedValue = gvr.Cells[1].Text;

            LoadUser();
            DrpUser.SelectedValue = gvr.Cells[0].Text;
            txtLoginId.Text = gvr.Cells[4].Text;
            if (System.Configuration.ConfigurationManager.AppSettings["Deployed"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Deployed"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
                {
                    txtpassword.Attributes.Add("value", Cryptography.Decrypt(gvr.Cells[5].Text, "b0tin@74"));
                }
            }
            else
            {
                txtpassword.Attributes.Add("value", gvr.Cells[5].Text);
            }
            chkIsActive.Checked = bool.Parse(gvr.Cells[7].Text);
            ddRole.SelectedValue = gvr.Cells[8].Text;

            chkright.Checked = bool.Parse(gvr.Cells[9].Text);
            chkright2.Checked = bool.Parse(gvr.Cells[10].Text);
            btnSave.Text = "Update";


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    

    private string CheckDeplyed()
    {
        if (System.Configuration.ConfigurationManager.AppSettings["Deployed"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Deployed"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
            {

                txtpassword.Attributes["type"] = "text";
            }
            return Cryptography.Encrypt(txtpassword.Text, "b0tin@74");
        }
        else
        {
            return txtpassword.Text;
        }
    }

    /// <summary>
    /// Saves Or Updates A User.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        txtpassword.Text = CheckDeplyed();
        try
        {
            if (btnSave.Text == "Save")
            {
                DataTable dt = UController.SelectSlashUser(txtLoginId.Text, txtpassword.Text);
                if (dt.Rows.Count > 0)
                {
                    lblErrorMsg.Text = "LoginId Already Exist";
                    return;
                }

                UController.InsertSlashUser(int.Parse(DrpUser.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()),
                    int.Parse(ddDistributorId.SelectedValue.ToString()), txtLoginId.Text, txtpassword.Text,
                    int.Parse(ddRole.SelectedValue.ToString()), chkright.Checked, chkright2.Checked);

            }
            else if (btnSave.Text == "Update")
            {

                UController.UpdateUser(int.Parse(DrpUser.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()),
                    txtLoginId.Text, txtpassword.Text, int.Parse(ddRole.SelectedValue.ToString()), chkIsActive.Checked,
                    int.Parse(ddDistributorId.SelectedValue.ToString()), chkright.Checked, chkright2.Checked);


            }
            LoadGrid();
            ClearControls();

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('"+ ex.Message.ToString() +"')", true);
        }
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void ClearControls()
    {
        try
        {
            txtLoginId.Text = "";

            txtpassword.Attributes.Add("value", "");

            txtpassword.Attributes["type"] = "password";

            lblErrorMsg.Text = "";
            btnSave.Text = "Save";
            chkright.Checked = false;
            chkright2.Checked = false;

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
