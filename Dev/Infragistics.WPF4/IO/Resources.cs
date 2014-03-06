
// JJD 8/28/02 - Added support for runtime string customizations
// This class needs to be outside the namespace
//
// MBS 4/9/07 - FxCop
// Made class static so that a constructor isn't generated by the compiler
internal static class ResourceCustomizerLocator
{
    internal static readonly Infragistics.Documents.DocumentsResourceCustomizer Customizer = new Infragistics.Documents.DocumentsResourceCustomizer();
}

//-----------------------------------------------------------------
// Note: The only code that needs to be changed is the namespace
//       below, which should be the name of the primary namespace
//       of the assembly. However, no 2 assemblies should have an
//       instance of this file with the same namespace specified 
//       below.
//-----------------------------------------------------------------
namespace Infragistics.Documents // change this line only to the unigue namespace of this assembly
{
	/// <summary>
    /// Exposes a <see cref="Infragistics.Documents.DocumentsResourceCustomizer"/> instance for this assembly. 
	/// </summary>
	/// <seealso cref="Customizer"/> 
    /// <seealso cref="Infragistics.Documents.DocumentsResourceCustomizer"/> 
    // MBS 4/10/07
    // Made class static so that a constructor isn't generated by the compiled
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class Resources
	{
		/// <summary>
        /// Returns the <see cref="Infragistics.Documents.DocumentsResourceCustomizer"/> for this assembly.
		/// </summary>
        /// <seealso cref="Infragistics.Documents.DocumentsResourceCustomizer"/> 
        public static Infragistics.Documents.DocumentsResourceCustomizer Customizer { get { return ResourceCustomizerLocator.Customizer; } }

		// AS 7/15/05
		// Provide a way for the customer to get the resolved resource
		// string/object that the assembly has access to.
		//
		#region GetString
		/// <summary>
		/// Returns the resource string using the specified name and default culture.
		/// </summary>
		/// <param name="name">Name of the string resource to return.</param>
		/// <param name="args">Arguments supplied to the string.Format method when formatting the string.</param>
		public static string GetString(string name, params object[] args)
		{
			return Infragistics.Shared.SR.GetString(name, args);
		}

		/// <summary>
		/// Returns the resource string using the specified resource name and default culture. The string is then formatted using the arguments specified.
		/// </summary>
		/// <param name="name">Name of the string resource to return.</param>
		public static string GetString(string name)
		{
			return Infragistics.Shared.SR.GetString(name);
		}
		#endregion //GetString

		#region GetObject
		/// <summary>
		/// Returns the resource object using the specified name.
		/// </summary>
		/// <param name="name">Name of the resource item</param>
		/// <returns>An object containing the specified resource</returns>
		public static object GetObject(string name)
		{
			return Infragistics.Shared.SR.GetObject(name);
		}
		#endregion //GetObject
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