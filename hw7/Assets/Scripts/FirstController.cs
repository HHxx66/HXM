using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	private bool isRuning;
	private int score;

	private CCActionManager actionManager;
	private GameEventManager eventManager = GameEventManager.GetInstance();
	private GameObject player;
	private GameObject BlueIdol;
    private List<GameObject> Zombies = new List<GameObject>();
	private int currentArea = 99;

	public GameObject plane;

	// the first scripts
	void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
		actionManager=this.gameObject.GetComponent<CCActionManager>();
		GameEventManager.onPlayerEnterArea+=OnPlayerEnterArea;
		GameEventManager.onZombieCollideWithPlayer+=OnZombieCollideWithPlayer;
		GameEventManager.onPlayerCollideWithBlueIdol+=OnPlayerCollideWithBlueIdol;
		score=0;
	}
	 
	// loading resources for the first scence
	public void LoadResources () {
		isRuning=true;
		// Map.LoadPlane();
		plane.SetActive(true);
		LoadZombies();
		LoadPlayer();
		LoadBlueIdol();
	}

	public bool GetIsRuning(){
		return isRuning;
	}

	public void JudgeCallback(bool isRuning,int score){
		this.score+=score;
        this.gameObject.GetComponent<UserGUI>().score=this.score;
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

	private void OnPlayerEnterArea(int area){
		if(isRuning&&currentArea!=area){
			if(currentArea!=99){
				score++;
				this.gameObject.GetComponent<UserGUI>().score=this.score;
				Zombies[currentArea].GetComponent<Zombie>().isFollowing=false;
			}
			currentArea=area;
			Zombies[currentArea].GetComponent<Zombie>().isFollowing=true;
			actionManager.Trace(Zombies[currentArea],player);
		}
	}

	private void OnZombieCollideWithPlayer(){
		isRuning=false;
		player.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("isDead");
		this.gameObject.GetComponent<UserGUI>().gameMessage="You Lose!!";
		player.tag="Finish";
		player.GetComponent<Rigidbody>().isKinematic=true;
		Zombies[currentArea].GetComponent<Zombie>().isFollowing=false;
		actionManager.Stop();
		for(int i=0;i<11;i++){
			Zombies[i].GetComponent<Animator>().SetTrigger("isWin");
		}
	}

	private void OnPlayerCollideWithBlueIdol(){
		isRuning=false;
		player.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("isWin");
		BlueIdol.GetComponent<Animator>().SetTrigger("isWin");
		this.gameObject.GetComponent<UserGUI>().gameMessage="You Win!!";
		player.tag="Finish";
		player.GetComponent<Rigidbody>().isKinematic=true;
		Zombies[currentArea].GetComponent<Zombie>().isFollowing=false;
		actionManager.Stop();
		for(int i=0;i<11;i++){
			Zombies[i].GetComponent<Animator>().SetTrigger("isDead");
		}
	}

	private void LoadZombies(){
		GameObject ZombiePrefab=Resources.Load<GameObject>("Prefabs/Zombie");
		for(int i=0;i<11;i++){
			GameObject Zombie = Instantiate(ZombiePrefab);
			Zombie.AddComponent<Zombie>().area=i;
			Zombie.GetComponent<Rigidbody>().freezeRotation=true;
			Zombie.AddComponent<ZombieCollider>();
			Zombie.name="Zombie"+i;
			Zombie.GetComponent<Animator>().Play("Initial State");
			Zombie.GetComponent<Animator>().SetBool("isRunning",true);
			Zombie.transform.position=Map.center[i]+new Vector3(0,5,0);
			Zombies.Add(Zombie);
		}
	}

	private void LoadPlayer(){
		player=Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
		player.GetComponent<Rigidbody>().freezeRotation=true;
		player.AddComponent<PlayerCollider>();
		player.transform.position=new Vector3(0,0,-380);
		player.tag="Player";
	}

	private void LoadBlueIdol(){
		BlueIdol=Instantiate(Resources.Load<GameObject>("Prefabs/BlueIdol"));
		BlueIdol.GetComponent<Rigidbody>().freezeRotation=true;
		BlueIdol.transform.position=new Vector3(0,0,140);
		BlueIdol.tag="Finish";
	}

	#region IUserAction implementation
	public void Restart ()
	{
		isRuning=true;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
		this.gameObject.GetComponent<UserGUI>().score=0;
		score=0;
		currentArea=99;
		player.transform.position=new Vector3(0,0,-380);
		player.GetComponent<Rigidbody>().isKinematic=false;
		player.transform.rotation=Quaternion.AngleAxis(0,Vector3.up);
		player.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("Initial State");
		for(int i=0;i<11;i++)
		{
			Zombies[i].GetComponent<Animator>().Play("Initial State");
			Zombies[i].GetComponent<Animator>().SetBool("isRunning",true);
			Zombies[i].transform.position=Map.center[i]+new Vector3(0,5,0);
			Zombies[i].GetComponent<Zombie>().isFollowing=false;
			actionManager.GoAround(Zombies[i]);
		}
		player.tag="Player";
	}
	#endregion


	// Use this for initialization
	void Start () {
		//give advice first
	}
	
	// Update is called once per frame
	void Update () {
		if(isRuning){
			for(int i=0;i<11;i++){
				if(i!=currentArea){
					actionManager.GoAround(Zombies[i]);
				}
				else{
					Zombies[i].GetComponent<Zombie>().isFollowing=true;
					actionManager.Trace(Zombies[i],player);
				}
			}
		}
	}

}
