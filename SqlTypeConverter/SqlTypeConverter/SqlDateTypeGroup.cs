using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlTypeConverter
{
    public class SqlDateTypeGroup : Enumeration
    {
        public static SqlDateTypeGroup MonetaryTypes = new SqlDateTypeGroup(nameof(MonetaryTypes));
        public static SqlDateTypeGroup IntegerTypes = new SqlDateTypeGroup(nameof(IntegerTypes));
        public static SqlDateTypeGroup FixedPrecisionTypes = new SqlDateTypeGroup(nameof(FixedPrecisionTypes));
        public static SqlDateTypeGroup CharTypes = new SqlDateTypeGroup(nameof(CharTypes));
        public static SqlDateTypeGroup VersionTypes = new SqlDateTypeGroup(nameof(VersionTypes));
        public static SqlDateTypeGroup TextTypes = new SqlDateTypeGroup(nameof(TextTypes));
        public static SqlDateTypeGroup BinaryTypes = new SqlDateTypeGroup(nameof(BinaryTypes));
        public static SqlDateTypeGroup BooleanTypes = new SqlDateTypeGroup(nameof(BooleanTypes));
        public static SqlDateTypeGroup DateTimeTypes = new SqlDateTypeGroup(nameof(DateTimeTypes));
        public static SqlDateTypeGroup DateTimeOffsetTypes = new SqlDateTypeGroup(nameof(DateTimeOffsetTypes));
        public static SqlDateTypeGroup FloatingPointTypes = new SqlDateTypeGroup(nameof(FloatingPointTypes));
        public static SqlDateTypeGroup GuidTypes = new SqlDateTypeGroup(nameof(GuidTypes));
        public static SqlDateTypeGroup TimeTypes = new SqlDateTypeGroup(nameof(TimeTypes));
        public static SqlDateTypeGroup OtherTypes = new SqlDateTypeGroup(nameof(OtherTypes));

        public SqlDateTypeGroup(string name)
            : base(name)
        {
        }
    }
}
