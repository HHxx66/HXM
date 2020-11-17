using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    public Vector3 target;
    public float speed;
    public int area;

    public static CCMoveToAction GetSSAction(Vector3 target, float speed, int area){
        CCMoveToAction action=ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target=target;
        action.speed=speed;
        action.area=area;
        return action;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        if(target-gameObject.transform.position!=Vector3.zero){
            Quaternion rotation=Quaternion.LookRotation(target-gameObject.transform.position,Vector3.up);
            gameObject.transform.rotation=rotation;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if((gameObject.transform.position-target).magnitude<0.1f){
            destroy=true;
            callback.SSActionEvent(this);
        }
        else{
            gameObject.transform.position=Vector3.MoveTowards(gameObject.transform.position,target,speed*Time.deltaTime);
        }
    }
}
