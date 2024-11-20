using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_cluePanel : MonoBehaviour
{
    private string[] txtContents = new string[] { "“我们失败了……这无尽的黑暗，仿佛是命运的终点。不甘心，真的不甘心。还留下了什么？只有这绝望的遗言，和未完成的梦想。”",
                                                  "“不！这不可能！它在吞噬一切！时间、空间，都被它扭曲得面目全非！必须逃！但这线条编织的囚笼，让我们无处可逃！救救我……谁来救救我！”",
                                                  "“时间掌控在我们手中！”\n\n“哈哈哈哈！你学得可真像。”\n\n“哎呀，都不知道听了多少遍了，还能不像吗？”\n\n“也是，组长天天在会上念叨这个。”",
                                                  "3 月 26 日：\n今天是我加入这个遗迹实验团队以来第一次参与研究工作，听说这里在研究的一项新技术能让我们“直接”跳过考古调查工作，也不知道这个直接有多直接。\n\n5 月 19 日：\n听说附近的引力波实验室探测到了一些异常波动，也许是幸运的信号呢，希望我们的工作也能顺利完成。\n\n5 月 28 日：\n这见鬼的机器，今天不知道出了什么毛病，把地板挖了个洞！那可都是不知道有多大研究价值的史料啊！第一次现场实验就出这种事，今后的经费也不知道怎么办了。\n\n6 月 4 日：\n明天就是最后一次实验了。炸地板事件给我们带来了不小的麻烦，在被叫停前，以正常功率进行最后一次测试。希望一切顺利。" };
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
            yield return new WaitForSeconds(0.035f);
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
