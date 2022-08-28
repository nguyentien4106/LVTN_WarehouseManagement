using DevExpress.EasyTest.Framework;
using System;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

// To run functional tests for ASP.NET Web Forms and ASP.NET Core Blazor XAF Applications,
// install browser drivers: https://www.selenium.dev/documentation/getting_started/installing_browser_drivers/.
//
// -For Google Chrome: download "chromedriver.exe" from https://chromedriver.chromium.org/downloads.
// -For Microsoft Edge: download "msedgedriver.exe" from https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/.
//
// Selenium requires a path to the downloaded driver. Add a folder with the driver to the system's PATH variable.
//
// Refer to the following article for more information: https://docs.devexpress.com/eXpressAppFramework/403852/

namespace Warehouse_Management.Module.E2E.Tests {
	public class Warehouse_ManagementTests : IDisposable {
        const string WinAppName = "Warehouse_ManagementWin";
        const string WebAppName = "Warehouse_ManagementWeb";
        const string AppDBName = "Warehouse_Management";
        EasyTestFixtureContext FixtureContext { get; } = new EasyTestFixtureContext();

		public Warehouse_ManagementTests() {
            FixtureContext.RegisterApplications(
                new WinApplicationOptions(WinAppName, string.Format(@"{0}\..\..\..\..\Warehouse_Management.Win\bin\EasyTest\Warehouse_Management.Win.exe", Environment.CurrentDirectory)),
                new WebApplicationOptions(WebAppName, string.Format(@"{0}\..\..\..\..\Warehouse_Management.Web", Environment.CurrentDirectory))
            );
            FixtureContext.RegisterDatabases(new DatabaseOptions(AppDBName, "Warehouse_ManagementEasyTest", server: @"(localdb)\mssqllocaldb"));	           
		}
        public void Dispose() {
            FixtureContext.CloseRunningApplications();
        }
        [Theory]
        [InlineData(WinAppName)]
        [InlineData(WebAppName)]
        public void Test(string applicationName) {
            FixtureContext.DropDB(AppDBName);
            var appContext = FixtureContext.CreateApplicationContext(applicationName);
            appContext.RunApplication();
            //appContext.Navigate("My Details");
        }
	}
}
