﻿using EnvDTE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteveCadwallader.CodeMaid.IntegrationTests.Helpers;
using SteveCadwallader.CodeMaid.Logic.Cleaning;
using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.IntegrationTests.Cleaning.Remove
{
    [TestClass]
    [DeploymentItem(@"Cleaning\Remove\Data\BlankLinesAfterAttributes.cs", "Data")]
    [DeploymentItem(@"Cleaning\Remove\Data\BlankLinesAfterAttributes_Cleaned.cs", "Data")]
    public class BlankLinesAfterAttributesTests
    {
        #region Setup

        private static RemoveWhitespaceLogic _removeWhitespaceLogic;
        private ProjectItem _projectItem;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _removeWhitespaceLogic = RemoveWhitespaceLogic.GetInstance(TestEnvironment.Package);
            Assert.IsNotNull(_removeWhitespaceLogic);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestEnvironment.CommonTestInitialize();
            _projectItem = TestEnvironment.LoadFileIntoProject(@"Data\BlankLinesAfterAttributes.cs");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestEnvironment.RemoveFromProject(_projectItem);
        }

        #endregion Setup

        #region Tests

        [TestMethod]
        [HostType("VS IDE")]
        public void CleaningRemoveBlankLinesAfterAttributes_CleansAsExpected()
        {
            Settings.Default.Cleaning_RemoveBlankLinesAfterAttributes = true;

            TestOperations.ExecuteCommandAndVerifyResults(RunRemoveBlankLinesAfterAttributes, _projectItem, @"Data\BlankLinesAfterAttributes_Cleaned.cs");
        }

        [TestMethod]
        [HostType("VS IDE")]
        public void CleaningRemoveBlankLinesAfterAttributes_DoesNothingOnSecondPass()
        {
            Settings.Default.Cleaning_RemoveBlankLinesAfterAttributes = true;

            TestOperations.ExecuteCommandTwiceAndVerifyNoChangesOnSecondPass(RunRemoveBlankLinesAfterAttributes, _projectItem);
        }

        [TestMethod]
        [HostType("VS IDE")]
        public void CleaningRemoveBlankLinesAfterAttributes_DoesNothingWhenSettingIsDisabled()
        {
            Settings.Default.Cleaning_RemoveBlankLinesAfterAttributes = false;

            TestOperations.ExecuteCommandAndVerifyNoChanges(RunRemoveBlankLinesAfterAttributes, _projectItem);
        }

        #endregion Tests

        #region Helpers

        private static void RunRemoveBlankLinesAfterAttributes(Document document)
        {
            var textDocument = TestUtils.GetTextDocument(document);

            _removeWhitespaceLogic.RemoveBlankLinesAfterAttributes(textDocument);
        }

        #endregion Helpers
    }
}