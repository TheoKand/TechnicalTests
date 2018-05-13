using Battleship.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship
{
    public class Game
    {

        public List<Player> players;

        public static Random Rnd = new Random(Environment.TickCount);

        public Game()
        {
            #region initialize game

            players = new List<Player>
            {
                new Human(),
                new Computer()
            };

            #region for debug purposes
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\nHuman Fleet:\t");
            foreach(Ship ship in players[0].Fleet)
            {
                sb.Append(ship.ToString() + "\t");
            }
            sb.Append("\r\nComputer Fleet:\t");
            foreach (Ship ship in players[1].Fleet)
            {
                sb.Append(ship.ToString() + "\t");
            }
            Console.WriteLine(sb.ToString() );
            #endregion

            #region randomly select first player to play
            int random = Rnd.Next(2);
            players[0].IsAttacking = random == 0 ? true : false;
            players[1].IsAttacking = random == 0 ? false : true;
            #endregion

            #region game loop
            while(true)
            {
                Coordinate shot = players.First(p => p.IsAttacking).Attack();
                players.First(p => !p.IsAttacking).Defend(shot);

                var playerWithNoShips = players.FirstOrDefault(p => !p.Fleet.Exists(f => !f.IsSunk));
                if (playerWithNoShips!=null)
                {
                    Console.WriteLine($"GAME OVER: {playerWithNoShips.ToString()} has no ships left.");
                    break;
                }

                players[0].IsAttacking = !players[0].IsAttacking;
                players[1].IsAttacking = !players[1].IsAttacking;
            }
            #endregion

            #endregion
        }
    }
}
