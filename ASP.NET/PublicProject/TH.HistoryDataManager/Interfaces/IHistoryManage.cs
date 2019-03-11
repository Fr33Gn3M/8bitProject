using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.DataBase;
using TH.DataModels;

namespace TH.HistoryDataManager.Interfaces
{
    public interface IHistoryManage
    {
        void updateToHistory(string tableName, Dictionary<string, object>[] dic);

        void updateHistoryObject(string tableName, Dictionary<string, object>[] dics);

        HistoryDiffModel[] GetHistoryDifference(QueryPageFilter[] resultDic);

        Dictionary<string, string> getReplaceDic(string tableName);

    }
}
