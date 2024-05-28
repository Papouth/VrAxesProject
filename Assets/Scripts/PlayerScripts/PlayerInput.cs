using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerInput : MonoBehaviour
{
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    private InputDevice _targetDevice;


    //List<InputDevice> inputDevices = new List<InputDevice>();
    public bool isGripped;

    private void Start()
    {
        InitializeInputReader();
    }

    private void InitializeInputReader()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            _targetDevice = devices[0];
        }
    }

    private void Update()
    {
        if (!_targetDevice.isValid) InitializeInputReader();
        else UpdateHand();
    }

    private void UpdateHand()
    {
        _targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out isGripped);
    }

    /*
    private void InitializeInputReader()
    {
        //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, inputDevices);
        //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, inputDevices);

        foreach (var inputDevice in inputDevices)
        {
            inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out isGripped);
            Debug.Log(inputDevice.name + " " + isGripped);
        }
    }

    private void Update()
    {
        if (inputDevices.Count < 2)
        {
            InitializeInputReader();
        }
    }
    */



}