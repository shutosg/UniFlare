using UnityEngine;

namespace UniFlare
{

    public static class UniFlareColorExtension
    {
        public static Color OffsetHue(this Color self, float hueOffset)
        {
            Color.RGBToHSV(self, out var h, out var s, out var v);
            h += hueOffset;
            var newColor = Color.HSVToRGB(h, s, v);
            newColor.a = self.a;
            return newColor;
        }
    }
}
