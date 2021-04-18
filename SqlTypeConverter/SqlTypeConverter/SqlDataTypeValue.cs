using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlTypeConverter
{
    public class SqlDataTypeValue
    {

        private PropertyInfo[] PropertyInfoArr = null;

        public SqlDataTypeValue()
        {
        }

        public SqlDataTypeValue(SqlDataType sqlDataType, int maxLength, int precision, int scale, bool isNullable)
        {
            SqlDataType = sqlDataType ?? throw new ArgumentNullException(nameof(sqlDataType));
            MaxLength = maxLength;
            Precision = precision;
            Scale = scale;
            IsNullable = isNullable;
        }

        public SqlDataTypeValue(String sqlDataTypeStr, int maxLength, int precision, int scale, bool isNullable)
        {

            SqlDataType = SqlDataType.GetSqlDataTypeByName(sqlDataTypeStr);

            if(SqlDataType == null)
                throw new ArgumentException($"Unable to convert string {sqlDataTypeStr} to type {nameof(SqlDataType)}", nameof(sqlDataTypeStr)); ;

            MaxLength = maxLength;
            Precision = precision;
            Scale = scale;
            IsNullable = isNullable;
        }

        public SqlDataType SqlDataType { get; set; }

        public int MaxLength { get; set; }

        public int Precision { get; set; }

        public int Scale { get; set; }

        public bool IsNullable { get; set; }

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
