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
	public class CategoriaDao
	{
		private const string SELECT_SQL = "select * from categoria order by nombre";

		public static List<Categoria> GetList()
		{
			List<Categoria> listaCategorias = new List<Categoria> ();

			IDbCommand dbCommand = ConexionSGBD.Instance.dbConnection.CreateCommand();
			dbCommand.CommandText = SELECT_SQL;

			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				long id = (long)dataReader ["id"];
				string nombre = (string)dataReader ["nombre"];
				Categoria categoria = new Categoria (id, nombre);
				listaCategorias.Add (categoria);
			}
			dataReader.Close ();
			return listaCategorias;
		}
	}
}

