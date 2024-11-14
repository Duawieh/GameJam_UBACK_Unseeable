using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    public float dropVelocity = 0.0f;

    public bool uponGround = false;
    public float horizonVelocity = 1.0f;
    public float gravityAccelerate = 1.0f;

    private void gravityBehaviours()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null) return;

        float rootHeight = rt.localPosition.y - (rt.rect.height / 2.0f);
        if (rootHeight > 0)
        {
            dropVelocity += gravityAccelerate * Time.deltaTime;
            rt.localPosition += new Vector3(0, -dropVelocity, 0) * rt.localScale.y;
        }
        else
        {
            dropVelocity = 0.0f;
            // 注意，此处乘法并非矩阵乘法，而是将对应分量相乘。
            rt.localPosition *= new Vector2(1, 0);
            rt.localPosition += new Vector3(0, rt.rect.height / 2.0f, 0);
            uponGround = false;
        }
    }

    private void keyboardControl()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += new Vector3(horizonVelocity, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition -= new Vector3(horizonVelocity, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!uponGround)
            {
                //transform.localPosition += new Vector3(0, 0.1f, 0);
                dropVelocity = -gravityAccelerate;
                uponGround = true;
            }
        }
        return;
    }

    private void initScale()
    {
        // 尺寸初始化
        Vector2 scaleRate = UITransform.getScreenScale(GetComponent<RectTransform>());
        if (scaleRate.y == 0.0f) return;
        transform.localScale *= 0.07f / scaleRate.y;

        // 向量长度初始化
        horizonVelocity = transform.localScale.x * 3.0f;
        gravityAccelerate = transform.localScale.y * 3.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        initScale();
    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
        gravityBehaviours();
    }
}
