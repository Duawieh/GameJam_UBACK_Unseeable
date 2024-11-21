using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_startButtons : MonoBehaviour
{
    private bool GameQuit = false;
    public GameObject buttonText_start;
    public GameObject buttonText_exit;

    private IEnumerator Anim_buttonsStart() {
        yield return new WaitForSeconds(1);

        float time = 0.0f;
        while (time < 0.3f) {
            time += Time.deltaTime;
            buttonText_start.GetComponent<Text>().color = Color.white * (time / 0.3f);
            buttonText_exit.GetComponent<Text>().color = Color.white * (time / 0.3f);
            yield return 0;
        }

        yield break;
    }

    private IEnumerator Anim_startGame() {
        buttonText_start.GetComponent<Text>().color = Color.black;
        buttonText_exit.GetComponent<Text>().color = Color.black;

        GameObject title = GameObject.Find("TitleImage");
        float time = 0.0f;

        while (time < 1f) {
            title.transform.localScale *= 0.8f;
            yield return new WaitForSeconds(0.02f);
            time += 0.02f;
        }

        SceneManager.LoadScene(1);

        yield break;
    }

    private IEnumerator Anim_exitGame() {
        float time = 0.0f;
        GameQuit = true;

        buttonText_start.GetComponent<Text>().color = Color.black;
        buttonText_exit.GetComponent<Text>().color = Color.black;

        while (time < 1.0f) {
            time += Time.deltaTime;
            GetComponent<Image>().color = new Color(0, 0, 0, time);
            yield return 0;
        }

        Application.Quit();
        yield break;
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonText_start.GetComponent<Text>().color = Color.black;
        buttonText_exit.GetComponent<Text>().color = Color.black;
        StartCoroutine(Anim_buttonsStart());
        GameQuit = false;
    }

    public void buttonClick_Start() {
        if (GameQuit) return;
        StartCoroutine(Anim_startGame());
        return;
    }

    public void buttonClick_Exit() {
        if (GameQuit) return;
        StartCoroutine(Anim_exitGame());
        return;
    }
}
