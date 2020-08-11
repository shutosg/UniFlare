using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
{
    public class UniFlareIrisSprite : UniFlareIris<UniFlareSpriteElement>
    {
        public override void InitializeRenderer(Sprite sprite) => _element.InitializeSprite(sprite);
    }
}
