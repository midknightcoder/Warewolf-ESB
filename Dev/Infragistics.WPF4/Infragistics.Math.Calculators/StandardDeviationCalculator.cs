using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Infragistics.Math;

namespace Infragistics.Math.Calculators 
{
    /// <summary>
    /// Represents the class that calculates the standard deviation of a set of data
    /// </summary>
    public class StandardDeviationCalculator : ValueCalculator, IErrorBarCalculator
    {
        /// <summary>
        /// Calculates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public override double Calculate(System.Collections.Generic.IList<double> input)
        {
            if (input == null || input.Count == 0)
            {
                return double.NaN;
            }

            Infragistics.Math.Vector vector = CreateInputVector(input);

            if (vector.Length == 0)
            {
                return double.NaN;
            }

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }

            return Compute.StandardDeviation(vector, StatisticsType.Population);
        }

        #region IErrorBarCalculator

        public ErrorBarCalculatorType GetCalculatorType()
        {
            return ErrorBarCalculatorType.StandardDeviation;
        }

        public double GetIndependentValue()
        {
            return this.Value;
        }

        public double GetDependentValue(double value)
        {
            throw new NotImplementedException();
        }

        public IFastItemColumn<double> GetItemColumn()
        {
            return null;
        }

        public event EventHandler<EventArgs> Changed;

        public bool HasConstantPosition()
        {
            return true;
        }

        public double GetPosition()
        {
            MeanCalculator mean = new MeanCalculator() { ItemsSource = this.ItemsSource, ValueMemberPath = this.ValueMemberPath };
            return mean.Value;
        }
        
        #endregion //IErrorBarCalculator


        
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