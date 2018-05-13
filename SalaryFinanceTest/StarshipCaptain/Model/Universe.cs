using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipCaptain.Model
{

    public class Universe
    {
        /// <summary>
        /// The list of coordinates that make up the cosmos and have either planets or monsters. All other points in the 3D world defined by the X,Y,Z axes are considered to be empty
        /// </summary>
        public List<UniverseLocation> cosmos = new List<UniverseLocation>();

        public Universe(bool generateRandom)
        {
            if (generateRandom)
            {
                //generate 15000 coordinates
                while (cosmos.Count < 15000)
                {
                    //create a coordinate
                    UniverseLocation newLocation = new UniverseLocation();

                    //check if it already exists
                    if (cosmos.FirstOrDefault(l => l.X == newLocation.X && l.Y == newLocation.Y && l.Z == newLocation.Z) == null)
                    {
                        cosmos.Add(newLocation);
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cosmos.Count; i++)
            {
                sb.AppendFormat("{0}: {1}\r\n", i + 1, cosmos[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the Euclidean distance between two points in the 3D world
        /// </summary>
        public static long GetDistanceBetweenLocations(UniverseLocation L1,UniverseLocation L2) {

            long deltaX = L1.X.InternalValue - L2.X.InternalValue;
            long deltaY = L1.Y.InternalValue - L2.Y.InternalValue;
            long deltaZ = L1.Z.InternalValue - L2.Z.InternalValue;

            long distance = (long)Math.Round(Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ),0);

            return distance;

        }

    }

    public static class RandomNumberGenerator
    {
        public static Random Rnd = new Random(DateTime.Now.Millisecond);
    }
}
