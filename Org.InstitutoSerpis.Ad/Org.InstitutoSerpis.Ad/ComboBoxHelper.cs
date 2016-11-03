using System;
using Gtk;
using System.Data;
using System.Collections;
using System.Reflection;
using PArticulo;
namespace Org.InstitutoSerpis.Ad
{
	public class ComboBoxHelper
	{
		public static void fillComboBox(ComboBox comboBox, IList list, string propertyName)
		{
			Type listType = list.GetType ();
			Type elementType = listType.GetGenericArguments () [0];
			PropertyInfo propertyInfo = elementType.GetProperty(propertyName);

			ListStore listStore = new ListStore (typeof(object));

			//LA PRIMERA OPCION VACIA
			Categoria vacio = new Categoria (0L," ");


			listStore.AppendValues (vacio);
			foreach (object item in list) {
				listStore.AppendValues (item);
			}
			comboBox.Model = listStore;

			CellRendererText cellRendererText = new CellRendererText ();

			comboBox.PackStart (cellRendererText,false);

			comboBox.SetCellDataFunc (cellRendererText,
			                          delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) 
			                          {
				object item = tree_model.GetValue (iter,0);
				object value = propertyInfo.GetValue(item,null);

				//COMPROBAMOS QUE EL NOMBRE DE LA CATEGORIA ESTA VACIA O NULL, I AÑADIMOS EL VALOR
				cellRendererText.Text = value == null || value.ToString() == "" ? "<sin asignar>" : value.ToString();


			});
		}
		public static object GetId(ComboBox comboBox)
		{
			TreeIter treeIter;
			comboBox.GetActiveIter(out treeIter);
			object item = comboBox.Model.GetValue(treeIter,0);

			//			if (item == Null.value)
			//				return null;
			//			Type elementType = item.GetType ();
			//			PropertyInfo propertyInfo = elementType.GetProperty ("Id");
			//
			//			return propertyInfo.GetValue (item,null);
			return item == Null.value ? null : item.GetType ().GetProperty ("Id").GetValue (item, null);
		}


	}
}
