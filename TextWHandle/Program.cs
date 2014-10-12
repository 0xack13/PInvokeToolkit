using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;

namespace TextWHandle
{
    class Program
    {
        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);

        static void Main(string[] args)
        {
            int handle = int.Parse(Console.ReadLine(), NumberStyles.HexNumber);

            // This is a bit tricky. To retrieve the text from a window, we have to know it's length beforehand.
            // This is because we have to send a StringBuilder of the correct length as a parameter. If it's too
            // small, it won't be able to contain the full text. If it's too large, it's inefficient. When using
            // the SendMessage function with the WM_GETTEXTLENGTH message, it returns the length of the
            // window text.
            int txtLength = SendMessage(handle, WM_GETTEXTLENGTH, 0, 0);

            // After having retrieved the length of the string, we create a StringBuilder to hold it.
            StringBuilder sb = new StringBuilder(txtLength + 1);

            // Sending the message WM_GETTEXT to the window, passing int he length of the text (the capacity
            // of the StringBuilder) as well as a reference to the StringBuilder will result in the
            // StringBuilder being filled up with the windows text.
            SendMessage(handle, WM_GETTEXT, sb.Capacity, sb);

            // Finally we'll write out the window text by ToString()'ing the StringBuilder.
            Console.Write(sb.ToString());
            Console.Read();
        }
    }
}