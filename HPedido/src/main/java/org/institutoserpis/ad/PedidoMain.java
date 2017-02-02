package org.institutoserpis.ad;

import java.util.Scanner;

public class PedidoMain {

	public static void main(String[] args) {
		boolean salir = false;
		Scanner scanner = new Scanner(System.in);
		do {
			System.out.println("***** GESTION DE PEDIDOS *****");
			System.out.println("1. Productos");
			System.out.println("2. Clientes");
			System.out.println("3. Pedidos");
			
		} while (salir == false);
	}

}
