using UnityEngine;

namespace shutosg.UniFlare.Elements.Sprites
{
    public class UniFlareIrisSprite : UniFlareIris<UniFlareSpriteElement>
    {
        public override void InitializeRenderer(Sprite sprite) => _element.InitializeSprite(sprite);
    }
}
