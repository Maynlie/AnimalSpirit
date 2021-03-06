﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour {
    public Camera _gameCam;
    public Text _numberOfMana;
    public int manaCollecté = 0;
	// Use this for initialization
	void Start () {
        
	
	}
	
	// Update is called once per frame
	void Update () {
        _numberOfMana.text = "Nombre de mana : " + manaCollecté;
        Vector3 myMousePos = Input.mousePosition;
        myMousePos.z = Mathf.Abs(_gameCam.transform.position.z - transform.position.z);
        Vector3 mousePosWorld = _gameCam.ScreenToWorldPoint(myMousePos);
        RaycastHit hit;
        LayerMask collectibleLayer = 8;
        if(Physics.Raycast(mousePosWorld,Vector3.forward,out hit,collectibleLayer))
        {
            if (hit.collider.tag == "Collectible")
            {
                hit.collider.gameObject.SetActive(false);
                manaCollecté++;
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Boutton");
            if (Physics.Raycast(mousePosWorld,Vector3.forward, out hit))
            {
                Debug.Log("Totem");

                if (hit.collider.tag == "TRenard" && manaCollecté>0)//&& NbCharge < ChargeMax
                {
                    Debug.Log(hit.ToString());
                    hit.collider.gameObject.SendMessage("ChargeUp");
                    //int max = hit.collider.GetComponent<>(TotemManager).ChargesMax;
                    //NbCharge++
                }
            }
        }

    }
}
