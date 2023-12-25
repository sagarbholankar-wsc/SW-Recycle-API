using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using PurchaseTrackerAPI.Models;
using PurchaseTrackerAPI.StaticStuff;
using PurchaseTrackerAPI.BL.Interfaces;
using PurchaseTrackerAPI.DAL.Interfaces;

namespace PurchaseTrackerAPI.DAL
{
    public class TblGlobalRatePurchaseDAO : ITblGlobalRatePurchaseDAO
    {


        private readonly IConnectionString _iConnectionString;
        public TblGlobalRatePurchaseDAO(IConnectionString iConnectionString)
        {
            _iConnectionString = iConnectionString;
        }
        #region Methods
        public String SqlSelectQuery()
        {
            String sqlSelectQry = "SELECT * from tblGlobalRatePurchase";
            return sqlSelectQry;
        }

        public String SelectLatestRateWithAvg()
        {
            String sqlSelectQry = " SELECT CAST(tblGlobalRatePurchase.createdOn AS DATE),COUNT(*), " +
                                  " SUM(tblGlobalRatePurchase.rate) / COUNT(*) as avgRate FROM tblGlobalRatePurchase tblGlobalRatePurchase ";
                                  


            return sqlSelectQry;
        }

        #endregion

        #region Selection
        /// <summary>
        /// swati pisal
        /// To get latest Global purchase rate
        /// </summary>
        /// <returns></returns>
        public List<TblGlobalRatePurchaseTO> SelectLatestRateOfPurchaseDCT(DateTime date, Boolean isGetLatest)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            try
            {
                conn.Open();

                //Saket [2018-01-18] Added new  query.
                //cmdSelect.CommandText = "SELECT max(idGlobalRatePurchase) as idGlobalRatePurchase, rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId  " + //,Rate_Band_Costing 
                //    " FROM tblGlobalRatePurchase" +
                //    " INNER JOIN[dbo].[tblRateBandDeclarationPurchase] Ratedec" +
                //    " ON Ratedec.globalRatePurchaseId=idGlobalRatePurchase" +
                //    " WHERE idGlobalRatePurchase = (select max(idGlobalRatePurchase) from tblGlobalRatePurchase" +
                //    " WHERE DAY(Ratedec.createdOn)= " + date.Day + " AND MONTH(Ratedec.createdOn)= " + date.Month + " AND YEAR(Ratedec.createdOn)= " + date.Year +
                //    " ) GROUP BY rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId"; //,Rate_Band_Costing";

                if (isGetLatest)
                {
                    cmdSelect.CommandText = "SELECT max(idGlobalRatePurchase) as idGlobalRatePurchase, rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId  " + //,Rate_Band_Costing 
                                                       " FROM tblGlobalRatePurchase" +
                                                       " INNER JOIN[dbo].[tblRateBandDeclarationPurchase] Ratedec" +
                                                       " ON Ratedec.globalRatePurchaseId=idGlobalRatePurchase" +
                                                       " WHERE idGlobalRatePurchase = (select max(idGlobalRatePurchase) from tblGlobalRatePurchase" +
                                                       //" WHERE DAY(Ratedec.createdOn)= " + date.Day + " AND MONTH(Ratedec.createdOn)= " + date.Month + " AND YEAR(Ratedec.createdOn)= " + date.Year +
                                                       " ) GROUP BY rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId"; //,Rate_Band_Costing";


                }
                else
                {
                      cmdSelect.CommandText = "SELECT max(idGlobalRatePurchase) as idGlobalRatePurchase, rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId  " + //,Rate_Band_Costing 
                                                       " FROM tblGlobalRatePurchase" +
                                                       " INNER JOIN[dbo].[tblRateBandDeclarationPurchase] Ratedec" +
                                                       " ON Ratedec.globalRatePurchaseId=idGlobalRatePurchase" +
                                                       " WHERE idGlobalRatePurchase = (select max(idGlobalRatePurchase) from tblGlobalRatePurchase" +
                                                       " WHERE tblGlobalRatePurchase.createdOn <= @date" +
                                                       " ) GROUP BY rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId"; //,Rate_Band_Costing";

                                                       cmdSelect.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = date;

                }


                //string g = "SELECT max(idGlobalRatePurchase) as idGlobalRatePurchase, rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId,Rate_Band_Costing   FROM tblGlobalRatePurchase" +
                //    "  INNER JOIN[dbo].[tblRateBandDeclarationPurchase] Ratedec" +
                //    " ON Ratedec.globalRatePurchaseId=idGlobalRatePurchase" +
                //    "WHERE idGlobalRatePurchase = (select max(idGlobalRatePurchase) from tblGlobalRatePurchase" +
                //    " WHERE DAY(Ratedec.createdOn)= " + date.Day + " AND MONTH(Ratedec.createdOn)= " + date.Month + " AND YEAR(Ratedec.createdOn)= " + date.Year +
                //    " ) GROUP BY rate, Ratedec.createdBy, Ratedec.createdOn, comments, rateReasonId,Rate_Band_Costing";

                //cmdSelect.CommandText = " SELECT MAX(idGlobalRatePurchase) as idGlobalRatePurchase, rate  FROM tblGlobalRatePurchase" +
                //                        " GROUP BY rate";
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                SqlDataReader TblGlobalRatePurchaseTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblGlobalRatePurchaseTO> list = ConvertDTToList(TblGlobalRatePurchaseTODT);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (sqlDataReader != null) sqlDataReader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }
        }
        public List<TblGlobalRatePurchaseTO> GetGlobalPurchaseRateList(DateTime fromDate, DateTime toDate)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery() + " WHERE CONVERT (DATE,createdOn,103)   BETWEEN @fromDate AND @toDate ORDER BY createdOn DESC";
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                cmdSelect.Parameters.Add("@fromDate", System.Data.SqlDbType.Date).Value = fromDate.Date.ToString(Constants.AzureDateFormat);
                cmdSelect.Parameters.Add("@toDate", System.Data.SqlDbType.Date).Value = toDate.Date.ToString(Constants.AzureDateFormat);

                SqlDataReader sqlDataReader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblGlobalRatePurchaseTO> list = ConvertDTToList(sqlDataReader);
                sqlDataReader.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public List<TblPurchaseScheduleSummaryTO> GetAvgSaleDateWise(DateTime fromDate, DateTime toDate)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = " SELECT CAST(sql1.invoiceDate AS DATE) AS invDate, sum(itemwiseTaxableAmt)/sum(itemQty) AS AvgRate ,isConfirmed FROM( " +
                        " SELECT tempInvoice.*,tblProdGstCodeDtls.MaterialId,tempInvoiceItemDetails.otherTaxId,tempInvoiceItemDetails.invoiceQty as itemQty,(tempInvoiceItemDetails.basicTotal - ISNULL(tempInvoiceItemDetails.cdAmt, 0)) AS itemwiseTaxableAmt from tempInvoice " +
                        " LEFT JOIN tempInvoiceItemDetails ON tempInvoice.idInvoice = tempInvoiceItemDetails.invoiceId "+
                        " LEFT JOIN tblProdGstCodeDtls ON tempInvoiceItemDetails.prodGstCodeId = tblProdGstCodeDtls.idProdGstCode "+
                        " UNION ALL "+
                        " SELECT finalInvoice.*,tblProdGstCodeDtls.MaterialId, finalInvoiceItemDetails.otherTaxId,finalInvoiceItemDetails.invoiceQty as itemQty, (finalInvoiceItemDetails.basicTotal - ISNULL(finalInvoiceItemDetails.cdAmt, 0)) AS itemwiseTaxableAmt from finalInvoice " +
                        " LEFT JOIN finalInvoiceItemDetails ON finalInvoice.idInvoice = finalInvoiceItemDetails.invoiceId " +
                        " LEFT JOIN tblProdGstCodeDtls ON finalInvoiceItemDetails.prodGstCodeId = tblProdGstCodeDtls.idProdGstCode " +
                        " )sql1 " +
                        " WHERE sql1.MaterialId > 0 and ISNULL(sql1.otherTaxId,0) = 0 AND CONVERT (DATE,sql1.invoiceDate,103) BETWEEN @fromDate AND @toDate" +
                        " GROUP BY  CAST(sql1.invoiceDate AS DATE) ,sql1.isConfirmed";


                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                cmdSelect.Parameters.Add("@fromDate", System.Data.SqlDbType.Date).Value = fromDate.Date.ToString(Constants.AzureDateFormat);
                cmdSelect.Parameters.Add("@toDate", System.Data.SqlDbType.Date).Value = toDate.Date.ToString(Constants.AzureDateFormat);

                SqlDataReader TblPurchaseScheduleSummaryDT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseScheduleSummaryTO> tblPurchaseScheduleSummaryList = new List<TblPurchaseScheduleSummaryTO>();
                if (TblPurchaseScheduleSummaryDT != null)
                {
                    while (TblPurchaseScheduleSummaryDT.Read())
                    {
                        TblPurchaseScheduleSummaryTO TblPurchaseScheduleSummaryTONew = new TblPurchaseScheduleSummaryTO();
                        if (TblPurchaseScheduleSummaryDT["isConfirmed"] != DBNull.Value)
                            TblPurchaseScheduleSummaryTONew.COrNcId = Convert.ToInt32(TblPurchaseScheduleSummaryDT["isConfirmed"]);
                        if (TblPurchaseScheduleSummaryDT["invDate"] != DBNull.Value)
                            TblPurchaseScheduleSummaryTONew.CreatedOn = Convert.ToDateTime(TblPurchaseScheduleSummaryDT["invDate"]);
                        if (TblPurchaseScheduleSummaryDT["AvgRate"] != DBNull.Value)
                            TblPurchaseScheduleSummaryTONew.Rate = Convert.ToDouble(TblPurchaseScheduleSummaryDT["AvgRate"]);
                        tblPurchaseScheduleSummaryList.Add(TblPurchaseScheduleSummaryTONew);
                    }
                }
                TblPurchaseScheduleSummaryDT.Dispose();

                return tblPurchaseScheduleSummaryList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public List<TblGlobalRatePurchaseTO> GetGlobalRateList(DateTime fromDate, DateTime toDate)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = " SELECT * from tblGlobalRate WHERE CONVERT (DATE,createdOn,103)   BETWEEN @fromDate AND @toDate ORDER BY createdOn DESC";
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                cmdSelect.Parameters.Add("@fromDate", System.Data.SqlDbType.Date).Value = fromDate.Date.ToString(Constants.AzureDateFormat);
                cmdSelect.Parameters.Add("@toDate", System.Data.SqlDbType.Date).Value = toDate.Date.ToString(Constants.AzureDateFormat);

                SqlDataReader TblGlobalRatePurchaseTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblGlobalRatePurchaseTO> TblGlobalRatePurchaseTOList = new List<TblGlobalRatePurchaseTO>();
                if (TblGlobalRatePurchaseTODT != null)
                {
                    while (TblGlobalRatePurchaseTODT.Read())
                    {
                        TblGlobalRatePurchaseTO TblGlobalRatePurchaseTONew = new TblGlobalRatePurchaseTO();
                        if (TblGlobalRatePurchaseTODT["idGlobalRate"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.IdGlobalRatePurchase = Convert.ToInt32(TblGlobalRatePurchaseTODT["idGlobalRate"]);
                        if (TblGlobalRatePurchaseTODT["createdBy"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.CreatedBy = Convert.ToInt32(TblGlobalRatePurchaseTODT["createdBy"]);
                        if (TblGlobalRatePurchaseTODT["createdOn"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.CreatedOn = Convert.ToDateTime(TblGlobalRatePurchaseTODT["createdOn"]);
                        if (TblGlobalRatePurchaseTODT["rate"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.Rate = Convert.ToDouble(TblGlobalRatePurchaseTODT["rate"]);
                        if (TblGlobalRatePurchaseTODT["comments"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.Comments = Convert.ToString(TblGlobalRatePurchaseTODT["comments"]);
                        if (TblGlobalRatePurchaseTODT["rateReasonId"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.RateReasonId = Convert.ToInt32(TblGlobalRatePurchaseTODT["rateReasonId"]);
                        TblGlobalRatePurchaseTOList.Add(TblGlobalRatePurchaseTONew);
                    }
                }
                TblGlobalRatePurchaseTODT.Dispose();

                return TblGlobalRatePurchaseTOList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public List<TblGlobalRatePurchaseTO> GetPurchaseRateWithAvgDtls(DateTime fromDate, DateTime toDate)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = SelectLatestRateWithAvg() + " WHERE tblGlobalRatePurchase.createdOn " +
                                        " BETWEEN @fromDate AND @toDate GROUP BY CAST(tblGlobalRatePurchase.createdOn AS DATE)";

                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                cmdSelect.Parameters.Add("@fromDate", System.Data.SqlDbType.Date).Value = fromDate.Date.ToString(Constants.AzureDateFormat);
                cmdSelect.Parameters.Add("@toDate", System.Data.SqlDbType.Date).Value = toDate.Date.ToString(Constants.AzureDateFormat);

                SqlDataReader sqlDataReader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblGlobalRatePurchaseTO> list = ConvertDTToList(sqlDataReader);
                sqlDataReader.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public Boolean IsRateAlreadyDeclaredForTheDate(DateTime date, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader TblGlobalRatePurchaseTODT = null;
            try
            {
                cmdSelect.CommandText = "SELECT COUNT(*) AS todayCount FROM tblGlobalRate " + " WHERE DAY(createdOn)=" + date.Day + " AND MONTH(createdOn)=" + date.Month + " AND YEAR(createdOn)=" + date.Year;
                cmdSelect.Connection = conn;
                cmdSelect.Transaction = tran;
                cmdSelect.CommandType = System.Data.CommandType.Text;


                TblGlobalRatePurchaseTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);

                if (TblGlobalRatePurchaseTODT != null)
                {
                    while (TblGlobalRatePurchaseTODT.Read())
                    {
                        TblGlobalRatePurchaseTO TblGlobalRatePurchaseTONew = new TblGlobalRatePurchaseTO();
                        if (TblGlobalRatePurchaseTODT[0] != DBNull.Value)
                        {
                            if (Convert.ToInt32(TblGlobalRatePurchaseTODT[0]) > 0)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                if (TblGlobalRatePurchaseTODT != null)
                    TblGlobalRatePurchaseTODT.Dispose();
                cmdSelect.Dispose();
            }
        }

        public List<TblGlobalRatePurchaseTO> ConvertDTToList(SqlDataReader TblGlobalRatePurchaseTODT)
        {
            List<TblGlobalRatePurchaseTO> TblGlobalRatePurchaseTOList = new List<TblGlobalRatePurchaseTO>();
            if (TblGlobalRatePurchaseTODT != null)
            {
                while (TblGlobalRatePurchaseTODT.Read())
                {
                    TblGlobalRatePurchaseTO TblGlobalRatePurchaseTONew = new TblGlobalRatePurchaseTO();
                    if (TblGlobalRatePurchaseTODT["idGlobalRatePurchase"] != DBNull.Value)
                        TblGlobalRatePurchaseTONew.IdGlobalRatePurchase = Convert.ToInt32(TblGlobalRatePurchaseTODT["idGlobalRatePurchase"]);
                    if (TblGlobalRatePurchaseTODT["createdBy"] != DBNull.Value)
                        TblGlobalRatePurchaseTONew.CreatedBy = Convert.ToInt32(TblGlobalRatePurchaseTODT["createdBy"]);
                    if (TblGlobalRatePurchaseTODT["createdOn"] != DBNull.Value)
                        TblGlobalRatePurchaseTONew.CreatedOn = Convert.ToDateTime(TblGlobalRatePurchaseTODT["createdOn"]);
                    if (TblGlobalRatePurchaseTODT["rate"] != DBNull.Value)
                        TblGlobalRatePurchaseTONew.Rate = Convert.ToDouble(TblGlobalRatePurchaseTODT["rate"]);
                    if (TblGlobalRatePurchaseTODT["comments"] != DBNull.Value)
                        TblGlobalRatePurchaseTONew.Comments = Convert.ToString(TblGlobalRatePurchaseTODT["comments"]);
                    if (TblGlobalRatePurchaseTODT["rateReasonId"] != DBNull.Value)
                        TblGlobalRatePurchaseTONew.RateReasonId = Convert.ToInt32(TblGlobalRatePurchaseTODT["rateReasonId"]);
                    try
                    {
                        if (TblGlobalRatePurchaseTODT["Rate_Band_Costing"] != DBNull.Value)
                            TblGlobalRatePurchaseTONew.Ratebandcosting = Convert.ToInt32(TblGlobalRatePurchaseTODT["Rate_Band_Costing"]);
                    }
                    catch (Exception ex)
                    {

                    }
                    TblGlobalRatePurchaseTOList.Add(TblGlobalRatePurchaseTONew);
                }
            }
            return TblGlobalRatePurchaseTOList;
        }

        /// <summary>
        /// swati pisal
        /// </summary>
        /// <param name="idGlobalRatePurchase"></param>
        /// <returns></returns>
        public TblGlobalRatePurchaseTO SelectTblGlobalRatePurchase1(Int32 idGlobalRatePurchase)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery() + " WHERE idGlobalRatePurchase = " + idGlobalRatePurchase + " ";
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                SqlDataReader TblGlobalRatePurchaseTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);

                List<TblGlobalRatePurchaseTO> list = ConvertDTToList(TblGlobalRatePurchaseTODT);
                TblGlobalRatePurchaseTODT.Dispose();
                if (list != null && list.Count == 1)
                    return list[0];
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public TblGlobalRatePurchaseTO SelectTblGlobalRatePurchase(Int32 idGlobalRatePurchase, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmdSelect.CommandText = SqlSelectQuery() + " WHERE idGlobalRatePurchase = " + idGlobalRatePurchase + " ";
                cmdSelect.Connection = conn;
                cmdSelect.Transaction = tran;

                cmdSelect.CommandType = System.Data.CommandType.Text;
                sqlReader = cmdSelect.ExecuteReader(CommandBehavior.Default);

                List<TblGlobalRatePurchaseTO> list = ConvertDTToList(sqlReader);
                if (list != null && list.Count == 1)
                    return list[0];
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlReader.Dispose();
                cmdSelect.Dispose();
            }
        }
        // Add By Samadhan 02 Dec 2022
        public Boolean IsPurchaseQuotaAlreadyDeclaredForTheDate(DateTime date, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader TblPurchaseQuotaTODT = null;
            try
            {
                cmdSelect.CommandText = "SELECT COUNT(*) AS todayCount FROM tblPurchaseQuota " + " WHERE  isactive=1 and DAY(createdOn)=" + date.Day + " AND MONTH(createdOn)=" + date.Month + " AND YEAR(createdOn)=" + date.Year ;
                cmdSelect.Connection = conn;
                cmdSelect.Transaction = tran;
                cmdSelect.CommandType = System.Data.CommandType.Text;


                TblPurchaseQuotaTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);

                if (TblPurchaseQuotaTODT != null)
                {
                    while (TblPurchaseQuotaTODT.Read())
                    {
                          if (TblPurchaseQuotaTODT[0] != DBNull.Value)
                        {
                            if (Convert.ToInt32(TblPurchaseQuotaTODT[0]) > 0)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                if (TblPurchaseQuotaTODT != null)
                    TblPurchaseQuotaTODT.Dispose();
                cmdSelect.Dispose();
            }
        }

        #endregion

        #region Insertion

        public int InsertTblGlobalRatePurchase(TblGlobalRatePurchaseTO TblGlobalRatePurchaseTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                cmdInsert.Connection = conn;
                cmdInsert.Transaction = tran;
                return ExecuteInsertionCommand(TblGlobalRatePurchaseTO, cmdInsert);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                cmdInsert.Dispose();
            }
        }

        public int ExecuteInsertionCommand(TblGlobalRatePurchaseTO TblGlobalRatePurchaseTO, SqlCommand cmdInsert)
        {
            //commented by swati for brand and purchase
            String sqlQuery = @" INSERT INTO [tblGlobalRatePurchase]( " +
                            "  [createdBy]" +
                            " ,[createdOn]" +
                            " ,[rate]" +
                            " ,[comments]" +
                            " ,[rateReasonId]" +
                            " )" +
                " VALUES (" +
                            "  @CreatedBy " +
                            " ,@CreatedOn " +
                            " ,@Rate " +
                            " ,@Comments " +
                            " ,@rateReasonId " +
                            " )";

            cmdInsert.CommandText = sqlQuery;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            cmdInsert.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int).Value = TblGlobalRatePurchaseTO.CreatedBy;
            cmdInsert.Parameters.Add("@CreatedOn", System.Data.SqlDbType.DateTime).Value = TblGlobalRatePurchaseTO.CreatedOn;
            cmdInsert.Parameters.Add("@Rate", System.Data.SqlDbType.NVarChar).Value = TblGlobalRatePurchaseTO.Rate;
            cmdInsert.Parameters.Add("@Comments", System.Data.SqlDbType.NVarChar).Value = Constants.GetSqlDataValueNullForBaseValue(TblGlobalRatePurchaseTO.Comments);
            cmdInsert.Parameters.Add("@rateReasonId", System.Data.SqlDbType.Int).Value = Constants.GetSqlDataValueNullForBaseValue(TblGlobalRatePurchaseTO.RateReasonId);

            if (cmdInsert.ExecuteNonQuery() == 1)
            {
                cmdInsert.CommandText = Constants.IdentityColumnQuery;
                TblGlobalRatePurchaseTO.IdGlobalRatePurchase = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return 1;
            }
            else return 0;
        }

        public int InsertTblPurchaseQuota(TblPurchaseQuotaTO TblPurchaseQuotaTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                cmdInsert.Connection = conn;
                cmdInsert.Transaction = tran;
                return ExecuteInsertionCommandforPurchaseQuota(TblPurchaseQuotaTO, cmdInsert);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                cmdInsert.Dispose();
            }
        }

        public int ExecuteInsertionCommandforPurchaseQuota(TblPurchaseQuotaTO TblPurchaseQuotaTO, SqlCommand cmdInsert)
        {
            
            String sqlQuery = @" INSERT INTO [tblPurchaseQuota]( " +
                            "  [quotaQty]" +
                            " ,[pendingQty]" +
                            " ,[CreatedOn]" +
                            " ,[createdBy]" +
                            " ,[quotaReasonId]" +
                            " ,[isActive]" +
                            " )" +
                " VALUES (" +
                            "  @QuotaQty " +
                            " ,@PendingQty " +
                            " ,@CreatedOn " +
                            " ,@CreatedBy " +
                            " ,@QuotaReasonId " +
                            " ,@isActive " +
                            " )";

            cmdInsert.CommandText = sqlQuery;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            cmdInsert.Parameters.Add("@QuotaQty", System.Data.SqlDbType.Decimal).Value = TblPurchaseQuotaTO.QuotaQty;
            cmdInsert.Parameters.Add("@PendingQty", System.Data.SqlDbType.Decimal).Value = TblPurchaseQuotaTO.PendingQty;
            cmdInsert.Parameters.Add("@CreatedOn", System.Data.SqlDbType.DateTime).Value = TblPurchaseQuotaTO.CreatedOn;            
            cmdInsert.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int).Value = TblPurchaseQuotaTO.CreatedBy;
            cmdInsert.Parameters.Add("@QuotaReasonId", System.Data.SqlDbType.Int).Value = TblPurchaseQuotaTO.QuotaReasonId;
            cmdInsert.Parameters.Add("@isActive", System.Data.SqlDbType.Int).Value = TblPurchaseQuotaTO.IsActive;
           
            if (cmdInsert.ExecuteNonQuery() == 1)
            {
                cmdInsert.CommandText = Constants.IdentityColumnQuery;
                TblPurchaseQuotaTO.IdQuota = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return 1;
            }
            else return 0;
        }
        public int InsertTblPurchaseQuotaDetails(TblPurchaseQuotaDetailsTO TblPurchaseQuotaDetailsTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                cmdInsert.Connection = conn;
                cmdInsert.Transaction = tran;
                return ExecuteInsertionCommandforPurchaseQuotaDetails(TblPurchaseQuotaDetailsTO, cmdInsert);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                cmdInsert.Dispose();
            }
        }

        public int ExecuteInsertionCommandforPurchaseQuotaDetails(TblPurchaseQuotaDetailsTO TblPurchaseQuotaDetailsTO, SqlCommand cmdInsert)
        {

            String sqlQuery = @" INSERT INTO [tblPurchaseQuotaDetails]( " +

                           " [quotaId]" +
                            " ,[purchaseManagerId]" +
                            "  ,[quotaQty]" +
                            " ,[pendingQty]" + 
                             " ,[isActive]" +
                            " , [createdOn]" +
                            " )" +
                " VALUES (" +
                            "  @QuotaId " +
                            " ,@PurchaseManagerId " +
                            " ,@QuotaQty " +
                            " ,@PendingQty " +                           
                            " ,@isActive " +
                            " ,@CreatedOn " +
                            " )";

            cmdInsert.CommandText = sqlQuery;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.Parameters.Add("@QuotaId", System.Data.SqlDbType.Int).Value = TblPurchaseQuotaDetailsTO.QuotaId;
            cmdInsert.Parameters.Add("@PurchaseManagerId", System.Data.SqlDbType.Int).Value = TblPurchaseQuotaDetailsTO.PurchaseManagerId;
            cmdInsert.Parameters.Add("@QuotaQty", System.Data.SqlDbType.Decimal).Value = TblPurchaseQuotaDetailsTO.QuotaQty;
            cmdInsert.Parameters.Add("@PendingQty", System.Data.SqlDbType.Decimal).Value = TblPurchaseQuotaDetailsTO.PendingQty;
           cmdInsert.Parameters.Add("@isActive", System.Data.SqlDbType.Int).Value = TblPurchaseQuotaDetailsTO.IsActive;
            cmdInsert.Parameters.Add("@CreatedOn", System.Data.SqlDbType.DateTime).Value = TblPurchaseQuotaDetailsTO.CreatedOn;

            if (cmdInsert.ExecuteNonQuery() == 1)
            {
                cmdInsert.CommandText = Constants.IdentityColumnQuery;
                TblPurchaseQuotaDetailsTO.IdQuotaDetails = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return 1;
            }
            else return 0;
        }


        public int UpdateTransferPurchaseQuota(TblPurchaseQuotaDetailsTO TblPurchaseQuotaDetailsTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                cmdInsert.Connection = conn;
                cmdInsert.Transaction = tran;
                return ExecuteUpdateCommandforTransferPurchaseQuota(TblPurchaseQuotaDetailsTO, cmdInsert);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                cmdInsert.Dispose();
            }
        }

        public int ExecuteUpdateCommandforTransferPurchaseQuota(TblPurchaseQuotaDetailsTO TblPurchaseQuotaDetailsTO, SqlCommand cmdInsert)
        {
            
            string strTransferQty = Convert.ToString(TblPurchaseQuotaDetailsTO.TransferQty);
            string strtransferpurchaseManagerId = Convert.ToString(TblPurchaseQuotaDetailsTO.PurchaseManagerSourceId);

        
            string sql1 = "BEGIN update D set " +
                " D.pendingQty = D.pendingQty + " + Convert.ToDecimal(TblPurchaseQuotaDetailsTO.TransferQty) + " " +
                  ", D.quotaQty = D.quotaQty + " + Convert.ToDecimal(TblPurchaseQuotaDetailsTO.TransferQty) + " " +
                " , D.transferedBy = " + Convert.ToInt32(TblPurchaseQuotaDetailsTO.TransferedBy) + "  " +
                ", D.transferQtyStr = (case when isnull(D.transferQtyStr,'') <> '' then isnull(D.transferQtyStr,'')  + ',' +  '" + Convert.ToString(strTransferQty) + "' else  '" + Convert.ToString(strTransferQty) + "'  end ) " +
                 ", D.transferpurchaseManagerIdStr = (case when isnull(D.transferpurchaseManagerIdStr,'') <> '' then isnull(transferpurchaseManagerIdStr,'')  +',' + '" + Convert.ToString(strtransferpurchaseManagerId) + "' else  '" + Convert.ToString(strtransferpurchaseManagerId) + "'   end ) " +
               " from tblPurchaseQuotaDetails D inner join tblPurchaseQuota H on H.idQuota = D.quotaId  " +
                          "   where cast (H.createdOn as date )= cast(getdate() as date)    " +
                 "  and D.purchasemanagerId = " + Convert.ToInt32(TblPurchaseQuotaDetailsTO.PurchaseManagerDesnId) + " " +
                 "  and H.isactive =" + Convert.ToInt32(TblPurchaseQuotaDetailsTO.IsActive) + " ;",
              
               sql2 = "update D set " +
               "D.pendingQty = D.pendingQty - " + Convert.ToDecimal(TblPurchaseQuotaDetailsTO.TransferQty) + "" +
                ",D.quotaQty = D.quotaQty - " + Convert.ToDecimal(TblPurchaseQuotaDetailsTO.TransferQty) + "" +
               " ,D.transferedBy=" + Convert.ToInt32(TblPurchaseQuotaDetailsTO.TransferedBy) + "" +
                " from tblPurchaseQuotaDetails D inner join tblPurchaseQuota H on H.idQuota = D.quotaId  " +
               " where cast (H.createdOn as date )= cast(getdate() as date)  " +
               "and D.purchasemanagerId = " + Convert.ToInt32(TblPurchaseQuotaDetailsTO.PurchaseManagerSourceId) + "" +
               "and H.isactive =  " + Convert.ToInt32(TblPurchaseQuotaDetailsTO.IsActive) + "; END;";

            string sqlQuery = string.Format("{0}{1}", sql1, sql2);

            cmdInsert.CommandText = sqlQuery;
           
            if (cmdInsert.ExecuteNonQuery() == 2)
            {
                return 1;
            }
            else return 0;
        }

        public List<TblPurchaseQuotaTO> SelectLatestPurchaseQuota(DateTime date)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            try
            {
                conn.Open();
              
               
                    cmdSelect.CommandText = "select * from tblPurchaseQuota where cast (createdOn as date )=cast(@date as date)  and isactive=1 ORDER BY idQuota DESC"; //,Rate_Band_Costing";

                    cmdSelect.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = date;

                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                SqlDataReader TblPurchaseQuotaTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseQuotaTO> list = ConvertDTToPurchaseQuotaList(TblPurchaseQuotaTODT);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (sqlDataReader != null) sqlDataReader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public List<TblPurchaseQuotaTO> SelectLatestPurchaseQuotaData(DateTime date)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            try
            {
                conn.Open();


                cmdSelect.CommandText = "select * from tblPurchaseQuota where cast (createdOn as date )=cast(@date as date)  "; //,Rate_Band_Costing";

                cmdSelect.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = date;

                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                SqlDataReader TblPurchaseQuotaTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseQuotaTO> list = ConvertDTToPurchaseQuotaList(TblPurchaseQuotaTODT);
                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (sqlDataReader != null) sqlDataReader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }
        }
        public List<TblPurchaseQuotaTO> ConvertDTToPurchaseQuotaList(SqlDataReader TblPurchaseQuotaTODT)
        {
            List<TblPurchaseQuotaTO> TblPurchaseQuotaTOList = new List<TblPurchaseQuotaTO>();
            if (TblPurchaseQuotaTODT != null)
            {
                while (TblPurchaseQuotaTODT.Read())
                {
                    TblPurchaseQuotaTO TblPurchaseQuotaTONew = new TblPurchaseQuotaTO();
                    if (TblPurchaseQuotaTODT["idQuota"] != DBNull.Value)
                        TblPurchaseQuotaTONew.IdQuota = Convert.ToInt32(TblPurchaseQuotaTODT["idQuota"]);
                    if (TblPurchaseQuotaTODT["createdBy"] != DBNull.Value)
                        TblPurchaseQuotaTONew.CreatedBy = Convert.ToInt32(TblPurchaseQuotaTODT["createdBy"]);
                    if (TblPurchaseQuotaTODT["createdOn"] != DBNull.Value)
                        TblPurchaseQuotaTONew.CreatedOn = Convert.ToDateTime(TblPurchaseQuotaTODT["createdOn"]);
                    if (TblPurchaseQuotaTODT["quotaQty"] != DBNull.Value)
                        TblPurchaseQuotaTONew.QuotaQty = Convert.ToDouble(TblPurchaseQuotaTODT["quotaQty"]);
                    if (TblPurchaseQuotaTODT["pendingQty"] != DBNull.Value)
                        TblPurchaseQuotaTONew.PendingQty = Convert.ToDouble(TblPurchaseQuotaTODT["pendingQty"]);
                    if (TblPurchaseQuotaTODT["quotaReasonId"] != DBNull.Value)
                        TblPurchaseQuotaTONew.QuotaReasonId = Convert.ToInt32(TblPurchaseQuotaTODT["quotaReasonId"]);
                   
                    TblPurchaseQuotaTOList.Add(TblPurchaseQuotaTONew);
                }
            }
            return TblPurchaseQuotaTOList;
        }
        public List<TblPurchaseQuotaDetailsTO> SelectPurchaseManagerWithQuota(DateTime date)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            try
            {
                conn.Open();


                cmdSelect.CommandText = "SELECT  userDtl.userDisplayName as purchaseManager ,QuotaDetails.* " +
                                           " FROM tblUser userDtl " +
                                           " LEFT JOIN tblUserRole userRole " +
                                           " ON userDtl.idUser = userRole.userId AND userDtl.isActive = 1 " +
                                           " AND userRole.isActive = 1 " +
                                           " LEFT JOIN tblRole Role ON Role.idRole = userRole.roleId " +
                                           " left join tblPurchaseQuotaDetails QuotaDetails on QuotaDetails.purchaseManagerId = userDtl.idUser " +
                                           " inner join tblPurchaseQuota tblPurchaseQuota on tblPurchaseQuota.idQuota = quotaId " +
                                           " WHERE cast(tblPurchaseQuota.createdOn as date )= cast(getdate() as date) " +
                                           " and tblPurchaseQuota.isActive = 1" +
                                           " AND QuotaDetails.quotaId IN (SELECT TOP 1 quotaId FROM tblPurchaseQuotaDetails ORDER BY  quotaId DESC)" +
                                           " ORDER BY QuotaDetails.quotaId DESC "; 

                cmdSelect.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = date;

                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                SqlDataReader TblPurchaseQuotaDetailsTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseQuotaDetailsTO> list = ConvertDTToPurchaseQuotaDetailsList(TblPurchaseQuotaDetailsTODT);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (sqlDataReader != null) sqlDataReader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }


        }

        public List<TblPurchaseQuotaDetailsTO> SelectPurchaseManagerWithQuotaData(DateTime date,int purchaseManagerId)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            try
            {
                conn.Open();


                cmdSelect.CommandText = "SELECT  userDtl.userDisplayName as purchaseManager ,QuotaDetails.* " +
                                           " FROM tblUser userDtl " +
                                           " LEFT JOIN tblUserRole userRole " +
                                           " ON userDtl.idUser = userRole.userId AND userDtl.isActive = 1 " +
                                           " AND userRole.isActive = 1 " +
                                           " LEFT JOIN tblRole Role ON Role.idRole = userRole.roleId " +
                                           " left join tblPurchaseQuotaDetails QuotaDetails on QuotaDetails.purchaseManagerId = userDtl.idUser " +
                                           " inner join tblPurchaseQuota tblPurchaseQuota on tblPurchaseQuota.idQuota = quotaId " +
                                           " WHERE cast(tblPurchaseQuota.createdOn as date )= cast(getdate() as date) " +
                                           " and QuotaDetails.purchaseManagerId="+purchaseManagerId+" ";

                cmdSelect.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = date;

                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;
                SqlDataReader TblPurchaseQuotaDetailsTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseQuotaDetailsTO> list = ConvertDTToPurchaseQuotaDetailsList(TblPurchaseQuotaDetailsTODT);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (sqlDataReader != null) sqlDataReader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }


        }
        public List<TblPurchaseQuotaDetailsTO> ConvertDTToPurchaseQuotaDetailsList(SqlDataReader TblPurchaseQuotaDetailsTODT)
        {
            List<TblPurchaseQuotaDetailsTO> TblPurchaseQuotaDetailsTOList = new List<TblPurchaseQuotaDetailsTO>();
            if (TblPurchaseQuotaDetailsTODT != null)
            {
                while (TblPurchaseQuotaDetailsTODT.Read())
                {
                    TblPurchaseQuotaDetailsTO TblPurchaseQuotaDetailsTONew = new TblPurchaseQuotaDetailsTO();
                    if (TblPurchaseQuotaDetailsTODT["idQuotaDetails"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.IdQuotaDetails = Convert.ToInt32(TblPurchaseQuotaDetailsTODT["idQuotaDetails"]);
                    if (TblPurchaseQuotaDetailsTODT["quotaId"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.QuotaId = Convert.ToInt32(TblPurchaseQuotaDetailsTODT["quotaId"]);
                    if (TblPurchaseQuotaDetailsTODT["transferedBy"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.TransferedBy = Convert.ToInt32(TblPurchaseQuotaDetailsTODT["transferedBy"]);
                    if (TblPurchaseQuotaDetailsTODT["createdOn"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.CreatedOn = Convert.ToDateTime(TblPurchaseQuotaDetailsTODT["createdOn"]);
                    if (TblPurchaseQuotaDetailsTODT["quotaQty"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.QuotaQty = Convert.ToDouble(TblPurchaseQuotaDetailsTODT["quotaQty"]);
                    if (TblPurchaseQuotaDetailsTODT["pendingQty"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.PendingQty = Convert.ToDouble(TblPurchaseQuotaDetailsTODT["pendingQty"]);
                    if (TblPurchaseQuotaDetailsTODT["purchaseManagerId"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.PurchaseManagerId = Convert.ToInt32(TblPurchaseQuotaDetailsTODT["purchaseManagerId"]);
                    if (TblPurchaseQuotaDetailsTODT["purchaseManager"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.PurchaseManager = Convert.ToString(TblPurchaseQuotaDetailsTODT["purchaseManager"]);
                    if (TblPurchaseQuotaDetailsTODT["transferpurchaseManagerIdStr"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.TransferpurchaseManagerIdStr = Convert.ToString(TblPurchaseQuotaDetailsTODT["transferpurchaseManagerIdStr"]);
                    if (TblPurchaseQuotaDetailsTODT["transferQtyStr"] != DBNull.Value)
                        TblPurchaseQuotaDetailsTONew.TransferQtyStr = Convert.ToString(TblPurchaseQuotaDetailsTODT["transferQtyStr"]);
                    
                    TblPurchaseQuotaDetailsTOList.Add(TblPurchaseQuotaDetailsTONew);
                }
            }
            return TblPurchaseQuotaDetailsTOList;
        }
        public Boolean IsCheckForTodaysQuotaDeclaration(DateTime date)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader TblPurchaseQuotaTODT = null;
            try
            {
                conn.Open();
                cmdSelect.CommandText = "SELECT COUNT(*) AS todayCount FROM tblPurchaseQuota " + " WHERE DAY(createdOn)=" + date.Day + " AND MONTH(createdOn)=" + date.Month + " AND YEAR(createdOn)=" + date.Year + " AND isnull(pendingQty,0) > 0 and isnull(isActive,0)= 1 ";
                cmdSelect.Connection = conn;               
                cmdSelect.CommandType = System.Data.CommandType.Text;

                TblPurchaseQuotaTODT = cmdSelect.ExecuteReader(CommandBehavior.Default);

                if (TblPurchaseQuotaTODT != null)
                {
                    while (TblPurchaseQuotaTODT.Read())
                    {
                        if (TblPurchaseQuotaTODT[0] != DBNull.Value)
                        {
                            if (Convert.ToInt32(TblPurchaseQuotaTODT[0]) > 0)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {               
                if (TblPurchaseQuotaTODT != null)
                    TblPurchaseQuotaTODT.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        #endregion

    }
}
