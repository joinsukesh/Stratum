namespace Stratum.Feature.AdminPages.DbFactory
{
    using Sitecore.Diagnostics;
    using Stratum.Foundation.Common;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class AppLogsDb
    {
        public DataTable GetAppLogs(string appName, DateTime fromDate, DateTime toDate, List<string> logLevels)
        {
            DataTable dtData = null;
            DataSet dsResult = new DataSet();
            using (SqlConnection con = new SqlConnection(CommonConstants.ConnectionStrings.App_Logs))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAppLogs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@app_name", SqlDbType.VarChar).Value = appName;
                    cmd.Parameters.AddWithValue("@log_levels", SqlDbType.VarChar).Value = (logLevels != null && logLevels.Count > 0) ? string.Join(CommonConstants.Characters.CommaStr, logLevels) : string.Empty;
                    cmd.Parameters.AddWithValue("@start_date", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.AddWithValue("@end_date", SqlDbType.DateTime).Value = toDate;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsResult);
                }
            }
            if (dsResult != null && dsResult.Tables != null && dsResult.Tables.Count > 0 &&
                dsResult.Tables[0].Rows != null && dsResult.Tables[0].Rows.Count > 0)
            {
                dtData = dsResult.Tables[0];
            }

            return dtData;
        }

        public void DeleteLogs(List<string> logIds)
        {
            using (SqlConnection con = new SqlConnection(CommonConstants.ConnectionStrings.App_Logs))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DeleteLogs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@log_Ids", SqlDbType.VarChar).Value = (logIds != null && logIds.Count > 0) ? string.Join(CommonConstants.Characters.CommaStr, logIds) : string.Empty;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}