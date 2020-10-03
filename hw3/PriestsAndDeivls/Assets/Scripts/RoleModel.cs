using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModel// : MonoBehaviour
{

    public GameObject role;
    public bool isPriest;
    public int tag;
    public bool isRight;
    public bool isInBoat;
    public Vector3 rightPos;
    public Vector3 leftPos;

    public RoleModel(bool isPriest,int tag){
        this.isPriest=isPriest;
        this.tag=tag;
        isRight=false;
        isInBoat=false;
        rightPos=new Vector3(9,0,-33.3f+tag*1.1f);
        leftPos=new Vector3(1,0,-33.3f+tag*1.1f);
        role=GameObject.Instantiate(Resources.Load(isPriest?"Priests"+tag:"Devils", typeof(GameObject))) as GameObject;
        role.transform.position=leftPos;
        role.AddComponent<Click>();
        role.AddComponent<BoxCollider>();
        role.GetComponent<BoxCollider>().size=new Vector3(0.6f,3,0.6f);
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
