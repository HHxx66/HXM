using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	private LandModelController landRoleController;
	private BoatController boatRoleController;
	private RoleModelController[] roleModelControllers;
	//private MoveController moveController;
	private bool isRuning;

	// the first scripts
	void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
        this.gameObject.AddComponent<Judgement>();
	}
	 
	// loading resources for the first scence
	public void LoadResources () {
		landRoleController=new LandModelController();
		landRoleController.CreateLand();
		roleModelControllers=new RoleModelController[6];
		for(int i=0;i<6;i++){
			roleModelControllers[i]=new RoleModelController();
			roleModelControllers[i].CreateRole(i<3? true:false,i);
			roleModelControllers[i].GetRoleModel().role.transform.localPosition=landRoleController.AddRole(roleModelControllers[i].GetRoleModel());
		}
		boatRoleController=new BoatController();
		boatRoleController.CreateBoat();
		//moveController=new MoveController();
		isRuning=true;
	}

	public LandModelController GetLandModelController(){
		return landRoleController;
	}
	public bool GetIsRuning(){
		return isRuning;
	}

	public void JudgeCallback(bool isRuning,string message){
        this.gameObject.GetComponent<UserGUI>().gameMessage=message;
        this.isRuning=isRuning;
    }

	public void Pause ()
	{
		throw new System.NotImplementedException ();
	}

	public void Resume ()
	{
		throw new System.NotImplementedException ();
	}

	#region IUserAction implementation
	public void MoveBoat(){
		if(!isRuning||this.gameObject.GetComponent<CCActionManager>().IsMoving()/*moveController.GetIsMoving()*/) return;
		if(boatRoleController.GetBoatModel().isRight){
			this.gameObject.GetComponent<CCActionManager>().MoveObj(boatRoleController.GetBoatModel().boat,new Vector3(3,-0.3f,-30),5);
			landRoleController.GetLandModel().leftPriestNum+=boatRoleController.GetBoatModel().priestNum;
			landRoleController.GetLandModel().leftDevilNum+=boatRoleController.GetBoatModel().devilNum;
			landRoleController.GetLandModel().rightPriestNum-=boatRoleController.GetBoatModel().priestNum;
			landRoleController.GetLandModel().rightDevilNum-=boatRoleController.GetBoatModel().devilNum;
		}
		else{
			this.gameObject.GetComponent<CCActionManager>().MoveObj(boatRoleController.GetBoatModel().boat,new Vector3(7.5f,-0.3f,-30),5);
			landRoleController.GetLandModel().leftPriestNum-=boatRoleController.GetBoatModel().priestNum;
			landRoleController.GetLandModel().leftDevilNum-=boatRoleController.GetBoatModel().devilNum;
			landRoleController.GetLandModel().rightPriestNum+=boatRoleController.GetBoatModel().priestNum;
			landRoleController.GetLandModel().rightDevilNum+=boatRoleController.GetBoatModel().devilNum;
		}
		boatRoleController.GetBoatModel().isRight=!boatRoleController.GetBoatModel().isRight;
	}
	public void MoveRole(RoleModel roleModel){
		if(!isRuning||this.gameObject.GetComponent<CCActionManager>().IsMoving()/*moveController.GetIsMoving()*/) return;
		if(roleModel.isInBoat){
			roleModel.isRight=boatRoleController.GetBoatModel().isRight;
			this.gameObject.GetComponent<CCActionManager>().MoveObj(roleModel.role,landRoleController.AddRole(roleModel),5);
			boatRoleController.RemoveRole(roleModel);
		}
		else if(boatRoleController.GetBoatModel().isRight==roleModel.isRight){
			landRoleController.RemoveRole(roleModel);
			this.gameObject.GetComponent<CCActionManager>().MoveObj(roleModel.role,boatRoleController.AddRole(roleModel),5);
		}
	}
	public void Restart ()
	{
		landRoleController.CreateLand();
		for(int i=0;i<6;i++){
			roleModelControllers[i].CreateRole(i<3? true:false,i);
			roleModelControllers[i].GetRoleModel().role.transform.localPosition=landRoleController.AddRole(roleModelControllers[i].GetRoleModel());
		}
		boatRoleController.CreateBoat();
		isRuning=true;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
	}
	public void Check(){
		if(!isRuning) return;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
		if(landRoleController.GetLandModel().rightPriestNum==3&&landRoleController.GetLandModel().rightDevilNum==3){
			this.gameObject.GetComponent<UserGUI>().gameMessage="You Win!!";
			isRuning=false;
		}
		else if((landRoleController.GetLandModel().leftPriestNum!=0&&landRoleController.GetLandModel().leftPriestNum<landRoleController.GetLandModel().leftDevilNum)
			||(landRoleController.GetLandModel().rightPriestNum!=0&&landRoleController.GetLandModel().rightPriestNum<landRoleController.GetLandModel().rightDevilNum)){
			this.gameObject.GetComponent<UserGUI>().gameMessage="Game Over!!";
			isRuning=false;
		}
	}
	#endregion


	// Use this for initialization
	void Start () {
		//give advice first
	}
	
	// Update is called once per frame
	void Update () {
		//give advice first
	}

}
