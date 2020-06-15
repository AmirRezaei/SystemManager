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
        /// <summary>
        /// …s…                             …s… 	                        …s…
        /// …stripes… 	                    …stripes… 	                    …stripes…
        /// …stripes.jpg 	                …stripes.jpg 	                …stripes.jpg
        /// …\stripes.jpg 	                …/stripes.jpg 	                …\stripes.jpg
        /// C…\stripes.jpg 	                /…/stripes.jpg 	                \…\stripes.jpg
        /// C:…\stripes.jpg 	            /U…/stripes.jpg 	            \\…\stripes.jpg
        /// C:\Users\chadk\…\stripes.jpg 	/Users/chadk/Pi…/stripes.jpg 	\\ps1\Themes\Mi…\stripes.jpg
        /// </summary>
        /// <param name="path">The path to compress</param>
        /// <param name="limit">The maximum length</param>
        /// <param name="delimiter">The character(s) to use to imply incompleteness</param>
        /// <returns></returns>
        public static string ShrinkPath(this string path, int limit, string delimiter = "…")
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            string fileName = Path.GetFileName(path);
            string dir = path.Substring(0, path.Length - fileName.Length);

            int delimlen = delimiter.Length;
            int idealminlen = fileName.Length + delimlen;

            var slash = (path.IndexOf("/") > -1 ? "/" : "\\");

            //less than the minimum amt
            if (limit < ((2 * delimlen) + 1))
                return string.Empty;

            //fullpath
            if (limit >= path.Length)
            {
                return path;
            }

            //file name condensing
            if (limit < idealminlen)
            {
                return delimiter + fileName.Substring(0, (limit - (2 * delimlen))) + delimiter;
            }

            //whole name only, no folder structure shown
            if (limit == idealminlen)
            {
                return delimiter + fileName;
            }

            return dir.Substring(0, (limit - (idealminlen + 1))) + delimiter + slash + fileName;
        }
    }
}
