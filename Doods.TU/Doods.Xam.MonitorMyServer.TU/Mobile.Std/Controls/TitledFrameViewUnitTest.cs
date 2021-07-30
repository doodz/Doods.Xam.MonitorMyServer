using System.Linq;
using Doods.Framework.Mobile.Std.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Mobile.Std.Controls
{
    [TestClass]
    public class TitledFrameViewUnitTest : BaselUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new TitledFrameView();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Title);
            Assert.IsNotNull(obj.TitleStyle);
            Assert.IsNull(obj.SubTitle);
            Assert.IsNotNull(obj.SubTitleStyle);
        }

        [TestMethod]
        public void Create_Set_Title()
        {
            var obj = new TitledFrameView();

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TitleStyle.Setters);

            obj.SetValue(TitledFrameView.TitleProperty, "toto");

            Assert.AreEqual("toto", obj.Title);
            Assert.IsNull(obj.SubTitle);
        }

        [TestMethod]
        public void Create_Set_SubTitle()
        {
            var obj = new TitledFrameView();

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TitleStyle.Setters);

            obj.SetValue(TitledFrameView.SubTitleProperty, "toto");

            Assert.AreEqual("toto", obj.SubTitle);
            Assert.IsNull(obj.Title);
        }


        [TestMethod]
        public void Create_Default_TitleStyle()
        {
            var obj = new TitledFrameView();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TitleStyle.Setters);

            var setters = obj.TitleStyle.Setters.ToList();


            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "FontAttributes"));
            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "FontSize"));
            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "TextColor"));
            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "VerticalTextAlignment"));

            Assert.AreEqual("Bold", setters.First(s => s.Property.PropertyName == "FontAttributes").Value.ToString());
            Assert.AreEqual(14, setters.First(s => s.Property.PropertyName == "FontSize").Value);
            Assert.AreEqual("[Color: A=1, R=1, G=1, B=1, Hue=0, Saturation=0, Luminosity=1]",
                setters.First(s => s.Property.PropertyName == "TextColor").Value.ToString()); //white
            Assert.AreEqual("Center",
                setters.First(s => s.Property.PropertyName == "VerticalTextAlignment").Value.ToString());
        }

        [TestMethod]
        public void Create_Default_SubTitleStyle()
        {
            var obj = new TitledFrameView();
            Assert.IsNotNull(obj);

            Assert.IsNotNull(obj.SubTitleStyle.Setters);

            var setters = obj.SubTitleStyle.Setters.ToList();


            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "FontAttributes"));
            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "FontSize"));
            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "TextColor"));
            Assert.IsTrue(setters.Any(s => s.Property.PropertyName == "VerticalTextAlignment"));

            Assert.AreEqual("Italic", setters.First(s => s.Property.PropertyName == "FontAttributes").Value.ToString());
            Assert.AreEqual(14, setters.First(s => s.Property.PropertyName == "FontSize").Value);
            Assert.AreEqual("[Color: A=1, R=0, G=0, B=0, Hue=0, Saturation=0, Luminosity=0]",
                setters.First(s => s.Property.PropertyName == "TextColor").Value.ToString()); //black
            Assert.AreEqual("Center",
                setters.First(s => s.Property.PropertyName == "VerticalTextAlignment").Value.ToString());
        }

        //Setters =
        //{
        //    new Setter
        //    {
        //        Property = Label.FontAttributesProperty, Value = FontAttributes.Italic
        //    },
        //    new Setter
        //    {
        //        Property = Label.FontSizeProperty, Value = 14
        //    },
        //    new Setter
        //    {
        //        Property = Label.TextColorProperty,
        //        Value = Color.Black
        //    },
        //    new Setter
        //    {
        //        Property = Label.VerticalTextAlignmentProperty,
        //        Value = TextAlignment.Center
        //    }
        //}
    }
}