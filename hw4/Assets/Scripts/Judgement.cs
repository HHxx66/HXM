using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{

    private FirstController controller;
    private LandModelController landRoleController;

    // Start is called before the first frame update
    void Start()
    {
        controller=SSDirector.getInstance().currentSceneController as FirstController;
        landRoleController=controller.GetLandModelController();
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.GetIsRuning()) return;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
		if(landRoleController.GetLandModel().rightPriestNum==3&&landRoleController.GetLandModel().rightDevilNum==3){
            controller.JudgeCallback(false,"You Win!!");
		}
		else if((landRoleController.GetLandModel().leftPriestNum!=0&&landRoleController.GetLandModel().leftPriestNum<landRoleController.GetLandModel().leftDevilNum)
			||(landRoleController.GetLandModel().rightPriestNum!=0&&landRoleController.GetLandModel().rightPriestNum<landRoleController.GetLandModel().rightDevilNum)){
			controller.JudgeCallback(false,"Game Over!!");
		}
    }
}
