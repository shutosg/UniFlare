using UnityEngine;

namespace UniFlare
{
    public interface IUniFlareElement
    {
        void Initialize();
        void UpdatePosition(Vector3 position, Vector3 center);
        void UpdateIntensity(float intensity);
        void UpdateScale(float scale);
        void UpdateColor(Color color);
        void UpdateOtherParams();
        void ShiftColorHue(float hueOffset);
    }
}
