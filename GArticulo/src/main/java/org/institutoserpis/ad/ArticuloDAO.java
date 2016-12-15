package org.institutoserpis.ad;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.InputMismatchException;
import java.util.Scanner;

/*MENU BASE DATOS
 * 	0 SALIR
 *	1 NUEVO
 *	2 MODIFICAR
 *	3 ELIMINAR
 *	4 CONSULTAR
 *	5 LISTAR DATOS
 * */
public class ArticuloDAO {
	private static Scanner scanner = new Scanner(System.in);
	
	public static void main(String[] args){
		Connection connection = null;
		boolean salir = false;
		
		try 
		{
			connection = DriverManager.getConnection("jdbc:mysql://localhost/dbprueba", "root", "sistemas");
			scanner = new Scanner(System.in);
			do {
				System.out.println("********** BASE DE DATOS **********");
				System.out.println("0. Salir");
				System.out.println("1. Nuevo");
				System.out.println("2. Modificar");
				System.out.println("3. Eliminar");
				System.out.println("4. Consultar");
				System.out.println("5. Listar datos");
				String opcion = scanner.nextLine();
				switch (Integer.parseInt(opcion)) 
				{
					case 0:
						salir = true;
						break;
					case 1:
						lanzarNuevo(connection);
						break;
					case 2:
						lanzarModificar(connection);
						break;
					case 3:
						lanzarBorrar(connection);
						break;
					case 4:
						lanzarConsultar(connection);
						break;
					case 5:
						lanzarListarDatos(connection);
						break;
					default:
						System.out.println("Introduce solo el numero de una de las opciones del menu. ");
						break;
				}
			} while (salir == false);
			
		} catch (SQLException e) {
			e.printStackTrace();
			if (connection != null) {
				try {
					connection.rollback();
				} catch (SQLException ex) {
					ex.printStackTrace();
				}
			}
		} 
		
		System.out.println("FIN DEL PROGRAMA");
	}

	private static void lanzarModificar(Connection connection) {
		do {
			try {
				String idArticulo="",nombreArticulo="", categoriaArticulo="",precioArticulo="";
				
				do {
					System.out.println("********** MODIFICAR ARTICULO **********");
					System.out.println("-Dime el id del articulo a modificar:");
					idArticulo = scanner.nextLine();		
					if (comprobarId(Integer.parseInt(idArticulo), connection) == true) {
						System.out.println("ID Encontrada. Procediendo con la modificación.");
						break;
					}else{
						System.out.println("ID No encontrada, por favor pruebe otra vez.");
					}
				} while (true);
				System.out.println("-Dime el nuevo nombre del articulo:");
				nombreArticulo = scanner.nextLine();
				System.out.println("-Dime el nuevo numero de la categoria del articulo:");
				categoriaArticulo = scanner.nextLine();
				System.out.println("-Dime el nuevo precio del articulo:");
				precioArticulo = scanner.nextLine();	
				
				PreparedStatement statement = connection.prepareStatement("UPDATE articulo SET nombre=?,categoria=?,precio=? WHERE id = ?");
				statement.setObject(1, nombreArticulo);
				statement.setObject(2, Integer.parseInt(categoriaArticulo));
				statement.setObject(3, Double.parseDouble(precioArticulo));
				statement.setObject(4, Integer.parseInt(idArticulo));
				int filasAfectadas = statement.executeUpdate();
				
				Esperar.espera(750);
				if (filasAfectadas>0) {
					System.out.println("Datos modificados con éxito. Volviendo al menu principal");
					break;
				}
				System.out.println("Por favor introduzca los datos de nuevo");
		
			
			} catch (InputMismatchException ie) {
				System.out.println("ERROR. Por favor introduce un digito.");
				break;
			} catch (SQLException e) {
				e.printStackTrace();
			}
		} while (true);
		
	}

	private static void lanzarConsultar(Connection connection) {
		do {
			try {
				String idArticulo="";
				
				do {
					System.out.println("********** CONSULTAR ARTICULO **********");
					System.out.println("-Dime el id del articulo:");
					idArticulo = scanner.nextLine();		
					if (comprobarId(Integer.parseInt(idArticulo), connection) == true) {
						System.out.println("ID Encontrada. Procediendo con la consulta.");
						break;
					}else{
						System.out.println("ID No encontrada, por favor pruebe otra vez.");
					}
				} while (true);
				
				PreparedStatement statement = connection.prepareStatement("SELECT * FROM articulo WHERE id = ?");
				statement.setObject(1, idArticulo);
				ResultSet resultSet = statement.executeQuery();
				System.out.printf("%5s %10s %10s %10s\n","ID","NOMBRE","PRECIO","CATEGORIA");
				while (resultSet.next()) {
					System.out.printf("%5s %10s %10s %10s\n"
							,resultSet.getObject("id")
							,resultSet.getObject("nombre")
							,resultSet.getObject("precio")
							,resultSet.getObject("categoria"));
				}
				Esperar.espera(750);
				System.out.println("Datos mostrados con éxito. Volviendo al menu principal");
				break;
		
			
			} catch (InputMismatchException ie) {
				System.out.println("ERROR. Por favor introduce un digito.");
				break;
			} catch (SQLException e) {
				e.printStackTrace();
			}
		} while (true);
		
	}

	private static void lanzarBorrar(Connection connection) {
		do {
			try {
				String idArticulo="";
				
				do {
					System.out.println("********** BORRAR ARTICULO **********");
					System.out.println("-Dime el id del articulo:");
					idArticulo = scanner.nextLine();		
					if (comprobarId(Integer.parseInt(idArticulo), connection) == true) {
						System.out.println("ID Encontrada. Procediendo con el borrado.");
						break;
					}else{
						System.out.println("ID No encontrada, por favor pruebe otra vez.");
					}
				} while (true);
				
				PreparedStatement statement = connection.prepareStatement("DELETE FROM articulo WHERE id = ?");
				statement.setObject(1, idArticulo);
				int filasAfectadas = statement.executeUpdate();
				
				Esperar.espera(750);
				if (filasAfectadas>0) {
					System.out.println("Datos borrados con éxito. Volviendo al menu principal");
					break;
				}
				System.out.println("Por favor introduzca los datos de nuevo");
		
			
			} catch (InputMismatchException ie) {
				System.out.println("ERROR. Por favor introduce un digito.");
				break;
			} catch (SQLException e) {
				e.printStackTrace();
			}
		} while (true);
		
	}
	private static boolean comprobarId(int id, Connection connection) throws SQLException{
		Statement statement = connection.createStatement();
		ResultSet resultSet = statement.executeQuery(
				"SELECT id FROM articulo");
		System.out.println("********** LISTAR DATOS **********");
		while (resultSet.next()) {
			if (resultSet.getInt("id") == id) {
				statement.close();
				return true;
			}
		}
		statement.close();
		return false;
	}

	private static void lanzarListarDatos(Connection connection) throws SQLException {

		System.out.println("******** LISTAR DATOS *******");
		Statement statement = connection.createStatement();
		ResultSet resultSet = statement.executeQuery(
				"SELECT * FROM articulo");
		System.out.printf("%5s %10s %10s %10s\n","ID","NOMBRE","PRECIO","CATEGORIA");
		while (resultSet.next()) {
			System.out.printf("%5s %10s %10s %10s\n"
					,resultSet.getObject("id")
					,resultSet.getObject("nombre")
					,resultSet.getObject("precio")
					,resultSet.getObject("categoria"));
		}
		Esperar.espera(750);
		statement.close();
	}

	private static void lanzarNuevo(Connection connection){
		do {
			try {
				String nombreArticulo="", categoriaArticulo="",precioArticulo="";
				
				System.out.println("********** NUEVO ARTICULO **********");
				System.out.println("-Dime el nombre del articulo:");
				nombreArticulo = scanner.nextLine();
				System.out.println("-Dime el numero de la categoria del articulo:");
				categoriaArticulo = scanner.nextLine();
				System.out.println("-Dime el precio del articulo:");
				precioArticulo = scanner.nextLine();	
				
				PreparedStatement statement = connection.prepareStatement("INSERT INTO articulo (nombre,categoria,precio) VALUES (?,?,?)");
				statement.setObject(1, nombreArticulo);
				statement.setObject(2, Integer.parseInt(categoriaArticulo));
				statement.setObject(3, Double.parseDouble(precioArticulo));
				int filasAfectadas = statement.executeUpdate();
				
				Esperar.espera(750);
				if (filasAfectadas>0) {
					System.out.println("Datos insertados con éxito. Volviendo al menu principal");
					break;
				}
				System.out.println("Por favor introduzca los datos de nuevo");
		
			
			} catch (InputMismatchException ie) {
				System.out.println("ERROR. Por favor introduce un digito.");
				break;
			} catch (SQLException e) {
				e.printStackTrace();
			}
		} while (true);
		
	}
	
	static class Esperar{
		public static void espera (long segundos) {
			try {
				Thread.currentThread().sleep(segundos);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
}
