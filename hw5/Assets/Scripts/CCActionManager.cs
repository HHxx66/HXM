using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    public CCFlyAction flyAction;
    public FirstController controller;

    protected new void Start()
    {
        controller=SSDirector.getInstance().currentSceneController as FirstController;
    }

    public void Fly(GameObject obj, float speed, Vector3 direction){
        flyAction=CCFlyAction.GetSSAction(direction,speed);
        this.RunAction(obj,flyAction,this);
    }
    public void SSActionEvent(SSAction source,
        SSActionEventType events=SSActionEventType.Competed,
        int intParam=0,
        string strParam=null,
        Object objectParam=null){
            controller.ufoFactory.FreeUFO(source.gameObject);
        }
}
