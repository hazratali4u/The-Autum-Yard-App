using System;
using System.Data;
using System.IO;
using CORNCommon.Classes;
using CORNDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace CORNBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU
    /// </item>
    /// <term>
    /// Update SKU
    /// </term>
    /// <item>
    /// Get SKU
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class SkuController
    {
        #region Constructors

        /// <summary>
        /// Constructor For SkuController
        /// </summary>
        public SkuController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region public Methods

        #region Select
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_UOM_ID"></param>
        /// <param name="p_UOM_TYPE_ID"></param>
        /// <returns></returns>
        public DataTable SelectUOM(int p_UOM_ID, int p_UOM_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectUOM mspSelectSkuInfo = new spSelectUOM();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.UOM_ID = p_UOM_ID;
                mspSelectSkuInfo.UOM_TYPE_ID = p_UOM_TYPE_ID;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        /// <summary>
        /// Gets SKUS Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_company_id">Principal</param>
        /// <param name="p_division_id">Dicision</param>
        /// <param name="p_category_id">Category</param>
        /// <param name="p_brand_id">Brand</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuInfo(int p_company_id, int p_division_id, int p_category_id, int p_brand_id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo mspSelectSkuInfo = new uspSelectSkuInfo();
                mspSelectSkuInfo.Connection = mConnection;

                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SelectSkusforOrder(int pCompanyId, int pCategoryId, int pBrandId, int pSkuId, int pDistributorId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSkusforOrder mspSelectSkuInfo = new spSelectSkusforOrder();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SKU_ID = pSkuId;
                mspSelectSkuInfo.Category_Id = pCategoryId;
                mspSelectSkuInfo.DISTRIBUTOR_ID = pDistributorId;
                mspSelectSkuInfo.BRAND_ID = pBrandId;
                mspSelectSkuInfo.company_id = pCompanyId;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SpGetPendingBill(long pSaleInvoiceId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPendingBill mspSelectSkuInfo = new spGetPendingBill();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SALE_INVOICE_ID = pSaleInvoiceId;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SpGetPendingBill(long pSaleInvoiceId, int pTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPendingBill mspSelectSkuInfo = new spGetPendingBill();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SALE_INVOICE_ID = pSaleInvoiceId;
                mspSelectSkuInfo.TYPE_ID = pTypeId;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SelectSkuInfo2(int p_company_id, int p_division_id, int p_category_id, int p_brand_id, int Companyid, int p_TAGID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo2 mspSelectSkuInfo = new uspSelectSkuInfo2();
                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;
                mspSelectSkuInfo.TAG_ID = p_TAGID;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets SKU Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_SKU_Id">SKU</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuData(int p_SKU_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKUS mSkuInfo = new spSelectSKUS();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_Id;
                mSkuInfo.COMPANY_ID = Companyid;
                DataTable dt = mSkuInfo.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets SKU UOM
        /// </summary>
        /// <param name="p_UOM_Id">UOM</param>
        /// <param name="p_UOM_Desc">Description</param>
        /// <returns>SKU UOM</returns>
        public DataTable SelectUOMs(int p_UOM_Id, string p_UOM_Desc)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectUOMS mUOMs = new spSelectUOMS();
                mUOMs.Connection = mConnection;

                mUOMs.UOM_ID = p_UOM_Id;
                mUOMs.UOM_DESC = p_UOM_Desc;
                mUOMs.TIME_STAMP = Constants.DateNullValue;
                mUOMs.STATUS = Constants.IntNullValue;

                DataTable dt = mUOMs.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectSkuCountry()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectCountrySku mUOMs = new spSelectCountrySku();
                mUOMs.Connection = mConnection;
                DataTable dt = mUOMs.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion
        public DataTable SearchProduct(string pSearchText)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSearchProduct mspSelectSkuInfo = new uspSearchProduct();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SEARCH_TEXT = pSearchText;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        #region Insert, Update

        /// <summary>
        /// Inserts Or Updates SKU Price From Excel File
        /// </summary>
        /// Returns True On Success And False On Failure
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportSKUS(int p_DistributorId, string pFileName, int p_Principal_Id, int p_Company_ID, int p_UserId)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            DataControl DC = new DataControl();

            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            mTransaction = ProviderFactory.GetTransaction(mConnection);

            Sourcefile = new FileStream(pFileName, FileMode.Open);
            ReadSourceFile = new StreamReader(Sourcefile);
            string FileContents = "";
            try
            {
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    uspImportSKUS mSKUS = new uspImportSKUS();
                    mSKUS.Connection = mConnection;
                    mSKUS.Transaction = mTransaction;
                    mSKUS.PRINCIPAL_ID = p_Principal_Id;
                    mSKUS.ISEXEMPTED = true;
                    mSKUS.ISACTIVE = true;
                    mSKUS.GST_ON = 'T';
                    mSKUS.COMPANY_ID = p_Company_ID;
                    mSKUS.SKU_CODE = ParametersArr[0].ToString();
                    mSKUS.SKU_NAME = ParametersArr[1].ToString();
                    mSKUS.BAR_CODE = ParametersArr[2].ToString();
                    mSKUS.DIVISION_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[3]));
                    mSKUS.CATEGORY_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[4]));
                    mSKUS.BRAND_ID = Convert.ToInt32(DC.chkNull_0(ParametersArr[5]));
                    mSKUS.SKU_TAG_ID = int.Parse(DC.chkNull_0(ParametersArr[6].ToString()));
                    mSKUS.COLOR = ParametersArr[7].ToString();
                    mSKUS.PACKSIZE = ParametersArr[8].ToString();
                    mSKUS.SKU_SEASON = ParametersArr[9].ToString();
                    mSKUS.SKU_COUNTRY = ParametersArr[10].ToString();
                    mSKUS.GST_RATE_REG = 0;
                    mSKUS.GST_RATE_UNREG = 0;
                    mSKUS.TIME_STAMP = System.DateTime.Now;
                    mSKUS.LASTUPDATE_DATE = System.DateTime.Now;
                    mSKUS.IP_ADDRESS = null;
                    mSKUS.USER_ID = p_UserId;

                    mSKUS.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;


            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                //ExceptionPublisher.PublishException(excp);
                // throw;
                return false;

            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();

            }
        }

        /// <summary>
        /// Insert SKU
        /// </summary>
        /// <remarks>
        /// Returns Inserted SKU ID as String
        /// </remarks>
        /// <param name="p_IsExempted">IsExempted</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Gst_On">GSTOn</param>
        /// <param name="p_Company_Id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Variant_Id">Variant</param>
        /// <param name="p_GST_Rate_Reg">GSTReg</param>
        /// <param name="p_GST_Rate_Unreg">GSTUnReg</param>
        /// <param name="p_Units_In_Case">Units</param>
        /// <param name="p_Sku_Code">Code</param>
        /// <param name="p_Sku_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_packSize">Packing</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Inserted SKU ID as String</returns>
        public string InsertSKUS2(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, string p_Units_In_Case, string p_Sku_Code, string p_Sku_Name,
            string p_Ip_Address, string p_packSize, int p_UserId, int Companyid, string p_BarCode, string p_color, int p_skuTagId, string p_skuCountry, string p_skuSeason)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS2 mSkus = new spInsertSKUS2();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = Companyid;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                mSkus.COLOR = p_Units_In_Case;
                mSkus.BAR_CODE = p_BarCode;
                mSkus.COLOR = p_color;
                mSkus.SKU_TAG_ID = p_skuTagId;
                mSkus.SKU_SEASON = p_skuSeason;
                mSkus.SKU_COUNTRY = p_skuCountry;

                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                //mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;

                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }


        public string InsertSKUS(bool p_IsDesc, bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id
            , int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int Companyid, string p_DESCRIPTION, int pSectionId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS mSkus = new spInsertSKUS();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.IS_DESC = p_IsDesc;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = Companyid;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.DESCRIPTION = p_DESCRIPTION;
                mSkus.SECTION_ID = pSectionId;

                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        /// <summary>
        /// Updates SKU
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Null On Failure
        /// </remarks>
        /// <param name="p_IsExempted">IsExempted</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Gst_On">GSTOn</param>
        /// <param name="p_Company_Id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Variant_Id">Variant</param>
        /// <param name="p_GST_Rate_Reg">GSTReg</param>
        /// <param name="p_GST_Rate_Unreg">GSTUnReg</param>
        /// <param name="p_Units_In_Case">Units</param>
        /// <param name="p_Sku_Id">SKU</param>
        /// <param name="p_Sku_Code">Code</param>
        /// <param name="p_Sku_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_packSize">Packing</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="CompanyId">Company</param>
        /// <returns>"Record Updated" On Success And Null On Failure</returns>
        public string UpdateSKUS(bool p_IsDesc, bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, int p_Sku_Id, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int CompanyId, string p_DESCRIPTION, int pSectionId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS mSkus = new spUpdateSKUS();

                mSkus.Connection = mConnection;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.IS_DESC = p_IsDesc;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = CompanyId;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_ID = p_Sku_Id;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = DateTime.Now;
                mSkus.LASTUPDATE_DATE = DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.DESCRIPTION = p_DESCRIPTION;
                mSkus.SECTION_ID = pSectionId;
                mSkus.ExecuteQuery();
                return "Record Updated";
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }


        public string UpdateSKUS2(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id,
            int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, string p_Units_In_Case,
            int p_Sku_Id, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int CompanyId, string p_BarCode, string p_color, int p_skuTagId, string p_skuCountry, string p_skuSeason)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS2 mSkus = new spUpdateSKUS2();

                mSkus.Connection = mConnection;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = CompanyId;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                mSkus.BAR_CODE = p_BarCode;
                mSkus.COLOR = p_color;
                mSkus.SKU_TAG_ID = p_skuTagId;
                mSkus.SKU_SEASON = p_skuSeason;
                mSkus.SKU_COUNTRY = p_skuCountry;

                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_ID = p_Sku_Id;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.ExecuteQuery();
                return "Record Updated";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        public bool InsertProductSection(string SECTION_CODE, string SECTION_NAME, string PRINTER_NAME, bool IS_PRINT)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRODUCT_SECTION mDepartment = new spInsertPRODUCT_SECTION();
                mDepartment.Connection = mConnection;
                mDepartment.SECTION_CODE = SECTION_CODE;
                mDepartment.SECTION_NAME = SECTION_NAME;
                mDepartment.PRINTER_NAME = PRINTER_NAME;
                mDepartment.IS_PRINT = IS_PRINT;
                bool a = mDepartment.ExecuteQuery();
                return a;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public bool UpdateProductSection(int SECTION_ID, string SECTION_CODE, string SECTION_NAME, string PRINTER_NAME, bool IS_ACTIVE, bool IS_PRINT)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePRODUCT_SECTION mSERVICE_TYPE = new spUpdatePRODUCT_SECTION();

                mSERVICE_TYPE.Connection = mConnection;
                mSERVICE_TYPE.IS_ACTIVE = IS_ACTIVE;
                mSERVICE_TYPE.PRINTER_NAME = PRINTER_NAME;
                mSERVICE_TYPE.SECTION_CODE = SECTION_CODE;
                mSERVICE_TYPE.SECTION_ID = SECTION_ID;
                mSERVICE_TYPE.SECTION_NAME = SECTION_NAME;
                mSERVICE_TYPE.IS_PRINT = IS_PRINT;
                bool a = mSERVICE_TYPE.ExecuteQuery();
                return a;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectProductSection(int SECTION_ID, string SECTION_CODE, string SECTION_NAME)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPRODUCT_SECTION mSERVICE_TYPE = new spSelectPRODUCT_SECTION();
                mSERVICE_TYPE.Connection = mConnection;

                mSERVICE_TYPE.SECTION_CODE = SECTION_CODE;
                mSERVICE_TYPE.SECTION_ID = SECTION_ID;
                mSERVICE_TYPE.SECTION_NAME = SECTION_NAME;

                //   mPAYMENT_TYPE.IS_ACTIVE = p_IS_ACTIVE;


                return mSERVICE_TYPE.ExecuteTable();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertBarcode(string p_Company_Name, string p_Product_Name, string p_Product_price, byte[] p_image)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBARCODE mSkus = new spInsertBARCODE();

                mSkus.Connection = mConnection;
                mSkus.COMPANY_NAME = p_Company_Name;
                mSkus.PRODUCT_NAME = p_Product_Name;
                mSkus.PRODUCT_PRICE = p_Product_price;
                mSkus.BARCODE_IMAGE = p_image;

                mSkus.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                //return exp.Message;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectSkuBarcode()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectBARCODE mBarcode = new spSelectBARCODE();
                mBarcode.Connection = mConnection;
                //mBarcode.PRODUCT_NAME = p_ROWNO;
                DataTable dt = mBarcode.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable GetMaxSku_ID()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMaxSKU_ID mdpt_id = new spSelectMaxSKU_ID();
                mdpt_id.Connection = mConnection;
                return mdpt_id.ExecuteTable();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable GetFinshedDetail(int p_FINISHED_SKU_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetFinishedDetail mSkuInfo = new uspGetFinishedDetail();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                mSkuInfo.TYPE_ID = p_TYPE_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool InsertFinishedSKU(int p_FINISHED_SKU_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtFinishedDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertFinishedMaster mFinishedMaster = new uspInsertFinishedMaster();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.Transaction = mTransaction;

                //------------Insert Finished Goods Master----------

                if (dtFinishedDetail.Rows.Count > 0)
                {
                    mFinishedMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                    mFinishedMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                    mFinishedMaster.USER_ID = p_USER_ID;

                    mFinishedMaster.ExecuteQuery();

                    //----------------Insert Finished Goods Detail-------------
                    uspInsertFinishedDetail mFinishedDetail = new uspInsertFinishedDetail();
                    mFinishedDetail.Connection = mConnection;
                    mFinishedDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtFinishedDetail.Rows)
                    {
                        mFinishedDetail.FINISHED_GOOD_MASTER_ID = mFinishedMaster.FINISHED_GOOD_MASTER_ID;
                        mFinishedDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mFinishedDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        mFinishedDetail.ExecuteQuery();
                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
        #endregion
    }
}
