using Grammophone.DataAccess.Tests.Domain;
using Grammophone.DataAccess.Tests.Domain.EntityFrameworkCore;
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
			var options = new DbContextOptionsBuilder<EFCoreTestDomainContainer>()
				.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GrammophoneDataAccess_EFCore_Test;Trusted_Connection=True;TrustServerCertificate=True")
				.Options;

			return new EFCoreTestDomainContainerAdapter(new EFCoreTestDomainContainer(options));
		}

		#endregion
	}
}
