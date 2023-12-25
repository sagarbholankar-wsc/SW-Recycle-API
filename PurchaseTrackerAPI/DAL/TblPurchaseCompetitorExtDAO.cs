using PurchaseTrackerAPI.BL.Interfaces;
using PurchaseTrackerAPI.DAL.Interfaces;
using PurchaseTrackerAPI.DashboardModels;
using PurchaseTrackerAPI.Models;
using PurchaseTrackerAPI.StaticStuff;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseTrackerAPI.DAL
{
    public class TblPurchaseCompetitorExtDAO : ITblPurchaseCompetitorExtDAO
    {
        private readonly IConnectionString _iConnectionString;
        public TblPurchaseCompetitorExtDAO(IConnectionString iConnectionString)
        {
            _iConnectionString = iConnectionString;
        }
        #region Methods
        public  String SqlSelectQuery()
        {
            String sqlSelectQry = " SELECT tblPurchaseCompetitorExt.* FROM dbo.[tblPurchaseCompetitorExt] tblPurchaseCompetitorExt " +
                                  " left join tblOrganization tblOrganization on tblOrganization.idOrganization= tblPurchaseCompetitorExt.organizationId "; 
            return sqlSelectQry;
        }
        #endregion

        #region Selection
        public  List<TblPurchaseCompetitorExtTO> SelectAllTblPurchaseCompetitorExt()
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery();
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                //cmdSelect.Parameters.Add("@idPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
               SqlDataReader sqlReader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseCompetitorExtTO> list = ConvertDTToList(sqlReader);
                sqlReader.Dispose();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }
         public  List<TblPurchaseCompetitorExtTO> GetMaterialListByCompetitorId(Int32 competitorId)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery() + " where tblPurchaseCompetitorExt.organizationId=" + competitorId;
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                //cmdSelect.Parameters.Add("@idPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
               SqlDataReader sqlReader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseCompetitorExtTO> list = ConvertDTToList(sqlReader);
                sqlReader.Dispose();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public  List<TblPurchaseCompetitorExtTO> SelectTblPurchaseCompetitorExt(Int32 idPurCompetitorExt)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                conn.Open();
                cmdSelect.CommandText = SqlSelectQuery()+ " WHERE idPurCompetitorExt = " + idPurCompetitorExt +" ";
                cmdSelect.Connection = conn;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                //cmdSelect.Parameters.Add("@idPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
                SqlDataReader sqlReader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseCompetitorExtTO> list = ConvertDTToList(sqlReader);
                sqlReader.Dispose();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                cmdSelect.Dispose();
            }
        }

        public  List<TblPurchaseCompetitorExtTO> SelectAllTblPurchaseCompetitorExt(SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdSelect = new SqlCommand();
            try
            {
                cmdSelect.CommandText = SqlSelectQuery();
                cmdSelect.Connection = conn;
                cmdSelect.Transaction = tran;
                cmdSelect.CommandType = System.Data.CommandType.Text;

                //cmdSelect.Parameters.Add("@idPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
                SqlDataReader sqlReader = cmdSelect.ExecuteReader(CommandBehavior.Default);
                List<TblPurchaseCompetitorExtTO> list = ConvertDTToList(sqlReader);
                sqlReader.Dispose();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                cmdSelect.Dispose();
            }
        }

 public  List<TblPurchaseCompetitorExtTO> ConvertDTToList(SqlDataReader tblPurchaseCompetitorExtTODT )
        {
            List<TblPurchaseCompetitorExtTO> tblPurchaseCompetitorExtTOList = new List<TblPurchaseCompetitorExtTO>();
            if (tblPurchaseCompetitorExtTODT != null)
            {
                while (tblPurchaseCompetitorExtTODT.Read())
                {
                    TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTONew = new TblPurchaseCompetitorExtTO();
                    if(tblPurchaseCompetitorExtTODT["idPurCompetitorExt"] != DBNull.Value)
                        tblPurchaseCompetitorExtTONew.IdPurCompetitorExt = Convert.ToInt32( tblPurchaseCompetitorExtTODT["idPurCompetitorExt"].ToString());
                    if(tblPurchaseCompetitorExtTODT["organizationId"] != DBNull.Value)
                        tblPurchaseCompetitorExtTONew.OrganizationId = Convert.ToInt32( tblPurchaseCompetitorExtTODT["organizationId"].ToString());
                    if(tblPurchaseCompetitorExtTODT["materialType"] != DBNull.Value)
                        tblPurchaseCompetitorExtTONew.MaterialType = Convert.ToString( tblPurchaseCompetitorExtTODT["materialType"].ToString());
                    if(tblPurchaseCompetitorExtTODT["materialGrade"] != DBNull.Value)
                        tblPurchaseCompetitorExtTONew.MaterialGrade = Convert.ToString( tblPurchaseCompetitorExtTODT["materialGrade"].ToString());
                    tblPurchaseCompetitorExtTOList.Add(tblPurchaseCompetitorExtTONew);
                }
            }
            return tblPurchaseCompetitorExtTOList;
        }
        #endregion
        
        #region Insertion
        public  int InsertTblPurchaseCompetitorExt(TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTO)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                conn.Open();
                cmdInsert.Connection = conn;
                return ExecuteInsertionCommand(tblPurchaseCompetitorExtTO, cmdInsert);
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                conn.Close();
                cmdInsert.Dispose();
            }
        }

        public  int InsertTblPurchaseCompetitorExt(TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdInsert = new SqlCommand();
            try
            {
                cmdInsert.Connection = conn;
                cmdInsert.Transaction = tran;
                return ExecuteInsertionCommand(tblPurchaseCompetitorExtTO, cmdInsert);
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                cmdInsert.Dispose();
            }
        }

        public  int ExecuteInsertionCommand(TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTO, SqlCommand cmdInsert)
        {
            String sqlQuery = @" INSERT INTO [tblPurchaseCompetitorExt]( " + 
            //"  [idPurCompetitorExt]" +
            "  [organizationId]" +
            " ,[materialType]" +
            " ,[materialGrade]" +
            " )" +
" VALUES (" +
            //"  @IdPurCompetitorExt " +
            "  @OrganizationId " +
            " ,@MaterialType " +
            " ,@MaterialGrade " + 
            " )";
            cmdInsert.CommandText = sqlQuery;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            //cmdInsert.Parameters.Add("@IdPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
            cmdInsert.Parameters.Add("@OrganizationId", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.OrganizationId;
            cmdInsert.Parameters.Add("@MaterialType", System.Data.SqlDbType.NVarChar).Value = tblPurchaseCompetitorExtTO.MaterialType;
            cmdInsert.Parameters.Add("@MaterialGrade", System.Data.SqlDbType.NVarChar).Value = tblPurchaseCompetitorExtTO.MaterialGrade;
            return cmdInsert.ExecuteNonQuery();
        }
        #endregion
        
        #region Updation
        public  int UpdateTblPurchaseCompetitorExt(TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTO)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdUpdate = new SqlCommand();
            try
            {
                conn.Open();
                cmdUpdate.Connection = conn;
                return ExecuteUpdationCommand(tblPurchaseCompetitorExtTO, cmdUpdate);
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                conn.Close();
                cmdUpdate.Dispose();
            }
        }

        public  int UpdateTblPurchaseCompetitorExt(TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTO, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdUpdate = new SqlCommand();
            try
            {
                cmdUpdate.Connection = conn;
                cmdUpdate.Transaction = tran;
                return ExecuteUpdationCommand(tblPurchaseCompetitorExtTO, cmdUpdate);
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                cmdUpdate.Dispose();
            }
        }

        public  int ExecuteUpdationCommand(TblPurchaseCompetitorExtTO tblPurchaseCompetitorExtTO, SqlCommand cmdUpdate)
        {
            String sqlQuery = @" UPDATE [tblPurchaseCompetitorExt] SET " + 
            //"  [idPurCompetitorExt] = @IdPurCompetitorExt" +
            "  [organizationId]= @OrganizationId" +
            " ,[materialType]= @MaterialType" +
            " ,[materialGrade] = @MaterialGrade" +
            " WHERE idPurCompetitorExt = @IdPurCompetitorExt "; 

            cmdUpdate.CommandText = sqlQuery;
            cmdUpdate.CommandType = System.Data.CommandType.Text;

            cmdUpdate.Parameters.Add("@IdPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
            cmdUpdate.Parameters.Add("@OrganizationId", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.OrganizationId;
            cmdUpdate.Parameters.Add("@MaterialType", System.Data.SqlDbType.NVarChar).Value = tblPurchaseCompetitorExtTO.MaterialType;
            cmdUpdate.Parameters.Add("@MaterialGrade", System.Data.SqlDbType.NVarChar).Value = tblPurchaseCompetitorExtTO.MaterialGrade;
            return cmdUpdate.ExecuteNonQuery();
        }
        #endregion
        
        #region Deletion
        public  int DeleteTblPurchaseCompetitorExt(Int32 idPurCompetitorExt)
        {
            String sqlConnStr = _iConnectionString.GetConnectionString(Constants.CONNECTION_STRING);
            SqlConnection conn = new SqlConnection(sqlConnStr);
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                conn.Open();
                cmdDelete.Connection = conn;
                return ExecuteDeletionCommand(idPurCompetitorExt, cmdDelete);
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                conn.Close();
                cmdDelete.Dispose();
            }
        }

        public  int DeleteTblPurchaseCompetitorExt(Int32 idPurCompetitorExt, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                cmdDelete.Connection = conn;
                cmdDelete.Transaction = tran;
                return ExecuteDeletionCommand(idPurCompetitorExt, cmdDelete);
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

        public  int ExecuteDeletionCommand(Int32 idPurCompetitorExt, SqlCommand cmdDelete)
        {
            cmdDelete.CommandText = "DELETE FROM [tblPurchaseCompetitorExt] " +
            " WHERE idPurCompetitorExt = " + idPurCompetitorExt +"";
            cmdDelete.CommandType = System.Data.CommandType.Text;

            //cmdDelete.Parameters.Add("@idPurCompetitorExt", System.Data.SqlDbType.Int).Value = tblPurchaseCompetitorExtTO.IdPurCompetitorExt;
            return cmdDelete.ExecuteNonQuery();
        }
        #endregion
        
    }
}
