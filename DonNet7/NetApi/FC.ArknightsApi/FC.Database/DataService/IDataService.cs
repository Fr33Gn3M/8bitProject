using FC.Database.FilterModel;
using FC.Database.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.DataService
{
    public interface IDataService
    {
        void Reload();  

        Dictionary<string, object> Get(string resource, int id);

        PageQueryResult Query(string resource, PageQueryFilter filter);

        string Create(string resource, JObject obj);

        string Update(string resource, JObject obj);

        bool Delete(string resource, string id);

        bool Drop(string resource, string id);
    }
}
