using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InventoryDAL
{
    public class DBHelper
    {
        public const string Param_DataSource = "data source";
        public const string Param_Database = "initial catalog";
        public const string Param_UserId = "user id";
        public const string Param_Password = "password";

        public OP GetDBAccess()
        {
            OP objOP = new OP();
            try
            {

              
                var connectionString = ConfigurationManager.ConnectionStrings["InventeryDetails"].ConnectionString;

                objOP.DB_DataSource = GetComponentFromConnectionString(connectionString, Param_DataSource);
                objOP.DB_InitialCatalog = GetComponentFromConnectionString(connectionString, Param_Database);
                objOP.DB_UserID = GetComponentFromConnectionString(connectionString, Param_UserId);
                objOP.DB_Password = GetComponentFromConnectionString(connectionString, Param_Password);

               
            }
            catch
            {

            }
            return objOP;
        }

        private string GetComponentFromConnectionString(string connectionString, string component, char seperator = '=')
        {
            if (connectionString == null || component == null)
                return "";

            var connectionData = connectionString.Split(';');
            var componentData = connectionData.FirstOrDefault(x => x.IndexOf(component, StringComparison.CurrentCultureIgnoreCase) >= 0);

            if (componentData?.Contains(seperator) == true)
                return componentData.Split(seperator)[1];

            return "";
        }

        public class OP
        {
            public string DB_DataSource { get; set; }
            public string DB_InitialCatalog { get; set; }
            public string DB_UserID { get; set; }
            public string DB_Password { get; set; }
        }

    }
}