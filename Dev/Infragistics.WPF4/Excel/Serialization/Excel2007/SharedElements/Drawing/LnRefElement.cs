using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using Infragistics.Documents.Excel.Serialization.Excel2007.SharedContentTypes;




using System.Drawing;


namespace Infragistics.Documents.Excel.Serialization.Excel2007.SharedElements.Drawing
{
	// MD 3/12/12 - TFS102234
	internal class LnRefElement : XmlElementBase
	{
		#region XML Schema fragment <docs>
		//<complexType name="CT_StyleMatrixReference">
		//    <sequence>
		//        <group ref="EG_ColorChoice" minOccurs="0" maxOccurs="1"/>
		//    </sequence>
		//    <attribute name="idx" type="ST_StyleMatrixColumnIndex" use="required"/>
		//</complexType>
		#endregion XML Schema fragment <docs>

		#region Constants

		public const string LocalName = "lnRef";

		public const string QualifiedName =
			DrawingsPart.MainNamespace +
			XmlElementBase.NamespaceSeparator +
			LnRefElement.LocalName;

		#endregion Constants

		#region Base class overrides

		#region ElementName

		public override string ElementName
		{
			get { return LnRefElement.QualifiedName; }
		}

		#endregion ElementName

		#region Load

		protected override void Load(Excel2007WorkbookSerializationManager manager, ExcelXmlElement element, string value, ref bool isReaderOnNextNode)
		{
			WorksheetShape shape = (WorksheetShape)manager.ContextStack[typeof(WorksheetShape)];
			if (shape == null)
			{
				Utilities.DebugFail("Something is wrong.");
				return;
			}

			if (shape.Outline == null)
				return;

			ISolidColorItem solidOutline = shape.Outline as ISolidColorItem;
			if (solidOutline != null && Utilities.ColorIsEmpty(solidOutline.Color) == false)
				return;

			manager.ContextStack.Push(shape.Outline);

			// Push on a ChildDataItem to capture the color.
			manager.ContextStack.Push(new ChildDataItem());
		}

		#endregion // Load

		#region Save

		protected override void Save(Excel2007WorkbookSerializationManager manager, ExcelXmlElement element, ref string value)
		{

		}

		#endregion // Save

		#endregion Base class overrides
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