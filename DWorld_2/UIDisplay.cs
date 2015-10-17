using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DWorld_2
{
    public class Tile_Image
    {
        public int tileSize = 15;

        public Bitmap defaultImage = new Bitmap(20, 20);
        public Bitmap currentImage = new Bitmap(20, 20);
        public string tileType;
        public Tile_Image(string type)
        {
            defaultImage = new Bitmap(tileSize, tileSize);
            Random rand = new Random();

            #region Wall
            if (type == "wall")
            {
                tileType = type;
                for (int i = 0; i < tileSize; i++)
                {
                    for (int j = 0; j < tileSize; j++)
                    {
                        defaultImage.SetPixel(i, j, Color.Black);
                    }
                }
            }
            #endregion

            #region Floor
            else if (type == "floor")
            {
                tileType = type;
                for (int i = 0; i < tileSize; i++)
                {
                    for (int j = 0; j < tileSize; j++)
                    {
                        defaultImage.SetPixel(i, j, Color.White);
                    }
                }
            }
            #endregion

            #region Not Currently Visible Wall
            else if (type == "wall_invis")
            {
                tileType = type;
                for (int i = 0; i < tileSize; i++)
                {
                    for (int j = 0; j < tileSize; j++)
                    {
                        defaultImage.SetPixel(i, j, Color.DimGray);
                    }
                }
            }
            #endregion

            #region Not Currently Visible Floor
            else if (type == "floor_invis")
            {
                tileType = type;
                for (int i = 0; i < tileSize; i++)
                {
                    for (int j = 0; j < tileSize; j++)
                    {
                        defaultImage.SetPixel(i, j, Color.LightGray);
                    }
                }
            }
            #endregion

            #region Generates static where party has not explored.
            else if (type == "unknown")
            {
                tileType = type;
                for (int i = 0; i < tileSize; i++)
                {
                    for (int j = 0; j < tileSize; j++)
                    {
                        if (rand.Next(0,2) == 0) { defaultImage.SetPixel(i, j, Color.Black); }
                        else { defaultImage.SetPixel(i, j, Color.White); }                        
                    }
                }
            }
            #endregion

            #region Exception
            else { System.Windows.Forms.MessageBox.Show("Error"); }
            #endregion

            //I think it might be a better idea to have a deep copy here, so that the tile doesn't have to be rebuilt every time the party moves.
            currentImage = defaultImage;
        }

        //Adds object on top of existing tile background.
        public Bitmap AddObject(string type)
        {
            currentImage = new Bitmap(tileSize, tileSize);
            for (int i = 0; i < tileSize; i++)
            {
                for (int j = 0; j < tileSize; j++)
                {
                    currentImage.SetPixel(i, j, defaultImage.GetPixel(i, j));
                    if (type == "party")
                    {
                        if (Math.Pow(i - 7.5, 2) + Math.Pow(j - 7.5, 2) < 36) { currentImage.SetPixel(i, j, Color.Blue); }
                    }
                    if (type == "debug")
                    {
                        if (Math.Pow(i - 7.5, 2) + Math.Pow(j - 7.5, 2) < 36) { currentImage.SetPixel(i, j, Color.Red); }
                    }
                }
            }
            return currentImage;
        }
    }

    //This is the map the DM sees to track everything that's happening across the board.
    public class DMMap
    {
        public List<List<Tile_Image>> tileImSet = new List<List<Tile_Image>>();
        public Bitmap visMap;
        public int tilesize = 15;

        public DMMap(Map map)
        {
            int xmax = map.tLList.Count;
            int ymax = map.tLList[0].Count;

            visMap = new Bitmap(xmax * tilesize, ymax * tilesize);

            #region Create Set of tiles
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    tileImSet.Add(new List<Tile_Image>());
                    if (map.tLList[i][j].isWall) { tileImSet[i].Add(new Tile_Image("wall")); }
                    else tileImSet[i].Add(new Tile_Image("floor"));
                }
            }
            foreach (Party p in GlobalVars.PartyList) { tileImSet[p.location.x][p.location.y].currentImage = tileImSet[p.location.x][p.location.y].AddObject("party"); }
            #endregion
            #region Create Image of map
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    for (int k = 0; k < tilesize; k++)
                    {
                        for (int l = 0; l < tilesize; l++)
                        {
                            visMap.SetPixel(i * tilesize + k, j * tilesize + l, tileImSet[i][j].currentImage.GetPixel(k, l));
                        }
                    }
                }
            }
            #endregion
        }

        //Basically the same. 
        public void RefreshMap(Map map)
        {
            #region Create set of tiles
            for (int i = 0; i < map.tLList.Count; i++)
            {
                for (int j = 0; j < map.tLList[0].Count; j++)
                {
                    if (map.tLList[i][j].isWall)
                    {
                        tileImSet[i][j] = new Tile_Image("wall");
                    }
                    else tileImSet[i][j] = new Tile_Image("floor");
                }
            }
            #endregion

            #region Draw Tiles
            foreach (Party p in GlobalVars.PartyList) { tileImSet[p.location.x][p.location.y].currentImage = tileImSet[p.location.x][p.location.y].AddObject("party"); }
            for (int i = 0; i < map.tLList.Count; i++)
            {
                for (int j = 0; j < map.tLList[0].Count; j++)
                {
                    for (int k = 0; k < tilesize; k++)
                    {
                        for (int l = 0; l < tilesize; l++)
                        {
                            visMap.SetPixel(i * tilesize + k, j * tilesize + l, tileImSet[i][j].currentImage.GetPixel(k, l));
                        }
                    }


                }
            }
            #endregion
        }
    }

    //Pretty self - explanatory.  
    public class PlayerViewMap
    {
        public List<List<Tile_Image>> tileImSet = new List<List<Tile_Image>>();
        //public List<List<Tile>> tLList = new List<List<Tile>>();
        public Bitmap playerMap = new Bitmap(10, 10);
        public int tilesize = 15;

        public PlayerViewMap() { }

        public PlayerViewMap(Map map, List<Party> parties)
        {
            int xmax = map.tLList.Count;
            int ymax = map.tLList[0].Count;
            playerMap = new Bitmap(xmax * tilesize, ymax * tilesize);            

            CheckVisibleTiles(map, parties);

            #region Create set of tiles
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    tileImSet.Add(new List<Tile_Image>());
                    if (map.tLList[i][j].isWall && map.tLList[i][j].isVisible) { tileImSet[i].Add(new Tile_Image("wall")); }
                    else if (map.tLList[i][j].isWall && !map.tLList[i][j].isVisible && map.tLList[i][j].isViewed) { tileImSet[i].Add(new Tile_Image("wall_invis")); }
                    else if (!map.tLList[i][j].isWall && map.tLList[i][j].isVisible) { tileImSet[i].Add(new Tile_Image("floor")); }
                    else if (!map.tLList[i][j].isWall && !map.tLList[i][j].isVisible && map.tLList[i][j].isViewed) { tileImSet[i].Add(new Tile_Image("floor_invis")); }
                    else { tileImSet[i].Add(new Tile_Image("unknown")); }
                }
            }
            foreach (Party p in GlobalVars.PartyList) { tileImSet[p.location.x][p.location.y].currentImage = tileImSet[p.location.x][p.location.y].AddObject("party"); }
            #endregion

            #region Create image
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    for (int k = 0; k < tilesize; k++)
                    {
                        for (int l = 0; l < tilesize; l++)
                        {
                            playerMap.SetPixel(i * tilesize + k, j * tilesize + l, tileImSet[i][j].currentImage.GetPixel(k, l));
                        }
                    }
                }
            }
            #endregion

        }

        public void RefreshMap(Map map)
        {
            int xmax = map.tLList.Count;
            int ymax = map.tLList[0].Count;
            playerMap = new Bitmap(xmax * tilesize, ymax * tilesize);

            CheckVisibleTiles(GlobalVars.GMap, GlobalVars.PartyList);
            tileImSet.Clear();

            #region Create set of tiles
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    tileImSet.Add(new List<Tile_Image>());
                    if (map.tLList[i][j].isWall && map.tLList[i][j].isVisible) { tileImSet[i].Add(new Tile_Image("wall")); }
                    else if (map.tLList[i][j].isWall && !map.tLList[i][j].isVisible && map.tLList[i][j].isViewed) { tileImSet[i].Add(new Tile_Image("wall_invis")); }
                    else if (!map.tLList[i][j].isWall && map.tLList[i][j].isVisible) { tileImSet[i].Add(new Tile_Image("floor")); }
                    else if (!map.tLList[i][j].isWall && !map.tLList[i][j].isVisible && map.tLList[i][j].isViewed) { tileImSet[i].Add(new Tile_Image("floor_invis")); }
                    else { tileImSet[i].Add(new Tile_Image("unknown")); }
                }
            }
            foreach (Party p in GlobalVars.PartyList) { tileImSet[p.location.x][p.location.y].currentImage = tileImSet[p.location.x][p.location.y].AddObject("party"); }
            //tileImSet[1][1].currentImage = tileImSet[1][1].AddObject("debug");
            #endregion

            #region Create image
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    for (int k = 0; k < tilesize; k++)
                    {
                        for (int l = 0; l < tilesize; l++)
                        {
                            playerMap.SetPixel(i * tilesize + k, j * tilesize + l, tileImSet[i][j].currentImage.GetPixel(k, l));
                        }
                    }
                }
            }
            #endregion

        }

        //Checks what tiles the party can see, and tracks which ones have been seen.
        public void CheckVisibleTiles(Map map, List<Party> parties)
        {
            double incR = .2;
            double incTh = .05;

            //Mark all tiles as not visible
            foreach(List<Tile> tList in map.tLList)
            {
                foreach (Tile t in tList) { t.isVisible = false; }
            }

            //Find all currently visible tiles and record that they have been viewed.
            foreach(Party p in parties)
            {
                
                double th = 0;
                for (int i = 0; i < 720; i++)
                {
                    double r = 0;
                    for (int j = 0; j < map.tLList.Count * 5; j++)
                    {
                        int viewX = Convert.ToInt16(Math.Round(Convert.ToDouble(p.location.x) + r * Math.Cos(th)));
                        int viewY = Convert.ToInt16(Math.Round(Convert.ToDouble(p.location.y) + r * Math.Sin(th)));
                        map.tLList[viewX][viewY].isVisible = true;
                        map.tLList[viewX][viewY].isViewed = true;
                        if (map.tLList[viewX][viewY].isWall){ break; }
                        r += incR;
                    }
                    th += incTh;
                }
            }
            int debugstep = 0;
        }
    }
}
