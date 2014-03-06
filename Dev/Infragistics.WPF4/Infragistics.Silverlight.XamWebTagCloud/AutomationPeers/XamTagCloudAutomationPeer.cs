using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using Infragistics.Controls.Menus;
using System.ComponentModel;

namespace Infragistics.AutomationPeers
{
    /// <summary>
    /// Exposes <see cref="XamTagCloud" /> types to UI
    /// </summary>

    [DesignTimeVisible(false)]

    public class XamTagCloudAutomationPeer : ItemsControlAutomationPeer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XamTagCloudAutomationPeer"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public XamTagCloudAutomationPeer(XamTagCloud owner)
            : base(owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
        }
        #endregion //Constructors

        #region Properties

        #region OwningTagCloud
        private XamTagCloud OwningTagCloud
        {
            get
            {
                return (XamTagCloud)this.Owner;
            }
        }
        #endregion //OwningTagCloud

        #endregion Properties

        #region Overrides

        #region GetAutomationControlTypeCore
        /// <summary>
        /// Returns the control type for the <see cref="T:System.Windows.UIElement"/> that is associated with this <see cref="T:System.Windows.Automation.Peers.FrameworkElementAutomationPeer"/>. This method is called by <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType"/>.
        /// </summary>
        /// <returns>A value of the enumeration.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }
        #endregion //GetAutomationControlTypeCore

        #region GetChildrenCore
        /// <summary>
        /// Gets the collection of child elements of the <see cref="T:System.Windows.Controls.ItemsControl"/> that is associated with this <see cref="T:System.Windows.Automation.Peers.ItemsControlAutomationPeer"/>.
        /// </summary>
        /// <returns>The collection of child elements.</returns>
        protected override List<AutomationPeer> GetChildrenCore()
        {
            int count = this.OwningTagCloud.Items.Count;

            List<AutomationPeer> items = new List<AutomationPeer>(count);
            
            for (int i = 0; i < count; i++)
            {
                UIElement item = this.OwningTagCloud.Items[i] as UIElement;
                if (item != null)
                {
                    AutomationPeer peer = FromElement(item);
                    if (peer == null)
                    {
                        CreatePeerForElement(item);
                    }

                    if (peer != null)
                    {
                        items.Add(peer);
                    }
                }
            }            

            return items;
        }
        #endregion //GetChildrenCore

        #region GetClassNameCore
        /// <summary>
        /// Returns the name of the <see cref="T:System.Windows.UIElement"/> that is associated with this <see cref="T:System.Windows.Automation.Peers.FrameworkElementAutomationPeer"/>. This method is called by <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClassName"/>.
        /// </summary>
        /// <returns>
        /// The name of the owner type that is associated with this <see cref="T:System.Windows.Automation.Peers.FrameworkElementAutomationPeer"/>. See Remarks.
        /// </returns>
        protected override string GetClassNameCore()
        {
            return this.Owner.GetType().Name;
        }
        #endregion //GetClassNameCore

        #region GetNameCore

        /// <summary>
        /// Returns the text label of the <see cref="T:System.Windows.FrameworkElement"/> that is associated with this <see cref="T:System.Windows.Automation.Peers.FrameworkElementAutomationPeer"/>. This method is called by <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName"/>.
        /// </summary>
        /// <returns>
        /// The text label of the element that is associated with this automation peer.
        /// </returns>
        protected override string GetNameCore()
        {
            string nameCore = base.GetNameCore();

            if (string.IsNullOrEmpty(nameCore))
            {
                nameCore = this.OwningTagCloud.Name;
            }

            return nameCore;
        }
        #endregion //GetNameCore

        #region CreateItemAutomationPeer
        /// <summary>
        /// When overridden in a derived class, creates a new instance of the <see cref="T:System.Windows.Automation.Peers.ItemAutomationPeer"/> for a data item in the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> collection of this <see cref="T:System.Windows.Controls.ItemsControl"/>.
        /// </summary>
        /// <param name="item">The data item that is associated with this <see cref="T:System.Windows.Automation.Peers.ItemAutomationPeer"/>.</param>
        /// <returns>
        /// An object that exposes the data item to UI automation.
        /// </returns>
        protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
        {
            ItemAutomationPeer peer = null;
            if (item is UIElement)
            {
                peer = FrameworkElementAutomationPeer.CreatePeerForElement((UIElement)item) as ItemAutomationPeer;
            }
            return peer;
        }
        #endregion // CreateItemAutomationPeer

        #endregion Overrides
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