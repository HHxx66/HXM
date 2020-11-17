using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Object
{
    private static GameObject planePrefab=Resources.Load<GameObject>("Prefabs/Plane");
    public static Vector3[] center=new Vector3[]{new Vector3(-55,0,-300),new Vector3(0,0,-300),new Vector3(55,0,-300),new Vector3(-55,0,-140),new Vector3(0,0,-115),new Vector3(55,0,-90),new Vector3(-30,0,-17),new Vector3(30,0,-17),new Vector3(0,0,50),new Vector3(55,0,120),new Vector3(-55,0,120)};

    public static void LoadPlane(){
        GameObject map=Instantiate(planePrefab);
    }
}
