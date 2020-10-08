using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    new Camera camera;
    private bool isDown = false;
    private Vector2 downPos;
    Character character;
    // Start is called before the first frame update
    private void Awake()
    {
        character = GetComponent<Character>();
    }
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.downPos = getMousePos();
            isDown = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }
      
        if (isDown)
        {
            Vector2 nowPos = getMousePos();
            Vector2 deltaPos = nowPos - this.downPos;
            if (deltaPos.magnitude > 0.01f)
            {
                float angle = Vector2.SignedAngle(Vector2.right, deltaPos);
                if (angle < 0)
                {
                    angle += 360;
                }
                character.dir = angle;
                if (deltaPos.magnitude > 0.15f)
                {
                    downPos = nowPos - deltaPos.normalized * 0.15f;
                }
            }
            else
            {
                character.dir = -1;
            }
        }
        else
        {
            character.dir = -1;
        }
    }
   

    Vector2 getMousePos()
    {
        //获得鼠标位置，转换成视口坐标，坐下角(0,0),右上角(1,1)
        Vector2 pos = camera.ScreenToViewportPoint(Input.mousePosition);
        //由于窗口不是1：1的，以宽为准，缩放高度
        pos.y = pos.y * Screen.height / Screen.width;
        return pos;
    }
}
