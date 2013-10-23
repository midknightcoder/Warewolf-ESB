﻿using System.Activities.Presentation.Model;
using System.Collections.Generic;
using Dev2.Activities.Designers2.DateTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;using System.Diagnostics.CodeAnalysis;
using Moq;
using Moq.Protected;

namespace Dev2.Activities.Designers.Tests.DateTimeTests
{
    [TestClass][ExcludeFromCodeCoverage]
    public class DateTimeViewModelTests
    {
        [TestMethod]
        [TestCategory("DateTimeActivityViewModel_SelectedTimeModifierTypeSelectedItem")]
        [Description("DateTime ViewModel clears the TimeModifierAmountDisplay property of the model item if the SelectedTimeModifierType property of the view model is set to a blank string")]
        [Owner("Ashley Lewis")]
        // ReSharper disable InconsistentNaming
        public void DateTimeActivityViewModel_SelectedTimeModifierTypeChange_SelectedTimeModifierTypeSetToABlankString_TimeModifierAmountDisplayCleared()
        // ReSharper restore InconsistentNaming
        {
            //init
            var expected = string.Empty;
            const string TimeModifierAmountDisplay = "TimeModifierAmountDisplay";

            var prop = new Mock<ModelProperty>();
            var properties = new Dictionary<string, Mock<ModelProperty>>();
            var propertyCollection = new Mock<ModelPropertyCollection>();
            var mockModel = new Mock<ModelItem>();

            prop.Setup(p => p.SetValue(expected)).Verifiable();
            properties.Add(TimeModifierAmountDisplay, prop);
            propertyCollection.Protected().Setup<ModelProperty>("Find", TimeModifierAmountDisplay, true).Returns(prop.Object);
            mockModel.Setup(s => s.Properties).Returns(propertyCollection.Object);

            //exe
            new DateTimeDesignerViewModel(mockModel.Object) { SelectedTimeModifierType = expected };

            //assert
            prop.Verify(c => c.SetValue(expected), Times.Once(), "Find Records ViewModel does not clear the match data property of the model item when it's no longer needed");
        }

        [TestMethod]
        [TestCategory("DateTimeActivityViewModel_SelectedTimeModifierTypeSelectedItem")]
        [Description("DateTime ViewModel does not clear the TimeModifierAmountDisplay property of the model item if the SelectedTimeModifierType property of the view model is set to some string")]
        [Owner("Ashley Lewis")]
        // ReSharper disable InconsistentNaming
        public void DateTimeActivityViewModel_SelectedTimeModifierTypeChange_SelectedTimeModifierTypeSetToABlankString_TimeModifierAmountDisplayNotCleared()
        // ReSharper restore InconsistentNaming
        {
            //init
            var expected = "Some Data";
            const string MatchDataPropertyName = "TimeModifierAmountDisplay";

            var prop = new Mock<ModelProperty>();
            var properties = new Dictionary<string, Mock<ModelProperty>>();
            var propertyCollection = new Mock<ModelPropertyCollection>();
            var mockModel = new Mock<ModelItem>();

            prop.Setup(p => p.SetValue(expected)).Verifiable();
            properties.Add(MatchDataPropertyName, prop);
            propertyCollection.Protected().Setup<ModelProperty>("Find", MatchDataPropertyName, true).Returns(prop.Object);
            mockModel.Setup(s => s.Properties).Returns(propertyCollection.Object);

            //exe
            new DateTimeDesignerViewModel(mockModel.Object) { SelectedTimeModifierType = expected };

            //assert
            prop.Verify(c => c.SetValue(expected), Times.Never(), "Find Records ViewModel does not clear the match data property of the model item when it's no longer needed");
        }
    }
}
