using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SystemManager
{
    public static class Helper
    {
        public static string ToHumanReadable(this long value)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (value >= 1024 && order < sizes.Length - 1)
            {
                order++;
                value /= 1024;
            }

            return String.Format("{0:0.##} {1}", value, sizes[order]);
        }
    }
}
