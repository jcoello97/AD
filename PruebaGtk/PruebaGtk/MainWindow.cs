using System;
using Gtk;
using Org.InstitutoSerpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		TreeViewHelper.appendColums (treeView,new String[]{"id","nombre","precio"});
		ListStore listStore = new ListStore (typeof(long),typeof(string),typeof(decimal));

		treeView.Model = listStore;

		listStore.AppendValues (1L,"CATEGORIA 1",50.45m);
		listStore.AppendValues (2L,"CATEGORIA 2",100.80m);
		listStore.AppendValues (3L,"CATEGORIA 3",30.13m);

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}