using SqlTypeConverter;
using System;
using System.Data.SqlTypes;
using System.Globalization;

namespace TestProj
{
    class Program
    {
        static void Main(string[] args)
        {
            /* test */

            //String str = SqlTypeConverter.SqlTypeConverter.ConvertToSqlType("   ", new SqlConvertSettings(new SqlDataTypeValue { SqlDataType = SqlDataType.MoneySql}, null, null, "   $34  ", SqlReplacementType.CUSTOM, SqlReplacementType.CUSTOM, CultureInfo.GetCultureInfo("en-US"))).ToString();

            //Console.WriteLine(str);

            string[] formats = new string[] {@"@M/dd/yyyy HH:m zzz", @"MM/dd/yyyy HH:m zzz",
                                 @"M/d/yyyy HH:m zzz", @"MM/d/yyyy HH:m zzz",
                                 @"M/dd/yy HH:m zzz", @"MM/dd/yy HH:m zzz",
                                 @"M/d/yy HH:m zzz", @"MM/d/yy HH:m zzz",
                                 @"M/dd/yyyy H:m zzz", @"MM/dd/yyyy H:m zzz",
                                 @"M/d/yyyy H:m zzz", @"MM/d/yyyy H:m zzz",
                                 @"M/dd/yy H:m zzz", @"MM/dd/yy H:m zzz",
                                 @"M/d/yy H:m zzz", @"MM/d/yy H:m zzz",
                                 @"M/dd/yyyy HH:mm zzz", @"MM/dd/yyyy HH:mm zzz",
                                 @"M/d/yyyy HH:mm zzz", @"MM/d/yyyy HH:mm zzz",
                                 @"M/dd/yy HH:mm zzz", @"MM/dd/yy HH:mm zzz",
                                 @"M/d/yy HH:mm zzz", @"MM/d/yy HH:mm zzz",
                                 @"M/dd/yyyy H:mm zzz", @"MM/dd/yyyy H:mm zzz",
                                 @"M/d/yyyy H:mm zzz", @"MM/d/yyyy H:mm zzz",
                                 @"M/dd/yy H:mm zzz", @"MM/dd/yy H:mm zzz",
                                 @"M/d/yy H:mm zzz", @"MM/d/yy H:mm zzz", @"MM/d/yy H zzz", @"MM/d/yy zzz"};

            IFormatProvider provider = CultureInfo.InvariantCulture.DateTimeFormat;
            DateTimeOffset result = new DateTimeOffset();

            result = DateTimeOffset.ParseExact("12/2/21 -08:01", formats, provider,
                                                DateTimeStyles.AllowWhiteSpaces);

            Console.WriteLine(result);

            Console.ReadKey(true);
        }
    }
}
