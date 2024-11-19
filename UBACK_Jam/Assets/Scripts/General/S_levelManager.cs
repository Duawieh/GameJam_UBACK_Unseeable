using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_levelManager : MonoBehaviour
{
    private int curScene;
    public GameObject tempObj_AudioPlayer;
    public AudioClip audio_levelFinished;

    public void NextLevel() 
    {
        // 禁用玩家操作
        GameMap.controllable = false;

        // 通关，游戏结束
        if (curScene == 4) 
        {

        }
        else 
        {
            StartCoroutine(Anim_nextLevel());
        }
    }

    private IEnumerator Anim_nextLevel()
    {
        float t = 0.0f;

        // 开始加载下一关场景
        AsyncOperation ao = SceneManager.LoadSceneAsync(curScene + 1);
        ao.allowSceneActivation = false;

        // 播放通关音乐
        GameObject p = Instantiate(tempObj_AudioPlayer);
        p.GetComponent<S_audioPlayer>().adc = audio_levelFinished;
        p.GetComponent<S_audioPlayer>().life = 10.0f;

        GameObject controllerItem = GameObject.Find("controllerItem").transform.parent.gameObject;
        if (controllerItem != null) {
            Vector3 beginPos = controllerItem.transform.localPosition;
            Vector3 toPos = Vector3.zero;

            // 图标动画
            while (t < 8) {
                Vector3 delta = (toPos - beginPos) / 8.0f * Time.deltaTime;
                controllerItem.transform.localPosition += delta;
                controllerItem.transform.localScale += Vector3.one * GameMap.gameScale.y * Time.deltaTime;

                t += Time.deltaTime;
                yield return 0;
            }
        }
        else 
        {
            while (t < 8) {
                t += Time.deltaTime;
                yield return 0;
            }
        }

        // 加载完成后自动启用下一关
        ao.allowSceneActivation = true;
        yield break;
    }

    void Awake() {
        curScene = SceneManager.GetActiveScene().buildIndex;
    }
}