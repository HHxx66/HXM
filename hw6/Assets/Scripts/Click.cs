using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    ClickAction clickAction;
    public void setClickAction(ClickAction clickAction){
        this.clickAction=clickAction;
    }
    void OnMouseDown(){
        clickAction.DealClick(this.gameObject);
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
