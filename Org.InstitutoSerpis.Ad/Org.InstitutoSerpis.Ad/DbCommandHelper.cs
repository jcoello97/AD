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
			dbCommand.Parameters.Add(dataParameter);

		}
	}
}

