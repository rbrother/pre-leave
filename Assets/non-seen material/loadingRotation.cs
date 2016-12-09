using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class loadingRotation : MonoBehaviour {

    RectTransform loadTransform;
    float clock;
    float aika;

    // Use this for initialization
    void Start () {
        loadTransform = GetComponent<RectTransform>();

        clock = 0;

        aika = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        clock = -360 * ((Time.time - aika) / 1);
        if (clock < -360) {
            aika = Time.time;
            clock = 0;
        }
        loadTransform.localEulerAngles = new Vector3(0, 0, clock);
	}
}
