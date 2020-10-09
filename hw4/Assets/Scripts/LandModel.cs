using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandModel
{

    public GameObject land;
	public int leftPriestNum;
	public int leftDevilNum;
	public int rightPriestNum;
	public int rightDevilNum;
    public LandModel(){
        land=GameObject.Instantiate(Resources.Load("Environment", typeof(GameObject))) as GameObject;
        land.transform.position=new Vector3(0,0,0);
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
