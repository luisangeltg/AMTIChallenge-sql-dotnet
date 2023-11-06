using System;
namespace AMTIPrueba.Repository
{
	public class DBConnection
	{
		private string SQLchain = string.Empty;

		public DBConnection()
		{
			this.SQLchain = "Data Source=localhost;Initial Catalog=CodeChallengeAlMaximoTI;User=sa;Password=Holamundo11_";

        }

		public string getSQLString()
		{
			return SQLchain;
		}

	}
}

