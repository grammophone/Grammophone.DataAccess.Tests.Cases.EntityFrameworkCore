using Grammophone.DataAccess.Tests.Cases;
using Grammophone.DataAccess.Tests.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grammophone.DataAccess.Tests.Cases.EntityFrameworkCore
{
	/// <summary>
	/// Entity Framework Core terminal method tests.
	/// </summary>
	[TestClass]
	public class EFCoreTerminalMethodTests : TerminalMethodTestCases
	{
		#region Protected methods

		/// <inheritdoc/>
		protected override ITestDomainContainer CreateDomainContainer()
		{
			return EFCoreTestFactory.CreateDomainContainer();
		}

		#endregion
	}
}
