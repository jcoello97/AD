using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PDbPrueba
{
	class MainClass
	{
		protected static IDbConnection dbcon = null;
		protected static IDbCommand dbcmd = null, dbcmdLeerID = null;
		protected static String opcion, idBuscado, nombreNuevo, id;
		protected static IDataReader reader = null;
		protected static Boolean encontrado = false;

		public static void Main (string[] args)
		{
			dbcon = new MySqlConnection ("Database=dbprueba;User ID=root;Password=sistemas");
			dbcmd = dbcon.CreateCommand (); //para ejecutar comandos sql
			dbcmdLeerID = dbcon.CreateCommand ();
			dbcmdLeerID.CommandText = "select id from categoria";
			dbcon.Open ();

			do
			{
				Console.Clear();
				Console.WriteLine ("*****MENU PRINCIPAL********");
				Console.WriteLine ("1. Nuevo");
				Console.WriteLine ("2. Editar");
				Console.WriteLine ("3. Eliminar");
				Console.WriteLine ("4. Listar todos");
				Console.WriteLine ("0. Salir");
				Console.WriteLine ("Elige una opción (numero): ");
				opcion = Console.ReadLine ();

				switch (leerNumero (opcion)) 
				{
					case 0:
					CerrarPrograma();
						break;
					case 1:
					NuevaFila();
						break;
					case 2:
					EditarFila();
						break;
					case 3:
					BorrarFila();
						break;
					case 4:
					ListarFilas();
						break;

					default:
					Console.WriteLine("\nElige solo un numero de las opciones del menu.");
					PulsarParaVolver();
						break;
				}

			}while(dbcon!=null);
		}

		private static long leerNumero (String s)
		{
			Boolean salir = false;
			while(salir !=true)
			{
				try
				{
					return long.Parse(s);;
				}catch 
				{
					Console.WriteLine ("Solo numeros por favor.");
					salir = true;
				}
			}
			return 8; //Para que vaya al case default del switch
		}
		private static void CerrarPrograma()
		{
			Console.Clear();
			Console.WriteLine ("Cerrando programa.. ");
			System.Environment.Exit (-1);
			dbcon.Close ();
			dbcon = null;
		}
		private static void NuevaFila()
		{
			Console.Clear ();
			Console.WriteLine ("******NUEVA FILA******");
			Console.WriteLine ("Escribe el nombre que se va añadir en la fila: ");
			nombreNuevo = leerString ("Nombre: ");
			if (!nombreNuevo.Equals ("vacio")) {
				dbcmd.CommandText = "insert into categoria (nombre) values (@nuevo)";
				AddParametros (dbcmd,"nuevo",nombreNuevo);
				dbcmd.Dispose ();
				Console.WriteLine("\nNueva fila añadida con éxito.");
			}
			PulsarParaVolver();
		}
		private static void EditarFila()
		{
			Console.Clear ();
			Console.WriteLine ("******EDITAR FILA******");

			Console.WriteLine ("Escribe el ID de la fila que se va a editar: ");
			idBuscado = Console.ReadLine ();

			//Comprobamos la ID.
			ComprobarID (idBuscado);
			dbcmdLeerID.Dispose ();
			reader.Close();

			if (encontrado !=false)
			{
				Console.WriteLine("Escribe el nombre nuevo que se va añadir en la fila:");
				nombreNuevo = leerString ("Nombre: ");
				if (!nombreNuevo.Equals ("vacio")) {
					dbcmd.CommandText = "update categoria set nombre=(@nuevo) where id='" + idBuscado + "';";
					AddParametros (dbcmd, "nuevo", nombreNuevo);
					dbcmd.Dispose ();
					Console.WriteLine ("\nEditada con éxito.");
				}
				PulsarParaVolver();

			}else
			{
				Console.WriteLine("\n*Número de identificacíon (ID) no encontrada.");
				PulsarParaVolver();
			}
		}
		private static void BorrarFila()
		{
			Console.Clear ();
			Console.WriteLine ("******ELIMINAR FILA******");
			Console.WriteLine ("Escribe el ID de la fila que se va a eliminar: ");
			idBuscado = Console.ReadLine ();

			//Comprobamos la ID.
			ComprobarID (idBuscado);
			dbcmdLeerID.Dispose ();
			reader.Close();

			if (encontrado !=false)
			{
				dbcmd.CommandText ="delete from categoria where id='"+idBuscado+"';";
				dbcmd.ExecuteNonQuery();
				dbcmd.Dispose ();
				Console.WriteLine("Borrada con éxito.");
				PulsarParaVolver();
			}else
			{
				Console.WriteLine("\n*Número de identificacíon (ID) no encontrada");
				PulsarParaVolver();
			}
		}
		private static void ListarFilas()
		{
			Console.Clear ();
			Console.WriteLine ("******MOSTRAR FILAS******");
			dbcmd.CommandText = "select * from categoria";
			reader = dbcmd.ExecuteReader ();
			while (reader.Read()) {
				Console.WriteLine ("ID: " + reader ["id"] + "\t Nombre: " + reader ["nombre"]);
			}
			reader.Close ();
			dbcmd.Dispose ();
			PulsarParaVolver();
		}

		private static string leerString(string label)
		{
			int intentos = 0;
			while(intentos!=3)
			{
				intentos++;
				Console.Write (label);
				String data = Console.ReadLine ();
				data = data.Trim ();
				if (!data.Equals ("")) 
					return data;
				Console.WriteLine ("Error, no puede quedar vacio el nombre.");
			}
			Console.WriteLine ("Intentos agotados.");
			return "vacio";
		}
		private static Boolean ComprobarID(String idBuscado)
		{
			reader = dbcmdLeerID.ExecuteReader (); //Buscamos si existe la idBuscado
			while (reader.Read() && encontrado== false) 
			{
				id =""+reader ["id"]+"";
				if(idBuscado.Equals(id) )
				{
					Console.WriteLine("*Número de identificacíon (ID) encontrada.");
					return encontrado=true;
				}
			}
			return encontrado = false;
		}
		private static void AddParametros(IDbCommand comando,String clave, String valor)
		{
			IDbDataParameter dbDataParameter = comando.CreateParameter ();
			dbDataParameter.ParameterName = clave;
			dbDataParameter.Value = valor;
			comando.Parameters.Add (dbDataParameter);
			comando.ExecuteNonQuery ();
		}
		private static void PulsarParaVolver()
		{
			Console.WriteLine("\nPulsa cualquier tecla para voler.");
			Console.ReadLine();
		}
	}
}
