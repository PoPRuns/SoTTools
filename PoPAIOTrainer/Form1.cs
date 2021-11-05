using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Memory;

namespace PoPAIOTrainer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public Mem m = new Mem();

		private void Form1_Load(object sender, EventArgs e)
		{
			if (!backgroundWorker1.IsBusy)
				backgroundWorker1.RunWorkerAsync();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			int pid = 0;
			while (true)
			{
				bool openproc = false;

				if (m.GetProcIdFromName("pop") > 0)
                {
					pid = m.GetProcIdFromName("pop");
					openproc = m.OpenProcess(pid);
					gameLabel.Invoke((MethodInvoker)delegate
					{
						gameLabel.Text = "YES";
					});
					pidLabel.Invoke((MethodInvoker)delegate
					{
						pidLabel.Text = pid.ToString();
					});

					comboBox1.Invoke((MethodInvoker)delegate
					{
						if (comboBox1.SelectedIndex == 1)
						{
							m.WriteMemory("base+0x006db8f4,0x0", "int", "0");   // Max Sands set to 0
							m.WriteMemory("base+0x006db8f4,0x4d4", "int", "1"); // Max HP set to 1
							m.WriteMemory("base+0x006db8a4,0x584", "int", "0"); // Max Moons set to 0
							if (m.ReadInt("base+006DB500,0x0") > 1)             // If HP > 1, it is capped to 1
							{
								m.WriteMemory("base+006DB500,0x0", "int", "1");
							}
						}

						if (comboBox1.SelectedIndex == 2)
						{
							m.WriteMemory("base+0x006db8f4,0x0", "int", "0");   // Max Sands set to 0
							m.WriteMemory("base+0x006db8f4,0x4d4", "int", "1"); // Max HP set to 1
							m.WriteMemory("base+0x006db8a4,0x584", "int", "0"); // Max Moons set to 0
							if (m.ReadInt("base+006DB500,0x0") > 1)             // If HP > 1, it is capped to 1
							{
								m.WriteMemory("base+006DB500,0x0", "int", "1");
							}
							m.WriteMemory("base+0x00699478,0x18,0x4,0x44,0x7C", "int", "0"); // Farah HP set to 0
						}


						if (comboBox1.SelectedIndex == 3)
						{
							m.WriteMemory("base+0x0040DE38,0x4,0x464,0x4,0x104,0x73C", "int", "3");   // Last Sword
							m.WriteMemory("base+0x006DB4DC,0x0", "int", "0");	// No Dagger
						}
					});
				}
				else
                {
					gameLabel.Invoke((MethodInvoker)delegate
					{
						gameLabel.Text = "NO";
					});
					pidLabel.Invoke((MethodInvoker)delegate
					{
						pidLabel.Text = "?";
					});
				}
			}
		}
    }
}
