using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWorld_2
{
    public class Party
    {
        public Coordinates location = new Coordinates();
        public decimal viewRange = 50;
        public List<PlayerChar> PartyMembers = new List<PlayerChar>();
        internal Random pRand = new Random();

        //For when the location of the party has already been determined
        public Party(Coordinates start, List<PlayerChar> chars)
        {
            location = new Coordinates(start.str_xy);
            foreach(PlayerChar p in chars) { PartyMembers.Add(p); }
        }

        //For party initialization at random location
        public Party(Map map, List<PlayerChar> chars)
        {
            foreach (PlayerChar p in chars) { PartyMembers.Add(p); }
            location = new Coordinates(map.openSpace[pRand.Next(0, map.openSpace.Count)].coord.str_xy);
        }

        //To be implemented later.
        public List<Party> PartySplit(List<int> indexListFirstParty)
        {
            List<PlayerChar> party1Chars = new List<PlayerChar>();
            foreach (int i in indexListFirstParty) { party1Chars.Add(this.PartyMembers[i]); }
            Party party1 = new Party(this.location, party1Chars);
            List<PlayerChar> party2Chars = new List<PlayerChar>();
            for (int i = 0; i < PartyMembers.Count; i++)
            {
                if (!indexListFirstParty.Contains(i)) { party2Chars.Add(this.PartyMembers[i]); }
            }
            Party party2 = new Party(this.location, party2Chars);
            List<Party> returnList = new List<Party>() { party1, party2 };
            return returnList;
        }


        public void MoveParty(int dx,int dy)
        {
            Coordinates testLocation = new Coordinates(location.str_xy);
            testLocation.Delta(dx, dy);
            if (!GlobalVars.GMap.tLList[testLocation.x][testLocation.y].isWall) { location.Delta(dx, dy); }
            location.Delta(dx, dy);
        }
    }

    public class PlayerChar
    {

        public PlayerChar() { }
    }

    public class NPC { }
}
