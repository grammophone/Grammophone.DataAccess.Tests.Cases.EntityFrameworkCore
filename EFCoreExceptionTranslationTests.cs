using Grammophone.DataAccess.Tests.Cases;
using Grammophone.DataAccess.Tests.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grammophone.DataAccess.Tests.Cases.EntityFrameworkCore
{
	/// <summary>
	/// Entity Framework Core exception translation tests.
	/// </summary>
	[TestClass]
	public class EFCoreExceptionTranslationTests : ExceptionTranslationTestCases
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
