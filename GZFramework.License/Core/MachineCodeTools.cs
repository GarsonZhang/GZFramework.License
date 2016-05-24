using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace GZFramework.License.Core
{
    internal class MachineCodeTools
    {

        internal string GenerateMachineCode()
        {
            string code = GetSystemInstallDate() + DeviceID() + BaseBoardID();
            code = Encrypt.DESEncrypt(code, "lic_code");
            return code;
        }

        /// <summary>
        /// 获得系统安装时间
        /// </summary>
        /// <returns></returns>
       private string GetSystemInstallDate()
        {
            System.Management.ObjectQuery MyQuery = new System.Management.ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            System.Management.ManagementScope MyScope = new System.Management.ManagementScope();
            ManagementObjectSearcher MySearch = new ManagementObjectSearcher(MyScope, MyQuery);
            ManagementObjectCollection MyCollection = MySearch.Get();
            string result = "";
            foreach (ManagementObject MyObject in MyCollection)
            {
                result = MyObject.Properties["InstallDate"].Value.ToString();
                break;
            }
            return result;
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        private string DeviceID()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query = new SelectQuery("Win32_DiskDrive", "", new string[] { "PNPDeviceID", "Signature" });
            ManagementObjectCollection myCollection = mos.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject moo = em.Current;
            return moo.Properties["signature"].Value.ToString().Trim();
        }
        /// <summary>
        /// 获得主板序列号
        /// </summary>
        private string BaseBoardID()
        {
            //主板序列号
            ManagementClass mc = new ManagementClass("WIN32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            string SerialNumber = "";
            foreach (ManagementObject mo in moc)
            {
                SerialNumber = mo["SerialNumber"].ToString();
                break;
            }
            return SerialNumber;
        }
    }
}
