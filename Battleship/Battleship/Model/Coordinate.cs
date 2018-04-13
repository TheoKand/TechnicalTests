
namespace Battleship.Model
{
    public struct Coordinate
    {
        public int x, y;

        /// <summary>
        /// This constructor will convert a coordinate in the format A5 to x,y
        /// </summary>
        public Coordinate(string input)
        {
            x = input[0] - 'A' +1 ;
            y = int.Parse(input.Substring(1));
        }

        public override string ToString()
        {
            return $"{((char)('A' + x -1)).ToString()}{y.ToString()}";
        }
    }


}
