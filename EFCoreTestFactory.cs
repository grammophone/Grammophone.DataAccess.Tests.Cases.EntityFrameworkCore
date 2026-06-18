using System;
using System.IO;
using Grammophone.DataAccess.Tests.Domain;
using Grammophone.DataAccess.Tests.Domain.EntityFrameworkCore;
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
		/// Create a test domain container.
		/// </summary>
		public static IMusicDomainContainer CreateDomainContainer()
		{
			var configuration = CreateConfiguration();
			var dataDirectory = GetDataDirectory(configuration);

			Directory.CreateDirectory(dataDirectory);
			AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

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

		#endregion
	}
}
