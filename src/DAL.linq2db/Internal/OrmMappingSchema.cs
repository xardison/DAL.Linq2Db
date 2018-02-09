/*
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DAL.linq2db.Internal
{
    /// NET -> ORM.OrmDataType
    internal class OrmMappingSchema
    {
        private static object _lockObject = new object();
        private static volatile ConcurrentDictionary<Type, OrmDataType> _dbTypes;

        /// LinqToDB.Mapping.MappingSchema
        static OrmMappingSchema()
        {
            lock (_lockObject)
            {
                if (_dbTypes == null)
                {
                    _dbTypes = new ConcurrentDictionary<Type, OrmDataType>();
                }
            }

            AddDbType(typeof(Enum), OrmDataType.Int32);
            AddDbType(typeof(char), OrmDataType.NChar);
            AddDbType(typeof(char?), OrmDataType.NChar);
            AddDbType(typeof(string), OrmDataType.NVarChar);
            AddDbType(typeof(Decimal), OrmDataType.Decimal);
            AddDbType(typeof(Decimal?), OrmDataType.Decimal);
            AddDbType(typeof(DateTime), OrmDataType.DateTime2);
            AddDbType(typeof(DateTime?), OrmDataType.DateTime2);
            AddDbType(typeof(DateTimeOffset), OrmDataType.DateTimeOffset);
            AddDbType(typeof(DateTimeOffset?), OrmDataType.DateTimeOffset);
            AddDbType(typeof(TimeSpan), OrmDataType.Time);
            AddDbType(typeof(TimeSpan?), OrmDataType.Time);
            AddDbType(typeof(byte[]), OrmDataType.VarBinary);
            AddDbType(typeof(Binary), OrmDataType.VarBinary);
            AddDbType(typeof(Guid), OrmDataType.Guid);
            AddDbType(typeof(Guid?), OrmDataType.Guid);
            AddDbType(typeof(object), OrmDataType.Variant);
            AddDbType(typeof(XmlDocument), OrmDataType.Xml);
            AddDbType(typeof(XDocument), OrmDataType.Xml);
            AddDbType(typeof(bool), OrmDataType.Boolean);
            AddDbType(typeof(bool?), OrmDataType.Boolean);
            AddDbType(typeof(sbyte), OrmDataType.SByte);
            AddDbType(typeof(sbyte?), OrmDataType.SByte);
            AddDbType(typeof(short), OrmDataType.Int16);
            AddDbType(typeof(short?), OrmDataType.Int16);
            AddDbType(typeof(int), OrmDataType.Int32);
            AddDbType(typeof(int?), OrmDataType.Int32);
            AddDbType(typeof(long), OrmDataType.Int64);
            AddDbType(typeof(long?), OrmDataType.Int64);
            AddDbType(typeof(byte), OrmDataType.Byte);
            AddDbType(typeof(byte?), OrmDataType.Byte);
            AddDbType(typeof(ushort), OrmDataType.UInt16);
            AddDbType(typeof(ushort?), OrmDataType.UInt16);
            AddDbType(typeof(uint), OrmDataType.UInt32);
            AddDbType(typeof(uint?), OrmDataType.UInt32);
            AddDbType(typeof(ulong), OrmDataType.UInt64);
            AddDbType(typeof(ulong?), OrmDataType.UInt64);
            AddDbType(typeof(float), OrmDataType.Single);
            AddDbType(typeof(float?), OrmDataType.Single);
            AddDbType(typeof(double), OrmDataType.Double);
            AddDbType(typeof(double?), OrmDataType.Double);
            AddDbType(typeof(BitArray), OrmDataType.BitArray);
        }

        public static OrmDataType GetDbType(Type type)
        {
            if (type == null)
            {
                return OrmDataType.Undefined;
            }

            if (type.BaseType != null && type.BaseType.Name == "Enum")
            {
                type = type.BaseType;
            }

            OrmDataType value;
            var ok = _dbTypes.TryGetValue(type, out value);

            if (!ok)
            {
                throw new ArgumentException("unknow type of value = " + type.Name);
            }

            /// todo: return ok ? value : OrmDataType.Undefined; 

            return value;
        }

        public static void AddDbType(Type type, OrmDataType dataType = OrmDataType.Undefined)
        {
            _dbTypes[type] = dataType;
        }
    }
}
*/