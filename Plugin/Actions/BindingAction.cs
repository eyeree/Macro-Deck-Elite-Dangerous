using EliteDangerousMacroDeckPlugin.Actions.Bindings;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace EliteDangerousMacroDeckPlugin.Actions
{
    public abstract class BindingAction : PluginAction
    {

        public override string Name => _name;
        public override string Description => _description;

        private readonly string _name;
        private readonly string _description;

        private readonly InputSimulator _inputSimulator;

        protected BindingAction(string name, string description)
        {
            _name = name;
            _description = description;
            _inputSimulator = new InputSimulator();
        }

        protected void SendKeyStrokes(BindingInfo keyboardBinding)
        {

            foreach (var modifier in keyboardBinding.Modifier)
            {
                if (modifier.Device == "Keyboard")
                {
                    SendKeyDown(modifier.Key);
                }
            }

            SendKeyDown(keyboardBinding.Key);
            Thread.Sleep(keyboardBinding.Hold != null ? 500 : 50);
            SendKeyUp(keyboardBinding.Key);

            foreach (var modifier in keyboardBinding.Modifier)
            {
                if (modifier.Device == "Keyboard")
                {
                    SendKeyUp(modifier.Key);
                }
            }

        }

        private void SendKeyDown(string key)
        {
            var keyCode = GetKeyCode(key);
            if (keyCode != null)
            {
                _inputSimulator.Keyboard.KeyDown((VirtualKeyCode)keyCode);
            }
        }

        private void SendKeyUp(string key)
        {
            var keyCode = GetKeyCode(key);
            if(keyCode != null)
            {
                _inputSimulator.Keyboard.KeyUp((VirtualKeyCode)keyCode);
            }
        }

        private VirtualKeyCode? GetKeyCode(string key)
        {
            if(!_keyToVirtualKeyCodeMap.TryGetValue(key, out VirtualKeyCode keyCode))
            {
                Trace.TraceWarning($"No mapping for key \"{key}\" was found.");
                return null;
            } 
            else
            {
                Trace.TraceInformation($"Using {keyCode} for \"{key}\".");
                return keyCode;
            }
        }

        protected static bool getBool(string variableName)
        {
            return VariableCache.GetBool(variableName);
        }

        private static Dictionary<string, VirtualKeyCode> _keyToVirtualKeyCodeMap = new Dictionary<string, VirtualKeyCode>
        {
            // { "", VirtualKeyCode.LBUTTON }, // Left mouse button
            // { "", VirtualKeyCode.RBUTTON }, // Right mouse button
            { "Key_Pause", VirtualKeyCode.CANCEL }, // Control-break processing
            // { "", VirtualKeyCode.MBUTTON }, // Middle mouse button (three-button mouse) - NOT contiguous with LBUTTON and RBUTTON
            // { "", VirtualKeyCode.XBUTTON1 }, // Windows 2000/XP: X1 mouse button - NOT contiguous with LBUTTON and RBUTTON
            // { "", VirtualKeyCode.XBUTTON2 }, // Windows 2000/XP: X2 mouse button - NOT contiguous with LBUTTON and RBUTTON
            { "Key_Backspace", VirtualKeyCode.BACK }, // BACKSPACE key
            { "Key_Tab", VirtualKeyCode.TAB }, // TAB key
            // { "", VirtualKeyCode.CLEAR }, // CLEAR key
            { "Key_Enter", VirtualKeyCode.RETURN }, // ENTER key
            { "Key_Numpad_Enter", VirtualKeyCode.NUMPAD_RETURN }, // supported by the GragsStack.InputSimulatorStandard, triggers Elite once, but not repeat.
            // { "", VirtualKeyCode.SHIFT }, // SHIFT key
            // { "", VirtualKeyCode.CONTROL }, // CTRL key
            // { "", VirtualKeyCode.MENU }, // ALT key
            // { "", VirtualKeyCode.PAUSE }, // PAUSE key
            { "Key_CapsLock", VirtualKeyCode.CAPITAL }, // CAPS LOCK key
            // { "", VirtualKeyCode.KANA }, // Input Method Editor (IME) Kana mode
            // { "", VirtualKeyCode.HANGEUL }, // IME Hanguel mode (maintained for compatibility; use HANGUL)
            // { "", VirtualKeyCode.HANGUL }, // IME Hangul mode
            // { "", VirtualKeyCode.JUNJA }, // IME Junja mode
            // { "", VirtualKeyCode.FINAL }, // IME final mode
            // { "", VirtualKeyCode.HANJA }, // IME Hanja mode
            // { "", VirtualKeyCode.KANJI }, // IME Kanji mode
            // { "", VirtualKeyCode.ESCAPE }, // ESC key
            // { "", VirtualKeyCode.CONVERT }, // IME convert
            // { "", VirtualKeyCode.NONCONVERT }, // IME nonconvert
            // { "", VirtualKeyCode.ACCEPT }, // IME accept
            // { "", VirtualKeyCode.MODECHANGE }, // IME mode change request
            { "Key_Space", VirtualKeyCode.SPACE }, // SPACEBAR
            { "Key_PageUp", VirtualKeyCode.PRIOR }, // PAGE UP key
            { "Key_PageDown", VirtualKeyCode.NEXT }, // PAGE DOWN key
            { "Key_End", VirtualKeyCode.END }, // END key
            { "Key_Home", VirtualKeyCode.HOME }, // HOME key
            { "Key_LeftArrow", VirtualKeyCode.LEFT }, // LEFT ARROW key
            { "Key_UpArrow", VirtualKeyCode.UP }, // UP ARROW key
            { "Key_RightArrow", VirtualKeyCode.RIGHT }, // RIGHT ARROW key
            { "Key_DownArrow", VirtualKeyCode.DOWN }, // DOWN ARROW key
            // { "", VirtualKeyCode.SELECT }, // SELECT key
            // { "", VirtualKeyCode.PRINT }, // PRINT key
            // { "", VirtualKeyCode.EXECUTE }, // EXECUTE key
            // { "", VirtualKeyCode.SNAPSHOT }, // PRINT SCREEN key
            { "Key_Insert", VirtualKeyCode.INSERT }, // INS key
            { "Key_Delete", VirtualKeyCode.DELETE }, // DEL key
            // { "", VirtualKeyCode.HELP }, // HELP key
            { "Key_0", VirtualKeyCode.VK_0 }, // 0 key
            { "Key_1", VirtualKeyCode.VK_1 }, // 1 key
            { "Key_2", VirtualKeyCode.VK_2 }, // 2 key
            { "Key_3", VirtualKeyCode.VK_3 }, // 3 key
            { "Key_4", VirtualKeyCode.VK_4 }, // 4 key
            { "Key_5", VirtualKeyCode.VK_5 }, // 5 key
            { "Key_6", VirtualKeyCode.VK_6 }, // 6 key
            { "Key_7", VirtualKeyCode.VK_7 }, // 7 key
            { "Key_8", VirtualKeyCode.VK_8 }, // 8 key
            { "Key_9", VirtualKeyCode.VK_9 }, // 9 key
            { "Key_A", VirtualKeyCode.VK_A }, // A key
            { "Key_B", VirtualKeyCode.VK_B }, // B key
            { "Key_C", VirtualKeyCode.VK_C }, // C key
            { "Key_D", VirtualKeyCode.VK_D }, // D key
            { "Key_E", VirtualKeyCode.VK_E }, // E key
            { "Key_F", VirtualKeyCode.VK_F }, // F key
            { "Key_G", VirtualKeyCode.VK_G }, // G key
            { "Key_H", VirtualKeyCode.VK_H }, // H key
            { "Key_I", VirtualKeyCode.VK_I }, // I key
            { "Key_J", VirtualKeyCode.VK_J }, // J key
            { "Key_K", VirtualKeyCode.VK_K }, // K key
            { "Key_L", VirtualKeyCode.VK_L }, // L key
            { "Key_M", VirtualKeyCode.VK_M }, // M key
            { "Key_N", VirtualKeyCode.VK_N }, // N key
            { "Key_O", VirtualKeyCode.VK_O }, // O key
            { "Key_P", VirtualKeyCode.VK_P }, // P key
            { "Key_Q", VirtualKeyCode.VK_Q }, // Q key
            { "Key_R", VirtualKeyCode.VK_R }, // R key
            { "Key_S", VirtualKeyCode.VK_S }, // S key
            { "Key_T", VirtualKeyCode.VK_T }, // T key
            { "Key_U", VirtualKeyCode.VK_U }, // U key
            { "Key_V", VirtualKeyCode.VK_V }, // V key
            { "Key_W", VirtualKeyCode.VK_W }, // W key
            { "Key_X", VirtualKeyCode.VK_X }, // X key
            { "Key_Y", VirtualKeyCode.VK_Y }, // Y key
            { "Key_Z", VirtualKeyCode.VK_Z }, // Z key
            // { "", VirtualKeyCode.LWIN }, // Left Windows key (Microsoft Natural keyboard)
            // { "", VirtualKeyCode.RWIN }, // Right Windows key (Natural keyboard)
            { "Key_Apps", VirtualKeyCode.APPS }, // Applications key (Natural keyboard)
            // { "", VirtualKeyCode.SLEEP }, // Computer Sleep key
            { "Key_Numpad_0", VirtualKeyCode.NUMPAD0 }, // Numeric keypad 0 key
            { "Key_Numpad_1", VirtualKeyCode.NUMPAD1 }, // Numeric keypad 1 key
            { "Key_Numpad_2", VirtualKeyCode.NUMPAD2 }, // Numeric keypad 2 key
            { "Key_Numpad_3", VirtualKeyCode.NUMPAD3 }, // Numeric keypad 3 key
            { "Key_Numpad_4", VirtualKeyCode.NUMPAD4 }, // Numeric keypad 4 key
            { "Key_Numpad_5", VirtualKeyCode.NUMPAD5 }, // Numeric keypad 5 key
            { "Key_Numpad_6", VirtualKeyCode.NUMPAD6 }, // Numeric keypad 6 key
            { "Key_Numpad_7", VirtualKeyCode.NUMPAD7 }, // Numeric keypad 7 key
            { "Key_Numpad_8", VirtualKeyCode.NUMPAD8 }, // Numeric keypad 8 key
            { "Key_Numpad_9", VirtualKeyCode.NUMPAD9 }, // Numeric keypad 9 key
            { "Key_Numpad_Multiply", VirtualKeyCode.MULTIPLY }, // Multiply key
            { "Key_Numpad_Add", VirtualKeyCode.ADD }, // Add key
            // { "", VirtualKeyCode.SEPARATOR }, // Separator key
            { "Key_Numpad_Subtract", VirtualKeyCode.SUBTRACT }, // Subtract key
            { "Key_Numpad_Decimal", VirtualKeyCode.DECIMAL }, // Decimal key
            { "Key_Numpad_Divide", VirtualKeyCode.DIVIDE }, // Divide key
            { "Key_F1", VirtualKeyCode.F1 }, // F1 key
            { "Key_F2", VirtualKeyCode.F2 }, // F2 key
            { "Key_F3", VirtualKeyCode.F3 }, // F3 key
            { "Key_F4", VirtualKeyCode.F4 }, // F4 key
            { "Key_F5", VirtualKeyCode.F5 }, // F5 key
            { "Key_F6", VirtualKeyCode.F6 }, // F6 key
            { "Key_F7", VirtualKeyCode.F7 }, // F7 key
            { "Key_F8", VirtualKeyCode.F8 }, // F8 key
            { "Key_F9", VirtualKeyCode.F9 }, // F9 key
            { "Key_F10", VirtualKeyCode.F10 }, // F10 key
            { "Key_F11", VirtualKeyCode.F11 }, // F11 key
            { "Key_F12", VirtualKeyCode.F12 }, // F12 key
            { "Key_F13", VirtualKeyCode.F13 }, // F13 key
            { "Key_F14", VirtualKeyCode.F14 }, // F14 key
            { "Key_F15", VirtualKeyCode.F15 }, // F15 key
            { "Key_F16", VirtualKeyCode.F16 }, // F16 key
            { "Key_F17", VirtualKeyCode.F17 }, // F17 key
            { "Key_F18", VirtualKeyCode.F18 }, // F18 key
            { "Key_F19", VirtualKeyCode.F19 }, // F19 key
            { "Key_F20", VirtualKeyCode.F20 }, // F20 key
            { "Key_F21", VirtualKeyCode.F21 }, // F21 key
            { "Key_F22", VirtualKeyCode.F22 }, // F22 key
            { "Key_F23", VirtualKeyCode.F23 }, // F23 key
            { "Key_F24", VirtualKeyCode.F24 }, // F24 key
            { "Key_NumLock", VirtualKeyCode.NUMLOCK }, // NUM LOCK key
            { "Key_ScrollLock", VirtualKeyCode.SCROLL }, // SCROLL LOCK key
            { "Key_LeftShift", VirtualKeyCode.LSHIFT }, // Left SHIFT key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
            { "Key_RightShift", VirtualKeyCode.RSHIFT }, // Right SHIFT key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
            { "Key_LeftControl", VirtualKeyCode.LCONTROL }, // Left CONTROL key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
            { "Key_RightControl", VirtualKeyCode.RCONTROL }, // Right CONTROL key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
            { "Key_LeftAlt", VirtualKeyCode.LMENU }, // Left MENU key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
            { "Key_RightAlt", VirtualKeyCode.RMENU }, // Right MENU key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
            // { "", VirtualKeyCode.BROWSER_BACK }, // Windows 2000/XP: Browser Back key
            // { "", VirtualKeyCode.BROWSER_FORWARD }, // Windows 2000/XP: Browser Forward key
            // { "", VirtualKeyCode.BROWSER_REFRESH }, // Windows 2000/XP: Browser Refresh key
            // { "", VirtualKeyCode.BROWSER_STOP }, // Windows 2000/XP: Browser Stop key
            // { "", VirtualKeyCode.BROWSER_SEARCH }, // Windows 2000/XP: Browser Search key
            // { "", VirtualKeyCode.BROWSER_FAVORITES }, // Windows 2000/XP: Browser Favorites key
            // { "", VirtualKeyCode.BROWSER_HOME }, // Windows 2000/XP: Browser Start and Home key
            // { "", VirtualKeyCode.VOLUME_MUTE }, // Windows 2000/XP: Volume Mute key
            // { "", VirtualKeyCode.VOLUME_DOWN }, // Windows 2000/XP: Volume Down key
            // { "", VirtualKeyCode.VOLUME_UP }, // Windows 2000/XP: Volume Up key
            // { "", VirtualKeyCode.MEDIA_NEXT_TRACK }, // Windows 2000/XP: Next Track key
            // { "", VirtualKeyCode.MEDIA_PREV_TRACK }, // Windows 2000/XP: Previous Track key
            // { "", VirtualKeyCode.MEDIA_STOP }, // Windows 2000/XP: Stop Media key
            // { "", VirtualKeyCode.MEDIA_PLAY_PAUSE }, // Windows 2000/XP: Play/Pause Media key
            // { "", VirtualKeyCode.LAUNCH_MAIL }, // Windows 2000/XP: Start Mail key
            // { "", VirtualKeyCode.LAUNCH_MEDIA_SELECT }, // Windows 2000/XP: Select Media key
            // { "", VirtualKeyCode.LAUNCH_APP1 }, // Windows 2000/XP: Start Application 1 key
            // { "", VirtualKeyCode.LAUNCH_APP2 }, // Windows 2000/XP: Start Application 2 key
            { "Key_SemiColon", VirtualKeyCode.OEM_1 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the ';:' key
            { "Key_Equals", VirtualKeyCode.OEM_PLUS }, // Windows 2000/XP: For any country/region, the '+' key
            { "Key_Comma", VirtualKeyCode.OEM_COMMA }, // Windows 2000/XP: For any country/region, the ',' key
            { "Key_Minus", VirtualKeyCode.OEM_MINUS }, // Windows 2000/XP: For any country/region, the '-' key
            { "Key_Period", VirtualKeyCode.OEM_PERIOD }, // Windows 2000/XP: For any country/region, the '.' key
            { "Key_Slash", VirtualKeyCode.OEM_2 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '/?' key
            { "Key_Apostrophe", VirtualKeyCode.OEM_3 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '`~' key
            { "Key_LeftBracket", VirtualKeyCode.OEM_4 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '[{' key
            { "Key_Backslash", VirtualKeyCode.OEM_5 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the '\|' key
            { "Key_RightBracket", VirtualKeyCode.OEM_6 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the ']}' key
            // { "", VirtualKeyCode.OEM_7 }, // Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP: For the US standard keyboard, the 'single-quote/double-quote' key
            // { "", VirtualKeyCode.OEM_8 }, // Used for miscellaneous characters; it can vary by keyboard.
            // { "", VirtualKeyCode.OEM_102 }, // Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard
            // { "", VirtualKeyCode.PROCESSKEY }, // Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
            // { "", VirtualKeyCode.PACKET }, // Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. The PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
            // { "", VirtualKeyCode.ATTN }, // Attn key
            // { "", VirtualKeyCode.CRSEL }, // CrSel key
            // { "", VirtualKeyCode.EXSEL }, // ExSel key
            // { "", VirtualKeyCode.EREOF }, // Erase EOF key
            // { "", VirtualKeyCode.PLAY }, // Play key
            // { "", VirtualKeyCode.ZOOM }, // Zoom key
            // { "", VirtualKeyCode.NONAME }, // Reserved
            // { "", VirtualKeyCode.PA1 }, // PA1 key
            // { "", VirtualKeyCode.OEM_CLEAR } // Clear key
        };

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        private static extern bool GetGUIThreadInfo(uint idThread, out GUITHREADINFO lpgui);

        public struct GUITHREADINFO
        {
            public int cbSize;
            public int flags;
            public int hwndActive;
            public int hwndFocus;
            public int hwndCapture;
            public int hwndMenuOwner;
            public int hwndMoveSize;
            public int hwndCaret;
            public System.Drawing.Rectangle rcCaret;
        }

        private void SendNumpadEnter()
        {
            bool keyDown = true; // true = down, false = up
            const uint WM_KEYDOWN = 0x0100;
            const uint WM_KEYUP = 0x0101;
            const int VK_RETURN = 0x0D;

            IntPtr handle = IntPtr.Zero;
            // Obtain the handle of the foreground window (active window and focus window are only relative to our own thread!!)
            IntPtr foreGroundWindow = GetForegroundWindow();
            Trace.TraceInformation("===================================================");
            Trace.TraceInformation($"foreGroundWindow {foreGroundWindow}");
            // now get process id of foreground window
            uint processID;
            uint threadID = GetWindowThreadProcessId(foreGroundWindow, out processID);
            Trace.TraceInformation($"processID {processID} -- threadID {threadID}");
            if (processID != 0)
            {
                // now get element with (keyboard) focus from process
                GUITHREADINFO threadInfo = new GUITHREADINFO();
                threadInfo.cbSize = Marshal.SizeOf(threadInfo);
                var success = GetGUIThreadInfo(threadID, out threadInfo);
                Trace.TraceInformation($"GetGUIThreadInfo {success} - threadInfo.hwndFocus {threadInfo.hwndFocus}");
                handle = (IntPtr)threadInfo.hwndFocus;
            }

            int lParam = 1 << 24; // this specifies NumPad key (extended key)
            lParam |= (keyDown) ? 0 : (1 << 30 | 1 << 31); // mark key as pressed if we use keyup message

            Trace.TraceInformation($"handle {handle}");
            PostMessage(handle, (keyDown) ? WM_KEYDOWN : WM_KEYUP, (IntPtr)VK_RETURN, (IntPtr)lParam); // send enter
        }

    }


}


