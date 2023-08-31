using FC.Core.AppSetting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.DataService
{
    public class DataService : IDataService
    {
        public AppDb Db { get; }

        public DataService(AppDb db)
        {
            Db = db;
        }

        public DataService()
        {
        }

        public Dictionary<string, object> Get(string resource, int id)
        {
            return Db.dataHelper.Get(resource, id);
        }

        public List<Dictionary<string, object>> Query(string resource, object filter)
        {
            throw new NotImplementedException();
        }

        public string Create(string resource, JObject obj)
        {
            throw new NotImplementedException();
        }

        public string Update(string resource, JObject obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string resource, string id)
        {
            throw new NotImplementedException();
        }

        public bool Drop(string resource, string id)
        {
            throw new NotImplementedException();
        }
    }
}
