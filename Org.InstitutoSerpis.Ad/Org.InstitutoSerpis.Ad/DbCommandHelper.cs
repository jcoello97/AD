using System;
using System.Data;
namespace Org.InstitutoSerpis.Ad
{
	public class DbCommandHelper
	{
		public static void AddParameters(IDbCommand dbCommand, string name, object value)
		{
			IDbDataParameter dataParameter = dbCommand.CreateParameter();
			dataParameter.ParameterName = name;
			dataParameter.Value = value;
			/*dataParameter.ParameterName = "precio";
				dataParameter.Value = precio;
				dataParameter.ParameterName = "categoria";
				dataParameter.Value = categoria;*/
			dbCommand.Parameters.Add(dataParameter);

		}
	}
}

