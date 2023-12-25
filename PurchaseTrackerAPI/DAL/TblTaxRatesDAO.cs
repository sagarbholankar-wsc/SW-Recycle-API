using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PurchaseTrackerAPI.Models;
using PurchaseTrackerAPI.StaticStuff;
using PurchaseTrackerAPI.BL.Interfaces;
using PurchaseTrackerAPI.DAL.Interfaces;

namespace PurchaseTrackerAPI.DAL
{
    public class TblTaxRatesDAO : ITblTaxRatesDAO
    {
        private readonly IConnectionString _iConnectionString;
        public TblTaxRatesDAO(IConnectionString iConnectionString)
        {
            _iConnectionString = iConnectionString;
        }
        #region Methods
        public  String SqlSelectQuery()
        {
            String sqlSelectQry = " SELECT * FROM [tblTaxRates]"; 
            return sqlSelectQry;
        }
        #endregion

        #region Selection
        public  List<TblTaxRatesTO> SelectAllTblTaxRates()
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery();
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                reader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblTaxRatesTO> list = ConvertDTToList(reader);
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public  List<TblTaxRatesTO> SelectAllTblTaxRates(int idGstCode,SqlConnection conn,SqlTransaction tran)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                cmdSelect.CommandText = SqlSelectQuery() + " WHERE isActive=1 AND gstCodeId=" + idGstCode;
                cmdSelect.Connection = conn;
                cmdSelect.Transaction= tran;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                reader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblTaxRatesTO> list = ConvertDTToList(reader);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Dispose();
                cmdSelect.Dispose();
            }
        }

        public List<TblTaxRatesTO> SelectAllTblTaxRatesAll(List<int> idGstCode, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                cmdSelect.CommandText = SqlSelectQuery() + " WHERE isActive=1 AND gstCodeId in ( " + string.Join(",", idGstCode.ToArray()) + " )";
                cmdSelect.Connection = conn;
                cmdSelect.Transaction = tran;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                reader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblTaxRatesTO> list = ConvertDTToList(reader);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Dispose();
                cmdSelect.Dispose();
            }
        }

        public  TblTaxRatesTO SelectTblTaxRates()
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery()+ "  ";
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                reader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblTaxRatesTO> list = ConvertDTToList(reader);
                if (list != null && list.Count == 1)
                    return list[0];

                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Dispose();
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public  List<TblTaxRatesTO> ConvertDTToList(SqlDataReader tblTaxRatesTODT)
        {
            List<TblTaxRatesTO> tblTaxRatesTOList = new List<TblTaxRatesTO>();
            if (tblTaxRatesTODT != null)
            {
                while (tblTaxRatesTODT.Read())
                {
                    TblTaxRatesTO tblTaxRatesTONew = new TblTaxRatesTO();
                    if (tblTaxRatesTODT["idTaxRate"] != DBNull.Value)
                        tblTaxRatesTONew.IdTaxRate = Convert.ToInt32(tblTaxRatesTODT["idTaxRate"].ToString());
                    if (tblTaxRatesTODT["gstCodeId"] != DBNull.Value)
                        tblTaxRatesTONew.GstCodeId = Convert.ToInt32(tblTaxRatesTODT["gstCodeId"].ToString());
                    if (tblTaxRatesTODT["taxTypeId"] != DBNull.Value)
                        tblTaxRatesTONew.TaxTypeId = Convert.ToInt32(tblTaxRatesTODT["taxTypeId"].ToString());
                    if (tblTaxRatesTODT["createdBy"] != DBNull.Value)
                        tblTaxRatesTONew.CreatedBy = Convert.ToInt32(tblTaxRatesTODT["createdBy"].ToString());
                    if (tblTaxRatesTODT["updatedBy"] != DBNull.Value)
                        tblTaxRatesTONew.UpdatedBy = Convert.ToInt32(tblTaxRatesTODT["updatedBy"].ToString());
                    if (tblTaxRatesTODT["effectiveFromDt"] != DBNull.Value)
                        tblTaxRatesTONew.EffectiveFromDt = Convert.ToDateTime(tblTaxRatesTODT["effectiveFromDt"].ToString());
                    if (tblTaxRatesTODT["effectiveToDt"] != DBNull.Value)
                        tblTaxRatesTONew.EffectiveToDt = Convert.ToDateTime(tblTaxRatesTODT["effectiveToDt"].ToString());
                    if (tblTaxRatesTODT["createdOn"] != DBNull.Value)
                        tblTaxRatesTONew.CreatedOn = Convert.ToDateTime(tblTaxRatesTODT["createdOn"].ToString());
                    if (tblTaxRatesTODT["updatedOn"] != DBNull.Value)
                        tblTaxRatesTONew.UpdatedOn = Convert.ToDateTime(tblTaxRatesTODT["updatedOn"].ToString());
                    if (tblTaxRatesTODT["taxPct"] != DBNull.Value)
                        tblTaxRatesTONew.TaxPct = Convert.ToDouble(tblTaxRatesTODT["taxPct"].ToString());
                    if (tblTaxRatesTODT["isActive"] != DBNull.Value)
                        tblTaxRatesTONew.IsActive = Convert.ToInt32(tblTaxRatesTODT["isActive"].ToString());
                    tblTaxRatesTOList.Add(tblTaxRatesTONew);
                }
            }
            return tblTaxRatesTOList;
        }

        #endregion

        #region Insertion
        public  int InsertTblTaxRates(TblTaxRatesTO tblTaxRatesTO)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                conn.Open();
                cmdInsert.Connection = conn;
                return ExecuteInsertionCommand(tblTaxRatesTO, cmdInsert);
            }
            catch(Exception ex)
            {
                return -1;
            }
            finally
            {
                conn.Close();
                cmdInsert.Dispose();
            }
        }

        public  int InsertTblTaxRates(TblTaxRatesTO tblTaxRatesTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                cmdInsert.Connection = conn;
                cmdInsert.Transaction = tran;
                return ExecuteInsertionCommand(tblTaxRatesTO, cmdInsert);
            }
            catch(Exception ex)
            {
                return -1;
            }
            finally
            {
                cmdInsert.Dispose();
            }
        }

        public  int ExecuteInsertionCommand(TblTaxRatesTO tblTaxRatesTO, SqlCommand cmdInsert)
        {
            String sqlQuery = @" INSERT INTO [tblTaxRates]( " + 
                                "  [gstCodeId]" +
                                " ,[taxTypeId]" +
                                " ,[createdBy]" +
                                " ,[updatedBy]" +
                                " ,[effectiveFromDt]" +
                                " ,[effectiveToDt]" +
                                " ,[createdOn]" +
                                " ,[updatedOn]" +
                                " ,[taxPct]" +
                                " ,[isActive]" +
                                " )" +
                    " VALUES (" +
                                "  @GstCodeId " +
                                " ,@TaxTypeId " +
                                " ,@CreatedBy " +
                                " ,@UpdatedBy " +
                                " ,@EffectiveFromDt " +
                                " ,@EffectiveToDt " +
                                " ,@CreatedOn " +
                                " ,@UpdatedOn " +
                                " ,@TaxPct " +
                                " ,@isActive " +
                                " )";
            cmdInsert.CommandText = sqlQuery;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            //cmdInsert.Parameters.Add("@IdTaxRate", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.IdTaxRate;
            cmdInsert.Parameters.Add("@GstCodeId", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.GstCodeId;
            cmdInsert.Parameters.Add("@TaxTypeId", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.TaxTypeId;
            cmdInsert.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.CreatedBy;
            cmdInsert.Parameters.Add("@UpdatedBy", System.Data.SqlDbType.Int).Value = Constants.GetSqlDataValueNullForBaseValue(tblTaxRatesTO.UpdatedBy);
            cmdInsert.Parameters.Add("@EffectiveFromDt", System.Data.SqlDbType.DateTime).Value = tblTaxRatesTO.EffectiveFromDt;
            cmdInsert.Parameters.Add("@EffectiveToDt", System.Data.SqlDbType.DateTime).Value = Constants.GetSqlDataValueNullForBaseValue(tblTaxRatesTO.EffectiveToDt);
            cmdInsert.Parameters.Add("@CreatedOn", System.Data.SqlDbType.DateTime).Value = tblTaxRatesTO.CreatedOn;
            cmdInsert.Parameters.Add("@UpdatedOn", System.Data.SqlDbType.DateTime).Value = Constants.GetSqlDataValueNullForBaseValue(tblTaxRatesTO.UpdatedOn);
            cmdInsert.Parameters.Add("@TaxPct", System.Data.SqlDbType.NVarChar).Value = tblTaxRatesTO.TaxPct;
            cmdInsert.Parameters.Add("@isActive", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.IsActive;
            if(cmdInsert.ExecuteNonQuery()==1)
            {
                cmdInsert.CommandText = Constants.IdentityColumnQuery;
                tblTaxRatesTO.IdTaxRate = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return 1;
            }

            return 0;
        }
        #endregion
        
        #region Updation
        public  int UpdateTblTaxRates(TblTaxRatesTO tblTaxRatesTO)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdUpdate = new SqlCommand();
            try
            {
                conn.Open();
                cmdUpdate.Connection = conn;
                return ExecuteUpdationCommand(tblTaxRatesTO, cmdUpdate);
            }
            catch(Exception ex)
            {
                return -1;
            }
            finally
            {
                conn.Close();
                cmdUpdate.Dispose();
            }
        }

        public  int UpdateTblTaxRates(TblTaxRatesTO tblTaxRatesTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdUpdate = new SqlCommand();
            try
            {
                cmdUpdate.Connection = conn;
                cmdUpdate.Transaction = tran;
                return ExecuteUpdationCommand(tblTaxRatesTO, cmdUpdate);
            }
            catch(Exception ex)
            {
                return -1;
            }
            finally
            {
                cmdUpdate.Dispose();
            }
        }

        public  int ExecuteUpdationCommand(TblTaxRatesTO tblTaxRatesTO, SqlCommand cmdUpdate)
        {
            String sqlQuery = @" UPDATE [tblTaxRates] SET " + 
                            "  [gstCodeId]= @GstCodeId" +
                            " ,[taxTypeId]= @TaxTypeId" +
                            " ,[updatedBy]= @UpdatedBy" +
                            " ,[effectiveFromDt]= @EffectiveFromDt" +
                            " ,[effectiveToDt]= @EffectiveToDt" +
                            " ,[updatedOn]= @UpdatedOn" +
                            " ,[taxPct] = @TaxPct" +
                            " ,[isActive] = @isActive" +
                            " WHERE [idTaxRate] = @IdTaxRate"; 

            cmdUpdate.CommandText = sqlQuery;
            cmdUpdate.CommandType = System.Data.CommandType.Text;

            cmdUpdate.Parameters.Add("@IdTaxRate", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.IdTaxRate;
            cmdUpdate.Parameters.Add("@GstCodeId", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.GstCodeId;
            cmdUpdate.Parameters.Add("@TaxTypeId", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.TaxTypeId;
            cmdUpdate.Parameters.Add("@UpdatedBy", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.UpdatedBy;
            cmdUpdate.Parameters.Add("@EffectiveFromDt", System.Data.SqlDbType.DateTime).Value = tblTaxRatesTO.EffectiveFromDt;
            cmdUpdate.Parameters.Add("@EffectiveToDt", System.Data.SqlDbType.DateTime).Value = Constants.GetSqlDataValueNullForBaseValue(tblTaxRatesTO.EffectiveToDt);
            cmdUpdate.Parameters.Add("@UpdatedOn", System.Data.SqlDbType.DateTime).Value = tblTaxRatesTO.UpdatedOn;
            cmdUpdate.Parameters.Add("@TaxPct", System.Data.SqlDbType.NVarChar).Value = tblTaxRatesTO.TaxPct;
            cmdUpdate.Parameters.Add("@isActive", System.Data.SqlDbType.Int).Value = tblTaxRatesTO.IsActive;
            return cmdUpdate.ExecuteNonQuery();
        }
        #endregion
        
        #region Deletion
        public  int DeleteTblTaxRates(Int32 idTaxRate)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                conn.Open();
                cmdDelete.Connection = conn;
                return ExecuteDeletionCommand(idTaxRate, cmdDelete);
            }
            catch(Exception ex)
            {
                return -1;
            }
            finally
            {
                conn.Close();
                cmdDelete.Dispose();
            }
        }

        public  int DeleteTblTaxRates(Int32 idTaxRate, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                cmdDelete.Connection = conn;
                cmdDelete.Transaction = tran;
                return ExecuteDeletionCommand(idTaxRate, cmdDelete);
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                cmdDelete.Dispose();
            }
        }

        public  int ExecuteDeletionCommand(Int32 idTaxRate, SqlCommand cmdDelete)
        {
            cmdDelete.CommandText = "DELETE FROM [tblTaxRates] " +
           " WHERE idTaxRate = " + idTaxRate + "";
            cmdDelete.CommandType = System.Data.CommandType.Text;

            return cmdDelete.ExecuteNonQuery();
        }
        #endregion
        
    }
}
