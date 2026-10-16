using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

public partial class Forms_frmRollBackForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadLegend();
        }
    }

    #region Load
    
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

    private void LoadLegend()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable m_dt = or.SelectLegend();
        clsWebFormUtil.FillDropDownList(this.DrpLenged, m_dt, 0, 2, true);
    }
    
    private void LoadRollbackDocument()
    {
        var or = new OrderEntryController();
        DataTable dtOrder = or.SelectRollBackDocument(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpDocumentType.SelectedValue), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();
    }
    
    private void LoadRollbackCheque()
    {
        ChequeEntryController CController = new ChequeEntryController();
        DataTable dt = CController.SelectChequeEntry(Constants.Cheque_Clear, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, 0);
        GrdCheque.DataSource = dt;
        GrdCheque.DataBind();
    }

    #endregion

    #region Click

    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedIndex == 3)
        {
            foreach (GridViewRow dr in GrdCheque.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                if (ChbInvoice.Checked == true)
                {
                    ChequeEntryController CController = new ChequeEntryController();
                    DataControl dc = new DataControl();
                    CController.RollbackChequeEntry(0, int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[6].Text, long.Parse(dr.Cells[0].Text), Constants.LongNullValue);
                }
            }
            LoadRollbackCheque();
        }
        else
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                var chbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                if (chbInvoice.Checked == true)
                {
                    var ord = new OrderEntryController();

                    ord.UpdateRollBackDocument(Convert.ToInt64(GrdOrder.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(DrpDocumentType.SelectedValue), int.Parse(DrpLenged.SelectedValue));
                }
            }
            LoadRollbackDocument();
        }
    }

    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedIndex == 3)
        {
            LoadRollbackCheque();
            GrdOrder.Visible = false;
            GrdCheque.Visible = true;
        }
        else
        {
            GrdOrder.Visible = true;
            GrdCheque.Visible = false;
            LoadRollbackDocument();

        }
    }
    
    #endregion

}
