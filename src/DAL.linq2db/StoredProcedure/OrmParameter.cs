using System.Data;

namespace DAL.linq2db.StoredProcedure
{
    public class OrmParameter
    {
        public OrmParameter()
        {
        }
        public OrmParameter(string name, object value)
        {
            Name = name;
            Value = value;
            Type = OrmDataType.Undefined;
        }
        public OrmParameter(string name, object value, OrmDataType type)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public OrmDataType Type { get; set; }
        public ParameterDirection? Direction { get; set; }
        public bool IsArray { get; set; }
        public int? Size { get; set; }
    }
}