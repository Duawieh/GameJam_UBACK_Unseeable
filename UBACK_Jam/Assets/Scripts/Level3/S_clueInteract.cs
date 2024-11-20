using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_clueInteract : MonoBehaviour
{
    private GameObject player;
    
    public GameObject cluePanel;
    public GameObject tempObj_audioPlayer;
    public AudioClip audio_interact;
    public int livingClip;

    private void interact() {
        if (livingClip != DimensionControl.getLevel()) return;

        Vector3 dis = player.transform.localPosition - transform.localPosition;
        if (dis.magnitude <= 0.256f) {
            GameObject adp = Instantiate(tempObj_audioPlayer);
            adp.GetComponent<S_audioPlayer>().adc = audio_interact;
            adp.GetComponent<S_audioPlayer>().life = 1.0f;

            GameMap.controllable = false;
            Instantiate(cluePanel, GameObject.Find("Canvas").transform);

            Destroy(gameObject);
        }

        return;
    }

    private void updateVisiable() {
        if (livingClip == DimensionControl.getLevel()) {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        interact();
        updateVisiable();
    }
}
