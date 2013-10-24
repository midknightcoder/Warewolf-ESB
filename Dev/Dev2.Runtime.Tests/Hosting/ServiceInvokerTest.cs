﻿using System;
using Dev2.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;using System.Diagnostics.CodeAnalysis;

namespace Dev2.Tests.Runtime.Hosting
{
    /// <summary>
    /// Summary description for ServiceInvokerTest
    /// </summary>
    [TestClass][ExcludeFromCodeCoverage]
    public class ServiceInvokerTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("ServiceInvoker_Invoke")]
        public void ServiceInvoker_Invoke_WhenDbTest_ExpectValidDbRecordsetDataWithCommasReplacedForUIParsing()
        {
            //------------Setup for test--------------------------
            var serviceInvoker = new ServiceInvoker();

            const string args = @"{""resourceID"":""00000000-0000-0000-0000-000000000000"",""resourceType"":""DbService"",""resourceName"":null,""resourcePath"":null,""source"":{""ServerType"":""SqlDatabase"",""Server"":""RSAKLFSVRGENDEV"",""DatabaseName"":""TU - Greenpoint"",""Port"":1433,""AuthenticationType"":""User"",""UserID"":""testUser"",""Password"":""test123"",""ConnectionString"":""Data Source=RSAKLFSVRGENDEV,1433;Initial Catalog=TU - Greenpoint;User ID=testUser;Password=test123;"",""ResourceID"":""eb2de0a3-4814-40b8-b825-f4601bfdb155"",""Version"":""1.0"",""ResourceType"":""DbSource"",""ResourceName"":""TU Greenpoint DB"",""ResourcePath"":""SQL SRC"",""IsValid"":false,""Errors"":null,""ReloadActions"":false},""method"":{""Name"":""dbo.proc_GetAllMapLocations"",""SourceCode"":""-- =============================================\r<br />-- Author:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<Author,,Name>\r<br />-- Create date: <Create Date,,>\r<br />-- Description:&nbsp;&nbsp;&nbsp;&nbsp;<Description,,>\r<br />-- =============================================\r<br />CREATE PROCEDURE dbo.proc_GetAllMapLocations \r<br />&nbsp;&nbsp;&nbsp;&nbsp;\r<br />AS\r<br />BEGIN\r<br />&nbsp;&nbsp;&nbsp;&nbsp;SET NOCOUNT ON;\r<br />\r<br />    SELECT MapLocationID, StreetAddress,Latitude,Longitude FROM dbo.MapLocation ORDER BY MapLocationID ASC\r<br />END\r<br />"",""Parameters"":[]},""recordset"":{""Name"":""dbo_proc_GetAllMapLocations"",""Fields"":[],""Records"":[],""HasErrors"":false,""ErrorMessage"":""""}}";
            
            //------------Execute Test---------------------------

            var result = serviceInvoker.Invoke("Services", "DbTest", args, Guid.Empty, Guid.Empty);

            // __COMMA__ is expected as this means sample has been delimited properly by the server.
            // It also means there is an implicit contract between the UI and server to handle __COMMA__ back into ,
            const string expected = @"{""Name"":""dbo_proc_GetAllMapLocations"",""HasErrors"":false,""ErrorMessage"":"""",""Fields"":[{""Name"":""MapLocationID"",""Alias"":""MapLocationID"",""RecordsetAlias"":null,""Path"":{""$type"":""Unlimited.Framework.Converters.Graph.String.Xml.XmlPath, Dev2.Core"",""ActualPath"":""DocumentElement().Table.MapLocationID"",""DisplayPath"":""DocumentElement().Table.MapLocationID"",""SampleData"":""1,2,3,4,5"",""OutputExpression"":""""}},{""Name"":""StreetAddress"",""Alias"":""StreetAddress"",""RecordsetAlias"":null,""Path"":{""$type"":""Unlimited.Framework.Converters.Graph.String.Xml.XmlPath, Dev2.Core"",""ActualPath"":""DocumentElement().Table.StreetAddress"",""DisplayPath"":""DocumentElement().Table.StreetAddress"",""SampleData"":""19 Pineside Road__COMMA__ New Germany,1244 Old North Coast Rd__COMMA__ Redhill,Westmead Road__COMMA__ Westmead,Turquoise Road__COMMA__ Queensmead,Old Main Road__COMMA__ Isipingo"",""OutputExpression"":""""}},{""Name"":""Latitude"",""Alias"":""Latitude"",""RecordsetAlias"":null,""Path"":{""$type"":""Unlimited.Framework.Converters.Graph.String.Xml.XmlPath, Dev2.Core"",""ActualPath"":""DocumentElement().Table.Latitude"",""DisplayPath"":""DocumentElement().Table.Latitude"",""SampleData"":"",,,,"",""OutputExpression"":""""}},{""Name"":""Longitude"",""Alias"":""Longitude"",""RecordsetAlias"":null,""Path"":{""$type"":""Unlimited.Framework.Converters.Graph.String.Xml.XmlPath, Dev2.Core"",""ActualPath"":""DocumentElement().Table.Longitude"",""DisplayPath"":""DocumentElement().Table.Longitude"",""SampleData"":"",,,,"",""OutputExpression"":""""}}],""Records"":[{""Label"":""dbo_proc_GetAllMapLocations(1)"",""Name"":""dbo_proc_GetAllMapLocations"",""Count"":4,""Cells"":[{""Name"":""dbo_proc_GetAllMapLocations(1).MapLocationID"",""Label"":""MapLocationID"",""Value"":""1""},{""Name"":""dbo_proc_GetAllMapLocations(1).StreetAddress"",""Label"":""StreetAddress"",""Value"":""19 Pineside Road__COMMA__ New Germany""},{""Name"":""dbo_proc_GetAllMapLocations(1).Latitude"",""Label"":""Latitude"",""Value"":""""},{""Name"":""dbo_proc_GetAllMapLocations(1).Longitude"",""Label"":""Longitude"",""Value"":""""}]},{""Label"":""dbo_proc_GetAllMapLocations(2)"",""Name"":""dbo_proc_GetAllMapLocations"",""Count"":4,""Cells"":[{""Name"":""dbo_proc_GetAllMapLocations(2).MapLocationID"",""Label"":""MapLocationID"",""Value"":""2""},{""Name"":""dbo_proc_GetAllMapLocations(2).StreetAddress"",""Label"":""StreetAddress"",""Value"":""1244 Old North Coast Rd__COMMA__ Redhill""},{""Name"":""dbo_proc_GetAllMapLocations(2).Latitude"",""Label"":""Latitude"",""Value"":""""},{""Name"":""dbo_proc_GetAllMapLocations(2).Longitude"",""Label"":""Longitude"",""Value"":""""}]},{""Label"":""dbo_proc_GetAllMapLocations(3)"",""Name"":""dbo_proc_GetAllMapLocations"",""Count"":4,""Cells"":[{""Name"":""dbo_proc_GetAllMapLocations(3).MapLocationID"",""Label"":""MapLocationID"",""Value"":""3""},{""Name"":""dbo_proc_GetAllMapLocations(3).StreetAddress"",""Label"":""StreetAddress"",""Value"":""Westmead Road__COMMA__ Westmead""},{""Name"":""dbo_proc_GetAllMapLocations(3).Latitude"",""Label"":""Latitude"",""Value"":""""},{""Name"":""dbo_proc_GetAllMapLocations(3).Longitude"",""Label"":""Longitude"",""Value"":""""}]},{""Label"":""dbo_proc_GetAllMapLocations(4)"",""Name"":""dbo_proc_GetAllMapLocations"",""Count"":4,""Cells"":[{""Name"":""dbo_proc_GetAllMapLocations(4).MapLocationID"",""Label"":""MapLocationID"",""Value"":""4""},{""Name"":""dbo_proc_GetAllMapLocations(4).StreetAddress"",""Label"":""StreetAddress"",""Value"":""Turquoise Road__COMMA__ Queensmead""},{""Name"":""dbo_proc_GetAllMapLocations(4).Latitude"",""Label"":""Latitude"",""Value"":""""},{""Name"":""dbo_proc_GetAllMapLocations(4).Longitude"",""Label"":""Longitude"",""Value"":""""}]},{""Label"":""dbo_proc_GetAllMapLocations(5)"",""Name"":""dbo_proc_GetAllMapLocations"",""Count"":4,""Cells"":[{""Name"":""dbo_proc_GetAllMapLocations(5).MapLocationID"",""Label"":""MapLocationID"",""Value"":""5""},{""Name"":""dbo_proc_GetAllMapLocations(5).StreetAddress"",""Label"":""StreetAddress"",""Value"":""Old Main Road__COMMA__ Isipingo""},{""Name"":""dbo_proc_GetAllMapLocations(5).Latitude"",""Label"":""Latitude"",""Value"":""""},{""Name"":""dbo_proc_GetAllMapLocations(5).Longitude"",""Label"":""Longitude"",""Value"":""""}]}]}";
            
            //------------Assert Results-------------------------
            Assert.AreEqual(expected, result.ToString());
        }
    }
}
