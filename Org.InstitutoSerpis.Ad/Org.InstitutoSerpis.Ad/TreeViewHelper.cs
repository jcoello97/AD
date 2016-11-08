using System;
using Gtk;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Org.InstitutoSerpis.Ad
{
	public class TreeViewHelper
	{
		public static void AppendColumns(TreeView treeView, string[] columnNames) {
			foreach (string columnName in columnNames) {
				treeView.AppendColumn (columnName, new CellRendererText (),
				                       delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
					int column = Array.IndexOf(treeView.Columns, tree_column);
					CellRendererText cellRendererText = (CellRendererText)cell;
					object value = tree_model.GetValue(iter, column);
					cellRendererText.Text = value.ToString();
				}
				);
			}
		}

		public static void AppendColumns(TreeView treeView, Type type) {
			PropertyInfo[] propertyInfos = type.GetProperties ();
			List<string> propertyNames = new List<string> ();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				propertyNames.Add (propertyInfo.Name);
			AppendColumns (treeView, propertyNames.ToArray ());
		}

		/// <summary>
		/// Appends the columns in the TreeView 
		/// </summary>
		/// <param name="treeView">Tree view.</param>
		/// <param name="list">List.</param>
		private static void appendColumns(TreeView treeView, IList list) {
			if (treeView.Columns.Length != 0)
				return;
			Type listType = list.GetType ();
			Type elementType = listType.GetGenericArguments () [0];
			PropertyInfo[] propertyInfos = elementType.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				string columnName = propertyInfo.Name;
				treeView.AppendColumn (columnName, new CellRendererText (), 
				                       delegate(TreeViewColumn tree_column, CellRenderer cell, 
				         TreeModel tree_model, TreeIter iter) {
					object item = tree_model.GetValue(iter, 0);
					object value = propertyInfo.GetValue(item, null);
					CellRendererText cellRendererText = (CellRendererText)cell;
					cellRendererText.Text = value == null ? "" : value.ToString();
				}
				);
			}
		}

		private static void appendValues (TreeView treeView, IList list) {
			ListStore listStore = new ListStore (typeof(object));
			foreach (object item in list) 
				listStore.AppendValues (item);
			treeView.Model = listStore;
		}

		public static void Fill(TreeView treeView, IList list) {
			appendColumns (treeView, list);
			appendValues (treeView, list);
		}
		public static object GetId(TreeView treeView)
		{
			TreeIter treeIter;
			treeView.Selection.GetSelected(out treeIter);
			object item = treeView.Model.GetValue(treeIter,0);
			return item == Null.value ? null : item.GetType ().GetProperty ("Id").GetValue (item, null);
		}
	}

}