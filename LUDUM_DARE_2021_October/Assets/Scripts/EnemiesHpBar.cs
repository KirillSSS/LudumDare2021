using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesHpBar : MonoBehaviour
{
    public Image BarImage;
    public Gradient Color;

    private Transform _anchor;
    private Camera _camera;
    private Enemies _entity;

    private void Awake()
    {
        _camera = Camera.main;
    }
    public void Initialize(Transform anchor, Enemies entity)
    {
        _anchor = anchor;
        transform.position = _camera.WorldToScreenPoint(_anchor.position);

        _entity = entity;

        BarImage.color = Color.Evaluate(1);

    }

    public void UpdateBar()
    {
        Debug.Log(_entity.health);
        var value = ((_entity.health * 100) / _entity.healthMax) / 100;
        BarImage.fillAmount = value;
        BarImage.color = Color.Evaluate(value);
    }
}
