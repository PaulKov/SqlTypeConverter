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

        public static SqlDataType BigIntSql             = new SqlDataType("bigint",             typeof(long), SqlDateTypeGroup.IntegerTypes);
        public static SqlDataType BinarySql             = new SqlDataType("binary",             typeof(byte[]), SqlDateTypeGroup.BinaryTypes);
        public static SqlDataType BitSql                = new SqlDataType("bit",                typeof(bool), SqlDateTypeGroup.BooleanTypes);
        public static SqlDataType CharSql               = new SqlDataType("char",               typeof(char), SqlDateTypeGroup.CharTypes);
        public static SqlDataType DateTimeSql           = new SqlDataType("datetime",           typeof(DateTime), SqlDateTypeGroup.DateTimeTypes);
        public static SqlDataType NumericSql            = new SqlDataType("numeric",            typeof(decimal), SqlDateTypeGroup.FixedPrecisionTypes);
        public static SqlDataType DecimalSql            = new SqlDataType("decimal",            typeof(decimal), SqlDateTypeGroup.FixedPrecisionTypes);
        public static SqlDataType FloatSql              = new SqlDataType("float",              typeof(double), SqlDateTypeGroup.FloatingPointTypes);
        public static SqlDataType ImageSql              = new SqlDataType("image",              typeof(byte[]), SqlDateTypeGroup.BinaryTypes);
        public static SqlDataType IntSql                = new SqlDataType("int",                typeof(int), SqlDateTypeGroup.IntegerTypes);
        public static SqlDataType MoneySql              = new SqlDataType("money",              typeof(decimal), SqlDateTypeGroup.MonetaryTypes);
        public static SqlDataType NCharSql              = new SqlDataType("nchar",              typeof(string), SqlDateTypeGroup.CharTypes);
        public static SqlDataType NTextSql              = new SqlDataType("ntext",              typeof(string), SqlDateTypeGroup.TextTypes);
        public static SqlDataType NVarCharSql           = new SqlDataType("nvarchar",           typeof(string), SqlDateTypeGroup.CharTypes);
        public static SqlDataType RealSql               = new SqlDataType("real",               typeof(float), SqlDateTypeGroup.FloatingPointTypes);
        public static SqlDataType UniqueIdentifierSql   = new SqlDataType("uniqueidentifier",   typeof(Guid), SqlDateTypeGroup.GuidTypes);
        public static SqlDataType SmallDateTimeSql      = new SqlDataType("smalldatetime",      typeof(DateTime), SqlDateTypeGroup.DateTimeTypes);
        public static SqlDataType SmallIntSql           = new SqlDataType("smallint",           typeof(short), SqlDateTypeGroup.IntegerTypes);
        public static SqlDataType SmallMoneySql         = new SqlDataType("smallmoney",         typeof(decimal), SqlDateTypeGroup.MonetaryTypes);
        public static SqlDataType TextSql               = new SqlDataType("text",               typeof(string), SqlDateTypeGroup.TextTypes);
        public static SqlDataType TimestampSql          = new SqlDataType("timestamp",          typeof(byte[]), SqlDateTypeGroup.VersionTypes);
        public static SqlDataType RowversionSql         = new SqlDataType("rowversion",         typeof(byte[]), SqlDateTypeGroup.VersionTypes);
        public static SqlDataType TinyIntSql            = new SqlDataType("tinyint",            typeof(byte), SqlDateTypeGroup.IntegerTypes);
        public static SqlDataType VarBinarySql          = new SqlDataType("varbinary",          typeof(byte[]), SqlDateTypeGroup.BinaryTypes);
        public static SqlDataType VarCharSql            = new SqlDataType("varchar",            typeof(string), SqlDateTypeGroup.CharTypes);
        public static SqlDataType VariantSql            = new SqlDataType("sql_variant",        typeof(object), SqlDateTypeGroup.OtherTypes);
        public static SqlDataType XmlSql                = new SqlDataType("xml",                typeof(string), SqlDateTypeGroup.TextTypes);
        public static SqlDataType DateSql               = new SqlDataType("date",               typeof(DateTime), SqlDateTypeGroup.DateTimeTypes);
        public static SqlDataType TimeSql               = new SqlDataType("time",               typeof(TimeSpan), SqlDateTypeGroup.TimeTypes);
        public static SqlDataType DateTime2Sql          = new SqlDataType("datetime2",          typeof(DateTime), SqlDateTypeGroup.DateTimeTypes);
        public static SqlDataType DateTimeOffsetSql     = new SqlDataType("datetimeoffset",     typeof(DateTimeOffset), SqlDateTypeGroup.DateTimeOffsetTypes);

        public Type NetDataType { get; private set; }
        public SqlDateTypeGroup SqlTypeGroup { get; private set; }

        public SqlDataType(string name, Type netDataType, SqlDateTypeGroup sqlTypeGroup)
            : base(name)
        {
            NetDataType = netDataType;
            SqlTypeGroup = sqlTypeGroup;
        }

        public static SqlDataType GetSqlDataTypeByName(String sqlDataTypeStr)
        {
            return FindByName<SqlDataType>(sqlDataTypeStr, StringComparison.OrdinalIgnoreCase);
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
