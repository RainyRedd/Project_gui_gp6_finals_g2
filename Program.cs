using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVI_Access_Lib;
using System.Text.Json;
using DVI_Gui;


namespace DVI_Gui
{
    // Statisk klasse "Program" indeholdende applikationens startpunkt
    static class Program
    {
        // Indgangsmetode til applikationen
        [STAThread]
        static void Main()
        {
            // Aktiverer visuelle stilarter for applikationen
            Application.EnableVisualStyles();

            // Aktiverer kompatibel tekstrendering
            Application.SetCompatibleTextRenderingDefault(false);

            // Starter applikationen og viser Vin_Gui-formen
            Application.Run(new Vin_Gui());

            Console.WriteLine();

        }
    }
}