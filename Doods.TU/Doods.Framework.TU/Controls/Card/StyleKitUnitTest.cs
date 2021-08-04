using System;
using System.Collections.Generic;
using Doods.Framework.Mobile.Std.Controls.Card;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Controls.Card
{
    [TestClass]
    public class StyleKitUnitTest
    {
      
        private string GetHexString(Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
        [TestMethod]
        public void GetMediumGrey()
        {
            Assert.IsNotNull(StyleKit.MediumGrey);
            Assert.AreEqual("FF9F9F9F", GetHexString(StyleKit.MediumGrey));
           
        }

        [TestMethod]
        public void GetCardBorderColor()
        {
            Assert.IsNotNull(StyleKit.CardBorderColor);
            Assert.AreEqual("FFE3E3E3", GetHexString(StyleKit.CardBorderColor));
        }

        [TestMethod]
        public void GetLightTextColor()
        {
            Assert.IsNotNull(StyleKit.LightTextColor);
            Assert.AreEqual("FF383838", GetHexString(StyleKit.LightTextColor));
        }

        [TestMethod]
        public void GetBarBackgroundColor()
        {
            Assert.IsNotNull(StyleKit.BarBackgroundColor);
            Assert.AreEqual("FF375587", GetHexString(StyleKit.BarBackgroundColor));
        }

        [TestMethod]
        public void GetCardFooterBackgroundColor()
        {
            Assert.IsNotNull(StyleKit.CardFooterBackgroundColor);
            Assert.AreEqual("FFF6F6F6", GetHexString(StyleKit.CardFooterBackgroundColor));
        }

    
    }

    [TestClass]
    public class StatusUnitTest
    {
        private string GetHexString(Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
        [TestMethod]
        public void GetCompletedColor()
        {
            Assert.IsNotNull(StyleKit.Status.CompletedColor);
            Assert.AreEqual("FF00A2D3", GetHexString(StyleKit.Status.CompletedColor));
        }

        [TestMethod]
        public void GetAlertColor()
        {
            Assert.IsNotNull(StyleKit.Status.AlertColor);
            Assert.AreEqual("FFE74C3C", GetHexString(StyleKit.Status.AlertColor));
        }

        [TestMethod]
        public void GetUnresolvedColor()
        {
            Assert.IsNotNull(StyleKit.Status.UnresolvedColor);
            Assert.AreEqual("FFC5C5C5", GetHexString(StyleKit.Status.UnresolvedColor));
        }

    }

    [TestClass]
    public class IconsUnitTest
    {
        [TestMethod]
        public void GetAlert()
        {
            Assert.IsNotNull(StyleKit.Icons.Alert);
            Assert.AreEqual("Alert.png", StyleKit.Icons.Alert.File);
        }

        [TestMethod]
        public void GetResume()
        {
            Assert.IsNotNull(StyleKit.Icons.Resume);
            Assert.AreEqual("Resume.png", StyleKit.Icons.Resume.File);
        }

        [TestMethod]
        public void GetCompleted()
        {
            Assert.IsNotNull(StyleKit.Icons.Completed);
            Assert.AreEqual("Completed.png", StyleKit.Icons.Completed.File);
        }
        [TestMethod]
        public void GetReport()
        {
            Assert.IsNotNull(StyleKit.Icons.Report);
            Assert.AreEqual("Report.png", StyleKit.Icons.Report.File);
        }

        [TestMethod]
        public void GetUnresolved()
        {
            Assert.IsNotNull(StyleKit.Icons.Unresolved);
            Assert.AreEqual("Unresolved.png", StyleKit.Icons.Unresolved.File);
        }

        [TestMethod]
        public void GetCog()
        {
            Assert.IsNotNull(StyleKit.Icons.Cog);
            Assert.AreEqual("Config.png", StyleKit.Icons.Cog.File);
        }
        [TestMethod]
        public void GetSmallCalendar()
        {
            Assert.IsNotNull(StyleKit.Icons.SmallCalendar);
            Assert.AreEqual("Calendarmini.png", StyleKit.Icons.SmallCalendar.File);
        }

        [TestMethod]
        public void GetSmallClock()
        {
            Assert.IsNotNull(StyleKit.Icons.SmallClock);
            Assert.AreEqual("Clockmini.png", StyleKit.Icons.SmallClock.File);
        }

        [TestMethod]
        public void GetShadow0240()
        {
            Assert.IsNotNull(StyleKit.Icons.Shadow0240);
            Assert.AreEqual("Shadow-0-2-4-0.png", StyleKit.Icons.Shadow0240.File);
        }
    }
}
