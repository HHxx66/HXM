using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiation: MonoBehaviour
{

    void Start () {
        GameObject table = Resources.Load("table") as GameObject;
        Instantiate(table);
        table.transform.position = new Vector3(0, 6, 0);
        table.transform.parent = this.transform;
    }

    void Update () {
		
    }
}