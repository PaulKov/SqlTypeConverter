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

        public static bool IsIntegerType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.BigIntSql, SqlDataType.IntSql, SqlDataType.TinyIntSql, SqlDataType.SmallIntSql);
        }

        public static bool IsMonetaryType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.SmallMoneySql, SqlDataType.MoneySql);
        }

        public static bool IsFixedPrecisionNumericType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.NumericSql, SqlDataType.DecimalSql);
        }

        public static bool IsCharType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.CharSql, SqlDataType.NCharSql, SqlDataType.VarCharSql, SqlDataType.NVarCharSql);
        }

        public static bool IsBinaryType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.BinarySql, SqlDataType.VarBinarySql);
        }

        public static bool IsBooleanType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.BitSql);
        }

        public static bool IsDateTimeType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.DateTimeSql, SqlDataType.DateTime2Sql, SqlDataType.DateSql);
        }

        public static bool IsDateTimeOffsetType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.DateTimeOffsetSql);
        }

        public static bool IsTextType(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.TextSql, SqlDataType.NTextSql);
        }

        public static bool IsFloatingPointNumber(this SqlDataType sqlDataType)
        {
            return sqlDataType.In(SqlDataType.FloatSql, SqlDataType.RealSql);
        }
    }
}
