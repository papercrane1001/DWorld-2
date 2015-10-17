using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DWorld_2
{
    public class UIDisplay
    {
    }

    public class Tile_Image
    {
        public int tileSize = 20;

        public Bitmap defaultImage = new Bitmap(20, 20);
        public string tileType;
        public Tile_Image(string type)
        {
            defaultImage = new Bitmap(tileSize, tileSize);

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
        }
    }

    public class VisibleMap
    {
        public List<List<Tile_Image>> tileImSet = new List<List<Tile_Image>>();
        public Bitmap visMap;
        public int tilesize = 20;

        public VisibleMap(Map map)
        {
            int xmax = map.tLList.Count;
            int ymax = map.tLList[0].Count;

            visMap = new Bitmap(xmax * tilesize, ymax * tilesize);

            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    tileImSet.Add(new List<Tile_Image>());
                    if (map.tLList[i][j].isWall) { tileImSet[i].Add(new Tile_Image("wall")); }
                    else tileImSet[i].Add(new Tile_Image("floor"));
                }
            }
            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    for (int k = 0; k < tilesize; k++)
                    {
                        for (int l = 0; l < tilesize; l++)
                        {
                            visMap.SetPixel(i * tilesize + k, j * tilesize + l, tileImSet[i][j].defaultImage.GetPixel(k, l));
                        }
                    }


                }
            }
        }

        public void RefreshMap(Map map)
        {
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
            for (int i = 0; i < map.tLList.Count; i++)
            {
                for (int j = 0; j < map.tLList[0].Count; j++)
                {
                    for (int k = 0; k < tilesize; k++)
                    {
                        for (int l = 0; l < tilesize; l++)
                        {
                            visMap.SetPixel(i * tilesize + k, j * tilesize + l, tileImSet[i][j].defaultImage.GetPixel(k, l));
                        }
                    }


                }
            }
        }
    }
}
