using UnityEngine;

namespace shutosg.UniFlare.Tools
{
    public class UniFlareValueFlicker
    {
        public float Value
        {
            get
            {
                if (Time.time == _time) return _value;
                _value = getPerlinNoise() * (Max - Min);
                _time = Time.time;
                return _value;
            }
        }
        public float TimeOffset { get; set; }
        public float Speed { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        private float _time = default;
        private float _value = default;

        public UniFlareValueFlicker(float timeOffset = 0, float speed = 1, float min = 0, float max = 1)
        {
            TimeOffset = timeOffset;
            Speed = speed;
            Min = min;
            Max = max;
        }

        private float getPerlinNoise() => Mathf.PerlinNoise((Time.time + TimeOffset) * Speed, 0);
    }
}
