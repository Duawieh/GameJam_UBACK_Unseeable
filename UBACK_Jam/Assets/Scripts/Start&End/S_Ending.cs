using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class S_Ending : MonoBehaviour
{
    private GameObject titleSprite;
    private Vector3 scale;
    private float time = 0.0f;

    private void initTitle() {
        titleSprite = transform.parent.gameObject;
        titleSprite.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

        Vector2 rate = UITransform.getScreenScale(titleSprite.GetComponent<RectTransform>());
        titleSprite.transform.localScale *= 0.5f / rate.x;
        scale = titleSprite.transform.localScale;
        return;
    }

    private void initList() {
        float screenHeight = Screen.height;
        transform.localPosition = new Vector3(0, -screenHeight / 2, 0) / scale.y;
        return;
    }

    private void skip() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape)) {
            if (time >= 65) return;
            time = 65;
        }
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        initTitle();
        initList();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        skip();

        if (time < 5) {
            titleSprite.GetComponent<RawImage>().color = new Color(1, 1, 1, time / 2);
        }
        else {
            titleSprite.transform.localPosition += Vector3.up * 64 * scale.y * Time.deltaTime;
        }

        if (time > 65) {
            GameObject p = GameObject.Find("Panel");
            p.GetComponent<Image>().color = Color.white * (1 - ((time - 65) / 5)) + new Color(0, 0, 0, 1);
        }
        if (time > 70) {
            SceneManager.LoadScene(0);
            GameObject adp;
            do {
                adp = GameObject.Find("AudioPlayer(Clone)");
                if (adp == null) break;
                DestroyImmediate(adp);
            }
            while (true);
        }
    }
}
