using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemManager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SystemManager());
        }
    }

    //public sealed class Singleton
    //{
    //    private static readonly Singleton instance = new Singleton();

    //    // Explicit static constructor to tell C# compiler
    //    // not to mark type as beforefieldinit
    //    static Singleton()
    //    {
    //    }

    //    private Singleton()
    //    {
    //    }

    //    public static Singleton Instance
    //    {
    //        get
    //        {
    //            return instance;
    //        }
    //    }
    //}
}
