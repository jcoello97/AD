using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;
using Org.InstitutoSerpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		string[] columNames = {"id","nombre","precio"};

		TreeViewHelper.AppendColumns(treeView,columNames);

		ListStore listStore = new ListStore (typeof(int),typeof(string),typeof(decimal));
		treeView.Model = listStore;
		listStore.AppendValues (1, "categoria1",20.5m);
		listStore.AppendValues (2, "categoria2",8.2m);
		listStore.AppendValues (3, "categoria3",156.9m);

	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
}

