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
			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand();

			dbCommand.CommandText = INSERT_SQL;

			DbCommandHelper.AddParameters(dbCommand,"nombre", articulo.Nombre);
			DbCommandHelper.AddParameters(dbCommand,"precio",articulo.Precio);
			DbCommandHelper.AddParameters(dbCommand,"categoria",articulo.Categoria);
			dbCommand.ExecuteNonQuery();
		}
	}
}

