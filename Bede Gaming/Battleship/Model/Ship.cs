using System.Collections.Generic;
using System.Linq;

namespace Battleship.Model
{
    public abstract class Ship
    {

        /// <summary>
        /// The coordinates of the ship
        /// </summary>
        public List<Coordinate> Coordinates;

        /// <summary>
        /// The coordinates of the ship that have been struck by the opponent
        /// </summary>
        private List<Coordinate> Hits;

        public abstract int Length { get; }

        public bool IsSunk
        {
            get
            {
                return (Hits.Count == Coordinates.Count);
            }
        }

        public Ship()
        {
            Hits = new List<Coordinate>();
        }

        public bool CheckForHit(Coordinate shot)
        {
            if (Coordinates.Exists(c => c.Equals(shot)))
            {
                Hits.Add(shot);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Join(",", Coordinates);
        }

        public static void PositionRandomly(Ship ship, List<Ship> existingFleet)
        {

            while (true)
            {
                ship.Coordinates = new List<Coordinate>();

                if (Game.Rnd.Next(2) == 0)
                {
                    //horizontal
                    int x = Game.Rnd.Next(1, 10 - ship.Length+1);
                    int y = Game.Rnd.Next(1, 11);

                    bool overlap = false;
                    for (int i = x; i < x + ship.Length; i++)
                    {

                        var newCoordinate = new Coordinate() { x = i, y = y };
                        bool exists = existingFleet.Exists(s => s.Coordinates.Any(c => c.Equals(newCoordinate)));
                        if (!exists)
                            ship.Coordinates.Add(newCoordinate);
                        else
                        {
                            overlap = true;
                            break;
                        }
                    }
                    if (overlap) continue;

                }
                else
                {
                    //vertical
                    int y = Game.Rnd.Next(1, 10 - ship.Length+1);
                    int x = Game.Rnd.Next(1, 11);

                    bool overlap = false;
                    for (int i = y; i < y + ship.Length; i++)
                    {
                        var newCoordinate = new Coordinate() { x = x, y = i };
                        bool exists = existingFleet.Exists(s => s.Coordinates.Any(c => c.Equals(newCoordinate)));
                        if (!exists)
                            ship.Coordinates.Add(newCoordinate);
                        else
                        {
                            overlap = true;
                            break;
                        }
                    }
                    if (overlap) continue;

                }

                break;

            }

            existingFleet.Add(ship);
        }


    }

    public class Destroyer : Ship
    {
        public override int Length
        {
            get { return 4; }

        }

        public override string ToString()
        {
            return "Destroyer " + base.ToString();
        }

    }

    public class Battleship : Ship
    {
        public override int Length
        {
            get { return 5; }

        }

        public override string ToString()
        {
            return "Destroyer " + base.ToString();
        }

    }

}
