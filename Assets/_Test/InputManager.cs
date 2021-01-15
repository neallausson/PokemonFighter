using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField]
    [Range(0, 10)]
    private int axisCount;

    [SerializeField]
    [Range(0, 20)]
    private int buttonCount;

    [SerializeField]
    private Controller controller;

    public int AxisCount
    {
        get
        {
            return axisCount;
        }

        set
        {
            axisCount = value;
        }
    }
    public int ButtonCount
    {
        get
        {
            return buttonCount;
        }

        set
        {
            buttonCount = value;
        }
    }

    public void PassInput(InputData data)
    {
        //Debug.Log("Movement: " + data.axes[0] + ", " + data.axes[1]);
        controller.ReadInput(data);
    }

    public void RefreshTracker()
    {
        DeviceTracker dt = GetComponent<DeviceTracker>();
        if (dt != null)
        {
            dt.Refresh();
        }
    }
}

public struct InputData
{
    public float[] axes;
    public bool[] buttons;

    public InputData(int axisCount,int buttonCount)
    {
        axes = new float[axisCount];
        buttons = new bool[buttonCount];
    }

    public void Reset()
    {
        for (int i = 0; i < axes.Length; i++)
        {
            axes[i] = 0f;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = false;
        }
    }
}
