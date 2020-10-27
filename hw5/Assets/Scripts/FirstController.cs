using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	private bool isRuning;

	public UFOFactory ufoFactory;
	private int[] roundUFOs;
	private int score;
	private int round;
	private int trial;
	private float sendTime;

	// the first scripts
	void Awake () {
		SSDirector director = SSDirector.getInstance ();
		director.setFPS (60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
		this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
		ufoFactory=UFOFactory.getInstance();
		roundUFOs=new int[]{2,3,3,4,4,4,5,5,5,5};
		score=round=trial=0;
		sendTime=0;
	}
	 
	// loading resources for the first scence
	public void LoadResources () {
		isRuning=true;
	}

	public void SendUFO(){
        GameObject ufo=ufoFactory.GetUFO(round);
        ufo.transform.position=new Vector3(ufo.GetComponent<UFOData>().direction.x>0? -9:9,UnityEngine.Random.Range(0f,8f),0);
        ufo.SetActive(true);
        this.gameObject.GetComponent<CCActionManager>().Fly(ufo,ufo.GetComponent<UFOData>().speed,ufo.GetComponent<UFOData>().direction);
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

	#region IUserAction implementation
	public void Restart ()
	{
		isRuning=true;
		this.gameObject.GetComponent<UserGUI>().gameMessage="";
		this.gameObject.GetComponent<UserGUI>().score=0;
		score=round=trial=0;
		sendTime=0;
		ufoFactory.FreeALL();
	}
	#endregion


	// Use this for initialization
	void Start () {
		//give advice first
	}
	
	// Update is called once per frame
	void Update () {
		//give advice first
		if(isRuning){
			sendTime+=Time.deltaTime;
			if(sendTime>1){
				sendTime=0;
				int count=(int)UnityEngine.Random.Range(0f,roundUFOs[round]);
				for(int i=0;i<count;i++){
					SendUFO();
				}
				if(round==9){
					gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
					isRuning=false;
				}
				if(trial==10){
					round++;
					trial=0;
				}
				else trial++;
			}
		}
	}

}
