namespace GamePlatform.Api.Entities
{
    public class Point3d
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return "X = {X}, Y = {Y}, Z = {Z}";
        }
    }
}