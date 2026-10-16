using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;    

public partial class Forms_frmDistributor : System.Web.UI.Page
{
    readonly DistributorController mController = new DistributorController();
    private static int DistributorId;
    private static int CompanyId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDistributorType();
            LoadGird();
            LoadCompany();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    private void LoadCompany()
    {
        CompanyController mCompany = new CompanyController();
        DataTable dt = mCompany.SelectCompany(Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(DrpCompanyName, dt, 0, 1, true);
    }

    private void GetDistributorType()
    {
        DataTable dt = mController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddDistributorType, dt, 0, 2);
    }

    private void LoadGird()
    {
        if (ddDistributorType.Items.Count > 0)
        {
            DataTable dtDistributor = mController.SelectAllDistributors(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 3:
                    dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 4:
                    dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 5:
                    dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dtDistributor.DefaultView.RowFilter = "Distributor_name" + " like '%" + "" + "%'";
                    break;
            }

            GridDistributor.DataSource = dtDistributor;
           
            GridDistributor.DataBind();
            
        }

    }

    protected void cbRegistered_CheckedChanged(object sender, EventArgs e)
    {
        if (cbRegistered.Checked == true)
        {
            txtgstno.Enabled = true;
            txtgstno.Focus();
        }
        else
        {
            txtgstno.Text = "";
            txtgstno.Enabled = false;
        }
    }
    protected void chkService_CheckedChanged(object sender, EventArgs e)
    {
        if (chkService.Checked)
        {
            txtServiceTax.Text = "0";
            
        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkService.Checked)
        {
            txtServiceTax.Text = "0";
        }

        int coverTable = 0;

        if (chkIsCoverTable.Checked)
        {
            coverTable = 1;
        }
        if (btnSave.Text == "Save")
        {
            mController.InsertDistributor(int.Parse(DrpCompanyName.SelectedValue.ToString()), coverTable, !chkIsActive.Checked, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue, Constants.IntNullValue
                , Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddDistributorType.SelectedValue.ToString()), txtcontactperson.Text.Trim(), txtPhoneNo.Text.Trim(), txtgstno.Text.Trim(),
                txtpassword.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), txtDistributorCode.Text.Trim(), txtDistributorName.Text.Trim(), txtFacebookAddress.Text.Trim(), cbRegistered.Checked, 1, int.Parse(this.Session["UserId"].ToString()),Convert.ToDecimal(txtServiceTax.Text.Trim()), chkService.Checked);

        }
        else
        {
            mController.UpdateDistributor(int.Parse(DrpCompanyName.SelectedValue.ToString()), coverTable, !chkIsActive.Checked, Constants.DateNullValue, System.DateTime.Now, Constants.IntNullValue, Constants.IntNullValue
            , Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddDistributorType.SelectedValue.ToString()), txtcontactperson.Text.Trim(), txtPhoneNo.Text.Trim(), txtgstno.Text.Trim(),
            txtpassword.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), DistributorId, txtDistributorCode.Text.Trim(), txtDistributorName.Text.Trim(), txtFacebookAddress.Text.Trim(), cbRegistered.Checked, 1, int.Parse(this.Session["UserId"].ToString()),Convert.ToDecimal(txtServiceTax.Text.Trim()), chkService.Checked);
        }
        
        ClearAll();
        this.LoadGird();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGird();
    }

   
    protected void GridDistributor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DistributorId = int.Parse(GridDistributor.Rows[e.RowIndex].Cells[0].Text);
        CompanyId = int.Parse(GridDistributor.Rows[e.RowIndex].Cells[11].Text);
        bool IsRegister = bool.Parse(GridDistributor.Rows[e.RowIndex].Cells[1].Text);
        mController.UpdateDistributor(CompanyId, Constants.IntNullValue, true, Constants.DateNullValue, System.DateTime.Now, Constants.IntNullValue, Constants.IntNullValue
               , Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, null, null, null, null, DistributorId, null,
               null, null, true, 1, int.Parse(this.Session["UserId"].ToString()),Constants.DecimalNullValue, true);
        this.LoadGird();
    }

    protected void GridDistributor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DistributorId = int.Parse(GridDistributor.Rows[e.NewEditIndex].Cells[0].Text);
        DrpCompanyName.SelectedValue = GridDistributor.Rows[e.NewEditIndex].Cells[11].Text;
        cbRegistered.Checked = bool.Parse(GridDistributor.Rows[e.NewEditIndex].Cells[1].Text);
        ddDistributorType.SelectedValue = GridDistributor.Rows[e.NewEditIndex].Cells[2].Text;
        txtAddress2.Text = GridDistributor.Rows[e.NewEditIndex].Cells[3].Text;
        txtDistributorCode.Text = GridDistributor.Rows[e.NewEditIndex].Cells[4].Text;
        txtDistributorName.Text = GridDistributor.Rows[e.NewEditIndex].Cells[5].Text;
        txtAddress1.Text = GridDistributor.Rows[e.NewEditIndex].Cells[7].Text;
        txtcontactperson.Text = GridDistributor.Rows[e.NewEditIndex].Cells[8].Text;
        txtPhoneNo.Text = GridDistributor.Rows[e.NewEditIndex].Cells[9].Text;
        if (GridDistributor.Rows[e.NewEditIndex].Cells[12].Text == "Active")
        {
            chkIsActive.Checked = true;
        }
        else
        {
            chkIsActive.Checked = false;
        }
        if (GridDistributor.Rows[e.NewEditIndex].Cells[10].Text == "&nbsp;")
        {
            txtgstno.Text = "";
            txtgstno.Enabled = false;
        }
        else
        {
            txtgstno.Text = GridDistributor.Rows[e.NewEditIndex].Cells[10].Text;
            txtgstno.Enabled = true;
        }
        txtServiceTax.Text=GridDistributor.Rows[e.NewEditIndex].Cells[14].Text;

        if (GridDistributor.Rows[e.NewEditIndex].Cells[15].Text == "1")
        {
            chkIsCoverTable.Checked = true;
        }
        else
        {
            chkIsCoverTable.Checked = false;
        }

        chkService.Checked = Convert.ToBoolean(GridDistributor.Rows[e.NewEditIndex].Cells[16].Text);
        txtFacebookAddress.Text= GridDistributor.Rows[e.NewEditIndex].Cells[17].Text.Replace("&nbsp;","");
        btnSave.Text = "Update";

    }    

    private void ClearAll()
    {
        txtDistributorName.Text = "";
        txtDistributorCode.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtcontactperson.Text = "";
        txtPhoneNo.Text = "";
        txtpassword.Text = "";
        txtgstno.Text = "";
        txtServiceTax.Text = "";
        btnSave.Text = "Save";
        cbRegistered.Checked = false;
        chkIsCoverTable.Checked = true;
        txtFacebookAddress.Text = "";
    }
}
