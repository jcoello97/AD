using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PDbPrueba
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IDbConnection dbcon = null;
			IDbCommand dbcmd = null, dbcmdLeerID = null;
			String opcion, idBuscado, nombreNuevo, id;
			IDataReader reader=null;
			Boolean encontrado=false;

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
					Console.Clear();
					Console.WriteLine ("Cerrando programa.. ");
					System.Environment.Exit (-1);
					dbcon.Close ();
					dbcon = null;
						break;

					case 1:
					Console.Clear ();
					Console.WriteLine ("******NUEVA FILA******");
					Console.WriteLine ("Escribe el nombre que se va añadir en la fila: ");
				 	nombreNuevo = Console.ReadLine ();

					dbcmd.CommandText = "insert into categoria (nombre) values (@nuevo)";
					AddParametros (dbcmd,"nuevo",nombreNuevo);

					dbcmd.Dispose ();
					Console.WriteLine("\nNueva fila añadida con éxito.");
					PulsarParaVolver();
						break;

					case 2:
					Console.Clear ();
					Console.WriteLine ("******EDITAR FILA******");

					Console.WriteLine ("Escribe el ID de la fila que se va a editar: ");
					idBuscado = Console.ReadLine ();
					reader = dbcmdLeerID.ExecuteReader (); //Buscamos si existe la idBuscado
					while (reader.Read() && encontrado== false) 
					{
						id =""+reader ["id"]+"";
						if(idBuscado.Equals(id) )
						{
							Console.WriteLine("*Número de identificacíon (ID) encontrada.");
							encontrado=true;
						}
					}
					dbcmdLeerID.Dispose ();
					reader.Close();
					if (encontrado !=false)
					{
						Console.WriteLine("Escribe el nombre nuevo que se va añadir en la fila:");
						nombreNuevo = Console.ReadLine ();
						dbcmd.CommandText ="update categoria set nombre=(@nuevo) where id='"+idBuscado+"';";
						AddParametros (dbcmd,"nuevo",nombreNuevo);
						Console.WriteLine("\nEditada con éxito.");
						PulsarParaVolver();

					}else
					{
						Console.WriteLine("\n*Número de identificacíon (ID) no encontrada.");
						PulsarParaVolver();
					}
					reader.Close ();
					dbcmd.Dispose ();
						break;

					case 3:
					Console.Clear ();
					Console.WriteLine ("******ELIMINAR FILA******");
					Console.WriteLine ("Escribe el ID de la fila que se va a eliminar: ");
					idBuscado = Console.ReadLine ();
					reader = dbcmdLeerID.ExecuteReader (); //Buscamos si existe la idBuscado
					while (reader.Read()&& encontrado== false) 
					{
						id =""+reader ["id"]+"";
						if(idBuscado.Equals(id) )
						{
							Console.WriteLine("*Número de identificacíon (ID) encontrada.");
							encontrado=true;
						}
					}
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
						break;

					case 4:
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
