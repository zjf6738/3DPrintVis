//BIG5 TRANS ALLOWED
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Basic
{
    static class Program
    {
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BasicForm());
        }
    }
}