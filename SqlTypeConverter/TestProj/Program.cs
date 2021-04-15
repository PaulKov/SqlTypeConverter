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

            DateTimeOffset d = (DateTimeOffset) SqlTypeConverter.SqlTypeConverter.ConvertToSqlType(" 20.12.1900T12:04:54.123 -08:00", new SqlConvertSettings(new SqlDataTypeValue { SqlDataType = SqlDataType.DateTimeOffsetSql}, new string[] { "dd.MM.yyyyTHH:mm:ss.fff zzz   " }));

            Console.WriteLine(d.ToString("dd.MM.yyyy HH:mm:ss.fff zzz"));

            Console.ReadKey(true);
        }
    }
}
