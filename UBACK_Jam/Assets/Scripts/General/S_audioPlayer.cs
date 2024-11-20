using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_audioPlayer : MonoBehaviour
{
    private float age;

    /// <summary>
    /// 临时对象的寿命（初始化时需指定）
    /// </summary>
    public float life;
    /// <summary>
    /// 要播放的音频剪辑（初始化时需指定）
    /// </summary>
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
