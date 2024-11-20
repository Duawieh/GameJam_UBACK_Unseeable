using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 与 UI 位置和尺寸相关的方法
/// </summary>
public class UITransform : MonoBehaviour
{
    /// <summary>
    /// 寻找给定对象在 Hierarchy 上的最近 Canvas 祖先。注意：如果给定对象即 Canvas 对象，返回结果也不会是所给对象。
    /// </summary>
    /// <returns>最近 Canvas 祖先，或 null。</returns>
    private static Transform getCanvasAncestor(Transform _cur)
    {
        Transform fa = _cur.transform.parent;
        if (fa != null)
        {
            if (fa.GetComponent<Canvas>() != null)
            {
                return fa;
            }
            else
            {
                return getCanvasAncestor(fa);
            }
        }
        return null;
    }

    /// <summary>
    /// 获取给定 RectTransform 对象在其所属 Canvas 上的宽高占比。
    /// </summary>
    /// <returns>返回其在最近 Canvas 祖先上的宽高占比，若其在 Hierarchy 中不属于任何 Canvas 对象，返回零向量。</returns>
    public static Vector2 getScreenScale(RectTransform _objRectTr)
    {
        if (_objRectTr.transform == null) return Vector2.zero;
        RectTransform canvasAnc = getCanvasAncestor(_objRectTr.transform).GetComponent<RectTransform>();
        if (canvasAnc == null) return Vector2.zero;
        return new Vector2(_objRectTr.rect.width  * _objRectTr.localScale.x / canvasAnc.rect.width, 
                           _objRectTr.rect.height * _objRectTr.localScale.y / canvasAnc.rect.height);
    }
}

/// <summary>
/// 与隐维层次控制相关的类
/// 非常重要！
/// </summary>
public static class DimensionControl
{
    private static int clipLevel;
    private static int minLevel, maxLevel;

    /// <summary>
    /// 设置层次范围，范围将被设置为传入向量的 x 分量到 y 分量之间的闭区间，仅限整数。
    /// </summary>
    /// <param name="_range">传入的范围向量</param>
    public static void setLevelRange(Vector2 _range)
    {
        minLevel = (int)_range.x;
        maxLevel = (int)_range.y;
        if (minLevel > maxLevel)
        {
            (minLevel, maxLevel) = (maxLevel, minLevel);
        }
        return;
    }

    public static void levelIncrease()
    {
        if (clipLevel == maxLevel) return;
        clipLevel++;
        return;
    }

    public static void levelDecrease()
    {
        if (clipLevel == minLevel) return;
        clipLevel--;
        return;
    }

    /// <returns>返回当前所在的隐维层次</returns>
    public static int getLevel()
    {
        return clipLevel;
    }

    /// <summary>
    /// 设置隐维层次
    /// </summary>
    /// <returns>设置成功则返回 true，否则返回 false。</returns>
    public static bool setLevel(int _l)
    {
        if (_l >= minLevel && _l <= maxLevel)
        {
            clipLevel = _l;
            return true;
        }
        return false;
    }

    public static bool outOfRange(int _l)
    {
        if (_l < minLevel) return true;
        if (_l > maxLevel) return true;
        return false;
    }
}

public class S_L0Controller : MonoBehaviour
{
    private Color[] clipColors = { new Color(1, 1, 1),
                                   new Color(1, 1, 1),
                                   new Color(1, 1, 1),
                                   new Color(1, 0.878f, 0.675f),
                                   new Color(1, 0.792f, 0.443f),
                                   new Color(1, 0.710f, 0.212f),
                                   new Color(1, 0.651f, 0.059f) };
    private bool levelFinished = false;
    private bool clueFounded = false;

    public GameObject tempObj_audioPlayer;
    public AudioClip audio_interact;

    private void initRect()
    {
        Vector2 localScaleRate = UITransform.getScreenScale(GetComponent<RectTransform>());
        Vector2 targetScaleRate = new Vector2(0.03f, 0.03f);
        // 避免除以 0
        if (localScaleRate.y == 0.0f) return;
        transform.localScale *= targetScaleRate.y / localScaleRate.y;
        return;
    }

    private void keyboardControl()
    {
        if (GameMap.controllable == false) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DimensionControl.levelIncrease();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DimensionControl.levelDecrease();
        }
        return;
    }

    private void updatePointColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        sr.color = clipColors[DimensionControl.getLevel() + 1];
    }

    private void itemIteractions() {
        if (DimensionControl.getLevel() == 5) {
            if (levelFinished) return;
            levelFinished = true;
            GameObject mc = GameObject.FindGameObjectWithTag("MainCamera");
            mc.GetComponent<S_levelManager>().NextLevel();
            StartCoroutine(Anim_nextLevel());
        }
        else if (DimensionControl.getLevel() == -1) {
            if (clueFounded) return;
            clueFounded = true;

            // 播放线索交互音效
            GameObject temp = Instantiate(tempObj_audioPlayer);
            temp.GetComponent<S_audioPlayer>().adc = audio_interact;
            temp.GetComponent<S_audioPlayer>().life = 1.0f;

            // 打开线索面板
        }
    }

    // 由于 Level 0 场景内没有 item 显示，这里单独编写一个动画协程
    // 该协程无需控制音效和 SceneManager，因为 S_LevelManager.NextLevel() 仍然正常调用
    // 但是 S_LevelManager.NextLevel() 用特判卡掉了 Level 0 的通关动画
    private IEnumerator Anim_nextLevel() {
        GameObject Tt_controller = GameObject.Find("Tt_glow");
        Tt_controller.GetComponent<SpriteRenderer>().enabled = true;
        Tt_controller.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        float t = 0.0f;

        while (t < 8) {
            t += Time.deltaTime;
            Tt_controller.transform.localScale += Vector3.one * 48 * Time.deltaTime;
            yield return 0;
        }

        yield break;
    }

    void Awake() 
    {
        GameMap.gameLevel = 0;
        GameMap.controllable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        initRect();
        DimensionControl.setLevelRange(new Vector2(-1, 5));
        DimensionControl.setLevel(1);
        return;
    }

    void Update()
    {
        keyboardControl();
        updatePointColor();
        itemIteractions();
    }
}
