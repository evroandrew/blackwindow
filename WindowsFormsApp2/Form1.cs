using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MyForm : Form
    {
        bool enableClosing=false;
        MyLock @lock = new MyLock("123#");
        int time = 1; Process p;
        public MyForm()
        {
            @lock.Unlock += lock_Unlock;
            TopMost = true;
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }
        private void lock_Unlock()
        {
            enableClosing = true;
            Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (@lock.Check(b.Text[0]) == -1)
                Thread.Sleep(time*100);
            time++;
        }
        void Form1_Load(object sender, EventArgs e)
        {
            p = new Process();
            p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
            p.StartInfo.FileName = "taskmgr.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            this.Focus();
        }
        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (enableClosing) return;
            Process.GetProcessesByName("taskmgr")[0].Kill();
            e.Cancel = true;
        }
    }
}
