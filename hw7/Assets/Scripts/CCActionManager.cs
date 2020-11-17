using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{

    private int currentArea=-1;
    Dictionary<int,CCMoveToAction> moveToActions=new Dictionary<int,CCMoveToAction>();

    protected new void Start()
    {
        
    }

    public void Trace(GameObject Zombie,GameObject player){
        var area=Zombie.GetComponent<Zombie>().area;
        if(area==currentArea){
            return;
        }
        currentArea=area;
        if(moveToActions.ContainsKey(area)){
            moveToActions[area].destroy=true;
        }
        CCTraceAction action=CCTraceAction.GetSSAction(player,25);
        this.RunAction(Zombie,action,this);
    }

    public void GoAround(GameObject Zombie){
        var area=Zombie.GetComponent<Zombie>().area;
        if(moveToActions.ContainsKey(area)){
            return;
        }
        var target=GetGoAroundTarget(Zombie);
        CCMoveToAction action=CCMoveToAction.GetSSAction(target,12,area);
        moveToActions.Add(area,action);
        this.RunAction(Zombie,action,this);
    }

    public void Stop(){
        foreach(var x in moveToActions.Values){
            x.destroy=true;
        }
        moveToActions.Clear();
        currentArea=-1;
    }

    private Vector3 GetGoAroundTarget(GameObject Zombie){
        Vector3 pos=Zombie.transform.position;
        var area=Zombie.GetComponent<Zombie>().area;
        float x_down=Map.center[area].x-27.5f;
        float x_up=Map.center[area].x+27.5f;
        float z_down=Map.center[area].z-40;
        float z_up=Map.center[area].z+40;
        var move=new Vector3(Random.Range(-15,15),0,Random.Range(-15,15));
        var next=pos+move;
        int tryCount=0;
        while(!(next.x>x_down&&next.x<x_up&&next.z>z_down&&next.z<z_up)||next==pos){
            move=new Vector3(Random.Range(-15,15),0,Random.Range(-15,15));
            next=pos+move;
            if((++tryCount)>100){
                next=Map.center[area];
                break;
            }
        }
        return next;
    }

    public void SSActionEvent(SSAction source,
        SSActionEventType events=SSActionEventType.Competed,
        int intParam=0,
        string strParam=null,
        Object objectParam=null){
            var area=source.gameObject.GetComponent<Zombie>().area;
            if(moveToActions.ContainsKey(area)){
                moveToActions.Remove(area);
            }
        }
}
