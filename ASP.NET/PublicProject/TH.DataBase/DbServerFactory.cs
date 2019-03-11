using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.DataBase
{
   public class DbServerFactory
    {
       public static ServerBase CreateDbServer(string provider)
       {
           ServerBase server = null;
           switch (provider)
           {
               case "System.Data.SqlClient":
                   server = new SqlServer.SqlDbServer() { Authentication = AuthenticationType.SQLService };
                   server.ProviderName = provider;
                   break;
               case "System.Data.OracleClient":
                   server = new Oracle.OracleDbServer() { };
                   server.ProviderName = provider;
                   break;
           }
           return server;
       }

       public static DataBaseBase CreateDataBase(ServerBase server,string name)
       {
           DataBaseBase database = null;
           switch (server.ProviderName)
           {
               case "System.Data.SqlClient":
                   database = new SqlServer.SqlDbDataBase(server, name);
                   break;
               case "System.Data.OracleClient":
                   database = new Oracle.OracleDbDataBase(server, name);
                   break;
           }
           return database;
       }

       public static TableViewBase CreateDataTable(DataBaseBase dataBase, string name)
       {
           TableViewBase table = null;
               var ll = (from o in dataBase.GetViews() where o.Name == name select o).FirstOrDefault();
               switch (dataBase.Server.ProviderName)
               {
                   case "System.Data.SqlClient":
                       if (ll == null)
                           table = new SqlServer.SqlDbTable(dataBase, name);
                       else
                           table = new SqlServer.SqlDbView(dataBase, name);
                       break;
                   case "System.Data.OracleClient":
                       if (ll == null)
                           table = new Oracle.OracleDbTable(dataBase, name);
                       else
                           table = new Oracle.OracleDbView(dataBase, name);
                       break;
               }
           return table;
       }
    }
}
