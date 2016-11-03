using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;
using Org.InstitutoSerpis.Ad;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

//		List<Articulo> lista = new List<Articulo>();
//
//		lista.Add (new Articulo(1L,"articulo1",25.6m));
//		lista.Add (new Articulo(2L,"articulo2",80.6m));
//		lista.Add (new Articulo(3L,"articulo3",50.6m));

		List<Categoria> lista = new List<Categoria>();
		lista.Add (new Categoria(1L,"categoria1"));
		lista.Add (new Categoria(2L,"categoria2"));

		TreeViewHelper.Fill (treeView,lista);


		lista.Add (new Categoria(3L,"categoria3"));
		TreeViewHelper.Fill (treeView,lista);

	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
}

public class Categoria{
	public Categoria(long id,string nombre){
		Id = id;
		Nombre = nombre;
	}
	public long Id { get; set;}
	public string Nombre { get; set;}
}
public class Articulo 
{
	public Articulo(long id,string nombre,decimal precio){
		Id = id;
		Nombre = nombre;
		Precio = precio;
	}
	public long Id { get; set;}
	public string Nombre { get; set;}
	public decimal Precio { get; set;}
}














