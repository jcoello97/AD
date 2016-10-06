using System;
using Gtk;

namespace Org.InstitutoSerpis.Ad
{
	public class TreeViewHelper
	{
		public static void appendColums(TreeView treeView , String[] columnNames)
		{

			for (int i = 0; i<columnNames.Length; i++) 
			{
				int aux = i;

				treeView.AppendColumn (columnNames[aux],new CellRendererText(),
			delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) 
			{
				
					CellRendererText cellRendererText = (CellRendererText) cell;
					object valor = tree_model.GetValue(iter,aux);
					cellRendererText.Text = valor.ToString();

			});
			}
		}
	}
}

