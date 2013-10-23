﻿using System;
using System.Collections;
using System.Collections.Generic;
using Dev2.Data.Compilers;
using Dev2.DataList.Contract;
using Dev2.DataList.Contract.Binary_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;using System.Diagnostics.CodeAnalysis;

namespace Dev2.Data.Tests.BinaryDataList
{
    /// <summary>
    /// Summary description for RecordsetGroupTest
    /// </summary>
    [TestClass][ExcludeFromCodeCoverage]
    public class RecordsetGroupTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("RecordsetGroup_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RecordsetGroup_Constructor_WhenDefinitionsNull_ExpectException()
        {
            //------------Setup for test--------------------------

            var entry = Dev2BinaryDataListFactory.CreateEntry(string.Empty, string.Empty, Guid.NewGuid());

            //------------Execute Test---------------------------
            var recordsetGroup = new RecordsetGroup(entry, null, definition => null, definition => null);
            
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("RecordsetGroup_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RecordsetGroup_Constructor_WhenInputExtractorNull_ExpectException()
        {
            //------------Setup for test--------------------------

            var entry = Dev2BinaryDataListFactory.CreateEntry(string.Empty, string.Empty, Guid.NewGuid());

            //------------Execute Test---------------------------
            IList<IDev2Definition> defs = new List<IDev2Definition>();
            var recordsetGroup = new RecordsetGroup(entry, defs, null, definition => null);

        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("RecordsetGroup_Constructor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RecordsetGroup_Constructor_WhenOutputExtractorNull_ExpectException()
        {
            //------------Setup for test--------------------------

            var entry = Dev2BinaryDataListFactory.CreateEntry(string.Empty, string.Empty, Guid.NewGuid());

            //------------Execute Test---------------------------
            IList<IDev2Definition> defs = new List<IDev2Definition>();
            var recordsetGroup = new RecordsetGroup(entry, defs, definition => null, null);

        }
    }
}
