using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Battleship.Model
{
    public abstract class Player
    {
        public List<Ship> Fleet { get; private set; }
        public List<Coordinate> ShotsFired { get; private set; }

        public bool IsAttacking = false;

        public abstract Coordinate Attack();

        public bool Defend(Coordinate shot)
        {
            foreach (Ship ship in Fleet)
            {
                bool result = ship.CheckForHit(shot);
                if (result)
                {
                    Console.WriteLine("That's a HIT");

                    //show sunken ships
                    List<string> sunkShips = Fleet.Where(s => s.IsSunk).Select(s => s.ToString()).ToList();
                    if (sunkShips.Count() > 0)
                    {
                        Console.WriteLine($"Sunken ships of {this.ToString()}: {String.Join(" ", sunkShips.ToArray())}");
                    }

                    return true;
                }
            }
            Console.WriteLine("That's a MISS");
            return false;
        }

        protected Player()
        {
            Fleet = new List<Ship>();
            ShotsFired = new List<Coordinate>();

            //randomly position two destroyers and one battleship
            Ship ship;

            ship = new Destroyer();
            Ship.PositionRandomly(ship, Fleet);

            ship = new Destroyer();
            Ship.PositionRandomly(ship, Fleet);

            ship = new Battleship();
            Ship.PositionRandomly(ship, Fleet);

        }

    }

    public class Human : Player
    {
        public override Coordinate Attack()
        {
            Console.WriteLine();
            Console.Write("Where do you want to shoot (e.g. A5): ");

            string input = Console.ReadLine();
            while (!Regex.IsMatch(input, "^[A-J](10|[1-9])$"))
            {
                Console.WriteLine("That's not a valid coordinate. Where do you want to shoot (e.g. A5) :");
                input = Console.ReadLine();
            }

            return new Coordinate(input);
        }

        public override string ToString()
        {
            return "HUMAN player";
        }

    }

    public class Computer : Player
    {

        public override Coordinate Attack()
        {

            Console.WriteLine();
            Console.Write("The Computer will attack...");

            System.Threading.Thread.Sleep(1000);

            //TODO: The computer should not fire randomly after a hit. it should try the squares next to the hit, until it shinks a ship.

            while (true)
            {
                Coordinate next = new Coordinate() { x = Game.Rnd.Next(0, 10) + 1, y = Game.Rnd.Next(0, 10) + 1 };
                if (ShotsFired.Exists(c => c.Equals(next)))
                    continue;
                else
                {
                    Console.WriteLine(next.ToString());
                    return next;
                }
            }
        }

        public override string ToString()
        {
            return "COMPUTER player";
        }


    }
}
