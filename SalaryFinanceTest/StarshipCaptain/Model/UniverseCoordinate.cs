using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipCaptain.Model
{

    /// <summary>
    /// This class represents a single coordinate e.g. 123.123.99.1
    /// </summary>
    public class UniverseCoordinate 
    {

        /// <summary>
        /// The value of the coordinate, from 0 to 1 billion. A coordinate such as 123.123.99.1 is represented by the long value 123123991
        /// </summary>
        private long internalValue;
        public long InternalValue {
            get
            {

                return internalValue;
            }
            set
            {
                if (value < 0 || value > 999999999) throw new Exception("Invalid value for coordinate. The coordinate system ranges from 000.000.00.0 to 999.999.99.9");
                this.internalValue = value;

            }
        }

        public UniverseCoordinate(long value)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// This overload allows to instantiate a coordinate with a string such as 123.123.99.1
        /// </summary>
        public UniverseCoordinate(string description)
        {
            this.InternalValue = long.Parse(description.Replace(".", ""));
        }

        /// <summary>
        /// Returns a random coordinate. Possible values are 1 to one billion.
        /// </summary>
        public static UniverseCoordinate GetRandom()
        {
            var random = new UniverseCoordinate(RandomNumberGenerator.Rnd.Next(0, 999999999));
            return random;
        }

        /// <summary>
        /// Returns the verbose string of the coordinate such as 123.123.99.1
        /// </summary>
        public override string ToString()
        {
            //pad the value with zeros up to 9 digits
            string stringValue = InternalValue.ToString("D9");
            string retVal = string.Format("{0}.{1}.{2}.{3}", stringValue.Substring(0, 3), stringValue.Substring(3, 3), stringValue.Substring(6, 2), stringValue.Substring(8, 1));
            return retVal;
        }

    }


}
