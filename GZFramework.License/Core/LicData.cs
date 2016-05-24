using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GZFramework.License.Core
{
    public class LicData
    {
        /// <summary>
        /// 机器码
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 加密产品名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 上次使用时间
        /// </summary>
        public DateTime PreTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime LastTime { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 总次数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 额外的内容
        /// </summary>
        public string ExtraContent { get; set; }

    }
}
