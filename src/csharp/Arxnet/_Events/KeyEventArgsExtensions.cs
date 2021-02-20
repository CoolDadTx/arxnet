using System;

using SFML.Window;

namespace P3Net.Arx
{
    public static class KeyEventArgsExtensions
    {
        //TODO: Deprecate
        //[Obsolete("Use version returning a virtual key")]
        public static string KeyString ( this KeyEventArgs source )
        {
            switch (source.Code)
            {
                case Keyboard.Key.Enter: return "RETURN";
                case Keyboard.Key.Escape: return "ESC";

                case Keyboard.Key.Num0: return "0";
                case Keyboard.Key.Num1: return "1";
                case Keyboard.Key.Num2: return "2";
                case Keyboard.Key.Num3: return "3";
                case Keyboard.Key.Num4: return "4";
                case Keyboard.Key.Num5: return "5";
                case Keyboard.Key.Num6: return "6";
                case Keyboard.Key.Num7: return "7";
                case Keyboard.Key.Num8: return "8";
                case Keyboard.Key.Num9: return "9";

                case Keyboard.Key.Left: return "left";
                case Keyboard.Key.Right: return "right";
                case Keyboard.Key.Up: return "up";
                case Keyboard.Key.Down: return "down";

                case Keyboard.Key.Comma: return ",";
                case Keyboard.Key.Period: return ".";
            };

            return source.Code.ToString().ToUpper();
        }
    }
}
