using System;

namespace SqlTypeConverter
{
    public static class SqlDataTypeExtensions
    {
        public static bool IsNumericType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.BigIntSql,
                                  SqlDataType.IntSql,
                                  SqlDataType.TinyIntSql,
                                  SqlDataType.FloatSql,
                                  SqlDataType.RealSql,
                                  SqlDataType.NumericSql,
                                  SqlDataType.DecimalSql,
                                  SqlDataType.SmallIntSql,
                                  SqlDataType.SmallMoneySql,
                                  SqlDataType.MoneySql);
        }
    }
}
