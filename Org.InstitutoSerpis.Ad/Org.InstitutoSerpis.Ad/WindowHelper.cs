using Gtk;
using System;

namespace Org.InstitutoSerpis.Ad
{
	public class WindowHelper
	{
		public static bool Confirm(Window parent, String message){
			MessageDialog messageDialog = new MessageDialog(
				parent,
				DialogFlags.Modal,
				MessageType.Question,
				ButtonsType.YesNo,
				message);
			messageDialog.Title = "Eliminar "+parent.Title;
			ResponseType response = (ResponseType) messageDialog.Run();

			messageDialog.Destroy();
	
			return response == ResponseType.Yes;
		}
	}
}

