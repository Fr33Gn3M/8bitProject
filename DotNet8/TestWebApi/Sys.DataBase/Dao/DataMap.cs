using System;
using System.Collections.Generic;

namespace Sys.DataBase.Dao
{
    public class DataMap : Dictionary<string, object>
    {

        public static DataMap Build(Dictionary<string, object> map)
        {
            DataMap dataMap = new DataMap();
            foreach (string key in map.Keys)
            {
                dataMap.Add(key, map[key]);
            }
            return dataMap;
        }

        public static DataMap Build(params string[] fields)
        {
            DataMap dataMap = new DataMap();
            foreach (string field in fields)
            {
                if ("*".Equals(field) || "ID".Equals(field))
                {
                    dataMap.Add("ID", Guid.NewGuid().ToString().Replace("-", ""));
                }
                if ("*".Equals(field) || "CJSJ".Equals(field))
                {
                    dataMap.Add("CJSJ", DateTime.Now);
                }
                if ("*".Equals(field) || "GXSJ".Equals(field))
                {
                    dataMap.Add("GXSJ", DateTime.Now);
                }
                if ("*".Equals(field) || "SFSC".Equals(field))
                {
                    dataMap.Add("SFSC", false);
                }
            }

            return dataMap;
        }

        public string GetString(string key, string defaultValue = null)
        {
            object answer = this[key];
            if (answer != null)
            {
                return answer.ToString();
            }

            return defaultValue;
        }

        public bool GetBoolean(string key, bool defaultValue = false)
        {
            object answer = this[key];
            if (answer != null)
            {
                if (answer is bool)
                {
                    return (bool)answer;
                }

                if (answer is string)
                {
                    return bool.Parse((string)answer);
                }
            }
            return defaultValue;
        }

        public int GetInteger(string key, int defaultValue = 0)
        {
            object answer = this[key];
            if (answer != null)
            {
                if (answer is int)
                {
                    return (int)answer;
                }
                if (answer is string)
                {
                    return int.Parse((string)answer);
                }
            }
            return defaultValue;
        }

        public long GetLong(string key, long defaultValue = 0)
        {
            object answer = this[key];
            if (answer != null)
            {
                if (answer is long)
                {
                    return (long)answer;
                }
                if (answer is string)
                {
                    return long.Parse((string)answer);
                }
            }
            return defaultValue;
        }

        public float GetFloat(string key, float defaultValue = 0)
        {
            object answer = this[key];
            if (answer != null)
            {
                if (answer is float)
                {
                    return (float)answer;
                }
                if (answer is string)
                {
                    return float.Parse((string)answer);
                }
            }
            return defaultValue;
        }

        public double GetDouble(string key, double defaultValue = 0)
        {
            object answer = this[key];
            if (answer != null)
            {
                if (answer is double)
                {
                    return (double)answer;
                }
                if (answer is string)
                {
                    return double.Parse((string)answer);
                }
            }
            return defaultValue;
        }

    }
}
