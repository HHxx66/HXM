using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTraceAction : SSAction
{

    public GameObject target;
    public float speed;

    public static CCTraceAction GetSSAction(GameObject target,float speed){
        CCTraceAction action = CreateInstance<CCTraceAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        gameObject.transform.position=Vector3.MoveTowards(gameObject.transform.position,target.transform.position,speed*Time.deltaTime);
        if(gameObject.GetComponent<Zombie>().isFollowing==false||(gameObject.transform.position-target.transform.position).sqrMagnitude<0.1f){
            destroy=true;
            callback.SSActionEvent(this);
        }
        else{
            Quaternion rotation=Quaternion.LookRotation(target.transform.position-gameObject.transform.position,Vector3.up);
            gameObject.transform.rotation=rotation;
        }
    }
}
