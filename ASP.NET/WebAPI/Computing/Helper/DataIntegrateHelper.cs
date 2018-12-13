using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computing
{
    public class DataIntegrateHelper
    {

        /// <summary>
        /// 生成房产和土地的上下手关系
        /// </summary>
        public static void geneSXS()
        {
            DateTime dateTime = DateTime.Now;
            //var sql1 = @"SELECT * FROM GB_FDCQ2 WHERE BDCDYH IN (SELECT BDCDYH FROM GB_FDCQ2 WHERE BDCDYH IN (
            //    SELECT BDCDYH FROM GB_FDCQ2 WHERE DJSJ IS NOT NULL AND BDCDYH IS NOT NULL) GROUP BY BDCDYH HAVING COUNT(*) > 1) AND XSCQZXH IS NULL ORDER BY BDCDYH,DJSJ";
            var sql1 = @"SELECT * FROM GB_FDCQ2 WHERE BDCDYH IN (
SELECT BDCDYH FROM GB_FDCQ2 WHERE  XMID IS NOT NULL AND BDCDYH IS NOT NULL GROUP BY BDCDYH HAVING COUNT(*) > 1) AND XMID IS NOT NULL ORDER BY GB_FDCQ2.BDCDYH,GB_FDCQ2.DJSJ";
            var fcArr = ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlToDic(sql1);
            var nodjsj = (from Dictionary<string, object> fc in fcArr where fc["DJSJ"] == null select fc["BDCDYH"].ToString()).ToArray();
            for (var i = 0; i < fcArr.Length - 1; i++)
            {
                if (nodjsj.Contains(fcArr[i]["BDCDYH"].ToString())) continue;
                if (fcArr[i]["BDCDYH"].ToString() == fcArr[i + 1]["BDCDYH"].ToString())
                {
                    fcArr[i]["XSCQZXH"] = fcArr[i + 1]["CQZXH"];
                    fcArr[i]["CLSJ"] = dateTime;
                }
                else
                {
                    fcArr[i]["XSCQZXH"] = fcArr[i]["CQZXH"];
                    fcArr[i]["CLSJ"] = dateTime;
                }
            }
            fcArr[fcArr.Length - 1]["XSCQZXH"] = fcArr[fcArr.Length - 1]["CQZXH"];
            fcArr[fcArr.Length - 1]["CLSJ"] = dateTime;
            ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_FDCQ2",fcArr,true);

            //var fcidArr = from Dictionary<string, object> fc in fcArr select fc["BDCDYH"].ToString();
            //var f = QueryPageFilter.Create("VWTDFCGLB").In("FCBDCDYH", fcidArr.ToArray()).OrderBy(new string[] { "BDCDYH", "FCDJSJ" });
            //var tdArr = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(f);
            //for (var i = 0; i < tdArr.Length - 1; i++)
            //{
            //    if (tdArr[i]["FCBDCDYH"].ToString() == tdArr[i + 1]["FCBDCDYH"].ToString() && tdArr[i]["ID"].ToString() != tdArr[i+1]["ID"].ToString())
            //    {
            //        tdArr[i]["XSLSH_TD"] = tdArr[i + 1]["LSH_TD"];
            //        tdArr[i]["GXSJ"] = dateTime;
            //    }
            //}
            //ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", tdArr, true);
        }

        /// <summary>
        /// 设置土地 是否小证
        /// </summary>
        public static void setTdSfxz()
        {
            DateTime dateTime = DateTime.Now;
            var updateList = new List<Dictionary<string, object>>();

            var f1 = QueryPageFilter.Create("GB_ZDJBXX");
            var zdArr = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(f1);

            var f2 = QueryPageFilter.Create("VWTDFCGLB");
            var tdArr = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(f2);

            foreach(var zd in zdArr)
            {
                var tdglzd = from Dictionary<string, object> td in tdArr where td["ZDID"].ToString() == zd["ID"].ToString() select td;
                if (tdglzd.Count() <= 0) continue;
                var tdglArr = tdglzd.ToArray();
                var fcbdcdyh = tdglArr[0]["FCBDCDYH"].ToString();
                var neCount = (from Dictionary<string, object> tdgl in tdglArr where tdgl["FCBDCDYH"].ToString() != fcbdcdyh select tdgl).Count();
                if (neCount <= 0)
                {
                    foreach (var td in tdglArr)
                    {
                        td["SFXZ"] = true;
                        td["GXSJ"] = dateTime;
                        updateList.Add(td);
                    }
                }
            }
            ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", updateList.ToArray(), true);
        }

        /// <summary>
        /// 设置商品房的权属状态
        /// </summary>
        public static void setFCQSZT()
        {
            DateTime dateTime = DateTime.Now;
            var sql1 = @"SELECT BDCDYH,DJSJ,XSCQZXH,CQZXH,QSZT,XMID,* FROM GB_FDCQ2 WHERE BDCDYH IN (
SELECT BDCDYH FROM GB_FDCQ2 WHERE XMID IS NOT NULL AND BDCDYH IS NOT NULL AND QSZT = 1  GROUP BY BDCDYH,QSZT HAVING COUNT(*) > 1) AND XMID IS NOT NULL ORDER BY GB_FDCQ2.BDCDYH,GB_FDCQ2.DJSJ";
            var cffcArr = ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlToDic(sql1);

            for (var i = 0; i < cffcArr.Length - 1; i++)
            {
                if (cffcArr[i]["BDCDYH"].ToString() == cffcArr[i + 1]["BDCDYH"].ToString())
                {
                    cffcArr[i]["QSZT"] = "0";
                    cffcArr[i]["CLSJ"] = dateTime;
                }
                if (cffcArr[i]["BDCDYH"].ToString() != cffcArr[i + 1]["BDCDYH"].ToString())
                {
                    cffcArr[i]["QSZT"] = "1";
                    cffcArr[i]["CLSJ"] = dateTime;
                }
            }
            cffcArr[cffcArr.Length - 1]["QSZT"] = "1";
            cffcArr[cffcArr.Length - 1]["CLSJ"] = dateTime;
            ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_FDCQ2", cffcArr, true);


            //var fcidArr = from Dictionary<string, object> fc in cffcArr select fc["BDCDYH"].ToString();
            //var f = QueryPageFilter.Create("VWTDFCGLB").In("FCBDCDYH", fcidArr.ToArray()).OrderBy(new string[] { "BDCDYH", "FCDJSJ" });
            //var tdArr = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(f);
            //for (var i = 0; i < tdArr.Length - 1; i++)
            //{
            //    if(tdArr[i]["FCQSZT"] == null)
            //        continue;
            //    if ((tdArr[i]["QSZT"] == null && tdArr[i]["FCQSZT"]!=null)|| tdArr[i]["QSZT"].ToString() != tdArr[i]["FCQSZT"].ToString())
            //    {
            //        tdArr[i]["QSZT"] = tdArr[i]["FCQSZT"];
            //        tdArr[i]["GXSJ"] = dateTime;
            //    }
            //}
            //ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", tdArr, true);


            //var sql2 = @"SELECT *FROM GB_JSYDSYQ WHERE BDCDYH IN(SELECT BDCDYH FROM GB_JSYDSYQ WHERE BDCDYH IN (
            //   SELECT BDCDYH FROM GB_JSYDSYQ WHERE DJSJ IS NOT NULL AND(BDCDYH IS NOT NULL or BDCDYH = '')) AND SFXZ = 1 AND QSZT = 1  GROUP BY BDCDYH,QSZT HAVING COUNT(*) > 1) AND SFXZ = 1 AND BDCDYH IS NOT NULL AND BDCDYH != '' ORDER BY BDCDYH,DJSJ";
            //var cftdArr = ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlToDic(sql2);

            //for (var i = 0; i < cftdArr.Length - 1; i++)
            //{
            //    if (cftdArr[i]["BDCDYH"].ToString() == cftdArr[i + 1]["BDCDYH"].ToString())
            //    {
            //        cftdArr[i]["QSZT"] = "0";
            //        cftdArr[i]["CLSJ"] = dateTime;
            //    }
            //    if (cftdArr[i]["BDCDYH"].ToString() != cftdArr[i + 1]["BDCDYH"].ToString())
            //    {
            //        cftdArr[i]["QSZT"] = "1";
            //        cftdArr[i]["CLSJ"] = dateTime;
            //    }
            //}
            //ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", cftdArr, true);
        }

        public static void setTDQSZT()
        {
            DateTime dateTime = DateTime.Now;
            var sql1 = @"SELECT BDCDYH,DJSJ,XSCQZXH,CQZXH,QSZT,XMID,* FROM GB_FDCQ2 WHERE BDCDYH IN (
SELECT BDCDYH FROM GB_FDCQ2 WHERE XMID IS NULL AND BDCDYH IS NOT NULL AND QSZT = 1  GROUP BY BDCDYH,QSZT HAVING COUNT(*) > 1) AND XMID IS NULL ORDER BY GB_FDCQ2.BDCDYH,GB_FDCQ2.DJSJ";
            var cffcArr = ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlToDic(sql1);

            for (var i = 0; i < cffcArr.Length - 1; i++)
            {
                if (cffcArr[i]["BDCDYH"].ToString() == cffcArr[i + 1]["BDCDYH"].ToString() && cffcArr[i]["DJSJ"] != cffcArr[i + 1]["DJSJ"])
                {
                    cffcArr[i]["QSZT"] = "0";
                    cffcArr[i]["CLSJ"] = dateTime;
                }
                if (cffcArr[i]["BDCDYH"].ToString() != cffcArr[i + 1]["BDCDYH"].ToString())
                {
                    cffcArr[i]["QSZT"] = "1";
                    cffcArr[i]["CLSJ"] = dateTime;
                }
            }
            cffcArr[cffcArr.Length - 1]["QSZT"] = "1";
            cffcArr[cffcArr.Length - 1]["CLSJ"] = dateTime;
            ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_FDCQ2", cffcArr, true);


            //var fcidArr = from Dictionary<string, object> fc in cffcArr select fc["BDCDYH"].ToString();
            //var f = QueryPageFilter.Create("VWTDFCGLB").In("FCBDCDYH", fcidArr.ToArray()).OrderBy(new string[] { "BDCDYH", "FCDJSJ" });
            //var tdArr = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(f);
            //for (var i = 0; i < tdArr.Length - 1; i++)
            //{
            //    if(tdArr[i]["FCQSZT"] == null)
            //        continue;
            //    if ((tdArr[i]["QSZT"] == null && tdArr[i]["FCQSZT"]!=null)|| tdArr[i]["QSZT"].ToString() != tdArr[i]["FCQSZT"].ToString())
            //    {
            //        tdArr[i]["QSZT"] = tdArr[i]["FCQSZT"];
            //        tdArr[i]["GXSJ"] = dateTime;
            //    }
            //}
            //ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", tdArr, true);


            //var sql2 = @"SELECT *FROM GB_JSYDSYQ WHERE BDCDYH IN(SELECT BDCDYH FROM GB_JSYDSYQ WHERE BDCDYH IN (
            //   SELECT BDCDYH FROM GB_JSYDSYQ WHERE DJSJ IS NOT NULL AND(BDCDYH IS NOT NULL or BDCDYH = '')) AND SFXZ = 1 AND QSZT = 1  GROUP BY BDCDYH,QSZT HAVING COUNT(*) > 1) AND SFXZ = 1 AND BDCDYH IS NOT NULL AND BDCDYH != '' ORDER BY BDCDYH,DJSJ";
            //var cftdArr = ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlToDic(sql2);

            //for (var i = 0; i < cftdArr.Length - 1; i++)
            //{
            //    if (cftdArr[i]["BDCDYH"].ToString() == cftdArr[i + 1]["BDCDYH"].ToString())
            //    {
            //        cftdArr[i]["QSZT"] = "0";
            //        cftdArr[i]["CLSJ"] = dateTime;
            //    }
            //    if (cftdArr[i]["BDCDYH"].ToString() != cftdArr[i + 1]["BDCDYH"].ToString())
            //    {
            //        cftdArr[i]["QSZT"] = "1";
            //        cftdArr[i]["CLSJ"] = dateTime;
            //    }
            //}
            //ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", cftdArr, true);
        }

        public static void setTdExQSZT()
        {
            DateTime dateTime = DateTime.Now;
            var sql2 = @"SELECT *FROM GB_JSYDSYQ WHERE BDCDYH IN(SELECT BDCDYH FROM GB_JSYDSYQ WHERE BDCDYH IN (
               SELECT BDCDYH FROM GB_JSYDSYQ WHERE DJSJ IS NOT NULL AND(BDCDYH IS NOT NULL or BDCDYH = '')) AND SFXZ = 1 AND QSZT = 1  GROUP BY BDCDYH,QSZT HAVING COUNT(*) > 1) AND SFXZ = 1 AND BDCDYH IS NOT NULL AND BDCDYH != '' ORDER BY BDCDYH,DJSJ";
            var cftdArr = ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlToDic(sql2);

            for (var i = 0; i < cftdArr.Length - 1; i++)
            {
                if (cftdArr[i]["BDCDYH"].ToString() == cftdArr[i + 1]["BDCDYH"].ToString())
                {
                    cftdArr[i]["QSZT"] = "0";
                    cftdArr[i]["CLSJ"] = dateTime;
                }
                if (cftdArr[i]["BDCDYH"].ToString() != cftdArr[i + 1]["BDCDYH"].ToString())
                {
                    cftdArr[i]["QSZT"] = "1";
                    cftdArr[i]["CLSJ"] = dateTime;
                }
            }
            ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects("GB_JSYDSYQ", cftdArr, true);
        }

        public static void deleteQLR()
        {
            var sql1 = @"DELETE GB_QLR";
            var sql2 = @"DELETE GLB_QLR_QZXX";

            ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlList(new List<string>() { sql1, sql2 });
        }
        public static void insertQLR()
        {
            var sql1 = @"insert into GB_QLR(ID, YSDM, BDCDYH, QLRMC, BDCQZH,ZJZL,ZJH, GJ, DH, QLBL, SYRXH, YWH, QXDM, QSZT, QLLX)
SELECT NEWID(),'6003000000',BDCDYH,GYRXM,BDCQZH,
CASE WHEN GYRZJ = '身份证' THEN '1'
WHEN GYRZJ = '营业执照' THEN '7'
WHEN GYRZJ = '事业单位法人证书' THEN '99'
WHEN GYRZJ = '护照' THEN '3'
WHEN GYRZJ = '户口簿' THEN '4'
WHEN GYRZJ = '港澳台身份证' THEN '2'
WHEN GYRZJ = '其他' THEN '99'
WHEN GYRZJ = '港澳台证件' THEN '2'
WHEN GYRZJ = '士官证' THEN '5'
WHEN GYRZJ = '警官证' THEN '5'
WHEN GYRZJ = '通行证' THEN '99'
WHEN GYRZJ = '机构代码证' THEN '6'
WHEN GYRZJ = '其它' THEN '99'
WHEN GYRZJ = '残疾人证' THEN '99'
WHEN GYRZJ = '军官证' THEN '5'
WHEN GYRZJ = '居民身份证' THEN '1'
ELSE GYRZJ END,
GYRZH,'142',GYRDH,GYRZYFE,CQZCQR20180115.CQRXH,GB_FDCQ2.YWH,'350581',GB_FDCQ2.QSZT, GB_FDCQ2.QLLX
FROM CQZCQR20180115,GB_FDCQ2 WHERE GB_FDCQ2.CQZXH = CQZCQR20180115.CQZXH AND GB_FDCQ2.BDCDYH IS NOT NULL";

            var sql2 = @"insert into GB_QLR(ID, YSDM, BDCDYH, QLRMC, BDCQZH,ZJZL,ZJH, GJ, DH, QLBL, SYRXH, YWH, QXDM, QSZT, QLLX, CJSJ, GXSJ)
SELECT NEWID(),'6003000000',BDCDYH,GYRXM,BDCQZH,
CASE WHEN GYRZJ = '身份证' THEN '1'
WHEN GYRZJ = '营业执照' THEN '7'
WHEN GYRZJ = '事业单位法人证书' THEN '99'
WHEN GYRZJ = '护照' THEN '3'
WHEN GYRZJ = '户口簿' THEN '4'
WHEN GYRZJ = '港澳台身份证' THEN '2'
WHEN GYRZJ = '其他' THEN '99'
WHEN GYRZJ = '港澳台证件' THEN '2'
WHEN GYRZJ = '士官证' THEN '5'
WHEN GYRZJ = '警官证' THEN '5'
WHEN GYRZJ = '通行证' THEN '99'
WHEN GYRZJ = '机构代码证' THEN '6'
WHEN GYRZJ = '其它' THEN '99'
WHEN GYRZJ = '残疾人证' THEN '99'
WHEN GYRZJ = '军官证' THEN '5'
WHEN GYRZJ = '居民身份证' THEN '1'
ELSE GYRZJ END,
GYRZH,'142',GYRDH,GYRZYFE,TDZSYR20180115.SYRXH,GB_JSYDSYQ.YWH,'350581',GB_JSYDSYQ.QSZT, GB_JSYDSYQ.QLLX,GETDATE(),GETDATE()
FROM TDZSYR20180115, GB_JSYDSYQ WHERE GB_JSYDSYQ.LSH_TD = TDZSYR20180115.LSH_TD AND GB_JSYDSYQ.BDCDYH IS NOT NULL AND GB_JSYDSYQ.SFXZ = 1";

            var sql3 = @"update GB_QLR SET QLRLX = CASE
WHEN ZJZL = '1' THEN '1'
WHEN ZJZL = '2' THEN '1'
WHEN ZJZL = '3' THEN '1'
WHEN ZJZL = '4' THEN '1'
WHEN ZJZL = '5' THEN '4'
WHEN ZJZL = '6' THEN '2'
WHEN ZJZL = '7' THEN '2'
WHEN ZJZL = '99' THEN '99' END";

            var sql4 = @"insert into GLB_QLR_QZXX(ID, QZID, QLRID, QZXH, CQRXH, LX)
SELECT NEWID(),GB_FDCQ2.ID,GB_QLR.ID,GB_FDCQ2.CQZXH,GB_QLR.SYRXH,'房产' FROM GB_QLR, GB_FDCQ2,[dbo].[CQZCQR20180115]
        where GB_QLR.SYRXH = [CQZCQR20180115].CQRXH AND GB_FDCQ2.CQZXH = [CQZCQR20180115].CQZXH and GB_QLR.CJSJ is null and GB_QLR.GXSJ is null";


            var sql5 = @"insert into GLB_QLR_QZXX(ID, QZID, QLRID, QZXH, CQRXH, LX)
 SELECT NEWID(), GB_JSYDSYQ.ID, GB_QLR.ID, GB_JSYDSYQ.LSH_TD, GB_QLR.SYRXH,'土地' FROM GB_QLR, GB_JSYDSYQ, [dbo].[TDZSYR20180115]
where GB_QLR.SYRXH = [TDZSYR20180115].SYRXH AND GB_JSYDSYQ.LSH_TD = [TDZSYR20180115].LSH_TD and GB_QLR.CJSJ is NOT null and GB_QLR.GXSJ is NOT null";

            var sql6 = @"update GB_QLR SET QLLX = GB_FDCQ2.QLLX FROM GB_FDCQ2 WHERE  GB_FDCQ2.BDCDYH = GB_QLR.BDCDYH";
            var sql7 = @"update GB_QLR SET QLLX = GB_JSYDSYQ.QLLX FROM GB_JSYDSYQ WHERE  GB_JSYDSYQ.BDCDYH = GB_QLR.BDCDYH";

            ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlList(new List<string>() { sql1, sql2, sql3, sql4, sql5, sql6, sql7 });
        }

        public static void setQLRQLLX()
        {
            var sql1 = @"update GB_QLR SET QLLX = GB_FDCQ2.QLLX FROM GB_FDCQ2 WHERE  GB_FDCQ2.BDCDYH = GB_QLR.BDCDYH";
            var sql2 = @"update GB_QLR SET QLLX = GB_JSYDSYQ.QLLX FROM GB_JSYDSYQ WHERE  GB_JSYDSYQ.BDCDYH = GB_QLR.BDCDYH";

            ServiceAppContext.Instance.DataBaseClassHelper.ExecuteSqlList(new List<string>() { sql1, sql2 });
        }

    }
}