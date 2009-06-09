using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataStructures;

namespace WordGenerator
{
    [Serializable]
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {


            runCicero();
        }

        [STAThread]
        public static void runCicero()
        {
            runCicero(null);
        }

        private RunLog runLog;
        public Program(RunLog runLog)
        {
            this.runLog = runLog;
        }

        public void runProgram()
        {
            runCicero(runLog);
        }

        [STAThread]
        public static void runCicero(RunLog runLog)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.EnableVisualStyles();
               
                try
                {

                    Application.SetCompatibleTextRenderingDefault(false);
                }
                catch (InvalidOperationException)
                {
                }

                if (runLog == null)
                {
                    Application.Run(new mainClientForm());
                }
                else
                {
                    Application.Run(new mainClientForm(runLog));
                }
            }
            catch (Exception e)
            {
                display_unhandled_exception(e);
            }
        }
        

        private static void display_unhandled_exception(Exception e)
        {
            if (e != null)
            {
                ExceptionViewerDialog dial = new ExceptionViewerDialog(e);
                dial.ShowDialog();
                System.Console.WriteLine("Caught an unhandled exception. " + e.Message + e.InnerException + e.Source + e.StackTrace);
            }
            else
            {
                MessageBox.Show("Caught an unhandled null exception.");
                System.Console.WriteLine("Caught an unhandled null exception.");
            }
            //throw e;

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            display_unhandled_exception(e.ExceptionObject as Exception);
        }
    }
}