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
    public partial class Player_View : Form
    {
        public Player_View()
        {
            InitializeComponent();

            this.Size = new Size(GlobalVars.Playerviewmap.playerMap.Width + 50, GlobalVars.Playerviewmap.playerMap.Height + 50);
            picturePView.Image = GlobalVars.Playerviewmap.playerMap;
            picturePView.Size = GlobalVars.Playerviewmap.playerMap.Size;


            //int debugstep = 0;
        }

        public void RefreshImage()
        {
            picturePView.Image = GlobalVars.Playerviewmap.playerMap;
        }
    }
}
