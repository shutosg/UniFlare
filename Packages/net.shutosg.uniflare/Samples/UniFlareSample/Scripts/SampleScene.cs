using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using shutosg.UniFlare;
using shutosg.UniFlare.Extensions;

public class SampleScene : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = default;
    [SerializeField] private UniFlareController[] _flares = default;
    [SerializeField] private Slider _intensitySlider = default;
    [SerializeField] private Slider _scaleSlider = default;
    [SerializeField] private Slider _hueSlider = default;
    private List<float> _originalHues = new List<float>();

    void Start()
    {
        foreach (var flare in _flares)
        {
            _originalHues.Add(flare.Color.ToHSV().x);
        }
        _intensitySlider.onValueChanged.AddListener(v =>
        {
            foreach (var flare in _flares)
            {
                flare.Intensity = v * 2 * 100f;
            }
        });
        _scaleSlider.onValueChanged.AddListener(v =>
        {
            foreach (var flare in _flares)
            {
                flare.Scale = v * 2 * 100f;
            }
        });
        _hueSlider.onValueChanged.AddListener(v =>
        {
            for (var i = 0; i < _flares.Length; i++)
            {
                _flares[i].SetColorHue(_originalHues[i] + (v - 0.5f));
            }
        });
    }

    public void OnDragPosition(int flareIndex)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out var localPoint);
        _flares[flareIndex].Position.localPosition = localPoint;
    }

    public void OnDragCenter(int flareIndex)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out var localPoint);
        _flares[flareIndex].Center.localPosition = localPoint;
    }
}
