using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_controllerUI : MonoBehaviour
{
    public GameObject tempObj_audioPlayer;
    public GameObject counterUI;
    public GameObject controllerUI;
    public Texture2D pressedUp, pressedDown, pressedBoth, pressedNone;
    public AudioClip audio_buttonClick;

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

    private void playAudioEffects()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject t = Instantiate(tempObj_audioPlayer);
            t.GetComponent<S_audioPlayer>().adc = audio_buttonClick;
            t.GetComponent<S_audioPlayer>().life = 1.0f;
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        updateCounterNum();
        updatePressState();
        playAudioEffects();
    }
}
