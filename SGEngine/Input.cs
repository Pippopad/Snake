using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGEngine
{
    public static class Input
    {
        private static Dictionary<char, bool> keys = new Dictionary<char, bool>();

        public static void UpdateKey(Keys key, bool val)
        {
            keys[(char)key] = val;
        }

        public static bool GetKey(char key)
        {
            if (!keys.ContainsKey(key)) return false;
            return keys[key];
        }
    }
}
