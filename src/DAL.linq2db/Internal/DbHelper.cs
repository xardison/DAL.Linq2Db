using System;
using System.Data;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;

namespace DAL.linq2db.Internal
{
    internal static class DbHelper
    {
        public static string GetProvider()
        {
            return DalParams.DatabaseProviderName;
        }

        public static string GetConnectionString(string database)
        {
            return ConnectionStringManager.GetConnectionString(database);
        }

        public static DataType GetDataType(OrmDataType ormDataType)
        {
            DataType result;
            if (!Enum.TryParse(ormDataType.ToString(), true, out result))
            {
                throw new ArgumentException(
                    $"Error during convert data type. Unknow data type = {ormDataType.ToString()}");
            }

            return result;
        }

        public static DataParameter[] GetDataParameter(OrmParameter[] ormParameters)
        {
            if (ormParameters == null)
            {
                return null;
            }

            var result = new DataParameter[ormParameters.Length];

            for (int i = 0; i < ormParameters.Length; i++)
            {
                result[i] = GetDataParameter(ormParameters[i]);
            }

            return result;
        }

        private static DataParameter GetDataParameter(OrmParameter ormParameter)
        {
            var dataParam = ormParameter.Type == OrmDataType.Undefined
                ? new DataParameter(ormParameter.Name, ormParameter.Value)
                : new DataParameter(ormParameter.Name, ormParameter.Value, GetDataType(ormParameter.Type));

            if (dataParam.Value is Enum)
            {
                var type = dataParam.Value.GetType();
                var baseType = Enum.GetUnderlyingType(type);
                dataParam.Value = Convert.ChangeType(dataParam.Value, baseType);
            }

            dataParam.Direction = ormParameter.Direction;
            dataParam.Size = ormParameter.Size;
            dataParam.IsArray = ormParameter.IsArray;

            return dataParam;
        }

        public static void UpdateConnectionInfo(IDbConnection conn)
        {
            if (DalParams.ClientId == null && DalParams.ClientInfo == null &&
                DalParams.ModuleName == null && DalParams.ActionName == null)
            {
                return;
            }

            var props = conn.GetType().GetProperties();

            if (DalParams.ClientId != null)
            {
                var clientIdProp = props.FirstOrDefault(x => x.Name == "ClientId");
                if (clientIdProp != null)
                {
                    clientIdProp.SetValue(conn, DalParams.ClientId);
                }
            }

            if (DalParams.ClientInfo != null)
            {
                var clientInfoProp = props.FirstOrDefault(x => x.Name == "ClientInfo");
                if (clientInfoProp != null)
                {
                    clientInfoProp.SetValue(conn, DalParams.ClientInfo);
                }
            }

            if (DalParams.ModuleName != null)
            {
                var moduleNameProp = props.FirstOrDefault(x => x.Name == "ModuleName");
                if (moduleNameProp != null)
                {
                    moduleNameProp.SetValue(conn, DalParams.ModuleName);
                }
            }

            if (DalParams.ActionName != null)
            {
                var actionNameProp = props.FirstOrDefault(x => x.Name == "ActionName");
                if (actionNameProp != null)
                {
                    actionNameProp.SetValue(conn, DalParams.ActionName);
                }
            }
        }

    }
}