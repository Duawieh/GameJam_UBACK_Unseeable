using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_cluePanel : MonoBehaviour
{
    private string[] txtContents = new string[] { "这是第一关的隐藏线索。",
                                                  "这是第二关的隐藏线索。",
                                                  "这是第三关的隐藏线索。",
                                                  "这是第四关的隐藏线索。" };
    private int curLevel;

    public GameObject textPanel;

    // Start is called before the first frame update
    void Start()
    {
        curLevel = GameMap.gameLevel;
        StartCoroutine(Anim_typer());
    }

    private IEnumerator Anim_typer()
    {
        string curStr = "";

        foreach (char c in txtContents[curLevel]) {
            curStr += c;
            textPanel.GetComponent<Text>().text = curStr;
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    public void Click_Exit()
    {
        GameMap.controllable = true;
        Destroy(gameObject);
        return;
    }
}
