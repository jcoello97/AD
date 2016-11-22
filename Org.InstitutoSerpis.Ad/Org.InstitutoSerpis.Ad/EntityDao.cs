using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using Org.InstitutoSerpis.Ad;
using System.Reflection;
using PArticulo;
namespace Org.InstitutoSerpis.Ad
{
	public class EntityDao
	{
		private const string SELECT_SQL = "select * from {0}";
		public static List<TEntity> getList<TEntity>()
		{
			List<TEntity> list = new List<TEntity> ();
			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand ();

			Type type = typeof(TEntity);			
			dbCommand.CommandText = string.Format(SELECT_SQL,type.Name.ToLower());

			IDataReader dataReader = dbCommand.ExecuteReader ();

			while (dataReader.Read()) {
				TEntity item = Activator.CreateInstance<TEntity>();
				SetProperties (item,dataReader);
				list.Add (item);
			}
			dataReader.Close ();
			return list;
		}
		private static void SetProperties(object entity, IDataReader dataReader)
		{
			Type typeEntity = entity.GetType ();
			foreach (PropertyInfo propertyInfo in typeEntity.GetProperties()) {
				object value = dataReader [propertyInfo.Name.ToLower ()];
				if(value is DBNull){
					value = null;
				}
				propertyInfo.SetValue (entity,value,null);
			}
		}
	}
}

