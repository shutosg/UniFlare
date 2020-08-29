using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace shutosg.UniFlare
{
    public class UniFlareImage : Image
    {
        public static readonly int ParamPrecision = 0xFFFF;
        private List<UIVertex> _stream = new List<UIVertex>();

        /// <summary>
        /// set param
        /// </summary>
        /// <param name="v">normalized 2bytes parameter(0 ~ 65535)</param>
        /// <param name="index">parameter index(0 ~ 3)</param>
        public void SetParam(int v, int index)
        {
            var currentValue = index < 2 ? (long)_texCoord2.x : (long)_texCoord2.y;
            var mask = index % 2 == 1 ? 0xFFFF : 0xFFFF0000;
            var masked = (currentValue & mask);
            // if (gameObject.name.Contains("Glow")) Debug.Log($"[Before] v: {v} index: {index}, masked: {masked:x8}");
            currentValue = masked + v * (long)Mathf.Pow(ParamPrecision + 1, index % 2);
            // if (gameObject.name.Contains("Glow")) Debug.Log($"[After]  v: {v} index: {index}, currentValue: {currentValue:x8}");
            if (index < 2) _texCoord2.x = currentValue;
            else _texCoord2.y = currentValue;
            SetVerticesDirty();
        }

        private Vector2 _texCoord2;

        protected void AddCustomShaderChannel()
        {
            if (canvas != null) canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
        }

        new void Start()
        {
            AddCustomShaderChannel();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);

            _stream.Clear();
            vh.GetUIVertexStream(_stream);
            vh.Clear();

            for (var i = 0; i < _stream.Count; ++i)
            {
                var vert = _stream[i];
                vert.uv2 = _texCoord2;
                _stream[i] = vert;
            }

            vh.AddUIVertexTriangleStream(_stream);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            AddCustomShaderChannel();
            SetVerticesDirty();
        }
#endif
    }
}
