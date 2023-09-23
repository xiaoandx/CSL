using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLC.Core
{
    /// <summary>
    /// Label枚举标识类，用于记录双语翻译LabelValue
    /// </summary>
    public class CL : Attribute
    {
        /// <summary>
        /// Chinese类型LabelValue
        /// </summary>
        public string Chinese { get; set; }
        /// <summary>
        /// English类型LabelValue
        /// </summary>
        public string English { get; set; }

        public CL(string _Chinese, string _English)
        {
            Chinese = _Chinese;
            English = _English;
        }
    }
}
