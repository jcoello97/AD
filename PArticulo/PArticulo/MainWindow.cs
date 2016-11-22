using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using Org.InstitutoSerpis.Ad;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	public static Gtk.Action refrescar;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		refrescar = refreshAction;
		dbConnection = ConexionSGBD.Instance.dbConnection;
		dbConnection.Open ();

		Fill ();
		treeView.Selection.Changed += delegate {
			bool selected = treeView.Selection.CountSelectedRows()> 0;
			editAction.Sensitive = selected;
			deleteAction.Sensitive = selected;

		};

		refreshAction.Activated += delegate{
			Fill();
		};

		newAction.Activated += delegate {
			Articulo articulo = new Articulo();
			articulo.Nombre = string.Empty;
			articulo.Precio = 0;
			new ArticuloView(articulo);
		};
		deleteAction.Activated += delegate{

			if(WindowHelper.Confirm(this,"¿Estas seguro que quieres eliminarlo?"))
			{
				ArticuloDao.delete(TreeViewHelper.GetId (treeView));
				refreshAction.Activate();
			}

		};
		editAction.Activated += delegate {
			Articulo articulo = ArticuloDao.Load(TreeViewHelper.GetId(treeView));
			new ArticuloView(articulo);
		};

	}
	protected void Fill()
	{
		editAction.Sensitive = false;
		deleteAction.Sensitive = false;
		IList list = EntityDao.getList<Articulo> ();
		TreeViewHelper.Fill (treeView, list);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		ConexionSGBD.Instance.dbConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}
}
