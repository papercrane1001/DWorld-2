using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWorld_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            //Map map = new Map(20, 20, 10);
            //pictureBox1.Image = map.visMap.visMap;
            //pictureBox1.Height = map.visMap.visMap.Height;
            //pictureBox1.Width = map.visMap.visMap.Width;

            //int debugstep = 0;
        }

        private void gen_button_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Please select a map type."); }
            else
            {
                Map map = new Map(Convert.ToInt16(numericUpDown1.Value), Convert.ToInt16(numericUpDown2.Value), Convert.ToInt16(numericUpDown3.Value), comboBox1.SelectedItem.ToString());
                pictureBox1.Image = map.visMap.visMap;
                pictureBox1.Height = map.visMap.visMap.Height;
                pictureBox1.Width = map.visMap.visMap.Width;
            }
            
        }
    }
}
