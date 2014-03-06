using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Infragistics.Documents.Core
{
    /// <summary>
    /// Duplicated:
    /// Infragistics.Documents.Excel.Serialization.Excel2007.ExcelXmlElement
    /// </summary>
	internal class OfficeXmlElement : OfficeXmlNode
	{
		private OfficeXmlAttributeCollection attributes = new OfficeXmlAttributeCollection();
		private string localName;
		private string namespaceURI;
		private string prefix;

		internal OfficeXmlElement( string prefix, string localName, string namespaceURI, OfficeXmlDocument ownerDocument )
			: base( ownerDocument )
		{
			this.localName = localName;
			this.namespaceURI = namespaceURI;
			this.prefix = prefix;
		}

		#region Base Class Overrides

		public override string LocalName
		{
			get { return this.localName; }
		}

		public override string NamespaceURI
		{
			get { return this.namespaceURI; }
		}

		public override XmlNodeType NodeType
		{
			get { return XmlNodeType.Element; }
		}

		public override string Prefix
		{
			get { return this.prefix; }
		}

		public override void WriteEnd()
		{
			this.OwnerDocument.Writer.WriteEndElement();
		}

		public override void WriteStart()
		{
			this.OwnerDocument.Writer.WriteStartElement( this.prefix, this.localName, this.namespaceURI );

			foreach ( OfficeXmlAttribute attribute in this.attributes )
				attribute.WriteNode();
		}

        //  BF 10/8/10  NA 2011.1 - Infragistics.Documents.Word
        //  This helps with debugging, remove it if it's annoying
        #region ToString
        /// <summary>
        /// Returns the string representation of this instance.
        /// </summary>
        public override string ToString()
        {



            return base.ToString();

        }
        #endregion ToString

		#endregion Base Class Overrides

		public OfficeXmlAttributeCollection Attributes
		{
			get { return this.attributes; }
		}
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