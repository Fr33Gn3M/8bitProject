using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework
{
    public class CommonMembers
    {
        public const string PublicKey = "<BitStrength>1024</BitStrength><RSAKeyValue><Modulus>xPNPvdEyBKF3wYcZe+FcwYUBf8kWP32P+hmVrKuh0oKE0ifAy7dowfw65Awylv59yK8Deo1kQN2KNTxCDXcK+rLrCHcfS6e8+mE4f+wea82+iWLru1uGLpnMJeG3W8N+5pFwYm8JSY6k9+uORE/sYJ4oCFGq3NF4PGozuW8CXr0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        // private const string privateKey = "<BitStrength>1024</BitStrength><RSAKeyValue><Modulus>xPNPvdEyBKF3wYcZe+FcwYUBf8kWP32P+hmVrKuh0oKE0ifAy7dowfw65Awylv59yK8Deo1kQN2KNTxCDXcK+rLrCHcfS6e8+mE4f+wea82+iWLru1uGLpnMJeG3W8N+5pFwYm8JSY6k9+uORE/sYJ4oCFGq3NF4PGozuW8CXr0=</Modulus><Exponent>AQAB</Exponent><P>4VeQkMEU62wX3Hn7Fnt/y70+IifFgAzg48WfXAD73qqVoKFq9TAL1qxxv296R2KB6avAq0cKHHXYqfn7nfCXvw==</P><Q>377lR67pVpOdXbPsj1aW0yZtian2s7REglugarCUHRsrqMPhdzJFOVkwhklHKRh2vxakiqoxmQt/eYjcg4lIgw==</Q><DP>jffF63zJujximBP9nb921xxF7ezSoAb9FGMoMCWqiTE9jXLE2O5JNxlznGEWT6z/HgGIJCBgfWe9dfE5ldrDLw==</DP><DQ>WrDBtqCLK/CBZK29QQxT7hoxZA9kU8rJyhzhlN0l6/ZLaAidpvbVYD1qkcO5+EpWN3YyE5KQr/wdG2ICH77RiQ==</DQ><InverseQ>N2qHInOHI3RsclgNqsDOxtUXCBHTd3QxCPFRTmzMNQAAL0CPhz3Qyvh8S+at5jObrWQO+jWp8qIcOOdaYVJeSg==</InverseQ><D>TtgIyDiDCY+KXZM0BH/HnkEcxIc/vNMLXFf5r1JWSeuuOGNpryQRb/cFrF2lswTWXgySG/GWfNEzjvQ8jR9m6RoGu1uDWjysdI6YkzUxQbisLa4rXhYRrwlcFRokS9v8powcxAbzyO8H4kWtH+NLhw6aTZ2/xltnjyMtlERxrBE=</D></RSAKeyValue>";

        private static bool m_IsRegistration;
        public static bool IsRegistration
        {
            get
            {
                if (m_IsRegistration == false)
                    CheckIsRgistration();
                return m_IsRegistration;
            }
            set
            {
                m_IsRegistration = value;
            }
        }
        public static string RegistrationFilePath = "";
        public static string RegistrationFileName = "licence.lic";
        public static string MachineCodeFileName = "machinecode.key";

        public static void CheckIsRgistration()
        {
            if (string.IsNullOrEmpty(RegistrationFilePath))
            {
                IsRegistration = false;
                return;
            }
            string code = SoftReg.GetMNum();
            DateTime? EffectiveTime = null;
            string str = EncryptKeyClassHelper.IsVerifySuccess(code, RegistrationFilePath, ref EffectiveTime);
            if (string.IsNullOrEmpty(str))
            {
                IsRegistration = true;
            }
            //else
            //    throw new Exception(str);
        }

    }
}
