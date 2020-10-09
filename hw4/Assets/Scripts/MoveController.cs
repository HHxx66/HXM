using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{

    private GameObject moveObject;
    public bool GetIsMoving(){
        return moveObject!=null&&moveObject.GetComponent<Move>().isMoving;
    }
    public void SetMove(Vector3 des,GameObject moveObject){
        Move test;
        this.moveObject=moveObject;
        if(!moveObject.TryGetComponent<Move>(out test)) moveObject.AddComponent<Move>();
        this.moveObject.GetComponent<Move>().des=des;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
