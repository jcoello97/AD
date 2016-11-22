using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using Org.InstitutoSerpis.Ad;
using PArticulo;

namespace PArticulo
{
	public class ArticuloDao
	{
		private const string SELECT_SQL = "select * from articulo";
		private const string INSERT_SQL = "insert into articulo (nombre, precio, categoria) values (@nombre,@precio,@categoria)";
		private const string DELETE_SQL = "delete from articulo where id = @id";
		private const string UPDATE_SQL = "update articulo set nombre = @nombre, precio = @precio, categoria = @categoria where id = @id";
		private const string SELECT_ID_SQL = "select * from articulo where id = @id";
		public static IList getList ()
		{
			List<Articulo> list = new List<Articulo>();

			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand ();
			dbCommand.CommandText = SELECT_SQL;
			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				long id = (long)dataReader ["id"];
				string nombre = (string)dataReader ["nombre"];
				decimal? precio = dataReader ["precio"] is DBNull ? null : (decimal?)dataReader ["precio"];
				long? categoria = dataReader["categoria"] is DBNull ? null : (long?)dataReader["categoria"];
				Articulo articulo = new Articulo(id, nombre, precio, categoria);
				list.Add (articulo);
			}
			dataReader.Close ();
			return list;
		}
		public static void save(Articulo articulo)
		{
			if (articulo.Id == 0)
				insert (articulo);
			else
				update (articulo);
		}
		public static void insert(Articulo articulo){
			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand();

			dbCommand.CommandText = INSERT_SQL;

			DbCommandHelper.AddParameters (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameters (dbCommand, "precio", articulo.Precio);
			DbCommandHelper.AddParameters (dbCommand, "categoria", articulo.Categoria);
			dbCommand.ExecuteNonQuery ();
		}
		public static void update(Articulo articulo){
			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand();
			dbCommand.CommandText = UPDATE_SQL;

			DbCommandHelper.AddParameters(dbCommand,"nombre", articulo.Nombre);
			DbCommandHelper.AddParameters(dbCommand,"precio",articulo.Precio);
			DbCommandHelper.AddParameters(dbCommand,"categoria",articulo.Categoria);
			DbCommandHelper.AddParameters(dbCommand,"id",articulo.Id);
			dbCommand.ExecuteNonQuery ();
		}

		public static void delete(object id){
			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand();

			dbCommand.CommandText = DELETE_SQL;

			DbCommandHelper.AddParameters(dbCommand,"id",id);
			dbCommand.ExecuteNonQuery();
			//TODO: Implementar excepción si no elimina ningún registro.
		}
		public static Articulo Load(object id)
		{
			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand ();
			dbCommand.CommandText = SELECT_ID_SQL;
			DbCommandHelper.AddParameters(dbCommand,"id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();

			dataReader.Read ();
			string nombre = (string)dataReader ["nombre"];
			decimal? precio = dataReader ["precio"] is DBNull ? null : (decimal?)dataReader ["precio"];
			long? categoria = dataReader["categoria"] is DBNull ? null : (long?)dataReader["categoria"];
			Articulo articulo = new Articulo((long)id, nombre, precio, categoria);

			dataReader.Close ();
			return articulo;
		}
	}
}

