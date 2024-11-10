using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// �� UI λ�úͳߴ���صķ���
/// </summary>
public class UITransform : MonoBehaviour
{
    /// <summary>
    /// Ѱ�Ҹ��������� Hierarchy �ϵ���� Canvas ���ȡ�ע�⣺����������� Canvas ���󣬷��ؽ��Ҳ��������������
    /// </summary>
    /// <returns>��� Canvas ���ȣ��� null��</returns>
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
    /// ��ȡ���� RectTransform ������������ Canvas �ϵĿ��ռ�ȡ�
    /// </summary>
    /// <returns>����������� Canvas �����ϵĿ��ռ�ȣ������� Hierarchy �в������κ� Canvas ���󣬷�����������</returns>
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
/// ����ά��ο�����ص���
/// �ǳ���Ҫ��
/// </summary>
public static class DimensionControl
{
    private static int clipLevel;
    private static int minLevel, maxLevel;

    /// <summary>
    /// ���ò�η�Χ����Χ��������Ϊ���������� x ������ y ����֮��ı����䣬����������
    /// </summary>
    /// <param name="_range">����ķ�Χ����</param>
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

    /// <returns>���ص�ǰ���ڵ���ά���</returns>
    public static int getLevel()
    {
        return clipLevel;
    }

    /// <summary>
    /// ������ά���
    /// </summary>
    /// <returns>���óɹ��򷵻� true�����򷵻� false��</returns>
    public static bool setLevel(int _l)
    {
        if (_l >= minLevel && _l <= maxLevel)
        {
            clipLevel = _l;
            return true;
        }
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

    private void initRect()
    {
        Vector2 localScaleRate = UITransform.getScreenScale(GetComponent<RectTransform>());
        Vector2 targetScaleRate = new Vector2(0.03f, 0.03f);
        // ������� 0
        if (localScaleRate.y == 0.0f) return;
        transform.localScale *= targetScaleRate.y / localScaleRate.y;
        return;
    }

    private void keyboardControl()
    {
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
    }
}
