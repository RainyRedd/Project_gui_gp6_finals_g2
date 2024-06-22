using System;  // Bruger systembiblioteker
using System.Collections.Generic;  // Bruger systemet til at administrere lister
using System.ComponentModel;  // Bruger systemet til at understøtte komponenter
using System.Data;  // Bruger systemet til at arbejde med data
using System.Drawing;  // Bruger systemet til at arbejde med grafik
using System.Linq;  // Bruger systemet til at understøtte LINQ-forespørgsler
using System.Text;  // Bruger systemet til at arbejde med tekststrengfunktioner
using System.Threading.Tasks;  // Bruger systemet til at arbejde med asynkrone opgaver
using System.Windows.Forms;  // Bruger systemet til at oprette Windows Forms-applikationer
using DVI_Access_Lib;  // Bruger DVI Access Library til at få adgang til specifik funktionalitet
using System.Text.Json;  // Bruger systembiblioteket til JSON-håndtering
using DVI_Gui;  // Bruger DVI Demotest-namespace til yderligere funktionalitet

namespace DVI_Gui
{
    // Delvis klasse "Vin_Gui", der udvider Windows Forms "Form"-klasse
    partial class Vin_Gui
    {
        private System.ComponentModel.IContainer components = null;  // Deklarerer en container til komponenter

        // Labels til at vise temperatur og luftfugtighed på brugergrænsefladen
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label humidityLabel;

        // Metode til at frigøre ressourcer
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();  // Frigiver komponenter, hvis de eksisterer
            }
            base.Dispose(disposing);  // Kalder basis metodens Dispose for yderligere oprydning
        }

        // Metode til at initialisere komponenter på formen
        private void InitializeComponent()
        {
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.humidityLabel = new System.Windows.Forms.Label();
            this.Line = new System.Windows.Forms.Label();
            this.Line2 = new System.Windows.Forms.Label();
            this.LagerTExt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.winesOverMaxListBox = new System.Windows.Forms.ListBox();
            this.winesUnderMinListBox = new System.Windows.Forms.ListBox();
            this.winesOnStockListBox = new System.Windows.Forms.ListBox();
            this.Vinovermaxtext = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.denmarkClockLabel = new System.Windows.Forms.Label();
            this.floridaClockLabel = new System.Windows.Forms.Label();
            this.japanClockLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TempUdLb = new System.Windows.Forms.Label();
            this.HumUdLb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.BackColor = System.Drawing.Color.Black;
            this.temperatureLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.temperatureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temperatureLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.temperatureLabel.Location = new System.Drawing.Point(12, 33);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(186, 28);
            this.temperatureLabel.TabIndex = 0;
            this.temperatureLabel.Text = "Temperature: N/A";
            // 
            // humidityLabel
            // 
            this.humidityLabel.AutoSize = true;
            this.humidityLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.humidityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humidityLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.humidityLabel.Location = new System.Drawing.Point(247, 33);
            this.humidityLabel.Name = "humidityLabel";
            this.humidityLabel.Size = new System.Drawing.Size(176, 28);
            this.humidityLabel.TabIndex = 1;
            this.humidityLabel.Text = "Luftfugtihed: N/A";
            this.humidityLabel.Click += new System.EventHandler(this.humidityLabel_Click);
            // 
            // Line
            // 
            this.Line.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Line.Location = new System.Drawing.Point(454, -3);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(1, 158);
            this.Line.TabIndex = 2;
            this.Line.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Line2
            // 
            this.Line2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Line2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Line2.Location = new System.Drawing.Point(0, 156);
            this.Line2.Name = "Line2";
            this.Line2.Size = new System.Drawing.Size(953, 1);
            this.Line2.TabIndex = 3;
            // 
            // LagerTExt
            // 
            this.LagerTExt.AutoSize = true;
            this.LagerTExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LagerTExt.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.LagerTExt.Location = new System.Drawing.Point(12, 4);
            this.LagerTExt.Name = "LagerTExt";
            this.LagerTExt.Size = new System.Drawing.Size(128, 26);
            this.LagerTExt.TabIndex = 4;
            this.LagerTExt.Text = "Lager Temp";
            this.LagerTExt.Click += new System.EventHandler(this.LagerTExt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(399, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lager status";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // winesOverMaxListBox
            // 
            this.winesOverMaxListBox.BackColor = System.Drawing.Color.Black;
            this.winesOverMaxListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winesOverMaxListBox.ForeColor = System.Drawing.Color.Yellow;
            this.winesOverMaxListBox.FormattingEnabled = true;
            this.winesOverMaxListBox.ItemHeight = 22;
            this.winesOverMaxListBox.Location = new System.Drawing.Point(12, 226);
            this.winesOverMaxListBox.Name = "winesOverMaxListBox";
            this.winesOverMaxListBox.Size = new System.Drawing.Size(305, 378);
            this.winesOverMaxListBox.TabIndex = 7;
            this.winesOverMaxListBox.SelectedIndexChanged += new System.EventHandler(this.winesOverMaxListBox_SelectedIndexChanged);
            // 
            // winesUnderMinListBox
            // 
            this.winesUnderMinListBox.BackColor = System.Drawing.Color.Black;
            this.winesUnderMinListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winesUnderMinListBox.ForeColor = System.Drawing.Color.Red;
            this.winesUnderMinListBox.FormattingEnabled = true;
            this.winesUnderMinListBox.ItemHeight = 22;
            this.winesUnderMinListBox.Location = new System.Drawing.Point(634, 226);
            this.winesUnderMinListBox.Name = "winesUnderMinListBox";
            this.winesUnderMinListBox.Size = new System.Drawing.Size(305, 378);
            this.winesUnderMinListBox.TabIndex = 8;
            this.winesUnderMinListBox.SelectedIndexChanged += new System.EventHandler(this.winesUnderMinListBox_SelectedIndexChanged);
            // 
            // winesOnStockListBox
            // 
            this.winesOnStockListBox.BackColor = System.Drawing.Color.Black;
            this.winesOnStockListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winesOnStockListBox.ForeColor = System.Drawing.Color.Green;
            this.winesOnStockListBox.FormattingEnabled = true;
            this.winesOnStockListBox.ItemHeight = 22;
            this.winesOnStockListBox.Location = new System.Drawing.Point(323, 226);
            this.winesOnStockListBox.Name = "winesOnStockListBox";
            this.winesOnStockListBox.Size = new System.Drawing.Size(305, 378);
            this.winesOnStockListBox.TabIndex = 9;
            // 
            // Vinovermaxtext
            // 
            this.Vinovermaxtext.AutoSize = true;
            this.Vinovermaxtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Vinovermaxtext.Location = new System.Drawing.Point(91, 197);
            this.Vinovermaxtext.Name = "Vinovermaxtext";
            this.Vinovermaxtext.Size = new System.Drawing.Size(107, 26);
            this.Vinovermaxtext.TabIndex = 10;
            this.Vinovermaxtext.Text = "Over max";
            this.Vinovermaxtext.Click += new System.EventHandler(this.label1_Click_3);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(421, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "På Lager";
            this.label1.Click += new System.EventHandler(this.label1_Click_4);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(741, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 26);
            this.label3.TabIndex = 12;
            this.label3.Text = "Under min";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(470, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(470, 28);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // denmarkClockLabel
            // 
            this.denmarkClockLabel.AutoSize = true;
            this.denmarkClockLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.denmarkClockLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.denmarkClockLabel.Location = new System.Drawing.Point(470, 35);
            this.denmarkClockLabel.Name = "denmarkClockLabel";
            this.denmarkClockLabel.Size = new System.Drawing.Size(155, 26);
            this.denmarkClockLabel.TabIndex = 14;
            this.denmarkClockLabel.Text = "Denmark Time";
            this.denmarkClockLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // floridaClockLabel
            // 
            this.floridaClockLabel.AutoSize = true;
            this.floridaClockLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floridaClockLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.floridaClockLabel.Location = new System.Drawing.Point(470, 71);
            this.floridaClockLabel.Name = "floridaClockLabel";
            this.floridaClockLabel.Size = new System.Drawing.Size(130, 26);
            this.floridaClockLabel.TabIndex = 15;
            this.floridaClockLabel.Text = "Usa Fl TIme";
            this.floridaClockLabel.Click += new System.EventHandler(this.floridaClockLabel_Click);
            // 
            // japanClockLabel
            // 
            this.japanClockLabel.AutoSize = true;
            this.japanClockLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.japanClockLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.japanClockLabel.Location = new System.Drawing.Point(475, 106);
            this.japanClockLabel.Name = "japanClockLabel";
            this.japanClockLabel.Size = new System.Drawing.Size(125, 26);
            this.japanClockLabel.TabIndex = 16;
            this.japanClockLabel.Text = "Japan Time";
            this.japanClockLabel.Click += new System.EventHandler(this.label6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(12, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 26);
            this.label4.TabIndex = 17;
            this.label4.Text = "Udenfor Temp";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // TempUdLb
            // 
            this.TempUdLb.AutoSize = true;
            this.TempUdLb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TempUdLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TempUdLb.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TempUdLb.Location = new System.Drawing.Point(12, 116);
            this.TempUdLb.Name = "TempUdLb";
            this.TempUdLb.Size = new System.Drawing.Size(97, 28);
            this.TempUdLb.TabIndex = 18;
            this.TempUdLb.Text = "TempUd";
            // 
            // HumUdLb
            // 
            this.HumUdLb.AutoSize = true;
            this.HumUdLb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HumUdLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HumUdLb.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.HumUdLb.Location = new System.Drawing.Point(247, 116);
            this.HumUdLb.Name = "HumUdLb";
            this.HumUdLb.Size = new System.Drawing.Size(89, 28);
            this.HumUdLb.TabIndex = 19;
            this.HumUdLb.Text = "HumUd";
            // 
            // Vin_Gui
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(952, 619);
            this.Controls.Add(this.HumUdLb);
            this.Controls.Add(this.TempUdLb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Vinovermaxtext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LagerTExt);
            this.Controls.Add(this.Line2);
            this.Controls.Add(this.Line);
            this.Controls.Add(this.humidityLabel);
            this.Controls.Add(this.temperatureLabel);
            this.Controls.Add(this.winesOverMaxListBox);
            this.Controls.Add(this.winesUnderMinListBox);
            this.Controls.Add(this.winesOnStockListBox);
            this.Controls.Add(this.denmarkClockLabel);
            this.Controls.Add(this.floridaClockLabel);
            this.Controls.Add(this.japanClockLabel);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Vin_Gui";
            this.ShowIcon = false;
            this.Text = "DVI Monitor";
            this.Load += new System.EventHandler(this.Vin_Gui_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label Line;
        private Label Line2;
        private Label LagerTExt;
        private Label label2;
        private ListBox winesOverMaxListBox;
        private ListBox winesUnderMinListBox;
        private ListBox winesOnStockListBox;
        private Label Vinovermaxtext;
        private Label label1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label denmarkClockLabel;
        private Label floridaClockLabel;
        private Label japanClockLabel;
        private Label label4;
        private Label TempUdLb;
        private Label HumUdLb;
    }
}