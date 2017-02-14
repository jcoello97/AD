package org.institutoserpis.ad;

import java.util.List;
import java.util.Scanner;

public class PedidoMain {

	public static void main(String[] args) {
		String opcionElegida;
		Scanner scanner = new Scanner(System.in);
		do {
			System.out.println("***** GESTION DE PEDIDOS *****");
			System.out.println("1. Productos");
			System.out.println("2. Clientes");
			System.out.println("3. Pedidos");
			System.out.println("---------");
			System.out.println("4. Salir");
			System.out.print("Elige una opción (numero):");
			opcionElegida = scanner.nextLine();
			switch (opcionElegida) {
			case "1":
				menuProductos();
				break;
			case "2":
				menuClientes();
				break;
			case "3":
				menuPedidos();
				break;
			case "4":
				System.out.println("Aplicacion finalizada");
				System.exit(0);
				break;
			default:
				System.out.println(
						"Introduce solo el numero de la opcion deseada.");
				break;
			}
		} while (true);
	}

	public static void menuProductos() {
		boolean salir = false;
		String opcionElegida;
		Scanner scanner = new Scanner(System.in);
		do {
			System.out.println("\n****** PRODUCTOS *******");
			System.out.println("-Articulo (producto)");
			System.out.println("1. Crear articulo.");
			System.out.println("2. Borrar articulo.");
			System.out.println("3. Modificar articulo.");
			System.out.println("-Categoria del articulo");
			System.out.println("4. Crear categoria.");
			System.out.println("5. Borrar categoria.");
			System.out.println("6. Modificar categoria.");
			System.out.println("-Listado");
			System.out.println("7. Listado de articulos.");
			System.out.println("8. Listado de categorias.");
			System.out.println("---------");
			System.out.println("9. Volver al menu principal");
			System.out.println("Elige una opción(numero):");
			opcionElegida = scanner.nextLine();
			switch (opcionElegida) {
			case "1":
				break;
			case "2":
				break;
			case "3":
				break;
			case "4":
				break;
			case "5":
				break;
			case "6":
				break;
			case "7":
				break;
			case "8":
				break;
			case "9":
				salir = true;
				break;
			default:
				System.out.println(
						"Introduce solo el numero de la opcion deseada.");
				break;
			}
		} while (salir == false);
	}

	public static void menuClientes() {
		String nombre = "";
		boolean salir1 = false;
		boolean salir2 = false;
		String opcionElegida;
		Scanner scanner = new Scanner(System.in);
		do {
			System.out.println("\n****** CLIENTES *******");
			System.out.println("-Cliente");
			System.out.println("1. Crear cliente.");
			System.out.println("2. Borrar cliente.");
			System.out.println("3. Modificar cliente.");
			System.out.println("-Listado");
			System.out.println("4. Listado de clientes.");
			System.out.println("---------");
			System.out.println("5. Volver al menu principal");
			System.out.println("Elige una opción(numero):");
			opcionElegida = scanner.nextLine();
			switch (opcionElegida) {
			case "1":
				System.out.println("\nDime el nombre del nuevo cliente: ");
				nombre = scanner.next();
				ClienteDao.createCliente(nombre);
				System.out.println("Cliente creado.");
				break;
			case "2":
				do {
					System.out.println("\n Dime el id del cliente: ");
					long id = scanner.nextLong();
					if (ClienteDao.existCliente(id)) {
						Cliente cliente = ClienteDao.getCliente(id);
						ClienteDao.removeCliente(cliente);
						System.out.println("Cliente borrado.");
						salir2 = true;
					} else {
						System.out.println("Id del cliente no encontrada.");
					}
				} while (salir2 == false);
				salir2 = false;
				break;
			case "3":
				do {
					System.out.println("\n Dime el id del cliente:");
					long id = scanner.nextLong();
					if (ClienteDao.existCliente(id)) {
						System.out.println("Dime el nuevo nombre del cliente: ");
						nombre = scanner.nextLine();
						Cliente cliente = ClienteDao.getCliente(id);
						cliente.setNombre(nombre);
						ClienteDao.updateCliente(cliente);
						System.out.println("Cliente modificado.");
						salir2 = true;
					} else {
						System.out.println("Id del cliente no encontrada.");
					}
				} while (salir2 == false);
				salir2 = false;
				break;
			case "4":
				System.out.println("LISTADO DE CLIENTES\n");
				List<Cliente> clientes = ClienteDao.getListClientes();
				for (Cliente cliente : clientes) {
					System.out.printf("\tID:%f \tNOMBRE:%s",
							cliente.getId(),
							cliente.getNombre());
				}
				break;
			case "5":
				salir1 = true;
				break;
			default:
				System.out.println(
						"Introduce solo el numero de la opcion deseada.");
				break;
			}
		} while (salir1 == false);
	}

	public static void menuPedidos() {
		boolean salir = false;
		String opcionElegida;
		Scanner scanner = new Scanner(System.in);
		do {
			System.out.println("\n****** PEDIDOS *******");
			System.out.println("-Pedido");
			System.out.println("1. Crear pedido.");
			System.out.println("2. Borrar pedido.");
			System.out.println("3. Modificar pedido.");
			System.out.println("-Listado");
			System.out.println("4. Listado de pedido.");
			System.out.println("---------");
			System.out.println("5. Volver al menu principal");
			System.out.println("Elige una opción(numero):");
			opcionElegida = scanner.nextLine();
			switch (opcionElegida) {
			case "1":
				break;
			case "2":
				break;
			case "3":
				break;
			case "4":
				break;
			case "5":
				salir = true;
				break;
			default:
				System.out.println(
						"Introduce solo el numero de la opcion deseada.");
				break;
			}
		} while (salir == false);
	}
}
