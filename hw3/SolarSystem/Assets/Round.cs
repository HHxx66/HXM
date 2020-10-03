using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    public Transform center;
    public int y,z,speed;
    // Start is called before the first frame update
    void Start()
    {
        y = Random.Range(-10, 10);
        z = Random.Range(-10, 10);
        speed = Random.Range(10, 100);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.transform.position, new Vector3(0, y, z), speed * Time.deltaTime);
    }
}
