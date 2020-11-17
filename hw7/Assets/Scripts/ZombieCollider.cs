using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollider : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag=="Player"){
            GameEventManager.GetInstance().ZombieCollideWithPlayer();
        }
        else{

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
