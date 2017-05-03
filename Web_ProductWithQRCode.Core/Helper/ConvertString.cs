using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Web_ProductWithQRCode.Core.Helper
{
    public static class ConvertString
    {
        #region[xoa khoang trang va dau cau]
        public static string ConvertToUnSign(string text)
        {
            //Duyệt qua từng phần tử mảng, chuyển các ký tự thường và đặc biệt từ có dấu thành không dấu (tương ứng với bảng mã ASCII)
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            text = text.Replace(" ", "-"); //Chuyển khoảng trắng giữa các từ trong chuỗi thành "-"

            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        #endregion
    }
}