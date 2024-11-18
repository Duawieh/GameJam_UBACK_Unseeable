using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_controllerUI : MonoBehaviour
{
    public GameObject counterUI;
    public GameObject controllerUI;
    public Texture2D pressedUp, pressedDown, pressedBoth, pressedNone;

    private void updateCounterNum()
    {
        Text t = counterUI.GetComponent<Text>();
        t.text = DimensionControl.getLevel().ToString();
        return;
    }

    private void updatePressState()
    {
        RawImage ri = controllerUI.GetComponent<RawImage>();
        
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            ri.texture = pressedBoth;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            ri.texture = pressedUp;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ri.texture = pressedDown;
        }
        else
        {
            ri.texture = pressedNone;
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        updateCounterNum();
        updatePressState();
    }
}
