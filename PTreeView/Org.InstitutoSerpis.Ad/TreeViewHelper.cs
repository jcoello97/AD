using System;
using Gtk;
namespace Org.InstitutoSerpis.Ad
{
	public class TreeViewHelper
	{
		public static void AppendColumns (TreeView treeView, String[] columnNames)
		{
			for(int index = 0;index< columnNames.Length;index++)
			{
				int column = index;
				treeView.AppendColumn 
					(columnNames[column],new CellRendererText(),
						delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) 
						{
							CellRendererText cellRendererText = (CellRendererText) cell;
							object value = tree_model.GetValue(iter,column);
							cellRendererText.Text = value.ToString();				
						}
					);
			}
		}
	}
}