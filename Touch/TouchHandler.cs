using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Touch
{
    public class TouchHandler
    {
        private static int maxTouchLocationsHistory = 10;

        public static int MaxTouchLocationsHistory
        {
            get { return maxTouchLocationsHistory; }
            set
            {
                // clamp between 2 and a hundred
                maxTouchLocationsHistory = Math.Min(100, Math.Max(2, value));
            }
        }

        /// <summary>
        /// Current frame touch state.
        /// </summary>
        public TouchCollection TouchState { get; set; }

        /// <summary>
        /// Current frame new touch locations.
        /// </summary>
        public List<TouchPoint> NewPoints { get; set; }

        /// <summary>
        /// List of all points currently touching the screen.
        /// </summary>
        public List<TouchPoint> Points { get; set; }

        /// <summary>
        /// Keep a list of all toches made so far.
        /// It allows for new touch detections.
        /// </summary>
        private List<int> allTouches = new List<int>();

        /// <summary>
        /// Convenient mapping from id to point.
        /// </summary>
        private readonly Dictionary<int, TouchPoint> idToPoint = new Dictionary<int, TouchPoint>();

        public TouchHandler()
        {
            Points = new List<TouchPoint>();
            NewPoints = new List<TouchPoint>();
            Points = new List<TouchPoint>();
        }

        public void Update()
        {
            TouchState = TouchPanel.GetState();

            NewPoints.Clear();

            for (var index = 0; index < TouchState.Count; index++)
            {
                var location = TouchState[index];
                switch (location.State)
                {
                    case TouchLocationState.Pressed:
                        {
                            AddTouchPoint(location);
                            break;
                        }

                    case TouchLocationState.Invalid:
                    case TouchLocationState.Released:
                        {
                            TouchPoint point;
                            if (idToPoint.TryGetValue(location.Id, out point))
                            {
                                point.Update(location);
                                Points.Remove(point);
                                idToPoint.Remove(location.Id);
                            }
                            break;
                        }

                    case TouchLocationState.Moved:
                        {
                            TouchPoint point;
                            if (idToPoint.TryGetValue(location.Id, out point))
                            {
                                point.Update(location);
                            }
                            else
                            {
                                AddTouchPoint(location);
                            }
                            break;
                        }
                }
            }

            allTouches = idToPoint.Select(i => i.Key).ToList();
        }

        private void AddTouchPoint(TouchLocation touch)
        {
            TouchPoint point;
            if (idToPoint.TryGetValue(touch.Id, out point))
            {
                Points.Remove(point);
                idToPoint.Remove(touch.Id);
                allTouches.Remove(touch.Id);
            }

            point = new TouchPoint(touch);
            Points.Add(point);
            idToPoint.Add(touch.Id, point);

            if (!allTouches.Contains(touch.Id))
                NewPoints.Add(point);
        }
    }
}