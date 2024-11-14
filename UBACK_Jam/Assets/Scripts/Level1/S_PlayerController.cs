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
            rt.localPosition += new Vector3(0, -dropVelocity, 0) * Time.deltaTime;
        }
        else
        {
            dropVelocity = 0.0f;
            // ????????????????????????????????????
            rt.localPosition *= new Vector2(1, 0);
            rt.localPosition += new Vector3(0, rt.rect.height / 2.0f, 0);
            uponGround = false;
        }
    }

    private void keyboardControl()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // if (!kickWall(new Vector3(1, 0, 0), transform.localPosition)) {
                transform.localPosition += new Vector3(horizonVelocity, 0, 0) * Time.deltaTime;
            // }
        }
        if (Input.GetKey(KeyCode.A))
        {
            // if (!kickWall(new Vector3(-1, 0, 0), transform.localPosition)) {
                transform.localPosition -= new Vector3(horizonVelocity, 0, 0) * Time.deltaTime;
            // }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!uponGround)
            {
                transform.localPosition += new Vector3(0, gravityAccelerate * 0.01f, 0);
                dropVelocity = -gravityAccelerate * 0.35f;
                uponGround = true;
            }
        }
        return;
    }

    private void initScale()
    {
        // ???????
        Vector2 scaleRate = UITransform.getScreenScale(GetComponent<RectTransform>());
        if (scaleRate.y == 0.0f) return;
        transform.localScale *= 0.07f / scaleRate.y;

        // ????????????
        horizonVelocity = transform.localScale.x * 3.0f;
        gravityAccelerate = transform.localScale.y * 10.0f;
    }

    private bool kickWall(Vector3 _walkDir, Vector3 _curPos) {
        _walkDir = _walkDir / _walkDir.magnitude * horizonVelocity * Time.deltaTime;
        _curPos = GameMap.getIntPos(_curPos);
        Vector3 checkPos = transform.localPosition + _walkDir;
        Vector3 intPos = GameMap.getIntPos(checkPos);
        if (_curPos.y < checkPos.y) return true;
        else return false;
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
