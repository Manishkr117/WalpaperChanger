using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace WalpaperChanger
{
    class Program
    {
        /// <summary>
        /// set the parameter of system
        /// </summary>
        /// <param name="uAction"></param>
        /// <param name="uParam"></param>
        /// <param name="lpvParam"></param>
        /// <param name="fuWinIni"></param>
        /// <example></example>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(UAction uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        int picIndex = 0;

        private static System.Timers.Timer aTimer;
        static void Main(string[] args)
        {
            Program p = new Program();
            GetBackgroud();
            p.SetTimer();
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string[] filenames = System.IO.Directory.GetFiles(@"C:\Users\maniskumar\Desktop\Image");
            List<string> list = new List<string>();
            list = filenames.ToList();
            if (picIndex < list.Count)
            {
                SetBackgroud(list[picIndex].ToString());
                picIndex += 1;
            }
            else
            {
                picIndex = 0;
                SetBackgroud(list[picIndex].ToString());
            }
            //SetBackgroud(@"C:\Users\v-wezan\Desktop\Qimage\2.png");
        }
        public enum UAction
        {
            /// <summary>
            /// set the desktop background image
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,
            /// <summary>
            /// set the desktop background image
            /// </summary>
            SPI_GETDESKWALLPAPER = 0x0073,
        }
        public static string GetBackgroud()
        {
            StringBuilder s = new StringBuilder(300);
            SystemParametersInfo(UAction.SPI_GETDESKWALLPAPER, 300, s, 0);
            return s.ToString();
        }
        /// <summary>
        /// set the desktop background image
        /// </summary>
        /// <param name="fileName">the path of file</param>
        /// <returns></returns>
        public static int SetBackgroud(string fileName)
        {
            int result = 0;
            if (File.Exists(fileName))
            {
                StringBuilder s = new StringBuilder(fileName);
                result = SystemParametersInfo(UAction.SPI_SETDESKWALLPAPER, 0, s, 0x2);
            }
            return result;
        }
    }
}
