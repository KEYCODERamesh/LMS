using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace HospitalManagementSystem
{
    public class MyLabstring
    {
        public static SqlConnection con = new SqlConnection();
        public static SqlConnection Servercon = new SqlConnection();
        public bool IsConnectedToInternet()
        {
            string host = "google.com"; ;
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }

        public static SqlConnection opencon()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LabCompanyContext"].ConnectionString;
            con.Open();
            return con;
        }

        public static SqlConnection close()
        {
            con.Close();
            return con;
        }
    }
}