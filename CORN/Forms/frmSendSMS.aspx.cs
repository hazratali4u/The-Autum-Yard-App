using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using CORNBusinessLayer.Classes;
using System.Data;
using CORNCommon.Classes;


/// <summary>
/// Form To Change User Password
/// </summary>
public partial class Forms_frmSendSMS : System.Web.UI.Page
{
    GsmCommMain comm;

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCustomers();
        }
    }
    private void LoadCustomers()
    {
        LstCustomer.Items.Clear();

        var mController = new CustomerDataController();
        DataTable dtCustomers = mController.UspSelectCustomer(Constants.IntNullValue, "SMS", "");
        clsWebFormUtil.FillListBox(LstCustomer, dtCustomers, 0, 3, false);
    }
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        hfNumber.Value = "";

        for (int i = 0; i < LstCustomer.Items.Count; i++)
        {
            if (LstCustomer.Items[i].Selected == true)
            {
                hfNumber.Value += LstCustomer.Items[i].Value.ToString() + ",";
            }
        }
        if (hfNumber.Value == "" && txtPhoneNo.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Select Customer or enter Number.');", true);
            return;
        }

        lblError.Text = string.Empty;
        string SMSSendingType = SMSSendingType = System.Configuration.ConfigurationManager.AppSettings["SMSSendingType"].ToString();

        if (SMSSendingType == "Web")
        {
            lblError.Text = readHtmlPage("http://www.sms4connect.com/api/sendsms.php/sendsms/url");
        }
        else
        {
            Int32 comport, baudrate, timeout;
            comport = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["comport"].ToString().Trim());
            baudrate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["baudrate"].ToString().Trim());
            timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["timeout"].ToString().Trim());

            CommSetting cs = new CommSetting();
            SmsSubmitPdu pdu;
            cs.comm = new GsmCommMain(comport, baudrate, timeout);
            comm = cs.comm;

            try
            {
                if (!comm.IsOpen())
                {
                    comm.Open();
                }
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishExceptionforSync(ex, "Exception from SMS");
                lblError.Text = ex.Message;
            }

            try
            {

                if (hfNumber.Value != "")
                {
                    for (int i = 0; i < LstCustomer.Items.Count; i++)
                    {
                        if (LstCustomer.Items[i].Selected == true)
                        {

                            pdu = new SmsSubmitPdu(txtSMS.Text, LstCustomer.Items[i].Value);
                            cs.comm.SendMessage(pdu);
                        }
                    }
                    lblError.Text = "Message Sent";
                }

                if (txtPhoneNo.Text != "")
                {

                    pdu = new SmsSubmitPdu(txtSMS.Text, txtPhoneNo.Text);
                    cs.comm.SendMessage(pdu);

                    lblError.Text = "Message Sent";
                }
                comm.Close();
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }

        }
    }

    private String readHtmlPage(string url)
    {

        string receipients = "";

        if (hfNumber.Value != "" && txtPhoneNo.Text.ToString() != "")
        {
            receipients = hfNumber.Value + txtPhoneNo.Text.ToString();
        }
        if (hfNumber.Value != "" && txtPhoneNo.Text.ToString() == "")
        {
            receipients = hfNumber.Value;
        }
        if (hfNumber.Value == "" && txtPhoneNo.Text.ToString() != "")
        {
            receipients = txtPhoneNo.Text.ToString();
        }
        string ID = System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString().Trim();
        string PWD = System.Configuration.ConfigurationManager.AppSettings["UPwd"].ToString().Trim();
        string MASK = System.Configuration.ConfigurationManager.AppSettings["Mask"].ToString().Trim();

        String result = "";
        String message = HttpUtility.UrlEncode(txtSMS.Text);

        String strPost = "id=" + ID + "&pass=" + PWD + "&msg=" + message + "&to=" + receipients + "&mask=" + MASK + "&type=xml&lang=English";

        StreamWriter myWriter = null;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        objRequest.Method = "POST";
        objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
        objRequest.ContentType = "application/x-www-form-urlencoded";
        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strPost);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }
        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();   // Close and clean up the StreamReader   
            sr.Close();
        }

        return result;
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.txtPhoneNo.Text = null;
        this.txtSMS.Text = null;        
    }
}
public class CommSetting : IDisposable
{
    private bool disposed = false;
    public static int Comm_Port = 2;
    public static int Comm_BaudRate = 9600;
    public static int Comm_TimeOut = 300;
    public static GsmCommMain comm2;
    public GsmCommMain comm;
    public static int count = 1;

    public CommSetting()
    {
 
    }

    public static Int32 Add()
    {
        return count++;
    }
    
    ~CommSetting()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this.comm);
    }

    private void Dispose(bool disposing)
    {
        if (this.comm.IsOpen())
        {
            this.comm.Close();
        }

        disposed = true;

    }
}