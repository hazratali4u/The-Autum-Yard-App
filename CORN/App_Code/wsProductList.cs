using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System;

/// <summary>
/// Summary description for wsProductList
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
public class wsProductList : System.Web.Services.WebService
{

    public wsProductList()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetProducts(string prefixText)
    {
        List<string> Products = new List<string>();
        SkuController mSKUController = new SkuController();
        DataTable Dtsku_Price = mSKUController.SearchProduct(prefixText);

        for (int i = 0; i < Dtsku_Price.Rows.Count; i++)
        {
            Products.Add(
                AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                    Dtsku_Price.Rows[i]["SKU_NAME"].ToString(), Dtsku_Price.Rows[i]["SKU_CODE"].ToString()));
        }
        return Products.ToArray();
    }

    [WebMethod]
    public string[] GetProducts2(string prefixText)
    {
        List<string> Products = new List<string>();
        SkuController mSKUController = new SkuController();
        DataTable Dtsku_Price = mSKUController.SearchProduct(prefixText);

        for (int i = 0; i < Dtsku_Price.Rows.Count; i++)
        {
            Products.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Dtsku_Price.Rows[i]["SKUDETAIL"].ToString(), Dtsku_Price.Rows[i]["SKU_ID"].ToString()));
        }

        return Products.ToArray();
    }

}
