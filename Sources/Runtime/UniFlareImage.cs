using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniFlareImage : Image
{
    private List<UIVertex> _stream = new List<UIVertex>();

    public float FlareParam0
    {
        set
        {
            setParam(value, 0);
            SetVerticesDirty();
        }
    }
    public float FlareParam1
    {
        set
        {
            setParam(value, 1);
            SetVerticesDirty();
        }
    }
    public float FlareParam2
    {
        set
        {
            setParam(value, 2);
            SetVerticesDirty();
        }
    }
    public float FlareParam3
    {
        set
        {
            setParam(value, 3);
            SetVerticesDirty();
        }
    }

    void setParam(float v, int index)
    {
        var uv2 = _texCoord2;
        // TODO: 精度落として4パラメータ詰め込む
        switch (index)
        {
            case 0:
                uv2.x = v;
                break;
            case 1:
                uv2.y = v;
                break;
        }
        _texCoord2 = uv2;
    }

    private Vector2 _texCoord2;

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
            vert.uv2 = _texCoord2;
            _stream[i] = vert;
        }

        vh.AddUIVertexTriangleStream(_stream);
    }
}
