using UnityEngine;

namespace shutosg.UniFlare.Elements.UI
{
    public class UniFlareIrisImage : UniFlareIris<UniFlareImageElement>
    {
        public override void InitializeRenderer(Sprite sprite) => _element.InitializeSprite(sprite);
    }
}
