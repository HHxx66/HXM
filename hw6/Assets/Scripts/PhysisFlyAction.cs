using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisFlyAction : SSAction
{
    float speed;
    Vector3 direction;

    public static PhysisFlyAction GetSSAction(Vector3 direction, float speed){
        PhysisFlyAction action = ScriptableObject.CreateInstance<PhysisFlyAction>();
        action.speed = speed;
        action.direction = direction;
        return action;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().velocity = speed * direction;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (this.transform.position.y<-10){
            this.destroy=true;
            this.enable=false;
            this.callback.SSActionEvent(this);
        }
    }
}
