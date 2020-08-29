using System.Linq;
using UnityEngine;
using shutosg.UniFlare.Tools;

namespace shutosg.UniFlare.Elements.UI
{
    [RequireComponent(typeof(UniFlareImage))]
    public class UniFlareImageElement : UniFlareElementBase
    {
        private UniFlareImage _cachedImage;
        protected UniFlareImage Image => _cachedImage != null ? _cachedImage : _cachedImage = GetComponent<UniFlareImage>();

        public void InitializeSprite(Sprite sprite) => Image.sprite = sprite;

        public override void SetMaterialIfNeeded(Material material)
        {
            base.SetMaterialIfNeeded(material);
            if (Image.material != _material) Image.material = _material;
        }

        public override void UpdateIntensity(float intensity)
        {
            const int Precision = 0xFFFF;
            const float Epsilon = 0.0001f;
            var normalized = UniFlareValueNormalizer.NormalizeLog(intensity * _intensity, Epsilon, -14, 14, 0, Precision);
            Image.SetParam(normalized, 0);
        }

        public override void UpdateColor(Color color)
        {
            Image.color = CalculateColor(color);
        }

#if UNITY_EDITOR
        public override Object[] GetRecordObjects() => base.GetRecordObjects().Append(Image).ToArray();
#endif
    }
}
