using FD.DataBase;
using System.Collections.Generic;

namespace FD.HistoryDataManager.Interfaces
{
    public interface IHistoryManage
    {
        void updateToHistory(string tableName, Dictionary<string, object>[] dic);

        void updateHistoryObject(string tableName, Dictionary<string, object>[] dics);

        HistoryDiffModel[] GetHistoryDifference(QueryPageFilter[] resultDic);

        Dictionary<string, string> getReplaceDic(string tableName);

    }
}
