using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public bool isMoving=false;
    public float speed=3;
    public Vector3 des;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition==des){
            isMoving=false;
            return;
        }
        isMoving=true;
        transform.localPosition=Vector3.MoveTowards(transform.localPosition,des,speed*Time.deltaTime);
    }
}
