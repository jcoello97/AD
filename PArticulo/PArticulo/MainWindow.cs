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
		/*dbConnection = new MySqlConnection (
			"Database=dbprueba;User Id=root;Password=sistemas"
			);*/
		refrescar = refreshAction;

		dbConnection = ConexionSGBD.Instance.dbConnection;
		dbConnection.Open ();

		Fill ();

		treeView.Selection.Changed += delegate {
			bool selected = treeView.Selection.CountSelectedRows()> 0;

			editAction.Sensitive = selected;
			deleteAction.Sensitive = selected;
			Console.WriteLine("Ha ocurrido el evento selection changed "+selected);

		};
		
		refreshAction.Activated += delegate{
			Fill();
		};

		newAction.Activated += delegate {
			new ArticuloView();

		};
		deleteAction.Activated += delegate{
			MessageDialog messageDialog = new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Question,
				ButtonsType.YesNo,"¿Estas seguro que quieres eliminarlo?"
				);
			messageDialog.Title = "WARNING BORRAR";
			ResponseType result = (ResponseType) messageDialog.Run();
			messageDialog.Destroy();
			if(result != ResponseType.Yes)
			{
				//messageDialog.Destroy();
				return;
			}

			ArticuloDao.delete(treeView);
			//messageDialog.Destroy();
			refreshAction.Activate();

		};

	}
	protected void Fill()
	{
		editAction.Sensitive = false;
		deleteAction.Sensitive = false;
		IList list = ArticuloDao.getList ();
		TreeViewHelper.Fill (treeView, list);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		ConexionSGBD.Instance.dbConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}
}
