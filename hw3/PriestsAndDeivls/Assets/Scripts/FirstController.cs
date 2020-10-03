using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene Controller
/// Usage: host on a gameobject in the scene   
/// responsiablities:
///   acted as a scene manager for scheduling actors.log something ...
///   interact with the director and players
/// </summary>
public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	private LandModelController landRoleController;
	private BoatController boatRoleController;
	private RoleModelController[] roleModelControllers;
	private MoveController moveController;
	private bool isRuning;
	private int leftPriestNum;
	private int leftDevilNum;
	private int rightPriestNum;
	private int rightDevilNum;

	// the first scripts
	void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		this.gameObject.AddComponent<UserGUI>();
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
		moveController=new MoveController();
		leftPriestNum=leftDevilNum=3;
		rightPriestNum=rightDevilNum=0;
		isRuning=true;
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
		if(!isRuning||moveController.GetIsMoving()) return;
		if(boatRoleController.GetBoatModel().isRight){
			moveController.SetMove(new Vector3(3,-0.3f,-30),boatRoleController.GetBoatModel().boat);
			leftPriestNum+=boatRoleController.GetBoatModel().priestNum;
			leftDevilNum+=boatRoleController.GetBoatModel().devilNum;
			rightPriestNum-=boatRoleController.GetBoatModel().priestNum;
			rightDevilNum-=boatRoleController.GetBoatModel().devilNum;
		}
		else{
			moveController.SetMove(new Vector3(7.5f,-0.3f,-30),boatRoleController.GetBoatModel().boat);
			leftPriestNum-=boatRoleController.GetBoatModel().priestNum;
			leftDevilNum-=boatRoleController.GetBoatModel().devilNum;
			rightPriestNum+=boatRoleController.GetBoatModel().priestNum;
			rightDevilNum+=boatRoleController.GetBoatModel().devilNum;
		}
		boatRoleController.GetBoatModel().isRight=!boatRoleController.GetBoatModel().isRight;
	}
	public void MoveRole(RoleModel roleModel){
		if(!isRuning||moveController.GetIsMoving()) return;
		if(roleModel.isInBoat){
			roleModel.isRight=boatRoleController.GetBoatModel().isRight;
			moveController.SetMove(landRoleController.AddRole(roleModel),roleModel.role);
			boatRoleController.RemoveRole(roleModel);
		}
		else if(boatRoleController.GetBoatModel().isRight==roleModel.isRight){
			landRoleController.RemoveRole(roleModel);
			moveController.SetMove(boatRoleController.AddRole(roleModel),roleModel.role);
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
		leftPriestNum=leftDevilNum=3;
		rightPriestNum=rightDevilNum=0;
		isRuning=true;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
	}
	public void Check(){
		if(!isRuning) return;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
		if(rightPriestNum==3&&rightDevilNum==3){
			this.gameObject.GetComponent<UserGUI>().gameMessage="You Win!!";
			isRuning=false;
		}
		else if((leftPriestNum!=0&&leftPriestNum<leftDevilNum)||(rightPriestNum!=0&&rightPriestNum<rightDevilNum)){
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
