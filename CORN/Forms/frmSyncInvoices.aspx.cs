using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;



public partial class Forms_frmSyncInvoices : System.Web.UI.Page
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

            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];

            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }


    }

    #region Load

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }


    private void LoadRollbackDocument()
    {
        var or = new OrderEntryController();
        DataTable dtOrder = or.SelectSyncDocument(int.Parse(drpDistributor.SelectedValue), 2, DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();

        GrdOrder2.DataSource = dtOrder;
        GrdOrder2.DataBind();

    }


    #endregion

    #region Click



    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        LoadRollbackDocument();
    }


    #region Sync to Rims
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        GrdOrder.Visible = true;
        GrdOrder2.Visible = false;
    }
    /// <summary>
    /// insert into rims invoice master, detail tables and sync to rims server 
    /// </summary>
    protected void btnSync_Click(object sender, EventArgs e)
    {
        OrderEntryController orCtrl = new OrderEntryController();

        using (var webClient = new WebClient())
        {
            var getKeyUrl = " http://rims.punjab.gov.pk/api/databaseupdate/formkey";
            var json = webClient.DownloadString(getKeyUrl);

            var results = JsonConvert.DeserializeObject<dynamic>(json);
            var k = results.Last.Value;
            lbl_key.Text = Convert.ToString(k);
        }
        if (lbl_key.Text != "")
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                CheckBox chInvoice = (CheckBox)dr.Cells[0].FindControl("ChbInvoice");
                if (chInvoice.Checked == true)
                {
                    try
                    {
                        string detail = GetPendingBill(Convert.ToInt64(dr.Cells[2].Text));

                        long ID = orCtrl.Add_RIMSInvoice(Convert.ToInt32(dr.Cells[7].Text), int.Parse(drpDistributor.SelectedValue)
                            , int.Parse(Session["UserID"].ToString()), Convert.ToInt64(dr.Cells[2].Text));

                        if (ID > 0)
                        {
                            if (dr.Cells[9].Text != "" || dr.Cells[10].Text != "")
                            {
                                string URI = " http://rims.punjab.gov.pk/api/databaseupdate/formdata";
                                //  string myParameters = "key=" + lbl_key.Text + "&data={\"pntn\":\"" + 25251490 + "\",\"branchcode\":\"" + dr.Cells[10].Text + "\",\"invoice_number\":\"" + ID + "\",\"invoice_date\": \"" + dr.Cells[3].Text + "\",\"invoice_time\": \"" + dr.Cells[4].Text + "\",\"discount_percent\": \"0\",\"service_charges_percent\": \"0\",\"tax_percent\":\"16\",\"table_no\": \"" + dr.Cells[7].Text + "\",\"phone\": \"042\",\"customer_name\":\"\",\"detail\":" + detail + "}";
                                string myParameters = "key=" + lbl_key.Text + "&data={\"pntn\":\"" + dr.Cells[9].Text + "\",\"branchcode\":\"" + dr.Cells[10].Text + "\",\"invoice_number\":\"" + ID + "\",\"invoice_date\": \"" + dr.Cells[3].Text + "\",\"invoice_time\": \"" + dr.Cells[4].Text + "\",\"tax_percent\":\"" + dr.Cells[8].Text + "\",\"table_no\": \"" + dr.Cells[7].Text + "\",\"detail\":" + detail + "}";

                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                    string HtmlResult = wc.UploadString(URI, myParameters);
                                }
                                orCtrl.Update_RIMSInvoice(int.Parse(drpDistributor.SelectedValue), true, ID);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please add Branch Code or GST No!');", true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionPublisher.PublishException(ex);

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred!');", true);

                    }
                }
            }
            LoadRollbackDocument();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please try again!');", true);
        }
        #endregion

        #endregion

    }
    public string GetPendingBill(long saleInvoiceMasterId)
    {
        var mSkuController = new SkuController();

        DataTable dtSkus = mSkuController.SpGetPendingBill(saleInvoiceMasterId, 2);
        return GetJson(dtSkus);
    }

    public static string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
}