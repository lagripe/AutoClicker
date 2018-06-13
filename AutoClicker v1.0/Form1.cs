using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Threading;

namespace AutoClicker_v1._0
{
    public partial class Form1 : Form
    {
        int Type=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((int)numericUpDown1.Value < 300)
            {
                MessageBox.Show("Minimum Value is 300");
                return;
            }
            Type = comboBox2.SelectedIndex;
            timer1.Interval = (int)numericUpDown1.Value;
            timer1.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy,
                       int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private void timer1_Tick(object sender, EventArgs e)
         {
            int cpt = 0;
            switch (Type)
            {

                case 0:
                    sendMouseUp();sendMouseDown();
                    
                    break;

                case 1:
                    sendMouseDoubleClick();
                    break;
                default:
                    break;
            }
            

        }
        void sendMouseRightclick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        void sendMouseDoubleClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0,0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP,0,0, 0, 0);
        }
       
        void sendMouseDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        void sendMouseUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        GlobalKeyboardHook gHook;
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            button2.Enabled = false;

            gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
                                              // Declare a KeyDown Event
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            // Add the keys you want to hook to the HookedKeys list
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
                
            gHook.hook();

        }
        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                if ((int)numericUpDown1.Value < 300)
                {
                    MessageBox.Show("Minimum Value is 300");
                    return;
                }
                Type = comboBox2.SelectedIndex;
                timer1.Interval = Type ==0 ? (int)numericUpDown1.Value : 200;
                timer1.Start();
                this.WindowState = FormWindowState.Minimized;
                button1.Enabled = false;
                button2.Enabled = true;
            }
            if (e.KeyCode == Keys.F3)
            {
                this.WindowState = FormWindowState.Normal;
                timer1.Stop();
                
                button2.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            gHook.unhook();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.WindowState = FormWindowState.Normal;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://fb.com/dznit0");

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/DzNiT0");
        }
    }
}
