using System;

namespace SqlTypeConverter
{

    public class SqlFormatException : Exception
    {
        public SqlFormatException(SqlDataTypeValue sqlDataTypeValue, object value, SqlFormatExceptionType sqlFormatExType)
            : this("", sqlDataTypeValue, value, sqlFormatExType)
        {}

        public SqlFormatException(string message, SqlDataTypeValue sqlDataTypeValue, object value, SqlFormatExceptionType sqlFormatExType)
            : base(message)
        {
            SqlDataTypeValue = sqlDataTypeValue;
            Value = value;
            SqlFormatExType = sqlFormatExType;
        }

        public SqlDataTypeValue SqlDataTypeValue { get; private set; }
        public object Value { get; private set; }
        public SqlFormatExceptionType SqlFormatExType { get; private set; }

        public override string Message
        {
            get
            {
                string mess;

                if (SqlFormatExType == SqlFormatExceptionType.NonStringError)
                {
                    mess = $"Error converting string value [{Value}] to non-string sql data type [{SqlDataTypeValue}].";

                } else if (SqlFormatExType == SqlFormatExceptionType.InvalidPrecision)
                {
                    mess = $"The precision of the [{Value}] exceeds the precision of the [{SqlDataTypeValue}] sql data type.";
                }
                else if (SqlFormatExType == SqlFormatExceptionType.InvalidValueLength)
                {
                    mess = $"The length of the [{Value}](length: {Value?.ToString()?.Length}) exceeds the maximum length of the [{SqlDataTypeValue}] sql data type.";
                }
                else if (SqlFormatExType == SqlFormatExceptionType.NullError)
                {
                    mess = $"Error saving null value in nonnullable [{SqlDataTypeValue}] sql data type.";
                }
                else
                {
                    mess = $"Error converting [{Value}] value to [{SqlDataTypeValue}] sql data type.";
                }

                if (string.IsNullOrWhiteSpace(base.Message))
                    return mess;
                else
                    return $"{mess} Additional error message: {base.Message}.";

            }
        }

    }
}
