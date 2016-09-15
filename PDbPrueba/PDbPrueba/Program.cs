using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PDbPrueba
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IDbConnection dbcon;
			IDbCommand dbcmd;
			String opcion;


			dbcon = new MySqlConnection ("Database=dbprueba;User ID=root;Password=sistemas");
			dbcon.Open ();


			Console.WriteLine ("*****MENU BASE DE DATOS DBPRUEBA********");
			Console.WriteLine ("1. Nuevo");
			Console.WriteLine ("2. Editar");
			Console.WriteLine ("3. Eliminar");
			Console.WriteLine ("4. Listar todos");
			Console.WriteLine ("0. Salir");
			Console.WriteLine ("Elige una opcion (numero): ");

			opcion = Console.ReadLine ();

		long num = leerNumero (opcion);
			switch (num) {
			case 0:

				Console.WriteLine ("Hasta luego ! ");
				System.Environment.Exit (-1);
				dbcon.Close ();
				break;

			}

			dbcmd = dbcon.CreateCommand (); //para ejecutar comandos sql


		}
		private static long leerNumero (String opcion)
		{
			while(true)
			{
				try
				{
					return long.Parse(opcion);
				}catch
				{
					Console.WriteLine ("Solo numeros por favor.");
				}
			}

		}
	}
}
