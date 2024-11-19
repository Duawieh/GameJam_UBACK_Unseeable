using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_audioPlayer : MonoBehaviour
{
    private float age;
    public float life;
    public AudioClip adc;

    // Start is called before the first frame update
    void Start()
    {
        age = 0.0f;
        GetComponent<AudioSource>().clip = adc;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        if (age >= life) Destroy(gameObject);
    }
}
