using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniFlare
{
    public class UniFlare : MonoBehaviour
    {
        [SerializeField] private Transform _position;
        [SerializeField] private Transform _center;
        [SerializeField] private float _intensity = 100f;
        [SerializeField] private Vector3 _scale = Vector3.one;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private UniFlareElementBase[] elements;
        private readonly List<IUniFlareElement> _elements = new List<IUniFlareElement>();

        private void initialize()
        {
            _elements.Clear();
            foreach (var element in elements)
            {
                _elements.Add(element);
            }
        }

        public void UpdateFlare()
        {
            if (_elements.Count == 0) initialize();
            foreach (var element in _elements)
            {
                element.UpdatePosition(_position.position, _center.position);
                element.UpdateIntensity(_intensity);
                element.UpdateScale(_scale);
            }
        }

        private void Update()
        {
            UpdateFlare();
        }
    }
}
