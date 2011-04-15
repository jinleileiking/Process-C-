using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace process
{
    public partial class Form1 : Form
    {
        private ArrayList sProcIds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sProcIds = new ArrayList();

            Process[] sProcess = Process.GetProcesses();
            
            foreach(Process p in sProcess)
            {
                listBox1.Items.Add(p.ProcessName);
                sProcIds.Add(p.Id);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = this.listBox1.SelectedIndex;

            Process proc = Process.GetProcessById((int)sProcIds[idx]);
            ProcessThreadCollection procThreads = proc.Threads;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Items.Clear();

            int nRow = 0;
            foreach (ProcessThread pt in procThreads)
            {
                string priority = "Normal";
                switch((int)proc.BasePriority)
                {
                    case 8:
                        priority = "Normal";
                        break;

                    case 13:
                        priority = "High";
                        break;

                    case 24:
                        priority = "Real Time";
                        break;
                    case 4:
                    default:
                        priority = "Idle";
                        break;

                }
                this.listView1.Items.Add(pt.Id.ToString());
                this.listView1.Items[nRow].SubItems.Add(priority);
                this.listView1.Items[nRow].SubItems.Add(pt.UserProcessorTime.ToString());
                nRow++;
            }
        }
    }
}