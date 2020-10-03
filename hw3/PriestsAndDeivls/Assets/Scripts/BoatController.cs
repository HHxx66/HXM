using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : ClickAction
{

    BoatModel boatModel;
    IUserAction userAction;

    public BoatController(){
        userAction=SSDirector.getInstance().currentSceneController as IUserAction;
    }
    public void CreateBoat(){
        if(boatModel!=null) Object.DestroyImmediate(boatModel.boat);
        boatModel=new BoatModel();
        boatModel.boat.GetComponent<Click>().setClickAction(this);
    }
    public BoatModel GetBoatModel(){
        return boatModel;
    }
    public Vector3 AddRole(RoleModel roleModel){
        if(boatModel.roles[0]==null){
            boatModel.roles[0]=roleModel;
            roleModel.isInBoat=true;
            roleModel.role.transform.parent=boatModel.boat.transform;
            if(roleModel.isPriest) boatModel.priestNum++;
            else boatModel.devilNum++;
            return new Vector3(-0.2f,0.2f,0.5f);
        }
        if(boatModel.roles[1]==null){
            boatModel.roles[1]=roleModel;
            roleModel.isInBoat=true;
            roleModel.role.transform.parent=boatModel.boat.transform;
            if(roleModel.isPriest) boatModel.priestNum++;
            else boatModel.devilNum++;
            return new Vector3(-0.2f,0.2f,-0.6f);
        }
        return roleModel.role.transform.localPosition;
    }
    public void RemoveRole(RoleModel roleModel){
        roleModel.role.transform.parent=null;
        if(boatModel.roles[0]==roleModel){
            boatModel.roles[0]=null;
            if(roleModel.isPriest) boatModel.priestNum--;
            else boatModel.devilNum--;
        }
        else if(boatModel.roles[1]==roleModel){
            boatModel.roles[1]=null;
            if(roleModel.isPriest) boatModel.priestNum--;
            else boatModel.devilNum--;
        }
    }
    public void DealClick(){
        if(boatModel.roles[0]!=null||boatModel.roles[1]!=null){
            userAction.MoveBoat();
        }
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
