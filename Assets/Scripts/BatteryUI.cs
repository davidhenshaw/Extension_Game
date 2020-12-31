using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BatteryUI : MonoBehaviour
{
    [SerializeField] GameObject gameObjectWithModel;
    ISliderUIModel _model;
    Slider _slider;

    private void Awake()
    {
        _model = gameObjectWithModel.GetComponent<ISliderUIModel>();
        if(_model == null)
        {
            Debug.LogError(gameObjectWithModel.ToString() + " is does not have the ISliderUIModel component attached");
            return;
        }

        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = Mathf.Clamp01(_model.GetSliderValue());
    }
}

public interface ISliderUIModel
{
    float GetSliderValue();
}
