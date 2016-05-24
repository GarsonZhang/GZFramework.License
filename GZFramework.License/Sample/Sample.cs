using GZFramework.License.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GZFramework.License.Sample
{
    public class Sample
    {
        private string _filename;
        private string FileName
        {
            get
            {
                if (_filename == null)
                    _filename = Path.GetFullPath(System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE")) + "\\.gzframwork\\_register.lic";
                return _filename;
            }
        }


        /// <summary>
        /// 验证是否注册
        /// </summary>
        /// <returns></returns>
        public bool DoValidateLic()
        {
            bool success = false;
            string content = null;
            try
            {
                if (File.Exists(FileName))
                {
                    using (StreamReader sr = new StreamReader(FileName, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        content = sr.ReadToEnd().ToString();
                    }
                    if (String.IsNullOrEmpty(content))
                        success = DoValidateLic(content);
                }

            }
            catch
            {
                success = false;
            }
            return success;

        }

        /// <summary>
        /// 验证注册码是否正确
        /// </summary>
        /// <param name=""></param>
        public bool DoValidateLic(string content)
        {
            try
            {
                RegisterHelper helper = new RegisterHelper();
                LicData data = helper.Dectry(content);
                string machineCode = new MachineCodeTools().GenerateMachineCode();
                return (data.LastTime > DateTime.Now) && (data.MachineCode == machineCode);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 应用注册码
        /// </summary>
        /// <param name="content"></param>
        public bool DoAppLic(string content)
        {
            if (DoValidateLic(content) == true)
            {
                using (FileStream OutFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(OutFileStream))
                    {
                        sw.WriteLine(content);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 生成注册码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DoGenerateLic(LicData data)
        {
            RegisterHelper helper = new RegisterHelper();
            return helper.Entry(data);
        }


    }
}
