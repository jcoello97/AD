using System;
using System.Collections.Generic;
using System.Collections;
using Gtk;
using System.Reflection;
using System.Data;
using Org.InstitutoSerpis.Ad;

namespace PArticulo

{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView (Articulo articulo) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			spinButtonPrecio.Value = (double) articulo.Precio;

			FillComboBoxCategoria (articulo.Categoria);

			refreshAction ();
			saveAction.Activated += delegate {
				articulo.Nombre= entryNombre.Text;
				articulo.Precio = (decimal)spinButtonPrecio.Value;
				articulo.Categoria = (long?)ComboBoxHelper.GetId(comboboxCategoria);

				ArticuloDao.save(articulo);
				MainWindow.refrescar.Activate();

			};
			entryNombre.Changed += delegate {
				refreshAction();
			};

		}
		private void refreshAction(){
			
			string value = entryNombre.Text.Trim();
			saveAction.Sensitive = !(value.Length == 0);
		}
		private void FillComboBoxCategoria(object categoria)
		{
			IList lista = CategoriaDao.GetList ();
			ComboBoxHelper.Fill(comboboxCategoria,lista,"Nombre",categoria);
		}

	}
	
}

