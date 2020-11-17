using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{

    public int area;

    public void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag=="Player"){
            GameEventManager.GetInstance().PlayerEnterArea(area);
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
