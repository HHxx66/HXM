using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : ClickAction
{
    private static UFOFactory _instance;

    public GameObject UFORed_Prefab;
    public GameObject UFOGreen_Prefab;
    public GameObject UFOBlue_Prefab;
    private List<UFOData> used;
    private List<UFOData> free;
    public FirstController controller;

    private UFOFactory(){
        used=new List<UFOData>();
        free=new List<UFOData>();
        UFORed_Prefab=GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("UFORed"), Vector3.zero, Quaternion.identity);
        UFOGreen_Prefab=GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("UFOGreen"), Vector3.zero, Quaternion.identity);
        UFOBlue_Prefab=GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("UFOBlue"), Vector3.zero, Quaternion.identity);
        UFORed_Prefab.SetActive(false);
        UFOGreen_Prefab.SetActive(false);
        UFOBlue_Prefab.SetActive(false);
        controller=SSDirector.getInstance().currentSceneController as FirstController;
    }

    public static UFOFactory getInstance() {
		if (_instance == null) {
			_instance = new UFOFactory();
		}
		return _instance;
	}

    public GameObject GetUFO(int round){
        GameObject ufo;
        if(free.Count>0){
            ufo=free[0].gameObject;
            free.Remove(free[0]);
        }
        else{
            float kind=UnityEngine.Random.Range(0f,round+1);
            if(kind<0.5f){
                ufo=GameObject.Instantiate<GameObject>(UFOBlue_Prefab, Vector3.zero, Quaternion.identity);
                ufo.AddComponent<UFOData>();
                ufo.AddComponent<BoxCollider>();
                ufo.AddComponent<Click>();
                ufo.GetComponent<UFOData>().score=1;
            }
            else if(kind<2.5f){
                ufo=GameObject.Instantiate<GameObject>(UFOGreen_Prefab, Vector3.zero, Quaternion.identity);
                ufo.AddComponent<UFOData>();
                ufo.AddComponent<BoxCollider>();
                ufo.AddComponent<Click>();
                ufo.GetComponent<UFOData>().score=2;
            }
            else{
                ufo=GameObject.Instantiate<GameObject>(UFORed_Prefab, Vector3.zero, Quaternion.identity);
                ufo.AddComponent<UFOData>();
                ufo.AddComponent<BoxCollider>();
                ufo.AddComponent<Click>();
                ufo.GetComponent<UFOData>().score=3;
            }
            ufo.GetComponent<Click>().setClickAction(this);
        }
        ufo.GetComponent<UFOData>().direction=new Vector3(UnityEngine.Random.Range(-2f,2f),1,0);
        float level=UnityEngine.Random.Range(0f,round);
        if(level<3){
            ufo.GetComponent<UFOData>().speed=3.0f+round/6;
        }
        else if(level<6){
            ufo.GetComponent<UFOData>().speed=4.0f+round/6;
        }
        else{
            ufo.GetComponent<UFOData>().speed=6.0f+round/6;
        }
        level=UnityEngine.Random.Range(0f,round);
        if(level<3){
            ufo.GetComponent<UFOData>().scale=3;
            ufo.GetComponent<BoxCollider>().size=new Vector3(3f,2f,3f);
        }
        else if(level<6){
            ufo.GetComponent<UFOData>().scale=2;
            ufo.GetComponent<BoxCollider>().size=new Vector3(4f,3f,4f);
        }
        else{
            ufo.GetComponent<UFOData>().scale=1;
            ufo.GetComponent<BoxCollider>().size=new Vector3(5f,4f,5f);
        }
        used.Add(ufo.GetComponent<UFOData>());
        return ufo;
    }

    public void FreeUFO(GameObject ufo){
        foreach(UFOData ufoData in used){
            if(ufoData.gameObject.GetInstanceID()==ufo.GetInstanceID()){
                ufo.SetActive(false);
                free.Add(ufoData);
                used.Remove(ufoData);
                break;
            }
        }
    }

    public void FreeALL(){
        foreach(UFOData ufoData in used){
            ufoData.gameObject.SetActive(false);
            free.Add(ufoData);
            used.Remove(ufoData);
        }
        used.Clear();
    }

    public void DealClick(GameObject ufo){
        controller.JudgeCallback(true,ufo.GetComponent<UFOData>().score+(int)ufo.GetComponent<UFOData>().speed-3+ufo.GetComponent<UFOData>().scale-1);
        FreeUFO(ufo);
    }
}
