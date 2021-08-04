using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls.Card
{
    public static class StyleKit
    {
        public static readonly Color MediumGrey = Color.FromHex("9F9F9F");
        public static readonly Color CardBorderColor = Color.FromHex("E3E3E3");
        public static readonly Color LightTextColor = Color.FromHex("383838");

        public static readonly Color BarBackgroundColor = Color.FromHex("375587");
        public static readonly Color CardFooterBackgroundColor = Color.FromHex("F6F6F6");

        public static class Status
        {
            public static readonly Color CompletedColor = Color.FromHex("00A2D3");
            public static readonly Color AlertColor = Color.FromHex("E74C3C");
            public static readonly Color UnresolvedColor = Color.FromHex("C5C5C5");
        }

        public class Icons
        {
            public static readonly FileImageSource Alert = new FileImageSource() {File = "Alert.png"};
            public static readonly FileImageSource Resume = new FileImageSource() {File = "Resume.png"};
            public static readonly FileImageSource Completed = new FileImageSource() {File = "Completed.png"};
            public static readonly FileImageSource Report = new FileImageSource() {File = "Report.png"};
            public static readonly FileImageSource Unresolved = new FileImageSource() {File = "Unresolved.png"};
            public static readonly FileImageSource Cog = new FileImageSource() {File = "Config.png"};
            public static readonly FileImageSource SmallCalendar = new FileImageSource() {File = "Calendarmini.png"};
            public static readonly FileImageSource SmallClock = new FileImageSource() {File = "Clockmini.png"};

            public static readonly FileImageSource Shadow0240 = new FileImageSource() {File = "Shadow-0-2-4-0.png"};
        }
    }
}