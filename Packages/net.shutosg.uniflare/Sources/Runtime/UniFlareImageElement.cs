using System.Linq;
using UnityEngine;

namespace UniFlare
{
    [RequireComponent(typeof(UniFlareImage))]
    public class UniFlareImageElement : UniFlareElementBase
    {
        private UniFlareImage _cachedImage;
        protected UniFlareImage Image => _cachedImage != null ? _cachedImage : _cachedImage = GetComponent<UniFlareImage>();

        public override Object[] GetRecordObjects() => base.GetRecordObjects().Concat(new[] { (Object)Image }).ToArray();

        public void InitializeSprite(Sprite sprite) => Image.sprite = sprite;

        public override void SetMaterialIfNeeded(Material material)
        {
            base.SetMaterialIfNeeded(material);
            if (Image.material != _material) Image.material = _material;
        }

        public override void UpdateIntensity(float intensity)
        {
            var val = intensity * (_intensity / 100f);
            Image.FlareParam0 = val;
        }

        public override void UpdateColor(Color color)
        {
            Image.color = CalculateColor(color);
        }
    }
}
