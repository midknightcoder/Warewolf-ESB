
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.ComponentModel.Composition;
using System.Windows;
using Dev2.Common.Interfaces.Studio.Controller;

// ReSharper disable CheckNamespace
namespace Dev2.Core.Tests.ProperMoqs
// ReSharper restore CheckNamespace
{
    [Export(typeof(IPopupController))]
    public class MoqPopup : IPopupController
    {
        readonly MessageBoxResult _result;

        public MoqPopup(string headerText, string discriptionText, MessageBoxImage imageType, MessageBoxButton buttons)
        {
            Header = headerText;
            Description = discriptionText;
            ImageType = imageType;
            Buttons = buttons;
        }

        public MoqPopup()
            : this(MessageBoxResult.OK)
        {

        }

        public MoqPopup(MessageBoxResult result)
        {
            _result = result;
        }

        public string Header { get; set; }

        public string Description { get; set; }

        public string Question { get; set; }

        public MessageBoxImage ImageType { get; set; }

        public MessageBoxButton Buttons { get; set; }

        public MessageBoxResult Show()
        {
            return _result;
        }

        public MessageBoxResult Show(string description, string header, MessageBoxButton buttons, MessageBoxImage image, string dontShowAgainKey)
        {
            Description = description;
            Header = header;
            Buttons = buttons;
            ImageType = image;
            DontShowAgainKey = dontShowAgainKey;
            return Show();
        }

        public MessageBoxResult ShowNotConnected()
        {
            return _result;
        }

        public MessageBoxResult ShowDeleteConfirmation(string nameOfItemBeingDeleted)
        {
            return _result;
        }

        public MessageBoxResult ShowNameChangedConflict(string oldName, string newName)
        {
            return _result;
        }

        public MessageBoxResult ShowSettingsCloseConfirmation()
        {
            return _result;
        }

        public MessageBoxResult ShowSchedulerCloseConfirmation()
        {
            return _result;
        }

        public MessageBoxResult ShowNoInputsSelectedWhenClickLink()
        {
            return MessageBoxResult.None;
        }

        public MessageBoxResult ShowSaveErrorDialog(string errorMessage)
        {
            return _result;
        }

        public MessageBoxResult ShowConnectionTimeoutConfirmation(string serverName)
        {
            return _result;
        }

        public void ShowInvalidCharacterMessage(string invalidText)
        {
        }

        public MessageBoxResult ShowDeleteVersionMessage(string displayName)
        {
            return MessageBoxResult.None;
            }

        public MessageBoxResult ShowRollbackVersionMessage(string displayName)
            {
            return MessageBoxResult.None;
        }

        public string DontShowAgainKey { get; set; }
    }
}
