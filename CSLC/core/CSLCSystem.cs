using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLC.Core
{
    public class CSLCSystem
    {

        public Result Execute()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Result result = new Result() { bSucc = false, strCode = "0001" };
            string labelCount = string.Empty;
            bool loadStatus = LoadSysMsgLabel(out labelCount);
            if (loadStatus)
            {
                return new Result()
                {
                    bSucc = true,
                    strCode = "0000",
                    strMsg = $"Label Update Success ({DateTime.Now.ToString()}) count:{labelCount} - (SpendTime:" + StrTimeSlot(timer) + "ms)"
                };
            }
            result.strMsg = $"Label Update Error ({DateTime.Now.ToString()}) count:{labelCount} - (SpendTime:" + StrTimeSlot(timer) + "ms)";
            timer.Stop();
            return result;
        }

        /// <summary>
        /// Application 启动时数据库查询UserLabel数据，转换为Dictionary对象保存在内存中，
        /// 判断查询指定地区的UserLabel是参考Web.config中的Region配置，地区类别名称必须在Camstar Dictionary中存在如： (Chinese、English)
        /// </summary>
        private bool LoadSysMsgLabel(out string labelCount)
        {
            bool resStatus = false;
            try
            {
                string Region = GetRegion();
                string sqlEqp = string.Empty;
                //DapperRepository<object> dapperRepository = new DapperRepository<object>(DBProvider.MESCon);
                if ("English".Equals(Region))
                {
                    sqlEqp = $@"select distinct ul.userlabelname as LabelKey,ul.labelvalue as LabelValue from userlabel ul where ul.userlabelname is not null";
                }
                else
                {
                    sqlEqp = $@"select ul.userlabelname as LabelKey, dl.labelvalue as LabelValue from dictionarylabel dl
                            inner join dictionary d on d.dictionaryid = dl.dictionaryid
                            inner join userlabel ul on ul.labelid = dl.labelid
                            where d.dictionaryname ='{Region}' and ul.userlabelname is not null";
                }

                //List<Msg> MsgList = dapperRepository.Query<Msg>(sqlEqp).ToList();
                //labelCount = MsgList.Count.ToString();
                //CSL.Msg = MsgList.ToDictionary(item => item.LabelKey, v => v.LabelValue);
                DataTable dt = CSLCDBHelper.QueryFromMES(sqlEqp);
                labelCount = dt.Rows.Count.ToString();
                Dictionary<string, string> temp = new Dictionary<string, string>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["LABELKEY"].ToString(), dt.Rows[i]["LABELVALUE"].ToString());
                    }
                }
                else
                {
                    temp = new Dictionary<string, string>();
                }
                CSL.Msg = temp;
                resStatus = true;
            }
            catch (Exception e)
            {
                CSL.Msg = new Dictionary<string, string>();
                labelCount = "0";
            }
            return resStatus;
        }

        /// <summary>
        /// Application 启动时数据库查询UserLabel数据，转换为Dictionary对象保存在内存中，
        /// 判断查询指定地区的UserLabel是参考Web.config中的Region配置，地区类别名称必须在Camstar Dictionary中存在如： (Chinese、English)
        /// </summary>
        public void LoadSysMsgLabel()
        {
            bool resStatus = false;
            try
            {
                string Region = GetRegion();
                string sqlEqp = string.Empty;
                //DapperRepository<object> dapperRepository = new DapperRepository<object>(DBProvider.MESCon);
                if ("English".Equals(Region))
                {
                    sqlEqp = $@"select distinct ul.userlabelname as LabelKey,ul.labelvalue as LabelValue from userlabel ul where ul.userlabelname is not null";
                }
                else
                {
                    sqlEqp = $@"select ul.userlabelname as LabelKey, dl.labelvalue as LabelValue from dictionarylabel dl
                            inner join dictionary d on d.dictionaryid = dl.dictionaryid
                            inner join userlabel ul on ul.labelid = dl.labelid
                            where d.dictionaryname ='{Region}' and ul.userlabelname is not null";
                }

                //List<Msg> MsgList = dapperRepository.Query<Msg>(sqlEqp).ToList();
                //CSL.Msg = MsgList.ToDictionary(item => item.LabelKey, v => v.LabelValue);
                DataTable dt = CSLCDBHelper.QueryFromMES(sqlEqp);
                Dictionary<string, string> temp = new Dictionary<string, string>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["LABELKEY"].ToString(), dt.Rows[i]["LABELVALUE"].ToString());
                    }
                }
                else
                {
                    temp = new Dictionary<string, string>();
                }
                CSL.Msg = temp;
                resStatus = true;
            }
            catch (Exception e)
            {
                CSL.Msg = new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// 获取Web.config中的Region配置，默认Chinese
        /// </summary>
        /// <returns>Region配置：Chinese或English</returns>
        private string GetRegion()
        {
            string Region = ConfigurationManager.AppSettings["Region"] ?? "Chinese";
            if ("Chinese-Traditional".Equals(Region))
            {
                Region = "Chinese";
            }
            return Region;
        }

        /// <summary>
        /// 计时结束
        /// </summary>
        /// <param name="timer"></param>
        /// <returns></returns>
        private string StrTimeSlot(Stopwatch timer)
        {
            string timeSlot = string.Empty;

            timer.Stop();
            timeSlot = timer.ElapsedMilliseconds.ToString();//计时结束
            timer.Reset();

            return timeSlot;
        }
    }
}
