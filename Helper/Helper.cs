using System;
using System.IO;

namespace Helper
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

            return $"{value:0.##} {sizes[order]}";
        }
        /// <summary>
        /// …s…                         …s… 	                    …s…
        /// …file… 	                    …file… 	                    …file…
        /// …file.jpg 	                …file.jpg 	                …file.jpg
        /// …\file.jpg 	                …/file.jpg 	                …\file.jpg
        /// C…\file.jpg 	            /…/file.jpg 	            \…\file.jpg
        /// C:…\file.jpg 	            /U…/file.jpg 	            \\…\file.jpg
        /// C:\Users\Test\…\file.jpg 	/Users/Test/Pi…/file.jpg 	\\pc\Themes\Mi…\file.jpg
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

            int totalLength = fileName.Length + delimiter.Length;

            var slash = (path.IndexOf("/", StringComparison.Ordinal) > -1 ? "/" : "\\");

            //less than the minimum amt
            if (limit < 2 * delimiter.Length + 1)
                return string.Empty;

            //full path
            if (limit >= path.Length)
            {
                return path;
            }

            //file name condensing
            if (limit < totalLength)
            {
                return delimiter + fileName.Substring(0, limit - 2 * delimiter.Length) + delimiter;
            }

            //whole name only, no folder structure shown
            if (limit == totalLength)
            {
                return delimiter + fileName;
            }

            return dir.Substring(0, limit - (totalLength + 1)) + delimiter + slash + fileName;
        }
    }
}
