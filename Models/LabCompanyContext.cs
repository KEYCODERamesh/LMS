using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HospitalManagementSystem.Models;
using System.Configuration;
using Dapper;
namespace HospitalManagementSystem.Models
{
    public class LabCompanyContext : DbContext
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LabCompanyContext"].ToString());
      
        public DbSet<LabCompanyDetails> labCompanyDetails { get; set; }
        public DbSet<TestGroupModel> testgroups { get; set; }
        public DbSet<ReferalDetails> referals { get; set; }
        public DbSet<LoginModels> userslist { get; set; }
        public DbSet<UsersRole> usersroles { get; set; }
        public DbSet<VendorModel> vendordetails { get; set; }
        public DbSet<LabItems> Products { get; set; }
      
        public DbSet<MyLabTestList> labtestlits { get; set; }
        public DbSet<MyUnits> unitlist { get; set; }
        public DbSet<TestParametsrsModels> parametrs { get; set; }
        public DbSet<MyTestPanels> mytestpanels { get; set; }
        // public DbSet<AccountsModels> accountslist { get; set; }

        // public System.Data.Entity.DbSet<HospitalManagementSystem.Models.VendorModel> VendorModels { get; set; }

      
        public DataTable SearchDataTables(string OPERATIONS,string CONDI1,string CONDI2,string CONDI3,string CONDI4,string CONDI5)
        {
            SqlCommand cmd = new SqlCommand("SP_Search", MyLabstring.opencon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Search";
            cmd.Parameters.AddWithValue("@OPERATION", OPERATIONS);
            cmd.Parameters.AddWithValue("@CONDI1", CONDI1);
            cmd.Parameters.AddWithValue("@CONDI2", CONDI2);
            cmd.Parameters.AddWithValue("@CONDI3", CONDI3);
            cmd.Parameters.AddWithValue("@CONDI4", CONDI4);
            cmd.Parameters.AddWithValue("@CONDI5", CONDI5);
            DataTable   DT = new DataTable();
            DT.Load(cmd.ExecuteReader());



            return DT;
        }

        public IEnumerable<LoginModels> GetLoginList()
        {
            string query = "SELECT DISTINCT [USER_ID] AS USER_ID,LoginID FROM User_DT ORDER BY LoginID ASC";
            var result = con.Query<LoginModels>(query);

            return result;
        }

        public List<LabItems> GetCustomers()
        {
            List<LabItems> LabItems = new List<LabItems>();
            string sqltxt = "SELECT * FROM LabITEMDT ";
            SqlCommand cmd = new SqlCommand(sqltxt, MyLabstring.opencon());
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                LabItems.Add(
                    new LabItems
                    {
                        ITEMID = Convert.ToInt32(dr["ITEMID"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        OPQTY = Convert.ToDecimal(dr["OPQTY"])
                    });
            }
            return LabItems;
        }


        public IEnumerable<MyLabTestList> GetGroupList()
        {
            //string query = "SELECT [Group_ID],[Group_Name]FROM [GroupDT].[dbo].[Mobiledata]";
            string query = "SELECT [Group_ID],[Group_Name]FROM [GroupDT]";
            var result = con.Query<MyLabTestList>(query);

            return result;
        }

        public IEnumerable<TestParametsrsModels> TestGroupsGroupList()
        {
            //string query = "SELECT [Group_ID],[Group_Name]FROM [GroupDT].[dbo].[Mobiledata]";
            string query = "SELECT [Group_ID],[Group_Name]FROM [GroupDT]";
            var result = con.Query<TestParametsrsModels>(query);

            return result;
        }
        public IEnumerable<TestParametsrsModels> GetTestList()
        {
            //string query = "SELECT [Group_ID],[Group_Name]FROM [GroupDT].[dbo].[Mobiledata]";
            string query = "SELECT Test_ID,Test_Name FROM TestDT";
            var result = con.Query<TestParametsrsModels>(query);

            return result;
        }

        public System.Data.Entity.DbSet<HospitalManagementSystem.Models.AccountsModels> AccountsModels { get; set; }

        public System.Data.Entity.DbSet<HospitalManagementSystem.Models.TestReportModel> TestReportModels { get; set; }
    }
}