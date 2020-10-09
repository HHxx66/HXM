using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModelController : ClickAction
{

    RoleModel roleModel;
    IUserAction userAction;
    public RoleModelController(){
        userAction=SSDirector.getInstance().currentSceneController as IUserAction;
    }
    public void CreateRole(bool isPriest,int tag){
        if(roleModel!=null) Object.DestroyImmediate(roleModel.role);
        roleModel=new RoleModel(isPriest,tag);
        roleModel.role.GetComponent<Click>().setClickAction(this);
    }
    public RoleModel GetRoleModel(){
        return roleModel;
    }
    public void DealClick(){
        userAction.MoveRole(roleModel);
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
