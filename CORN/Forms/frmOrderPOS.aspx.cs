using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using Newtonsoft.Json;


using System.Drawing.Printing;
using System.Drawing;

namespace Forms
{
    public partial class FormsfrmOrderPos : System.Web.UI.Page
    {
        //DataTable PurchaseSKU;

        static DataTable dtValue;
        static DataTable dtSection;
        private PrintDocument prnDocument;

       
        private static string _section;
       
        #region Printing Variables
        // for Report:
        private static int CurrentY;
        private static int CurrentX;
        private static int leftMargin;
        private static int rightMargin;
        private static int topMargin;
        private static int bottomMargin;
        private static int InvoiceWidth;
        private static int InvoiceHeight;

        //private bool ReadInvoice;

        private static int XNoOfuntit;
        private static int YNoOfUnit;

        // for Invoice Head:
        private static string InvTitle;
        private static string InvSubTitle1;
        private static string InvSubTitle2;
        private static string InvSubTitle3;
        private static string InvSubTitle4;

        // Font and Color:------------------
        // Title Font
        private static Font InvTitleFont = new Font("sans-serif", 22, FontStyle.Regular);
        // Title Font height
        private static int InvTitleHeight;
        // SubTitle Font
        private static Font InvSubTitleFont = new Font("sans-serif", 12, FontStyle.Regular);
        private static Font InvOrderTitleFont = new Font("sans-serif", 14, FontStyle.Bold);
        // SubTitle Font height
        private static int InvSubTitleHeight;
        // Invoice Font
        private static Font InvoiceFont = new Font("sans-serif", 11, FontStyle.Regular);

        private static Font InvoiceFont2 = new Font("sans-serif", 9, FontStyle.Regular);
        
      
       
        // Invoice Font height
        private static int InvoiceFontHeight;
        // Blue Color
        private static SolidBrush BlueBrush = new SolidBrush(Color.Blue);
       
        // Black Color
        private static SolidBrush BlackBrush = new SolidBrush(Color.Black);

        #endregion

        private readonly GeoHierarchyController _gCtl = new GeoHierarchyController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                txtCustomerDOB.Attributes.Add("readonly", "readonly");

                Session["TableId"] = 0;

                hfCompanyName.Value = Session["COMPANY_NAME"].ToString();
                lblUserName.Text = Session["UserName"].ToString();
                DateTime date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
                lblDateTime.Text = date.ToString("dd-MMMM-yyyy");
                hfCurrentWorkDate.Value = date.ToString("dd-MMM-yyyy");


                DataSet ds = _gCtl.SelectDataForPosLoad(Constants.IntNullValue,
                    int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                    ,int.Parse(Session["RoleID"].ToString()));

                Session.Add("dtTables", ds.Tables[0]);

                FillTableList();

                if (ds.Tables[1].Rows.Count > 0)
                {

                    hfSalesTax.Value = ds.Tables[1].Rows[0]["GST"].ToString();
                    hfIsCoverTable.Value = ds.Tables[1].Rows[0]["ISCOVERTABLE"].ToString();
                    hfServiceCharges.Value = ds.Tables[1].Rows[0]["SERVICE_CHARGES"].ToString();
                    hfLocationName.Value = ds.Tables[1].Rows[0]["DISTRIBUTOR_NAME"].ToString();
                    hfAddress.Value = ds.Tables[1].Rows[0]["ADDRESS1"].ToString();
                    hfPhoneNo.Value = ds.Tables[1].Rows[0]["CONTACT_NUMBER"].ToString();
                    hfRegNo.Value = ds.Tables[1].Rows[0]["GST_NUMBER"].ToString();
                    hfFacebkId.Value = ds.Tables[1].Rows[0]["FACEBOOK"].ToString();

                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                }
                else
                {
                    Server.Transfer("Level.aspx?LevelID=27&LevelType=1");
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    hfCompanyEmail.Value = ds.Tables[2].Rows[0]["EMAIL_ADDRESS"].ToString();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    hfReport.Value=GetJson(ds.Tables[3]);
                    
                }
            }
        }
        public static string GetJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

           //serializer.MaxJsonLength = Int32.MaxValue;

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

        [WebMethod]
        [ScriptMethod]
        public static string LoadSaleForce(string customerType)
        {
            int customerTypeId = Constants.SALES_FORCE_ORDERBOOKER;

            if (customerType == "Delivery")
            {
                customerTypeId = Constants.SALES_FORCE_DELIVERYMAN;
            }
            else
            {
                customerTypeId = Constants.SALES_FORCE_ORDERBOOKER;
            }

            SaleForceController mDController = new SaleForceController();
            DataTable dt = mDController.SelectSaleForceAssignedArea(customerTypeId, int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString())
                , Constants.IntNullValue, int.Parse(HttpContext.Current.Session["companyId"].ToString()), Constants.IntNullValue);

            return GetJson(dt);
        }


        [WebMethod]
        
        public static string LoadMaxOrderNo()
        {
             GeoHierarchyController _gCtl = new GeoHierarchyController();
                string Id = _gCtl.SelectMaxOrderNo(
                    int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()));
           // JObject json = JObject.Parse(str);
            return Id;
        }

        #region Products

        [WebMethod]
        public static string LoadProductCategroy()
        {
            SkuHierarchyController mController = new SkuHierarchyController();
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, 1, null, null, true, 1);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> dRow = new Dictionary<string, object>
                {
                    {"CATEGORY_ID", row["SKU_HIE_ID"]},
                    {"CATEGORY_NAME", row["SKU_HIE_NAME"]}
                };
                rows.Add(dRow);
            }
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
                new System.Web.Script.Serialization.JavaScriptSerializer();
            return jSearializer.Serialize(rows);
        }

        [WebMethod]
        public static string LoadAllProducts()
        {
            var mSkuController = new SkuController();
            DataTable dtSkus = mSkuController.SelectSkusforOrder(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                    Constants.IntNullValue, 1, Constants.IntNullValue,
                    int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()));

            return GetJson(dtSkus);
        }

        #endregion

        #region Bills

        [WebMethod]
        [ScriptMethod]
        public static string SelectPendingBills(string customerType)
        {
            int customerTypeId = 1;

            if (customerType == "Dine In")
            {
                customerTypeId = 1;

            }
            else if (customerType == "Delivery")
            {
                customerTypeId = 2;
            }
            else {
                customerTypeId = 3;


            }
            var mSkuController = new OrderEntryController();
            DataTable dtSkus = mSkuController.SelectPendingBills(Constants.LongNullValue,
                DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), customerTypeId);
            return GetJson(dtSkus);
        }

        [WebMethod]
        public static string GetPendingBill(long saleInvoiceMasterId)
        {
            var mSkuController = new SkuController();
            DataTable dtSkus = mSkuController.SpGetPendingBill(saleInvoiceMasterId);

            //  HttpContext.Current.Session["CustomerType"] = dtSkus.Rows[0]["CUSTOMER_TYPE_ID"];
            HttpContext.Current.Session["TableId"] = dtSkus.Rows[0]["TABLE_ID"];

            return GetJson(dtSkus);
        }

        #endregion

        #region Customer


        [WebMethod]
        [ScriptMethod]
        public static string LoadAllCustomers(string customerName, string type)
        {

            var mController = new CustomerDataController();
            try
            {
                DataTable dtCustomers = mController.UspSelectCustomer(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()), type, customerName);

                return GetJson(dtCustomers);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [WebMethod]
        [ScriptMethod]
        public static void InsertCustomer(string cardID, string CNIC, string contactNumer, string contactNumer2, string customerName, string address, string DOB)
        {
            try
            {

                //  int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());

                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                //   if (1 > 0)
                //   {
                CustomerDataController.InsertCustomer(distributerId, cardID, CNIC, DOB, contactNumer, contactNumer2, customerName, address);
                //  }
                //   else
                //   {

                // }
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;
            }
        }

        #endregion

        #region DML

        [WebMethod]
        [ScriptMethod]
        public static void HoldOrder(string orderedProducts, string orderBooker, string coverTable, string customerType, string CustomerName
            , string maxOrderNo, string printType, string tableName, string bookerName)
        {
            try
            {
                dtValue = new DataTable();

                int tableId = 0;
                int customerTypeId = 0;
                Int64 customerId = 0;
                
                if (customerType == "Dine In")
                {
                    customerTypeId = 1;

                }
                else if (customerType == "Delivery")
                {
                    customerTypeId = 2;
                    customerId = Convert.ToInt64(CustomerName);
                }
                else {
                    customerTypeId = 3;

                }
                //1 for DINE IN for others set table zero

                if (customerTypeId == 1)
                {
                    tableId = int.Parse(HttpContext.Current.Session["TableId"].ToString());
                }

                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());
               
                    if (orderedProducts != "")
                    {
                        dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));
                    }
                
              
                #region Print
                if (printType == "1")
                {

                    HttpContext.Current.Session.Add("maxOrderNo", maxOrderNo);
                    HttpContext.Current.Session.Add("customerType", customerType);
                    HttpContext.Current.Session.Add("tableName", tableName);
                    HttpContext.Current.Session.Add("bookerName", bookerName);

                    if (dtValue.Rows.Count > 0)
                    {
                        dtSection = new DataTable();
                        dtSection.Columns.Add("SECTION_NAME", typeof(string));

                        foreach (DataRow dr in dtValue.Rows)
                        {
                            if (dr["IS_PRINT"].ToString() == "true")
                            {
                                decimal qty = Convert.ToDecimal(dr["QTY"].ToString()) - Convert.ToInt32(dr["PRINT_COUNT"].ToString());
                                string IS_VOID = dr["IS_VOID"].ToString();

                                if (IS_VOID == "false")
                                {
                                    if (qty > 0)
                                    {

                                        DataRow drSection = dtSection.NewRow();
                                        drSection["SECTION_NAME"] = dr["SECTION_NAME"];
                                        dtSection.Rows.Add(drSection);
                                    }
                                }
                            }
                        }
                        DataView view = new DataView(dtSection);
                        dtSection = view.ToTable(true, "SECTION_NAME");
                    }


                    if (dtSection.Rows.Count > 0)
                    {
                        _section = null;

                        foreach (DataRow dr in dtSection.Rows)
                        {
                            _section = dr.ItemArray[0].ToString();

                            PrintReport();
                        }

                    }


                }
                #endregion
               
                
                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString().Trim() != "N/A")
                {
                    OrderEntryController.HoldOrder(
                        long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()), Constants.IntNullValue,
                        customerTypeId, tableId, 0, 0, 0, 0, 0, true, false, userId, currentWorkDate, distributerId, 0, null,
                        dtValue, int.Parse(orderBooker), coverTable);

                }
                else
                {
                    OrderEntryController.Add_Invoice(Constants.IntNullValue, customerTypeId, tableId, 0, 0, 0, true,
                       userId, currentWorkDate, distributerId, dtValue, int.Parse(orderBooker), coverTable, customerId, maxOrderNo);
                    
                }
               

            }
            catch (FormatException ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw new Exception("Please enter price", ex);
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;
               
            }
        }

        [WebMethod]
        [ScriptMethod]
        //Run on Print Invoice
        public static void UpdateOrder(string orderedProducts, string amountDue, string discount,
             string Gst, string DiscType, string approvedBy, string gstPerAge, string Service)
        {
            try
            {

                if (DiscType == "null")
                {
                    DiscType = "0";
                }

                var dc = new DataControl();


                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()) > 0)
                {
                    OrderEntryController.Update_Order(
                        long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()), int.Parse("0"),
                        Constants.IntNullValue, Constants.IntNullValue, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                        decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0("0")), Convert.ToDecimal(gstPerAge), true, true, userId,
                        currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service)));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        [ScriptMethod]
        public static void InsertInvoice(string orderedProducts, string amountDue, string discount, string paidIn,
            string payType, string Gst, string DiscType, string approvedBy, string gstPerAge, string Service)
        {
            try
            {
                if (DiscType == "null")
                {
                    DiscType = "0";
                }

                var dc = new DataControl();

                int userId = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                DateTime currentWorkDate = DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString());
                int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

                var dtValue = (DataTable)JsonConvert.DeserializeObject(orderedProducts, (typeof(DataTable)));

                if (dtValue != null && dtValue.Rows.Count > 0 &&
                    long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()) > 0)
                {
                    OrderEntryController.Update_Invoice(
                        long.Parse(dtValue.Rows[0]["SALE_INVOICE_MASTER_ID"].ToString()), int.Parse(payType),
                        Constants.IntNullValue, Constants.IntNullValue, decimal.Parse(amountDue), decimal.Parse(dc.chkNull_0(discount)),
                        decimal.Parse(dc.chkNull_0(Gst)), decimal.Parse(dc.chkNull_0(paidIn)), Convert.ToDecimal(gstPerAge), false, true, userId,
                        currentWorkDate, distributerId, int.Parse(dc.chkNull_0(DiscType)), approvedBy, dtValue, Convert.ToDecimal(dc.chkNull_0(Service)));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Reports

        [WebMethod]
        [ScriptMethod]
        public static string SelectSalesSummary()
        {
            RptSaleController rptSaleCtl = new RptSaleController();
            DataTable dt =
                rptSaleCtl.SelectSaleReport(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), 1);
            return GetJson(dt);
        }

        [WebMethod]
        [ScriptMethod]
        public static string SelectSalesDetail()
        {
            RptSaleController rptSaleCtl = new RptSaleController();
            DataTable dt =
                rptSaleCtl.SelectSaleReport(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), 2);
            return GetJson(dt);
        }

        [WebMethod]
        [ScriptMethod]
        public static string SelectItemSalesDetail()
        {
            RptSaleController rptSaleCtl = new RptSaleController();
            DataTable dt =
                rptSaleCtl.SelectSaleReport(int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString()),
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    DateTime.Parse(HttpContext.Current.Session["CurrentWorkDate"].ToString()), 3);
            return GetJson(dt);
        }

        #endregion

        #region Table
        private void FillTableList()
        {
            DataTable dtTables = (DataTable)Session["dtTables"];

            lstTables.DataSource = dtTables;
            lstTables.DataBind();
        }

       

        protected void lstTables_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "view")
            {
                SetActiveClassForTables(e);
            }
        }

        private void SetActiveClassForTables(ListViewCommandEventArgs e)
        {
            foreach (ListViewDataItem item in lstTables.Items)
            {
                LinkButton lnkTables = (LinkButton)(item.FindControl("lnkTables"));
                lnkTables.CssClass = "box-last-vc";
            }
            LinkButton lnkTable = (LinkButton)e.Item.FindControl("lnkTables");

            lnkTable.CssClass = "box-last-vc active-vac";

            hfTableNo.Value = lnkTable.Text;

            Session["TableId"] = e.CommandArgument.ToString();
        }

        protected void UPTables_OnLoad(object sender, EventArgs e)
        {
            DataSet ds = _gCtl.SelectDataForPosLoad(Constants.IntNullValue,
                int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.IntNullValue);

            Session.Add("dtTables", ds.Tables[0]);


            FillTableList();
        }

        #endregion

        #region Print-INvoice


        private static void PrintReport()
        {
            try
            {
                PrintDocument prnDocument = new PrintDocument();
                prnDocument.PrinterSettings.PrinterName = (System.Configuration.ConfigurationManager.AppSettings["PrinterName"]);
                // The Event of 'PrintPage'
                
                prnDocument.PrintPage += new PrintPageEventHandler(prnDocument_PrintPage);

                prnDocument.Print();

            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw;
            }
        }

        private static void prnDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            leftMargin = (int)e.MarginBounds.Left;//100
            rightMargin = (int)e.MarginBounds.Right;//215
            topMargin = (int)e.MarginBounds.Top;//100
            topMargin = 1;
            bottomMargin = (int)e.MarginBounds.Bottom;//1069
            InvoiceWidth = (int)e.MarginBounds.Width;//115
            InvoiceHeight = (int)e.MarginBounds.Height;//969

            SetInvoiceHead(e.Graphics); // Draw Invoice Head
            SetInvoiceData(e.Graphics,e); // Draw Invoice Data
        }


        private static void SetInvoiceHead(Graphics g)
        {
            CurrentY = topMargin;
            CurrentX = leftMargin;
            
            InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
            InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g));

            // Get Titles Length:
           
            int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;
            int lenInvSubTitle4 = (int)g.MeasureString(InvSubTitle4, InvSubTitleFont).Width;
            // Set Titles Left:
          
            int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;
            int xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2;
            
            InvSubTitle3 = "SECTION: " + _section;
          
            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight + 10;
                g.DrawString(InvSubTitle3, InvSubTitleFont, BlueBrush, 10, CurrentY);
            }
            CurrentY = CurrentY + 25;
            g.DrawString("Service Type: " + HttpContext.Current.Session["customerType"], InvoiceFont, BlueBrush, 10, CurrentY);

            if (HttpContext.Current.Session["customerType"].ToString() == "Delivery")
            {
                CurrentY = CurrentY + 20;
                g.DrawString("D-M: " + HttpContext.Current.Session["bookerName"], InvoiceFont, BlueBrush, 10, CurrentY);

                CurrentY = CurrentY + 20;
                g.DrawString("Customer: " + HttpContext.Current.Session["tableName"], InvoiceFont, BlueBrush, 10, CurrentY);
            }

            else if (HttpContext.Current.Session["customerType"].ToString() == "Dine In")
            {
                CurrentY = CurrentY + 20;
                g.DrawString("O-T: " + HttpContext.Current.Session["bookerName"], InvoiceFont, BlueBrush, 10, CurrentY);

                CurrentY = CurrentY + 20;
                g.DrawString("Table No: " + HttpContext.Current.Session["tableName"], InvoiceFont, BlueBrush, 10, CurrentY);
            }
            else
            {
                CurrentY = CurrentY + 20;
                g.DrawString("O-T: " + HttpContext.Current.Session["bookerName"], InvoiceFont, BlueBrush, 10, CurrentY);

                //CurrentY = CurrentY + 20;
                //g.DrawString("Customer: " + HttpContext.Current.Session["tableName"], InvoiceFont, BlueBrush, 10, CurrentY);
            }

          

            CurrentY = CurrentY + 20;
            g.DrawString("Date: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"), InvoiceFont, BlueBrush, 10, CurrentY);
            g.DrawString(HttpContext.Current.Session["maxOrderNo"].ToString(), InvOrderTitleFont, BlueBrush, 250, CurrentY-30);

            XNoOfuntit = (int)g.MeasureString("", InvoiceFont).Width + 180;
            YNoOfUnit = CurrentY;
           
            CurrentY = CurrentY + 25;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);//
            CurrentY = CurrentY + 5;
           
        }

        
        private static void SetInvoiceData(Graphics g, PrintPageEventArgs e)
        {
            
            // Set Invoice Table:
            string FieldValue = "";
            int CurrentRecord = 0;
            
            // Set Table Head:
            int xProductID = 10;//leftMargin;

            CurrentY = CurrentY + 4;//InvoiceFontHeight;
            g.DrawString("Item Name", InvoiceFont, BlueBrush, xProductID, CurrentY);

            int xProductName = xProductID + (int)g.MeasureString("Item Name", InvoiceFont).Width + 70;
            g.DrawString("Qty", InvoiceFont, BlueBrush, xProductName, CurrentY);
            
            // Set Invoice Table:
            CurrentY = CurrentY + 20;//InvoiceFontHeight + 8;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
            CurrentY = CurrentY + 10;//InvoiceFontHeight + 8;
                                     //while (CurrentRecord < RecordsPerPage)

            if (dtValue.Rows.Count > 0)
            {
                foreach (DataRow dr in dtValue.Rows)
                {
                    if (dr["SECTION_NAME"].ToString() == _section)
                    {
                        decimal qty = Convert.ToDecimal(dr["QTY"].ToString()) - Convert.ToInt32(dr["PRINT_COUNT"].ToString());

                        string IS_VOID = dr["IS_VOID"].ToString();

                        if (IS_VOID == "false")
                        {
                            if (qty > 0)
                            {
                                if (dr["IS_DESC"].ToString() == "true")
                                {
                                    FieldValue = Convert.ToString(dr["SKU_NAME"]) +" (" + Convert.ToString(dr["DESCRIPTION"])+")";
                                }
                                else
                                {
                                    FieldValue = Convert.ToString(dr["SKU_NAME"]);// name
                                }
                                if (FieldValue.Length > 30)
                                {
                                    string FieldValue2 = FieldValue.Substring(0, 31);
                                    g.DrawString(FieldValue2, InvoiceFont2, BlackBrush, xProductID, CurrentY);

                                    FieldValue = FieldValue.Substring(31);
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY + 20);

                                    FieldValue = Convert.ToString(qty);// Qty
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                    CurrentY = CurrentY + 40;
                                    g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);

                                }
                                else
                                {
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY);


                                    FieldValue = Convert.ToString(qty);// Qty
                                    g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                    CurrentY = CurrentY + 20;
                                    g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
                                }
                                CurrentY = CurrentY + 10;
                                CurrentRecord++;
                            }
                            if (Convert.ToDecimal(dr["QTY"].ToString()) == 0)
                            {
                                if (qty == 0)
                                {
                                    if (dr["IS_DESC"].ToString() == "true")
                                    {
                                        FieldValue = Convert.ToString(dr["SKU_NAME"]) + " (" + Convert.ToString(dr["DESCRIPTION"]) + ")";
                                    }
                                    else
                                    {
                                        FieldValue = Convert.ToString(dr["SKU_NAME"]);// name
                                    }
                                    if (FieldValue.Length > 30)
                                    {
                                        string FieldValue2 = FieldValue.Substring(0, 31);
                                        g.DrawString(FieldValue2, InvoiceFont2, BlackBrush, xProductID, CurrentY);

                                        FieldValue = FieldValue.Substring(31);
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY + 20);

                                        FieldValue = Convert.ToString(qty);// Qty
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                        CurrentY = CurrentY + 40;
                                        g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);

                                    }
                                    else
                                    {
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductID, CurrentY);


                                        FieldValue = Convert.ToString(qty);// Qty
                                        g.DrawString(FieldValue, InvoiceFont2, BlackBrush, xProductName, CurrentY);

                                        CurrentY = CurrentY + 20;
                                        g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
                                    }
                                    CurrentY = CurrentY + 10;
                                    CurrentRecord++;
                                }
                            }
                        }
                    }
                }
            }
            
            if (CurrentRecord > 0)
            {
                
                g.Dispose();
            }
        }

        
        #endregion

        [WebMethod]
        [ScriptMethod]
        public static string ValidateUser(string UserId, string UserPass, string UserClick)
        {
            var _mController = new UserController();
            var mSkuController = new OrderEntryController();

            int distributerId = int.Parse(HttpContext.Current.Session["DISTRIBUTOR_ID"].ToString());

            if (System.Configuration.ConfigurationManager.AppSettings["Deployed"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Deployed"].ToString() == Cryptography.Encrypt("Deployed", "b0tin@74"))
                {
                    UserPass = Cryptography.Encrypt(UserPass, "b0tin@74");
                }
            }
            
            DataTable dt = _mController.SelectValidateUser(distributerId, UserId, UserPass);
            if (dt != null)
            {
                DataRow[] foundRow = null;
                if (UserClick == "Delete")
                {
                    foundRow = dt.Select("IsDelRight=" + 1);
                }
                else
                {
                    foundRow = dt.Select("IsLessRight=" + 1);
                }
                if (foundRow.Length > 0)
                {
                    return GetJson(dt);
                }
                return GetJson(null);
            }
            return GetJson(null);

        }

        protected void lnkExit_OnClick(object sender, EventArgs e)
        {
            Server.Transfer("Level.aspx?LevelID=27&LevelType=1");
        }
        
    }
}