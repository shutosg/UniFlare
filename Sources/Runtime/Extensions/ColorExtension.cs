using UnityEngine;

namespace UniFlare
{
    public static class UniFlareColorExtension
    {
        public static Color OffsetHue(this Color self, float hueOffset)
        {
            if (Mathf.Approximately(hueOffset, 0f)) return self;
            var hsv = self.ToHSV();
            hsv.x += hueOffset;
            var newColor = hsv.ToRGB();
            newColor.a = self.a;
            return newColor;
        }

        public static Vector3 ToHSV(this Color self)
        {
            Color.RGBToHSV(self, out var h, out var s, out var v);
            return new Vector3(h, s, v);
        }

        public static Color ToRGB(this Vector3 hsv)
            => Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
    }
}
