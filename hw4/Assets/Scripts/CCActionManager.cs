using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    private bool isMoving=false;
    public CCMoveToAction moveObjAction;
    public FirstController controller;

    protected new void Start()
    {
        controller=SSDirector.getInstance().currentSceneController as FirstController;
    }

    public bool IsMoving(){
        return isMoving;
    }
    public void MoveObj(GameObject obj,Vector3 target,float speed){
        if(isMoving) return;
        isMoving=true;
        moveObjAction=CCMoveToAction.GetSSAction(target,speed);
        this.RunAction(obj,moveObjAction,this);
    }
    public void SSActionEvent(SSAction source,
        SSActionEventType events=SSActionEventType.Competed,
        int intParam=0,
        string strParam=null,
        Object objectParam=null){
            isMoving=false;
        }
}
