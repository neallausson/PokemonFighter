using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTracker : DeviceTracker {

    [SerializeField]
    private KeyCode[] buttonsKeys;
    [SerializeField]
    private AxisKeys[] axisKeys;

    public KeyCode[] ButtonsKeys
    {
        get
        {
            return buttonsKeys;
        }

        set
        {
            buttonsKeys = value;
        }
    }
    public AxisKeys[] AxisKeys
    {
        get
        {
            return axisKeys;
        }

        set
        {
            axisKeys = value;
        }
    }

    void Reset()
    {
        im = GetComponent<InputManager>();
        axisKeys = new AxisKeys[im.AxisCount];
        buttonsKeys = new KeyCode[im.ButtonCount];
    }

    public override void Refresh()
    {
        im = GetComponent<InputManager>();

        //create 2 temp arrays for buttons and axes
        KeyCode[] newButtons = new KeyCode[im.ButtonCount];
        AxisKeys[] newAxes = new AxisKeys[im.AxisCount];

        if (buttonsKeys != null)
        {
            for (int i = 0; i < Mathf.Min(newButtons.Length,buttonsKeys.Length); i++)
            {
                newButtons[i] = buttonsKeys[i];
            }
        }
        buttonsKeys = newButtons;

        if (axisKeys != null)
        {
            for (int i = 0; i < Mathf.Min(newAxes.Length, axisKeys.Length); i++)
            {
                newAxes[i] = axisKeys[i];
            }
        }
        axisKeys = newAxes;
    }

    // Update is called once per frame
    void Update () {
        //ckeck for inputs,if inputs detected , set newData to true
        //populate InputData to pass to the InputManager

        for (int i = 0; i < axisKeys.Length; i++)
        {
            float val = 0f;
            if (Input.GetKey(axisKeys[i].positive))
            {
                val += 1;
                newData = true;
            }
            if (Input.GetKey(axisKeys[i].negative))
            {
                val -= 1;
                newData = true;
            }
            data.axes[i] = val;
        }

        for (int i = 0; i < buttonsKeys.Length; i++)
        {
            if (Input.GetKey(buttonsKeys[i]))
            {
                data.buttons[i] = true;
                newData = true;
            }
        }

        if (newData)
        {
            im.PassInput(data);
            newData = false;
            data.Reset();
        }
	}
}

[System.Serializable]
public struct AxisKeys
{
    public KeyCode positive;
    public KeyCode negative;
}