using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json;

namespace MagLevSignal.Models
{
    public class Body
    {
        #region ///  Properties  ///

        public string Color { get; set; }

        public List<Vector2> FuturePositions { get; set; } = new List<Vector2>();
        public float Mass { get; set; }

        public string Name { get; set; }

        //todo:replace this property with local var in orbit method
        [JsonIgnore] public Body ParentBody { get; set; }

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Velocity { get; set; }

        #endregion
    }
}