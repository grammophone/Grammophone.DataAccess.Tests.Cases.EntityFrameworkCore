using System;
using System.IO;
using Grammophone.DataAccess.Tests.Domain;
using Grammophone.DataAccess.Tests.Domain.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Grammophone.DataAccess.Tests.Cases.EntityFrameworkCore
{
	/// <summary>
	/// Factory methods for Entity Framework Core tests.
	/// </summary>
	internal static class EFCoreTestFactory
	{
		#region Public methods

		/// <summary>
		/// Ensure that <c>DataDirectory</c> points to the configured database scratchpad.
		/// </summary>
		public static void EnsureDataDirectory()
		{
			var configuration = CreateConfiguration();
			var dataDirectory = GetDataDirectory(configuration);

			Directory.CreateDirectory(dataDirectory);
			AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
		}

		/// <summary>
		/// Drop the configured LocalDB test database and delete scratch database files.
		/// </summary>
		public static void DropDatabase()
		{
			EnsureDataDirectory();

			using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True"))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText =
						"IF DB_ID(N'GrammophoneDataAccess_EFCore_Test') IS NOT NULL "
						+ "BEGIN "
						+ "ALTER DATABASE [GrammophoneDataAccess_EFCore_Test] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; "
						+ "DROP DATABASE [GrammophoneDataAccess_EFCore_Test]; "
						+ "END";

					command.ExecuteNonQuery();
				}
			}

			var dataDirectory = (string)AppDomain.CurrentDomain.GetData("DataDirectory");

			foreach (var databaseFile in Directory.GetFiles(dataDirectory, "GrammophoneDataAccess_EFCore_Working*.mdf"))
			{
				File.Delete(databaseFile);
			}

			foreach (var logFile in Directory.GetFiles(dataDirectory, "GrammophoneDataAccess_EFCore_Working*.ldf"))
			{
				File.Delete(logFile);
			}
		}

		/// <summary>
		/// Create the configured LocalDB test database files.
		/// </summary>
		public static void CreateDatabase()
		{
			EnsureDataDirectory();

			var dataDirectory = (string)AppDomain.CurrentDomain.GetData("DataDirectory");
			var databaseFile = Path.Combine(dataDirectory, "GrammophoneDataAccess_EFCore_Working.mdf");
			var logFile = Path.Combine(dataDirectory, "GrammophoneDataAccess_EFCore_Working_log.ldf");

			using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True"))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText =
						"CREATE DATABASE [GrammophoneDataAccess_EFCore_Test] "
						+ "ON (NAME = N'GrammophoneDataAccess_EFCore_Test', FILENAME = N'" + EscapeSqlString(databaseFile) + "') "
						+ "LOG ON (NAME = N'GrammophoneDataAccess_EFCore_Test_log', FILENAME = N'" + EscapeSqlString(logFile) + "')";

					command.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Create a test domain container.
		/// </summary>
		public static IMusicDomainContainer CreateDomainContainer()
		{
			EnsureDataDirectory();

			var configuration = CreateConfiguration();

			var options = new DbContextOptionsBuilder<EFCoreMusicDomainContainer>()
				.UseSqlServer(configuration.GetConnectionString("default"))
				.Options;

			return new EFCoreMusicDomainContainerAdapter(new EFCoreMusicDomainContainer(options));
		}

		#endregion

		#region Private methods

		private static IConfigurationRoot CreateConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false)
				.Build();
		}

		private static string GetDataDirectory(IConfiguration configuration)
		{
			var path = Path.Combine(
				AppDomain.CurrentDomain.BaseDirectory,
				configuration["DataDirectory"]);

			return Path.GetFullPath(path);
		}

		private static string EscapeSqlString(string value)
		{
			return value.Replace("'", "''");
		}

		#endregion
	}
}
