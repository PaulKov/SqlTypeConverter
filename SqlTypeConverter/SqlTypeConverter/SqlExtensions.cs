using System;
using System.Collections.Generic;


namespace SqlTypeConverter
{
    internal static class SqlExtensions
    {

        public static IEnumerable<T> Select<T>(
             this System.Data.IDataReader reader
            , Func<System.Data.IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}

