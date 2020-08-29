using UnityEngine;

namespace shutosg.UniFlare.Elements.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class UniFlareSpriteElement : UniFlareElementBase
    {
        private SpriteRenderer _cachedSprite;
        protected SpriteRenderer _sprite
        {
            get
            {
                if (_cachedSprite == null)
                {
                    _cachedSprite = GetComponent<SpriteRenderer>();
                }
                return _cachedSprite;
            }
        }

        public void InitializeSprite(Sprite sprite) => _sprite.sprite = sprite;

        public override void SetMaterialIfNeeded(Material material)
        {
            if (_sprite.material != material) _sprite.material = material;
        }

        public override void UpdateIntensity(float intensity)
        {
            // throw new NotImplementedException();
        }

        public override void UpdateColor(Color color)
        {
            _sprite.color = CalculateColor(color);
        }
    }
}
