using NUnit.Framework;
using System;
using FinalProject.Helpers;

namespace FinalProject.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("\nTestInputValidation setup starting...");
        }

        [Test]
        public void TestForSQLInjection()
        {
            Console.WriteLine("🚨 Running TestForSQLInjection...");

            var safeInput = "john_doe";
            var injection1 = "' OR 1=1 --";
            var injection2 = "admin'; DROP TABLE Users;--";
            var injection3 = "Robert'); DROP TABLE Students;--";

            Assert.That(InputValidator.IsSafeFromSqlInjection(safeInput), Is.True, "Safe input should pass validation.");
            Assert.That(InputValidator.IsSafeFromSqlInjection(injection1), Is.False, "Injection pattern should be blocked.");
            Assert.That(InputValidator.IsSafeFromSqlInjection(injection2), Is.False, "SQL drop attempt should be blocked.");
            Assert.That(InputValidator.IsSafeFromSqlInjection(injection3), Is.False, "Classic Bobby Tables should be blocked.");

            Console.WriteLine("✅ SQL injection validation tests passed.");
        }

        [Test]
        public void TestForXSS()
        {
            Console.WriteLine("🛡️ Running TestForXSS...");

            var safeInput = "hello@world.com";
            var scriptInput = "<script>alert('XSS')</script>";
            var iframeInput = "<iframe src='evil.com'></iframe>";
            var mixedCaseInput = "<ScRiPt>alert()</ScRiPt>";

            Assert.That(InputValidator.IsValidXssInput(safeInput), Is.True, "Safe input should be valid.");
            Assert.That(InputValidator.IsValidXssInput(scriptInput), Is.False, "Script tag should be blocked.");
            Assert.That(InputValidator.IsValidXssInput(iframeInput), Is.False, "Iframe tag should be blocked.");
            Assert.That(InputValidator.IsValidXssInput(mixedCaseInput), Is.False, "Mixed-case script tag should be blocked.");

            Console.WriteLine("✅ XSS validation tests passed.");
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("✅ Test finished.");
        }
    }
}
