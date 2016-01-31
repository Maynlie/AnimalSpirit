using UnityEngine;
using System.Collections;

public class TotemManager : MonoBehaviour {
    public int NbCharges;
    public int ChargeMax;
    public Sprite[] sprites;
    float ActiveTime;
	// Use this for initialization
	void Start () {
        ActiveTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time - ActiveTime > 1.0f)
        GetComponent<SpriteRenderer>().sprite = sprites[NbCharges];
	}

    void ChargeUp()
    {
        if(NbCharges<ChargeMax)
        {
            NbCharges++;
            GameObject.Find("Main Camera").GetComponent<Collectible>().manaCollecté--;
            if(NbCharges == ChargeMax)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[ChargeMax+1];
                ActiveTime = Time.time;
            }
        }
    }
}
