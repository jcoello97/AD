using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PDbPrueba
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			IDbConnection dbcon = new MySqlConnection ("Database=dbprueba;User ID=root;Password=sistems");
			dbcon.Open ();

			dbcon.Close ();
		}
	}
}
