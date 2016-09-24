using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class menuContent : MonoBehaviour {

    string selectedOption;

    int x = 1;
    int y = 1;
    Dictionary<Vector2, GameObject> menuObjects = new Dictionary<Vector2, GameObject>();

    // Use this for initialization
    void Start() {
        //below gameobject initialization
        menuObjects.Add(new Vector2(1, 1), GameObject.Find("expeFileLogo"));
        menuObjects.Add(new Vector2(2, 1), GameObject.Find("settingsLogo"));
        menuObjects.Add(new Vector2(3, 1), GameObject.Find("exitLogo"));
        menuObjects.Add(new Vector2(1, 2), GameObject.Find("estimatedRecordLogo"));
        menuObjects.Add(new Vector2(2, 2), GameObject.Find("personalFilesLogo"));
        menuObjects.Add(new Vector2(3, 2), GameObject.Find("GCR_Logo"));
    }

    void Highlighting(int oldX,int oldY,int newX,int newY) {
        Debug.Log(oldX + " " + oldY + " " + newX + " " + newY);
        var toBeDehighlighted = menuObjects[new Vector2(oldX, oldY)];
        var unHighlightImageComponent = toBeDehighlighted.GetComponent<RawImage>();
        unHighlightImageComponent.color = new Color(1, 1, 1, 0.25f);

        var toBeHighlighted = menuObjects[new Vector2(newX, newY)];
        var imageComponent = toBeHighlighted.GetComponent<RawImage>();
        imageComponent.color = new Color(1,1,1,1);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("e")) {
        }

        if (Input.GetKeyDown("d") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("w")) {
            int oldX = x;
            int oldY = y;
            if (Input.GetKeyDown("d") && x < 3) {
                x++;
            } else if (Input.GetKeyDown("a") && x > 1) {
                x--;
            } else if (Input.GetKeyDown("w") && y == 2) {
                y--;
            } else if (Input.GetKeyDown("s") && y == 1) {
                y++;
            }
            Highlighting(oldX,oldY,x,y);
        }
    }
}

//link to dictionary tutuorial: http://www.dotnetperls.com/dictionary
