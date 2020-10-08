using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 deltaPos;
    void Start()
    {
        deltaPos = new Vector3(0, 5.1f, -8.8f);
        //print("aaaa");
        //deltaPos =  transform.position-player.position;
        //print("deltaPos:" + deltaPos);
    }

    public void init(Transform player)
    {
        this.player = player;
    }
    // Update is called once per frame
   
    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + deltaPos;
        }
        
    }
}
