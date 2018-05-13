using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipCaptain.Model
{
    /// <summary>
    /// This class represents a location in space that is either a monster or a planet. 
    /// </summary>
    public class UniverseLocation
    {
        public UniverseCoordinate X;
        public UniverseCoordinate Y;
        public UniverseCoordinate Z;

        public enum UniverseLocationType
        {
            Planet=0,
            Monster=1
        }

        public UniverseLocationType Type;
        public int PlanetArea;
        public bool IsHabitable;

        public UniverseLocation()
        {
            //position randomly inside the 3D world
            X = UniverseCoordinate.GetRandom();
            Y = UniverseCoordinate.GetRandom();
            Z = UniverseCoordinate.GetRandom();

            //randomly set the type (monster,planet)
            bool isPlanet = RandomNumberGenerator.Rnd.Next(0, 10) >= 5;
            if (isPlanet)
            {
                //randomly set the area and habitable status of the planet
                Type = UniverseLocationType.Planet;
                PlanetArea = RandomNumberGenerator.Rnd.Next(1000000, 99999999);
                IsHabitable = RandomNumberGenerator.Rnd.Next(0, 10) >= 5;
            }
            else
            {
                Type = UniverseLocationType.Monster;
            }
        }

        public UniverseLocation(UniverseCoordinate X, UniverseCoordinate Y, UniverseCoordinate Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }


        public override string ToString()
        {
            return string.Format("X:{0} Y:{1} Z:{2} {3} {4} {5}", this.X.ToString(), this.Y.ToString(), this.Z.ToString(),this.Type,this.Type== UniverseLocationType.Planet?"Area:" + this.PlanetArea:"" ,this.Type== UniverseLocationType.Planet && this.IsHabitable?"Habitable":"" );
        }
    }

}
