using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;







using Infragistics.Documents.Excel.FormulaUtilities.Tokens;
using Infragistics.Documents.Excel.Serialization;

namespace Infragistics.Documents.Excel.FormulaUtilities

{
    internal class XLSXFormulaStringGenerator : FormulaStringGenerator
    {
        #region Members

        Dictionary<WorkbookReferenceBase, int> externalReferences;

        #endregion //Members

        #region Constructor

        public XLSXFormulaStringGenerator(Formula formula, Dictionary<WorkbookReferenceBase, int> externalReferences)
			// MD 5/25/11 - Data Validations / Page Breaks
			// Formula string should always be written in the invariant culture. 
			
            //: base(formula, CellReferenceMode.A1, CultureInfo.CurrentCulture)
			: base(formula, CellReferenceMode.A1, CultureInfo.InvariantCulture)
        {
            this.externalReferences = externalReferences;

			// MD 2/4/11 - TFS65015
			// The string generated by this class are always used for saving.
			this.IsForSaving = true;
        }
        #endregion //Constructor

        #region Base Class Overrides

		// MD 5/25/11 - Data Validations / Page Breaks
		#region CombineWhitespaceAndOperandStrings

		protected override string CombineWhitespaceAndOperandStrings(FormulaToken currentToken, Dictionary<WorkbookReferenceBase, int> externalReferences, string whitespace)
		{
			NameToken nameToken = currentToken as NameToken;

			if (nameToken != null)
			{
				NamedReferenceBase namedRef = nameToken.NamedReference;

				if (namedRef != null && namedRef.IsBuiltIn)
				{
					Worksheet worksheet = namedRef.Scope as Worksheet;
					if (worksheet != null)
					{
						return whitespace +
							Utilities.CreateReferenceString(null, worksheet.Name) +
							NamedReferenceBase.BuildInNamePrefixFor2007 +
							namedRef.Name;
					}
				}
			}

			return base.CombineWhitespaceAndOperandStrings(currentToken, externalReferences, whitespace);
		}

		#endregion  // CombineWhitespaceAndOperandStrings

		#region ExternalReferences

		protected override Dictionary<WorkbookReferenceBase, int> ExternalReferences
		{
			get { return this.externalReferences; }
		}

		#endregion // ExternalReferences

        #endregion //Base Class Overrides
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