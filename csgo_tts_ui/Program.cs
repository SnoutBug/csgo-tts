﻿using System;
using System.Windows.Forms;

namespace csgo_tts_ui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1());
            }
            catch (ObjectDisposedException e)
            {
                Application.Exit();
            }
        }
    }
}
