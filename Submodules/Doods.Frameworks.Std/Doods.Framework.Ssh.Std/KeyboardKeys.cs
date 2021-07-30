namespace Doods.Framework.Ssh.Std
{
    /// <summary>
    ///     https://www.novell.com/documentation/extend5/Docs/help/Composer/books/TelnetAppendixB.html
    /// </summary>
    public class KeyboardKeys
    {
        public const string Arrow_Down = "\u001b[B";
        public const string Arrow_Left = "\u001b[D";
        public const string Arrow_Right = "\u001b[C";
        public const string Arrow_Up = "\u001b[A";
        public const string BackSpace = "\u0008";
        public const string Back_Tab = "\u001bOP\u0009";
        public const string Delete = "\u007f";
        public const string Escape = "\u001b";
        public const string Linefeed = "\u000a";
        public const string Return = "\u000d";
        public const string Tab = "\u0009";
        public const string F1 = "\u001bOP";
        public const string F2 = "\u001bOQ";
        public const string F3 = "\u001bOR";
        public const string F4 = "\u001bOS";
        public const string F5 = "\u001b[15~";
        public const string F6 = "\u001b[17~";
        public const string F7 = "\u001b[18~";
        public const string F8 = "\u001b[19~";
        public const string F9 = "\u001b[20~";
        public const string F10 = "\u001b[21~";
        public const string F11 = "\u001b[23~";
        public const string F12 = "\u001b[24~";
        public const string F13 = "\u001b[25~";
        public const string F14 = "\u001b[26~";
        public const string F15 = "\u001b[28~";
        public const string F16 = "\u001b[29~";
        public const string F17 = "\u001b[31~";
        public const string F18 = "\u001b[32~";
        public const string F19 = "\u001b[33~";
        public const string F20 = "\u001b[34~";
        public const string zero = "\u001bOp";
        public const string one = "\u001bOq";
        public const string two = "\u001bOr";
        public const string three = "\u001bOs";
        public const string four = "\u001bOt";
        public const string five = "\u001bOu";
        public const string six = "\u001bOv";
        public const string seven = "\u001bOw";
        public const string eight = "\u001bOx";
        public const string nine = "\u001bOy";
        public const string Minus = "\u001bOm";
        public const string Comma = "\u001bOl";
        public const string Period = "\u001bOn";
        public const string Enter = "\r"; //"\u001bOM";
        public const string Do = "\u001b[29~";
        public const string Find = "\u001b[1~";
        public const string Help = "\u001b[28~";
        public const string Insert = "\u001b[2~";
        public const string KeyEnd = "\u001b[F";
        public const string KeyHome = "\u001b[H";
        public const string NextScn = "\u001b[6~";
        public const string PrevScn = "\u001b[5~";
        public const string Remove = "\u001b[3~";
        public const string Select = "\u001b[44~";
    }
}