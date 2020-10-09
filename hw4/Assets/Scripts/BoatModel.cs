using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatModel
{

    public GameObject boat;
    public RoleModel[] roles;
    public bool isRight;
    public int priestNum,devilNum;

    public BoatModel(){
        priestNum=devilNum=0;
        roles=new RoleModel[2];
        boat=GameObject.Instantiate(Resources.Load("WoodBoat", typeof(GameObject))) as GameObject;
        boat.transform.position=new Vector3(3,-0.3f,-30);
        boat.AddComponent<BoxCollider>();
        boat.AddComponent<Click>();
        boat.GetComponent<BoxCollider>().size=new Vector3(1.5f,0.6f,2.5f);
        isRight=false;
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
