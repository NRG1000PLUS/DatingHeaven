using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer {
    public static class SqlInjectedValueFormatter {
        public static string ObjectToString(object value){
            if (value == null){
                throw new NullReferenceException("value");
            }

             if (value is string){
                // Any string to Unicode string
                return StringToString((string) value);
            }

            if (value is char){
                // Any symbol to string
                return CharToString((char) value);
            }

            if (value is Guid){
                // Guid --> string, '{33D69AC8-A114-44A4-A3CB-8E8CCB3E05E4}'
                return GuidToString((Guid) value);
            }

            if (value is ValueType){
                // uint, ulong, long 
                return value.ToString();
            }

            return null;
        }

        public static string IntToString(int intValue){
            return intValue.ToString();
        }


        public static string StringToString(string strValue){
            return string.Format("N'{0}'", strValue);
        }

        public static string CharToString(char ch){
            return string.Format("'{0}'", ch);
        }

        public static string GuidToString(Guid guid){
            return string.Format("'{0}'", guid);
        }
    }
}
