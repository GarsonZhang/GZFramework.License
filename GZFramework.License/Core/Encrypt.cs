using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GZFramework.License.Core
{
    internal class Encrypt
    {
        //Ĭ����Կ����
        private static byte[] DefaultIV = { 0x47, 0x56, 0x56, 0x6f, 0x5a, 0x56, 0x61, 0xEF };
        private static byte[] DefaultKey = { 0x47, 0x56, 0x72, 0x56, 0x56, 0x6e, 0x5a, 0x68 };

        #region DES�ԳƼ��ܽ���
        /// <summary>
        /// DES�ԳƼ���
        /// </summary>
        /// <param name="data">��������</param>
        /// <param name="key">8λ�ַ�����Կ�ַ���</param>
        /// <param name="iv">8λ�ַ��ĳ�ʼ�������ַ���</param>
        /// <returns></returns>
        internal static string DESEncrypt(string data, string key = null, string iv = null)
        {
            byte[] byKey = String.IsNullOrEmpty(key) ? DefaultKey : System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = String.IsNullOrEmpty(iv) ? DefaultIV : System.Text.ASCIIEncoding.ASCII.GetBytes(iv);


            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// DES�Գƽ���
        /// </summary>
        /// <param name="data">��������</param>
        /// <param name="key">8λ�ַ�����Կ�ַ���(��Ҫ�ͼ���ʱ��ͬ)</param>
        /// <param name="iv">8λ�ַ��ĳ�ʼ�������ַ���(��Ҫ�ͼ���ʱ��ͬ)</param>
        /// <returns></returns>
        internal static string DESDecrypt(string data, string key = null, string iv = null)
        {
            if (String.IsNullOrWhiteSpace(data)) return null;
            byte[] byKey = String.IsNullOrEmpty(key) ? DefaultKey : System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = String.IsNullOrEmpty(iv) ? DefaultIV : System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            try
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Base64���ܽ���
        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ���</param>
        /// <returns></returns>
        internal static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }



        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ���</param>
        /// <param name="encode">�ַ�����</param>
        /// <returns></returns>
        internal static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ���</param>
        /// <returns></returns>
        internal static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ���</param>
        /// <param name="encode">�ַ��ı���</param>
        /// <returns></returns>
        internal static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion


    }
}
