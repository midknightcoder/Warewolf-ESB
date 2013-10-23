﻿using System;
using System.IO;
using System.Xml.Linq;
using Dev2.Common;
using Dev2.Common.Common;
using Dev2.Data.ServiceModel;
using Dev2.Runtime.Diagnostics;
using Dev2.Runtime.ServiceModel.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Dev2.Tests.Runtime.ServiceModel
{
    // PBI: 801
    // BUG: 8477

    /// <author>trevor.williams-ros</author>
    /// <date>2013/02/13</date>
    [TestClass][ExcludeFromCodeCoverage]
    public class ServicesTests
    {

        #region CreateInputsMethod

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_Plugin_EmptyType()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType="""">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(object), sm.Parameters[0].Type);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_Plugin_StringType()
        {
            //------------Setup for test--------------------------
            var service = new Service();
            
            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(typeof(string), sm.Parameters[0].Type);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_Plugin_NameCorrect()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual("EmitStringData", sm.Name);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_PluginEmptyToNullNotSet_EmptyToNullFalse()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(false, sm.Parameters[0].EmptyToNull);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_PluginEmptyToNullSet_EmptyToNullTrue()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"" EmptyToNull=""true"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(true, sm.Parameters[0].EmptyToNull);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_PluginRequiredNotSet_RequiredFalse()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"" EmptyToNull=""true"">
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(false, sm.Parameters[0].IsRequired);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_PluginRequiredSet_RequiredTrue()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"" EmptyToNull=""true"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(true, sm.Parameters[0].IsRequired);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_PluginDefaultValueNotSet_DefaultValueEmpty()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue="""" NativeType=""String"" EmptyToNull=""true"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual(string.Empty, sm.Parameters[0].DefaultValue);
        }

        [TestMethod]
        [Owner("Travis Frisinger")]
        [TestCategory("Service_CreateInputsMethods")]
        public void Service_CreateInputsMethods_PluginDefaultValueSet_DefaultValueReturned()
        {
            //------------Setup for test--------------------------
            var service = new Service();

            #region Test String

            var input = @"<Action Name=""EmitStringData"" Type=""Plugin"" SourceName=""Anything To Xml Hook Plugin"" SourceMethod=""EmitStringData"" NativeType=""String"">
  <Inputs>
    <Input Name=""StringData"" Source=""StringData"" DefaultValue=""XXX"" NativeType=""String"" EmptyToNull=""true"">
      <Validator Type=""Required"" />
    </Input>
  </Inputs>
  <Outputs>
    <Output Name=""CompanyName"" MapsTo=""CompanyName"" Value=""[[Names().CompanyName]]"" Recordset=""Names"" />
    <Output Name=""DepartmentName"" MapsTo=""DepartmentName"" Value=""[[Names().DepartmentName]]"" Recordset=""Names"" />
    <Output Name=""EmployeeName"" MapsTo=""EmployeeName"" Value=""[[Names().EmployeeName]]"" Recordset=""Names"" />
  </Outputs>
  <OutputDescription><![CDATA[<z:anyType xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:d1p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.Ouput"" i:type=""d1p1:OutputDescription"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"">
                <d1p1:DataSourceShapes xmlns:d2p1=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                  <d2p1:anyType i:type=""d1p1:DataSourceShape"">
                    <d1p1:Paths>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().CompanyName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev2</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Motto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Eat lots of cake</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.PreviousMotto</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments:TestAttrib</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">testing</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().DepartmentName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Dev,Accounts</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Name</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Name</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">[[Names().EmployeeName]]</OutputExpression>
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Brendon,Jayd</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments().Department.Employees().Person:Surename</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.Departments.Department.Employees().Person:Surename</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Page,Page</SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().InlineRecordSet</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">
    RandomData
   ,
    RandomData1
   </SampleData>
                      </d2p1:anyType>
                      <d2p1:anyType xmlns:d5p1=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph.String.Xml"" i:type=""d5p1:XmlPath"">
                        <ActualPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company().OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</ActualPath>
                        <DisplayPath xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">Company.OuterNestedRecordSet().InnerNestedRecordSet:ItemValue</DisplayPath>
                        <OutputExpression xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"" />
                        <SampleData xmlns=""http://schemas.datacontract.org/2004/07/Unlimited.Framework.Converters.Graph"">val1,val2</SampleData>
                      </d2p1:anyType>
                    </d1p1:Paths>
                  </d2p1:anyType>
                </d1p1:DataSourceShapes>
                <d1p1:Format>ShapedXML</d1p1:Format>
              </z:anyType>]]></OutputDescription>
</Action>";

            #endregion

            //------------Execute Test---------------------------

            XElement xe = XElement.Parse(input);
            ServiceMethod sm = service.CreateInputsMethod(xe);

            //------------Assert Results-------------------------

            Assert.AreEqual("XXX", sm.Parameters[0].DefaultValue);
        }

        #endregion

        #region Save

        [TestMethod]
        public void SaveWithNullArgsExpectedReturnsErrorValidationResult()
        {
            var services = new Dev2.Runtime.ServiceModel.Services();
            var result = services.Save(null, Guid.Empty, Guid.Empty);
            var validationResult = JsonConvert.DeserializeObject<ValidationResult>(result);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void SaveWithInvalidArgsExpectedReturnsErrorValidationResult()
        {
            var services = new Dev2.Runtime.ServiceModel.Services();
            var result = services.Save("xxxxx", Guid.Empty, Guid.Empty);
            var validationResult = JsonConvert.DeserializeObject<ValidationResult>(result);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void SaveWithValidArgsAndEmptyResourceIDExpectedAssignsNewResourceID()
        {
            var svc = DbServicesTests.CreateCountriesDbService();
            svc.ResourceID = Guid.Empty;
            var args = svc.ToString();

            var workspaceID = Guid.NewGuid();
            var workspacePath = EnvironmentVariables.GetWorkspacePath(workspaceID);
            try
            {
                var services = new Dev2.Runtime.ServiceModel.Services();
                var result = services.Save(args, workspaceID, Guid.Empty);
                var service = JsonConvert.DeserializeObject<Service>(result);
                Assert.AreNotEqual(Guid.Empty, service.ResourceID);
            }
            finally
            {
                if(Directory.Exists(workspacePath))
                {
                    DirectoryHelper.CleanUp(workspacePath);
                }
            }
        }

        [TestMethod]
        public void SaveWithValidArgsAndResourceIDExpectedDoesNotAssignNewResourceID()
        {
            var svc = DbServicesTests.CreateCountriesDbService();
            var args = svc.ToString();
            var workspaceID = Guid.NewGuid();
            var workspacePath = EnvironmentVariables.GetWorkspacePath(workspaceID);
            try
            {
                var services = new Dev2.Runtime.ServiceModel.Services();
                var result = services.Save(args, workspaceID, Guid.Empty);
                var service = JsonConvert.DeserializeObject<Service>(result);
                Assert.AreEqual(svc.ResourceID, service.ResourceID);
            }
            finally
            {
                if(Directory.Exists(workspacePath))
                {
                    DirectoryHelper.CleanUp(workspacePath);
                }
            }
        }

        [TestMethod]
        public void SaveWithValidArgsExpectedSavesXmlToDisk()
        {
            var svc = DbServicesTests.CreateCountriesDbService();
            var args = svc.ToString();
            var workspaceID = Guid.NewGuid();
            var workspacePath = EnvironmentVariables.GetWorkspacePath(workspaceID);
            var path = Path.Combine(workspacePath, Dev2.Runtime.ServiceModel.Resources.RootFolders[ResourceType.DbService]);
            var fileName = String.Format("{0}\\{1}.xml", path, svc.ResourceName);
            try
            {
                var services = new Dev2.Runtime.ServiceModel.Services();
                services.Save(args, workspaceID, Guid.Empty);
                var exists = File.Exists(fileName);

                Assert.IsTrue(exists);
            }
            finally
            {
                if(Directory.Exists(workspacePath))
                {
                    DirectoryHelper.CleanUp(workspacePath);
                }
            }
        }

        #endregion

        #region Get

        [TestMethod]
        public void GetWithNullArgsExpectedReturnsNewService()
        {
            var services = new Dev2.Runtime.ServiceModel.Services();
            var result = services.Get(null, Guid.Empty, Guid.Empty);

            Assert.AreEqual(Guid.Empty, result.ResourceID);
        }

        [TestMethod]
        public void GetWithInvalidArgsExpectedReturnsNewService()
        {
            var services = new Dev2.Runtime.ServiceModel.Services();
            var result = services.Get("xxxxx", Guid.Empty, Guid.Empty);

            Assert.AreEqual(Guid.Empty, result.ResourceID);
        }

        [TestMethod]
        public void GetWithValidArgsExpectedReturnsService()
        {
            var svc = DbServicesTests.CreateCountriesDbService();
            var saveArgs = svc.ToString();
            var getArgs = string.Format("{{\"resourceID\":\"{0}\",\"resourceType\":\"{1}\"}}", svc.ResourceID, ResourceType.DbService);

            var workspaceID = Guid.NewGuid();
            var workspacePath = EnvironmentVariables.GetWorkspacePath(workspaceID);
            try
            {
                var services = new Dev2.Runtime.ServiceModel.Services();
                services.Save(saveArgs, workspaceID, Guid.Empty);
                var getResult = services.Get(getArgs, workspaceID, Guid.Empty);
                Assert.AreEqual(svc.ResourceID, getResult.ResourceID);
                Assert.AreEqual(svc.ResourceName, getResult.ResourceName);
                Assert.AreEqual(svc.ResourcePath, getResult.ResourcePath);
                Assert.AreEqual(svc.ResourceType, getResult.ResourceType);
            }
            finally
            {
                if(Directory.Exists(workspacePath))
                {
                    DirectoryHelper.CleanUp(workspacePath);
                }
            }
        }

        #endregion


    }
}