namespace Stratum.Feature.AdminPages.DbFactory
{
    using Stratum.Foundation.Common;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ScFormReportDb
    {
        public DataTable GetFormData(Guid formId, DateTime fromDate, DateTime toDate)
        {
            DataTable dtData = null;
            DataSet dsResult = new DataSet();
            SqlConnection con = new SqlConnection(CommonConstants.ConnectionStrings.ExperienceForms);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_GetSitecoreFormData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormId", SqlDbType.UniqueIdentifier).Value = formId;
                cmd.Parameters.AddWithValue("@StartDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.AddWithValue("@EndDate", SqlDbType.DateTime).Value = toDate;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dsResult);                
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
                con.Close();                
            }

            if (dsResult != null && dsResult.Tables != null && dsResult.Tables.Count > 0 &&
                dsResult.Tables[0].Rows != null && dsResult.Tables[0].Rows.Count > 0)
            {
                dtData = dsResult.Tables[0];
            }

            return dtData;

        }
    }
}