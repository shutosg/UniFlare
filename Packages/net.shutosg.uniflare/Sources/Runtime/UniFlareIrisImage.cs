﻿using UnityEngine;

namespace UniFlare
{
    public class UniFlareIrisImage : UniFlareIris<UniFlareImageElement>
    {
        public override void InitializeRenderer(Sprite sprite) => _element.InitializeSprite(sprite);
    }
}