using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlTypeConverter
{
    public class SqlDataType : Enumeration
    {
        private PropertyInfo[] PropertyInfoArr = null;

        public static SqlDataType BigIntSql             = new SqlDataType("bigint", typeof(long));
        public static SqlDataType BinarySql             = new SqlDataType("binary", typeof(byte[]));
        public static SqlDataType BitSql                = new SqlDataType("bit", typeof(bool));
        public static SqlDataType CharSql               = new SqlDataType("char", typeof(char));
        public static SqlDataType DateTimeSql           = new SqlDataType("datetime", typeof(DateTime));
        public static SqlDataType NumericSql            = new SqlDataType("numeric", typeof(decimal));
        public static SqlDataType DecimalSql            = new SqlDataType("decimal", typeof(decimal));
        public static SqlDataType FloatSql              = new SqlDataType("float", typeof(double));
        public static SqlDataType ImageSql              = new SqlDataType("image", typeof(byte[]));
        public static SqlDataType IntSql                = new SqlDataType("int", typeof(int));
        public static SqlDataType MoneySql              = new SqlDataType("money", typeof(decimal));
        public static SqlDataType NCharSql              = new SqlDataType("nchar", typeof(string));
        public static SqlDataType NTextSql              = new SqlDataType("ntext", typeof(string));
        public static SqlDataType NVarCharSql           = new SqlDataType("nvarchar", typeof(string));
        public static SqlDataType RealSql               = new SqlDataType("real", typeof(float));
        public static SqlDataType UniqueIdentifierSql   = new SqlDataType("uniqueidentifier", typeof(Guid));
        public static SqlDataType SmallDateTimeSql      = new SqlDataType("smalldatetime", typeof(DateTime));
        public static SqlDataType SmallIntSql           = new SqlDataType("smallint", typeof(short));
        public static SqlDataType SmallMoneySql         = new SqlDataType("smallmoney", typeof(decimal));
        public static SqlDataType TextSql               = new SqlDataType("text", typeof(string));
        public static SqlDataType TimestampSql          = new SqlDataType("timestamp", typeof(byte[]));
        public static SqlDataType RowversionSql         = new SqlDataType("rowversion", typeof(byte[]));
        public static SqlDataType TinyIntSql            = new SqlDataType("tinyint", typeof(byte));
        public static SqlDataType VarBinarySql          = new SqlDataType("varbinary", typeof(byte[]));
        public static SqlDataType VarCharSql            = new SqlDataType("varchar", typeof(string));
        public static SqlDataType VariantSql            = new SqlDataType("sql_variant", typeof(object));
        public static SqlDataType XmlSql                = new SqlDataType("xml", typeof(string));
        public static SqlDataType DateSql               = new SqlDataType("date", typeof(DateTime));
        public static SqlDataType TimeSql               = new SqlDataType("time", typeof(TimeSpan));
        public static SqlDataType DateTime2Sql          = new SqlDataType("datetime2", typeof(DateTime));
        public static SqlDataType DateTimeOffsetSql     = new SqlDataType("datetimeoffset", typeof(DateTimeOffset));

        public Type NetDataType { get; private set; }

        public SqlDataType(string name, Type netDataType)
            : base(name)
        {
            this.NetDataType = netDataType;
        }

        public override string ToString()
        {
            if (PropertyInfoArr == null)
                PropertyInfoArr = GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in PropertyInfoArr)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine($"{info.Name}: {value}");
            }

            return sb.ToString();
        }
    }
}
