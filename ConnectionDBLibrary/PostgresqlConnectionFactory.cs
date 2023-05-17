using System;
using System.Data;
using System.Collections.Generic;
using Npgsql;

namespace ConnectionDBLibrary
{

	public class PostgresqlConnectionFactory : ConnectionFactory
	{
		public override NpgsqlConnection createConnection(IDictionary<string, string> props)
		{
			//Console.WriteLine(connection)
			String connectionString = props["ConnectionString"];
			Console.WriteLine("Postgresql ---se deschide o conexiune la  ... {0}", connectionString);

			return new NpgsqlConnection(connectionString);

		}
	}

}
