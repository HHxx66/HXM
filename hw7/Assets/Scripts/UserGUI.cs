using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

	private IUserAction action;
	public string gameMessage;
	public int score;

	void Start () {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
		score=0;
	}

	void OnGUI() {  
		float width = Screen.width / 6;  
		float height = Screen.height / 12;

		//action.Check();
		GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 30;
		GUI.Label(new Rect(320,100,50,200),gameMessage,style);
		GUI.Label(new Rect(width*2, 0, width, height),"Score: ",style);
		GUI.Label(new Rect(width*3, 0, width, height),score.ToString(),style);

		if (GUI.Button(new Rect(0, 0, width, height), "Restart")) {  
			action.Restart();  
		} 

		string paused_title = SSDirector.getInstance ().Paused ? "Resume" : "Pause!"; 
		if (GUI.Button(new Rect(width, 0, width, height), paused_title)) { 
			SSDirector.getInstance ().Paused = !SSDirector.getInstance ().Paused;
		} 
	}


}
