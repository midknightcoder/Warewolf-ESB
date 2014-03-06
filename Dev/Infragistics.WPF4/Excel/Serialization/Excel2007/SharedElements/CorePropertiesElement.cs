using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;






using Infragistics.Documents.Excel.Serialization.Excel2007.SharedContentTypes;

namespace Infragistics.Documents.Excel.Serialization.Excel2007.SharedElements

{
    internal class CorePropertiesElement : XmlElementBase
	{
		#region Constants

		public const string LocalName = "coreProperties";

		public const string QualifiedName =
			CorePropertiesPart.CorePropertiesNamespace +
			XmlElementBase.NamespaceSeparator +
			CorePropertiesElement.LocalName;

		#endregion Constants

		#region Base Class Overrides

		#region ElementName

		public override string ElementName
		{
			get { return CorePropertiesElement.QualifiedName; }
		}

		#endregion ElementName

		#region Load

		protected override void Load( Excel2007WorkbookSerializationManager manager, ExcelXmlElement element, string value, ref bool isReaderOnNextNode )
		{
			manager.ContextStack.Push( manager.Workbook );
			manager.ContextStack.Push( manager.Workbook.DocumentProperties );
		}

		#endregion Load

		#region Save

		protected override void Save( Excel2007WorkbookSerializationManager manager, ExcelXmlElement element, ref string value )
		{
			manager.ContextStack.Push( manager.Workbook );
			manager.ContextStack.Push( manager.Workbook.DocumentProperties );

            XmlElementBase.AddNamespaceDeclaration(
                element,
                CorePropertiesPart.DcNamespacePrefix,
                CorePropertiesPart.DcNamespace );

            XmlElementBase.AddNamespaceDeclaration(
                element,
                CorePropertiesPart.DctermsNamespacePrefix,
                CorePropertiesPart.DctermsNamespace );

            XmlElementBase.AddNamespaceDeclaration(
                element,
                CorePropertiesPart.XsiNamespacePrefix,
                CorePropertiesPart.XsiNamespace );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Title ) == false )
                XmlElementBase.AddElement( element, TitleElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Subject ) == false )
                XmlElementBase.AddElement( element, SubjectElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Author ) == false )
                XmlElementBase.AddElement( element, CreatorElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Keywords ) == false )
                XmlElementBase.AddElement( element, KeywordsElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Comments ) == false )
                XmlElementBase.AddElement( element, DescriptionElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Author ) == false )
                XmlElementBase.AddElement( element, LastModifiedByElement.QualifiedName );

            XmlElementBase.AddElement( element, CreatedElement.QualifiedName );
            XmlElementBase.AddElement( element, ModifiedElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Category ) == false )
                XmlElementBase.AddElement( element, CategoryElement.QualifiedName );

            if ( String.IsNullOrEmpty( manager.Workbook.DocumentProperties.Status ) == false )
                XmlElementBase.AddElement( element, ContentStatusElement.QualifiedName );
		}

        #endregion Save 

		#endregion Base Class Overrides
	}
}

#region Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved
/* ---------------------------------------------------------------------*
*                           Infragistics, Inc.                          *
*              Copyright (c) 2001-2012 All Rights reserved               *
*                                                                       *
*                                                                       *
* This file and its contents are protected by United States and         *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF INFRAGISTICS, INC. *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY INFRAGISTICS PRODUCT.    *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF INFRAGISTICS,      *
* INC.  THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO       *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved