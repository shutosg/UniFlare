using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
{
    public class UniFlareIris : UniFlareSpriteElement
    {
        public void Initialize(float distance, float intensity, Vector3 scale)
        {
            _distance = distance;
            _intensity = intensity;
            _scale = scale;
        }

        public void InitializeDistance(float distance) => _distance = distance;
        public void InitializeIntensity(float intensity) => _intensity = intensity;
        public void InitializeScale(Vector3 scale) => _scale = scale;
        public void InitializeColor(Color color) => _color = color;
        public void InitializeSprite(Sprite sprite) => _sprite.sprite = sprite;
    }
}
