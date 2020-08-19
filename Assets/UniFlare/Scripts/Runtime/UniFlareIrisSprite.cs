using UnityEngine;

namespace UniFlare
{
    public class UniFlareIrisSprite : UniFlareIris<UniFlareSpriteElement>
    {
        public override void InitializeRenderer(Sprite sprite) => _element.InitializeSprite(sprite);
    }
}
