using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    float gravity;
    float speed;
    Vector3 direction;
    float time;

    public static CCFlyAction GetSSAction(Vector3 direction, float speed){
        CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
        action.gravity = 9.8f;
        action.time = 0;
        action.speed = speed;
        action.direction = direction;
        return action;
    }

    public override void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void Update(){
        time+=Time.deltaTime;
        transform.Translate(Vector3.down*gravity*time*Time.deltaTime);
        transform.Translate(direction*speed*Time.deltaTime);
        if (this.transform.position.y<-10){
            this.destroy=true;
            this.enable=false;
            this.callback.SSActionEvent(this);
        }
    }
}
