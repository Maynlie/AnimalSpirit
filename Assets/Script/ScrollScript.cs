﻿using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void ScrollR()
    {
        Vector2 offset = new Vector2(-Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
    void ScrollL()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
