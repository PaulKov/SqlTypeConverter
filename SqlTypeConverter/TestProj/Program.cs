using SqlTypeConverter;
using System;
using System.Globalization;

namespace TestProj
{
    class Program
    {
        static void Main(string[] args)
        {
            /* test */

            String str = SqlTypeConverter.SqlTypeConverter.ConvertToSqlType("   ", new SqlConvertSettings(new SqlDataTypeValue { SqlDataType = SqlDataType.MoneySql}, null, null, "   $34  ", SqlReplacementType.CUSTOM, SqlReplacementType.CUSTOM, CultureInfo.GetCultureInfo("en-US"))).ToString();

            Console.WriteLine(str);

            Console.ReadKey(true);
        }
    }
}
