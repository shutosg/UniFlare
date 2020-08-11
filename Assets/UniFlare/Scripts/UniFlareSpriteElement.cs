using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
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

        public override void UpdateIntensity(float intensity)
        {
            // throw new NotImplementedException();
        }

        public override void UpdateColor(Color color)
        {
            _sprite.color = color * _color;
        }
    }
}
