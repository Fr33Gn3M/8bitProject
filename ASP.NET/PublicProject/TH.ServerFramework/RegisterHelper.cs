using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TH.ServerFramework.Configuration;

namespace TH.ServerFramework
{
   public class RegisterHelper
    {
        private static System.Timers.Timer timer = new System.Timers.Timer();

        private static bool isInit = false;
        private static void UpdateRegistration()
        {
            if (isInit == false)
            {
                timer.Interval = 86400000;
                timer.Elapsed += timer_Elapsed;
                isInit = true;
                timer.Start();
            }
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IsRegistration = false;
        }
      
        private const string PublicKey = "<BitStrength>1024</BitStrength><RSAKeyValue><Modulus>xPNPvdEyBKF3wYcZe+FcwYUBf8kWP32P+hmVrKuh0oKE0ifAy7dowfw65Awylv59yK8Deo1kQN2KNTxCDXcK+rLrCHcfS6e8+mE4f+wea82+iWLru1uGLpnMJeG3W8N+5pFwYm8JSY6k9+uORE/sYJ4oCFGq3NF4PGozuW8CXr0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        /// <summary>
        /// 是否注册
        /// </summary>
        public static bool IsRegistration { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public static DateTime? EffectiveTime { get; set; }
        /// <summary>
        /// 授权文件路径
        /// </summary>
        /// 
        public static  string ApplicationDataFolder
        {
            get
            {
                string path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Encrypt)).Location);
                //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"SkySeaInfoTech\" + SkyseaMapSystemName+@"\");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string LicenseFilePath
        {
            get
            {
                return Path.Combine(ApplicationDataFolder, "licence.lic");
            }
        }
       // public static string SystemName =  RegistrationSystemName
        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <returns></returns>
        public static string CheckIsRgistration()
        {
            UpdateRegistration();
            if (IsRegistration == true)
                return "";
            string SystemName = SettingsSection.GetSection().ApplicationServices.TokenService.RegistrationSystemName;
            if (string.IsNullOrEmpty(LicenseFilePath) || !System.IO.File.Exists(LicenseFilePath))
            {
                return "授权文件不存在";
            }
            string code = Regmutou.GetComputerbit(SystemName);
            string str = IsVerifySuccess(code, LicenseFilePath);
            if (string.IsNullOrEmpty(str))
                IsRegistration = true;
            return str;
        }
        /// <summary>
        /// 验证机器码与授权码是否配对
        /// </summary>
        /// <param name="code">机器码</param>
        /// <param name="path">授权文件路径</param>
        /// <returns></returns>
        public static string IsVerifySuccess(string code, string path)
        {
            if (!File.Exists(path))
                return "无密钥，不可用！";
            var arr = File.ReadAllBytes(path);
            var str = ASCIIEncoding.ASCII.GetString(arr);
            //定义这个byte[]数组的长度 为文件的length          
            //把fs文件读入到arrFile数组中，0是指偏移量，从0开始读，arrFile.length是指需要读的长度，也就是整个文件的长度  
            DateTime? effectiveTime = null;
            var reslut = EncryptKeyClassHelper.Decrypt(code, str, RegisterHelper.PublicKey, ref effectiveTime);
            EffectiveTime = effectiveTime;
            if (string.IsNullOrEmpty(reslut))
                EncryptKeyClassHelper.UpdateEncryptInfo(DateTime.Now, path);
            return reslut;
        }
        /// <summary>
        /// 保存授权文件
        /// </summary>
        /// <param name="licenseFilePath">授权文件路径</param>
        public static void SaveLicenseFile(string licenseFilePath)
        {
            var arr = File.ReadAllBytes(licenseFilePath);
            File.WriteAllBytes(LicenseFilePath, arr);
        }

        /// <summary>
        /// 获取Assembly的运行路径
        /// </summary>
        ///<returns></returns>
        private static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是file:// 的长度

            string[] arrSection = _CodeBase.Split(new char[] { '/' });

            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "/";
            }

            return _FolderPath;
        }
    }
}
