using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace Org.InstitutoSerpis.Ad
{
	public class ConexionSGBD
	{
		private static ConexionSGBD instance = new ConexionSGBD();
		private IDbConnection connection = new MySqlConnection (
			"Database=dbprueba;User Id=root;Password=sistemas"
			);
		private ConexionSGBD()
		{
			//VACIO
		}
		public static ConexionSGBD Instance
		{
			get{return instance;}
		}


		public IDbConnection dbConnection
		{
			get { return connection;}
			set { connection = value;}
		}

	}
}

