using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandModelController
{

    private LandModel landModel;

    public void CreateLand(){
        if(landModel==null) landModel=new LandModel();
    }
    public LandModel GetLandModel(){
        return landModel;
    }
    public Vector3 AddRole(RoleModel roleModel){
        roleModel.role.transform.parent=this.landModel.land.transform;
        roleModel.isInBoat=false;
        if(roleModel.isRight) return roleModel.rightPos;
        else return roleModel.leftPos;
    }
    public void RemoveRole(RoleModel roleModel){

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
