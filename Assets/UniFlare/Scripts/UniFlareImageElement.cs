using UnityEngine;
using UnityEngine.UI;

namespace UniFlare
{
    [RequireComponent(typeof(Image))]
    public class UniFlareImageElement : UniFlareElementBase
    {
        private Image _cachedImage;
        protected Image _image => _cachedImage != null ? _cachedImage : _cachedImage = GetComponent<Image>();

        public void InitializeSprite(Sprite sprite) => _image.sprite = sprite;

        public override void UpdateIntensity(float intensity)
        {
            // throw new NotImplementedException();
        }

        public override void UpdateColor(Color color)
        {
            _image.color = color * _color;
        }
    }
}
