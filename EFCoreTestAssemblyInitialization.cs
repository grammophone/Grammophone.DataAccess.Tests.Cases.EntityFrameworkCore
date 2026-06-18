using Grammophone.DataAccess.Tests.Cases;
using Grammophone.DataAccess.Tests.Domain.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grammophone.DataAccess.Tests.Cases.EntityFrameworkCore
{
	/// <summary>
	/// Assembly initialization for Entity Framework Core tests.
	/// </summary>
	[TestClass]
	public class EFCoreTestAssemblyInitialization
	{
		#region Public methods

		/// <summary>
		/// Initialize the test database.
		/// </summary>
		/// <param name="context">The test context.</param>
		[AssemblyInitialize]
		public static void Initialize(TestContext context)
		{
			EFCoreTestFactory.DropDatabase();
			EFCoreTestFactory.CreateDatabase();

			using (var domainContainer = EFCoreTestFactory.CreateDomainContainer())
			{
				var underlyingContext = (EFCoreMusicDomainContainer)domainContainer.UnderlyingContext;

				underlyingContext.Database.EnsureCreated();

				MusicTestDataSeeder.Seed(domainContainer);
			}
		}

		#endregion
	}
}
