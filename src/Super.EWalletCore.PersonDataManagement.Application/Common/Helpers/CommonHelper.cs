using System;
using System.Text.RegularExpressions;

namespace Super.EWalletCore.PersonDataManagement.Application.Common.Helpers
{
    public class CommonHelper
    {
        public static string GetEnumsToString(Type enumType)
        {
            var values = Enum.GetValues(enumType);

            String enumInfo = "(";
            int i = 0;

            foreach (var value in values)
            {
                if(i == values.Length - 1) enumInfo = enumInfo + " " + value + ":" + (int)value;
                else enumInfo = enumInfo + " " + value + ":" + (int)value + ",";
                i++;
            }

            enumInfo += " )";

            return enumInfo;
        }

        public static bool OnlyLetters(string str) {
           return Regex.IsMatch(str, @"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
        }

        public static bool IsInt(string numb) {

            try
            {
                Convert.ToInt32(numb);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailFormat(string email) {
            return Regex.IsMatch(email, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        }

        public static bool DateFormat(string date) {

            try
            {
                DateTime.Parse(date);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
