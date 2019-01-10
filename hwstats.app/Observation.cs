using System;

namespace hwstats.app
{
    public class Observation
    {
        private float _height = 0.0f;
        private float _weight = 0.0f;

        public float Height
        {
            get
            {
                return _height;
            }
            set
            {
                if(value <= 0.0f)
                {
                    throw new ArgumentException("Height must be greater than 0.");
                }
                _height = value;
            }
        }
        public float Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if(value <= 0.0f)
                {
                    throw new ArgumentException("Weight must be greater than 0.");
                }
                _weight = value;
            }
        }
    }
}
 
