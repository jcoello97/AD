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
		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			spinButtonPrecio.Value = 0;
			saveAction.Sensitive = false;

			entryNombre.Changed += delegate {
				string value = entryNombre.Text.Trim();
				saveAction.Sensitive = !(value.Length == 0);
			};
			saveAction.Activated += delegate {
				Articulo articulo = new Articulo();
				articulo.Nombre= entryNombre.Text;
				articulo.Precio = (decimal)spinButtonPrecio.Value;
				articulo.Categoria = (long)ComboBoxHelper.GetId(comboboxCategoria);

				ArticuloDao.save(articulo);

			};
			Fill ();
		}
		private void Fill()
		{
			IList listaCategorias = CategoriaDao.GetList ();
			ComboBoxHelper.fillComboBox (comboboxCategoria,listaCategorias,"Nombre");
		}

	}
	
}

