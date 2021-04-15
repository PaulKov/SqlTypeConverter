

namespace SqlTypeConverter
{
    internal class AutoIncrement
    {
        private int id = 1;
        public int GenerateId()
        {
            return id++;
        }
    }
}
