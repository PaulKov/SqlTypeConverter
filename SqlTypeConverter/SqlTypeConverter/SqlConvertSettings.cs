﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlTypeConverter
{
    public class SqlConvertSettings
    {

        private object replacementIfNull = null;
        private object replacementIfEmptyOrWhiteSpaceString = null;

        private string[] sourceFormats;
        private SqlDataTypeValue sqlDataTypeValue;
       
        public SqlConvertSettings(SqlDataTypeValue sqlDataTypeValue, string[] sourceFormats = null)
        {
            SqlDataTypeValue = sqlDataTypeValue ?? throw new ArgumentNullException(nameof(sqlDataTypeValue));
            SourceFormats = sourceFormats;
        }

        public SqlConvertSettings(SqlDataTypeValue sqlDataTypeValue, string[] sourceFormats, object replacementIfNull,
            object replacementIfEmptyOrWhiteSpaceString, SqlReplacementType nullReplacementType, SqlReplacementType emptyStringReplacementType) 
            : this(sqlDataTypeValue, sourceFormats)
        {
            ReplacementIfNull = replacementIfNull;
            ReplacementIfEmptyOrWhiteSpaceString = replacementIfEmptyOrWhiteSpaceString;
            NullReplacementType = nullReplacementType;
            EmptyStringReplacementType = emptyStringReplacementType;
        }

        public SqlDataTypeValue SqlDataTypeValue
        {

            get => sqlDataTypeValue;
            set => sqlDataTypeValue = value ?? throw new ArgumentNullException(nameof(sqlDataTypeValue));
        }

        public string[] SourceFormats
        {

            get => sourceFormats;
            set => sourceFormats = value;
        }

        public object ReplacementIfNull
        {

            get
            {

                if (NullReplacementType == SqlReplacementType.DEFAULT)
                {
                    if (SqlDataTypeValue.SqlDataType.IsNumericType() && !sqlDataTypeValue.IsNullable)
                    {
                        return 0;
                    }
                    else if (SqlDataTypeValue.SqlDataType.IsBinaryType() && !sqlDataTypeValue.IsNullable)
                    {
                        return new byte[] { };
                    }
                    else if (sqlDataTypeValue.IsNullable)
                    {
                        return DBNull.Value;
                    }
                }

                return this.replacementIfNull;
            }

            set
            {
                this.replacementIfNull = value;
            }
        }

        public object ReplacementIfEmptyOrWhiteSpaceString
        {

            get
            {

                if (EmptyStringReplacementType == SqlReplacementType.DEFAULT)
                {
                    if (SqlDataTypeValue.SqlDataType.IsNumericType() && !sqlDataTypeValue.IsNullable)
                    {
                        return 0;
                    }
                    else if (SqlDataTypeValue.SqlDataType.IsBinaryType() && !sqlDataTypeValue.IsNullable)
                    {
                        return new byte[] { };
                    }
                    else if (!SqlDataTypeValue.SqlDataType.IsCharType() && !SqlDataTypeValue.SqlDataType.IsTextType() 
                            && SqlDataTypeValue.SqlDataType != SqlDataType.VariantSql 
                            && !SqlDataTypeValue.SqlDataType.IsBinaryType()
                            && sqlDataTypeValue.IsNullable
                    )
                    {
                        return DBNull.Value;
                    }
                }

                return this.replacementIfEmptyOrWhiteSpaceString;
            }

            set
            {
                this.replacementIfEmptyOrWhiteSpaceString = value;
            }
        }

        public DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.AllowWhiteSpaces;

        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        public NumberStyles NumberStyles { get; set; } = NumberStyles.AllowLeadingSign
            | NumberStyles.AllowLeadingWhite
            | NumberStyles.AllowTrailingWhite;

        public SqlReplacementType NullReplacementType { get; set; } = SqlReplacementType.DEFAULT;
        public SqlReplacementType EmptyStringReplacementType { get; set; } = SqlReplacementType.DEFAULT;
    }
}
