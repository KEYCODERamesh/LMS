using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HospitalManagementSystem.Models
{
    public class SearchModels
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LabCompanyContext"].ToString());
        public DataSet GetDS(string operation,string condi1, string condi2, string condi3, string condi4, string condi5)
        {
            SqlCommand cmd = new SqlCommand("SP_SEARCH_LABORATORY", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_SEARCH_LABORATORY";
            cmd.Parameters.AddWithValue("@OPERATION",operation);
            cmd.Parameters.AddWithValue("@CONDI1", condi1);
            cmd.Parameters.AddWithValue("@CONDI2", condi2);
            cmd.Parameters.AddWithValue("@CONDI3", condi3);
            cmd.Parameters.AddWithValue("@CONDI4", condi4);
            cmd.Parameters.AddWithValue("@CONDI5", condi5);
            con.Open();
            DataSet DS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DS);

            return DS;
        }
    }
}