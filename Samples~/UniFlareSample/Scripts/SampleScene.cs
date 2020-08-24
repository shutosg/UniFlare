using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniFlare;

public class SampleScene : MonoBehaviour
{
    [SerializeField] private Button _yellow = default;
    [SerializeField] private Button _red = default;
    [SerializeField] private Button _green = default;
    [SerializeField] private UniFlareController[] _flares = default;
    [SerializeField] private Dropdown _selector = default;

    void Start()
    {
        foreach (var flare in _flares)
        {
            _selector.options.Add(new Dropdown.OptionData(flare.gameObject.name));
        }
        _yellow.onClick.AddListener(() => ChangeColor(Color.yellow));
        _red.onClick.AddListener(() => ChangeColor(Color.red));
        _green.onClick.AddListener(() => ChangeColor(Color.green));
    }

    // Update is called once per frame
    private void ChangeColor(Color color)
    {
        // Debug.Log($"{color}, {color.ToHSV()}");
        _flares[_selector.value].SetColorHue(color.ToHSV().x);
    }
}
