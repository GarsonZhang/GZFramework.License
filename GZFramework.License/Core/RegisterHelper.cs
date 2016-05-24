using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZFramework.License.Core
{
    public class RegisterHelper
    {
        //默认密钥向量
        private readonly string _DefaultIV = "garsonZhang";
        private readonly string _DefaultKey = "GZFramework";

        /// <summary>
        /// 加密注册信息对象并返回加密结果
        /// </summary>
        /// <param name="data">加密对象</param>
        /// <returns>返回加密后的字符串</returns>
        public string Entry(LicData data)
        {
            try
            {
                string str = ConvertObject.ConvertObjectToXML(data);
                string entrystr = DESEncrypt(str);
                return entrystr;
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 返回注册信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public LicData Dectry(string str)
        {
            try
            {
                string s = DESDecrypt(str);
                LicData data = ConvertObject.ConvertXMLToObject<LicData>(s);
                return data;
            }
            catch
            {
                return null;
            }
        }

   
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string DESEncrypt(string str)
        {
            return Encrypt.DESEncrypt(str, DefaultKey, DefaultIV);
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string DESDecrypt(string str)
        {
            return Encrypt.DESDecrypt(str, DefaultKey, DefaultIV);
        }

        /// <summary>
        /// 保证只能是8位字符
        /// </summary>
        private string DefaultIV
        {
            get
            {
                string tmp = _DefaultIV;
                if (String.IsNullOrEmpty(tmp))
                {
                    tmp = "GarsonZH";
                }
                while (tmp.Length < 8)
                {
                    tmp += tmp;
                }
                return tmp.Substring(0, 8);
            }
        }
        /// <summary>
        /// 保证只能是8位字符
        /// </summary>
        private string DefaultKey
        {
            get
            {
                string tmp = _DefaultKey;
                if (String.IsNullOrEmpty(tmp))
                {
                    tmp = "GarsonZH";
                }
                while (tmp.Length < 8)
                {
                    tmp += tmp;
                }
                return tmp.Substring(0, 8);
            }
        }
    }
}
