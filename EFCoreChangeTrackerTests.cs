using Grammophone.DataAccess.Tests.Cases;
using Grammophone.DataAccess.Tests.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grammophone.DataAccess.Tests.Cases.EntityFrameworkCore
{
	/// <summary>
	/// Entity Framework Core change tracker tests.
	/// </summary>
	[TestClass]
	public class EFCoreChangeTrackerTests : ChangeTrackerTestCases
	{
		#region Protected methods

		/// <inheritdoc/>
		protected override IMusicDomainContainer CreateDomainContainer()
		{
			return EFCoreTestFactory.CreateDomainContainer();
		}

		#endregion
	}
}
