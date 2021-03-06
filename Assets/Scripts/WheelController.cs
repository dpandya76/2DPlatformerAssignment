﻿using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {

    // PRIVATE INSTANCE VARIABLES
    private Transform _transform;
    private Vector3 pos1 = new Vector3(1099, 270, 0);
    private Vector3 pos2 = new Vector3(1099, 40, 0);
    public float speed = 0.5f;


    // Use this for initialization
    void Start()
    {
        this._transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this._transform.Rotate(0, 0, 3f);
        this._transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

    }
}
