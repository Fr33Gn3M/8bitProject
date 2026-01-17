using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using LX.Commons.Common;
using LX.Commons.ExceptionManager;
using ThoughtWorks.QRCode.Codec;

namespace LX.Commons
{
    public class QRCodeHelper
    {

        private static ConfigurationManagerHelper _configManager;

        public static void NewQRCodeByThoughtWorks(string imgPath, string codeContent, string cdqname)
        {
            var WorkPath = _configManager.GetMainProjectSetting("FilePath");
            var logoPath = Path.Combine(WorkPath, "Logo\\logo.png");
            if (!File.Exists(logoPath))
            {
                throw new ServiceException(9999, "生成二维码时未找到logo图片");
            }
            var img = CreateQRCodeWithLogo(codeContent, logoPath, cdqname);
            img.Save(imgPath, ImageFormat.Png);
            img.Dispose();
        }

        public static Bitmap CreateQRCode(string content)
        {
            try
            {
                QRCodeEncoder qrEncoder = new QRCodeEncoder();
                //二维码类型
                qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //二维码尺寸
                qrEncoder.QRCodeScale = 20;
                //二维码版本
                qrEncoder.QRCodeVersion = 0;
                //二维码容错程度
                qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                //字体与背景颜色
                qrEncoder.QRCodeBackgroundColor = Color.White;
                qrEncoder.QRCodeForegroundColor = Color.Black;
                //UTF-8编码类型
                Bitmap qrcode = qrEncoder.Encode(content, Encoding.UTF8);

                return qrcode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Bitmap CreateQRCodeWithLogo(string content, string logopath, string name)
        {
            //生成二维码
            Bitmap qrcode = CreateQRCode(content);
            //生成logo
            Bitmap logo = new Bitmap(logopath);
            //合成
            ImageUtility util = new ImageUtility();
            //Bitmap finalImage = util.MergeQrImg(qrcode, logo);
            Bitmap finalImage = util.MergeLogoAndString(qrcode, logo, name);

            return finalImage;
        }

        /// <summary>
        /// 保存二维码
        /// </summary>
        /// <param name="QRCode">二维码图片</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="QRCodeName">图片名称</param>
        public static void SaveQRCode(Bitmap QRCode, string ImgPath)
        {
            QRCode.Save(ImgPath, ImageFormat.Png);
            QRCode.Dispose();
        }
    }
}
