using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	protected IDbConnection dbcon = null;
	protected IDbCommand dbcmd = null;
	protected IDataReader reader = null;
	protected ListStore listStore = null;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		string[] columNames = {"id","nombre","precio"};
		for(int index = 0;index< columNames.Length;index++)
		{
			int column = index;
			treeView.AppendColumn (columNames[column],new CellRendererText(),
			delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) 
			{
				CellRendererText cellRendererText = (CellRendererText) cell;
				object value = tree_model.GetValue(iter,column);
				cellRendererText.Text = value.ToString();				
			}
			);

		}
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

