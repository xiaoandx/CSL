using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLC.Core
{
    public class Result
    {
        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool bSucc { get; set; }
        /// <summary>
        /// 结果代码(0000=成功，其余为错误代码)
        /// </summary>
        public string strCode { get; set; } = "0000";
        /// <summary>
        /// 错误讯息
        /// </summary>
        public string strMsg { get; set; }
        /// <summary>
        /// 资料时间
        /// </summary>
        public DateTime DataTime { get; set; }
        /// <summary>
        /// 资料详细内容
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Result()
        {
            DataTime = DateTime.Now;
        }

        /// <summary>
        /// 建立成功结果
        /// </summary>
        /// <param name="_data"></param>
        public Result(object _data)
        {
            strCode = "0000";
            bSucc = true;
            DataTime = DateTime.Now;
            Content = _data;
        }

        /// <summary>
        /// 建立失败结果
        /// </summary>
        /// <param name="_code"></param>
        /// <param name="_msg"></param>
        /// /// <param name="_data"></param>
        public Result(string _code, string _msg, object _data)
        {
            strCode = _code;
            bSucc = false;
            this.DataTime = DateTime.Now;
            Content = _data;
            strMsg = _msg;
        }
    }
}
