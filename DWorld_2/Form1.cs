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
        }

        //Used to interpret the key pressed into a vector.
        internal int[] KeyPress(string d)
        {
            int[,] choices = { { -1, -1 }, { 0, -1 }, { 1, -1 }, { -1, 0 }, { 0, 0 }, { 1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 } };
            int[] tOut = {0,0 }; 
            int dInt = Convert.ToInt16(d);
            tOut[0] = choices[dInt - 1, 0];
            tOut[1] = choices[dInt - 1, 1];

            return tOut;
        }
        
        //Generates map.  Currently deletes the old one.  Also currently creates an empty party.
        private void gen_button_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Please select a map type."); }
            else
            {
                GlobalVars.PartyList.Clear();
                //Map map = new Map(Convert.ToInt16(numericUpDown1.Value), Convert.ToInt16(numericUpDown2.Value), Convert.ToInt16(numericUpDown3.Value), comboBox1.SelectedItem.ToString());
                GlobalVars.GMap = new Map(Convert.ToInt16(numericUpDown1.Value), Convert.ToInt16(numericUpDown2.Value), Convert.ToInt16(numericUpDown3.Value), comboBox1.SelectedItem.ToString());
                GlobalVars.PartyList.Add(new Party(GlobalVars.GMap, new List<PlayerChar>() { new PlayerChar() }));
                GlobalVars.GMap.visMap.RefreshMap(GlobalVars.GMap);
                pictureBox1.Image = GlobalVars.GMap.visMap.visMap;
                pictureBox1.Height = GlobalVars.GMap.visMap.visMap.Height;
                pictureBox1.Width = GlobalVars.GMap.visMap.visMap.Width;

                
                GlobalVars.Playerviewmap = new PlayerViewMap(GlobalVars.GMap, GlobalVars.PartyList);
                GlobalVars.PlayerViewTest = new Player_View();
                GlobalVars.PlayerViewTest.Show();

                label1.Text = "Party 1:";
            }
            
        }

        //Rendered Irrelevant
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { goto NoText; }
            int[] dir = new int[2];
            int[] noChange = { 0, 0 };
            try
            {
                dir = KeyPress(textBox1.Text);
            }
            catch { }
            textBox1.Text = "";
            //MessageBox.Show(dir[0].ToString() + " " + dir[1].ToString());

            if(dir == noChange){ goto NoText; }




        NoText:
            int wat = 0;
        }

        //To be implemented.
        private void partySplitButton_Click(object sender, EventArgs e)
        {

        }

        //Used to move the party.
        //TODO:  edit code so that only the dot changes on the DM's screen to reduce build time.
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { goto NoText; }
            int[] dir = new int[2];
            int[] noChange = { 0, 0 };
            try
            {
                dir = KeyPress(textBox1.Text);
            }
            catch { }
            textBox1.Text = "";
            if (dir == noChange) { goto NoText; }
            GlobalVars.PartyList[0].MoveParty(dir[0], dir[1]);
            GlobalVars.GMap.RefreshMaps();
            pictureBox1.Image = GlobalVars.GMap.visMap.visMap;
            GlobalVars.PlayerViewTest.RefreshImage();

        NoText:
            int wat = 0;
        }
    }

    public static class GlobalVars
    {
        public static Map gMap = new Map();
        public static Map GMap { get { return gMap; } set { gMap = value; } }
        public static List<Party> partyList = new List<Party>();
        public static List<Party> PartyList { get { return partyList; } set { partyList = value; } }
        public static PlayerViewMap playerviewmap = new PlayerViewMap();
        public static PlayerViewMap Playerviewmap { get { return playerviewmap; } set { playerviewmap = value; } }

        public static Player_View playerViewTest = new Player_View();
        public static Player_View PlayerViewTest { get { return playerViewTest; } set { playerViewTest = value; } }
    }
}
