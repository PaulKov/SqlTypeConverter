using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlTypeConverter
{
    public abstract class Enumeration : IComparable
    {

        private static AutoIncrement IncID = new AutoIncrement();

        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        protected Enumeration(string name) => (Id, Name) = (IncID.GenerateId(), name);

        public override string ToString() => Name;

        public bool EqualsByName(string otherName, StringComparison comparisonType)
        {
            if (string.IsNullOrWhiteSpace(otherName))
                return false;

            return Name.Equals(otherName, comparisonType);
        }

        public bool EqualsByName(string otherName)
        {
            if (string.IsNullOrWhiteSpace(otherName))
                return false;

            return Name.Equals(otherName);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public static T FindByName<T>(string otherName, StringComparison comparisonType) where T : Enumeration =>
            GetAll<T>().Where(en => en.EqualsByName(otherName, comparisonType)).FirstOrDefault();

        public static T FindByName<T>(string otherName) where T : Enumeration =>
            GetAll<T>().Where(en => en.EqualsByName(otherName)).FirstOrDefault();

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    }
}
