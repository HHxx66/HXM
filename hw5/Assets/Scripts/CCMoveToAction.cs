using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    public Vector3 target;
    public float speed;

    public static CCMoveToAction GetSSAction(Vector3 target, float speed){
        CCMoveToAction action=ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target=target;
        action.speed=speed;
        return action;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        this.transform.localPosition=Vector3.MoveTowards(this.transform.localPosition,target,speed*Time.deltaTime);
        if(this.gameObject==null||this.transform.localPosition==target){
            this.destroy=true;
            this.callback.SSActionEvent(this);
        }
    }
}
