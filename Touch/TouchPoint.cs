using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Common.Touch
{
    public class TouchPoint
    {
        public int Id { get; private set; }

        public TouchLocation CurrentLocation { get; set; }

        public List<TouchLocation> Points { get; set; }

        public TouchLocationState State { get; set; }
        public bool Finished { get; set; }
        public Vector2 Delta { get; set; }

        public TouchPoint(TouchLocation location)
        {
            this.Id = location.Id;
            this.Points = new List<TouchLocation>();

            Update(location);
        }

        public void Update(TouchLocation location)
        {
            this.CurrentLocation = location;
            this.Points.Add(location);
            this.State = location.State;

            if (this.Points.Count > 1)
            {
                this.Delta = this.Points[this.Points.Count - 1].Position -
                             this.Points[this.Points.Count - 2].Position;
            }

            this.Finished = location.State == TouchLocationState.Released ||
                            location.State == TouchLocationState.Invalid;
        }
    }
}
