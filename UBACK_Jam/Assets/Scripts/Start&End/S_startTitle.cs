using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_startTitle : MonoBehaviour
{
    private float time;
    private RawImage ri;

    // Start is called before the first frame update
    void Start()
    {
        float rate = 0.5f / UITransform.getScreenScale(GetComponent<RectTransform>()).x;
        transform.localScale *= rate;
        ri = GetComponent<RawImage>();
        ri.color = Color.black;
        time = 0;

        StartCoroutine(Anim_titleFlash());
    }

    private IEnumerator Anim_titleFlash() {
        while (true) {
            time += Time.deltaTime;
            if (time < 0.2f) {
                ri.color = Color.white * ((time - 0.0f) / 1.0f) + new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            if (time < 0.4f && time > 0.2f) {
                ri.color = Color.white * ((time - 0.2f) / 1.0f) + new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            if (time > 0.4f && time < 0.6f) {
                ri.color = Color.black;
                if (Random.Range(0, 1.0f) > 0.7f) {
                    if (Random.Range(0, 1.0f) > 0.5f) {
                        time = 0;
                    }
                    else {
                        time = 0.6f;
                    }
                }
                else {
                    time += 0.2f;
                    yield return new WaitForSeconds(0.2f);
                }
            }
            if (time > 0.6f) {
                ri.color = Color.white;
                if (Random.Range(0, 1.0f) > 0.9f) {
                    time = 0;
                }
                else yield return new WaitForSeconds(0.3f);
            }
            yield return 0;
        }
    }
}
