using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniFlareImage : Image
{
    private List<UIVertex> _stream = new List<UIVertex>();
    public float Intensity
    {
        set
        {
            _intensity = value;
            SetVerticesDirty();
        }
    }
    private float _intensity;
    public float Size
    {
        set
        {
            _size = value;
            SetVerticesDirty();
        }
    }
    private float _size;

    protected void AddCustomShaderChannel()
    {
        if (canvas != null) canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
    }

    new void OnValidate()
    {
        AddCustomShaderChannel();
        SetVerticesDirty();
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
            vert.uv2.x = _intensity;
            vert.uv2.y = _size;
            _stream[i] = vert;
        }

        vh.AddUIVertexTriangleStream(_stream);
    }
}
