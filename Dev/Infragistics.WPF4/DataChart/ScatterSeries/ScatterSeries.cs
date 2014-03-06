using System.Windows;
using Infragistics.Controls.Charts.Util;
using System.Windows.Media;


namespace Infragistics.Controls.Charts
{
    /// <summary>
    /// Represents a XamDataChart scatter series
    /// </summary>
    public sealed class ScatterSeries: ScatterBase
    {
        /// <summary>
        /// Called to create the view for the series.
        /// </summary>
        /// <returns>The created view.</returns>
        protected override SeriesView CreateView()
        {
            return new ScatterSeriesView(this);
        }
        
        /// <summary>
        /// Called when the view for the series is created.
        /// </summary>
        /// <param name="view">The view that has been created.</param>
        protected internal override void OnViewCreated(SeriesView view)
        {
            base.OnViewCreated(view);

            ScatterSeriesView = (ScatterSeriesView)view;
        }
        internal ScatterSeriesView ScatterSeriesView { get; set; }

        #region constructor and initialisation
        /// <summary>
        /// Initializes a new instance of the ScatterSeries class. 
        /// </summary>
        public ScatterSeries()
        {
            DefaultStyleKey = typeof(ScatterSeries);
            PreviousFrame = new ScatterFrame();
            TransitionFrame = new ScatterFrame();
            CurrentFrame = new ScatterFrame();
        }
        #endregion
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