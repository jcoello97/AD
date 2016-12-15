package org.institutoserpis.ad;

public class Articulo 
{
	String nombre;
	int categoria;
	double precio;
	
	public Articulo(String nombre, int categoria, double precio) {
		this.nombre = nombre;
		this.categoria = categoria;
		this.precio = precio;
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}

	public int getCategoria() {
		return categoria;
	}

	public void setCategoria(int categoria) {
		this.categoria = categoria;
	}

	public double getPrecio() {
		return precio;
	}

	public void setPrecio(double precio) {
		this.precio = precio;
	}
	
	
	
	

}
