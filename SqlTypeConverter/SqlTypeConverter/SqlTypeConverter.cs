using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;

namespace SqlTypeConverter
{
    public class SqlTypeConverter
    {

        public static object ConvertToSqlType(object value, SqlConvertSettings convertSettings)
        {

            if (convertSettings == null)
                throw new ArgumentNullException(nameof(convertSettings));

            SqlDataTypeValue sqlDataTypeValue = convertSettings.SqlDataTypeValue;
            SqlDataType sqlDataType = sqlDataTypeValue.SqlDataType;

            try
            {
                if (value.In(null, DBNull.Value))
                {
                    if (convertSettings.ReplacementIfNull != null
                        && convertSettings.ReplacementIfNull != DBNull.Value
                        && convertSettings.NullReplacementType != SqlReplacementType.NONE)
                    {
                        value = convertSettings.ReplacementIfNull;

                    }
                    else
                    {
                        if (!sqlDataTypeValue.IsNullable)
                            throw new SqlFormatException(sqlDataTypeValue, value, SqlFormatExceptionType.NullError);

                        return DBNull.Value;

                    }

                }
                else if (string.IsNullOrWhiteSpace(value.ToString()))
                {

                    if (convertSettings.EmptyStringReplacementType != SqlReplacementType.NONE)
                    {
                        value = convertSettings.ReplacementIfEmptyOrWhiteSpaceString;

                        if (value.In(null, DBNull.Value))
                        {
                            if (sqlDataTypeValue.IsNullable)
                                return DBNull.Value;
                            else 
                                throw new SqlFormatException(sqlDataTypeValue, value, SqlFormatExceptionType.NullError);
                        }                            
                    }
                }

                string[] formats = convertSettings.SourceFormats;

                if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.TextTypes || sqlDataType == SqlDataType.VariantSql)
                {
                    return value;
                }
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.CharTypes)
                {
                    if (sqlDataTypeValue.MaxLength > 0 && value.ToString().Length > sqlDataTypeValue.MaxLength)
                        throw new SqlFormatException(sqlDataTypeValue, value, SqlFormatExceptionType.InvalidValueLength);

                    return value;
                }
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.DateTimeTypes)
                {

                    if (value is DateTime || value is SqlDateTime)
                    {

                        return value;
                    }
                    else if (DateTime.TryParseExact(value.ToString().Trim(), formats, convertSettings.Culture, convertSettings.DateTimeStyles, out DateTime resDt))
                    {
                        return resDt;
                    }
                }
                else if(sqlDataType.SqlTypeGroup == SqlDateTypeGroup.IntegerTypes)
                {
                    return ConvertToSqlIntegerType(value, convertSettings);
                }
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.TimeTypes)
                {

                    if (value is TimeSpan ts)
                    {
                        return ts;
                    }
                    else if (TimeSpan.TryParseExact(value.ToString().Trim(), formats, convertSettings.Culture, TimeSpanStyles.None, out TimeSpan resTime))
                    {

                        return resTime;
                    }

                }
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.DateTimeOffsetTypes)
                {

                    if (value is DateTimeOffset dtOffset)
                    {

                        return dtOffset;
                    }
                    else if (DateTimeOffset.TryParseExact(value.ToString().Trim(), formats, convertSettings.Culture, convertSettings.DateTimeStyles, out DateTimeOffset resDtOffset))
                    {

                        return resDtOffset;
                    }

                }
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.BooleanTypes)
                {

                    if (value is bool || value is SqlBoolean)
                    {
                        return value;
                    }
                    else if (value.ToString().Trim().In("1", "0"))
                    {
                        return value.ToString().Trim() == "1";
                    }
                    else if (bool.TryParse(value.ToString().Trim(), out bool resBool))
                    {
                        return resBool;
                    }

                }
                else if (sqlDataType.SqlTypeGroup.In(SqlDateTypeGroup.MonetaryTypes, SqlDateTypeGroup.FixedPrecisionTypes))
                {
                    decimal d;
                    
                    if (value is decimal || value is SqlDecimal || value is SqlMoney)
                    {
                        d = (decimal)value;
                    }
                    else
                    {
                        d = decimal.Parse(value.ToString().Trim(), convertSettings.NumberStyles | NumberStyles.AllowDecimalPoint | NumberStyles.Number | NumberStyles.AllowCurrencySymbol, convertSettings.Culture);
                    }

                    return d;

                }
                else if (sqlDataType == SqlDataType.FloatSql)
                {
                    if (value is double || value is SqlDouble)
                    {
                        return value;

                    }
                    else if (double.TryParse(value.ToString().Trim(), convertSettings.NumberStyles | NumberStyles.Float, convertSettings.Culture, out double resDouble))
                    {
                        return resDouble;
                    }
                }
                else if (sqlDataType == SqlDataType.RealSql)
                {
                    if (value is float || value is SqlSingle)
                    {
                        return value;

                    }
                    else if (float.TryParse(value.ToString().Trim(), convertSettings.NumberStyles | NumberStyles.Float, convertSettings.Culture, out float resFloat))
                    {
                        return resFloat;
                    }
                }
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.BinaryTypes)
                {
                    if (value is byte[] || value is SqlBinary)
                    {
                        return value;
                    }
                    else
                    {

                        var arr = value.ToString().ToCharArray().Select(c => (byte) c).ToArray();

                        if(sqlDataType != SqlDataType.ImageSql)
                        {
                            if (sqlDataTypeValue.MaxLength > 0 && arr.Length > sqlDataTypeValue.MaxLength)
                                throw new SqlFormatException(sqlDataTypeValue, value, SqlFormatExceptionType.InvalidValueLength);

                        }

                        return arr;
                    }

                } 
                else if (sqlDataType.SqlTypeGroup == SqlDateTypeGroup.GuidTypes)
                {
                    if(value is Guid || value is SqlGuid)
                    {
                        return value;

                    } else if(Guid.TryParse(value.ToString().Trim(), out Guid resGuid))
                    {
                        return resGuid;
                    }
                }                
                else
                {
                    return value;
                }

  
            } catch(Exception ex)
            {
                if (ex is SqlFormatException)
                    throw ex;
                else
                    throw new SqlFormatException(ex.Message, sqlDataTypeValue, value, SqlFormatExceptionType.InvalidValueError);
            }

            throw new SqlFormatException(sqlDataTypeValue, value, SqlFormatExceptionType.InvalidValueError);
        }

        private static object ConvertToSqlIntegerType(object value, SqlConvertSettings convertSettings)
        {
            SqlDataTypeValue sqlDataTypeValue = convertSettings.SqlDataTypeValue;
            SqlDataType sqlDataType = sqlDataTypeValue.SqlDataType;

            try
            {
                if (sqlDataType == SqlDataType.BigIntSql)
                {
                    if(value is long || value is SqlInt64)
                    {
                        return value;
                    }
                    else if (long.TryParse(value.ToString().Trim(), convertSettings.NumberStyles, convertSettings.Culture, out long res))
                    {
                        return res;
                    }

                } else if (sqlDataType == SqlDataType.IntSql) {

                    if(value is int || value is SqlInt32)
                    {
                        return value;
                    }
                    else if (int.TryParse(value.ToString().Trim(), convertSettings.NumberStyles, convertSettings.Culture, out int res))
                    {
                        return res;
                    }
                }
                else if (sqlDataType == SqlDataType.SmallIntSql)
                {
                    if(value is short || value is SqlInt16)
                    {
                        return value;
                    }
                    else if (short.TryParse(value.ToString().Trim(), convertSettings.NumberStyles, convertSettings.Culture, out short res))
                    {
                        return res;
                    }
                }
                else if (sqlDataType == SqlDataType.TinyIntSql)
                {
                    if(value is byte || value is SqlByte)
                    {
                        return value;
                    }
                    else if (byte.TryParse(value.ToString().Trim(), convertSettings.NumberStyles, convertSettings.Culture, out byte res))
                    {
                        return res;
                    }
                }

            } catch(Exception ex)
            {
                throw new SqlFormatException(ex.Message, sqlDataTypeValue, value, SqlFormatExceptionType.InvalidValueError);
            }

            throw new SqlFormatException(sqlDataTypeValue, value, SqlFormatExceptionType.InvalidValueError);
        }

    }
}
