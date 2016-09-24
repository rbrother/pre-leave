using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


class MenuItem {
    public GameObject gameObject { get; set; }
    public string infoText { get; set; }
}

public class menuContent : MonoBehaviour {

    int x = 1;
    int y = 1;
    Dictionary<Vector2, MenuItem> menuObjects = new Dictionary<Vector2, MenuItem>();
    Text currentlySelectedText;

    // Use this for initialization
    void Start() {
        //below gameobject initialization
        menuObjects.Add(new Vector2(1, 1), new MenuItem { gameObject = GameObject.Find("expeFileLogo"), infoText = "Experiment Management" } );
        menuObjects.Add(new Vector2(2, 1), new MenuItem { gameObject = GameObject.Find("settingsLogo"), infoText = "Settings"});
        menuObjects.Add(new Vector2(3, 1), new MenuItem { gameObject = GameObject.Find("exitLogo"), infoText = "Shutdown" });//GameObject.Find("exitLogo"));
        menuObjects.Add(new Vector2(1, 2), new MenuItem { gameObject = GameObject.Find("estimatedRecordLogo"), infoText = "Estimated Record" });//GameObject.Find("estimatedRecordLogo"));
        menuObjects.Add(new Vector2(2, 2), new MenuItem { gameObject = GameObject.Find("personalFilesLogo"), infoText = "Personal Files" });//GameObject.Find("personalFilesLogo"));
        menuObjects.Add(new Vector2(3, 2), new MenuItem { gameObject = GameObject.Find("GCR_Logo"), infoText = "Access GCR" });//GameObject.Find("GCR_Logo"));
        currentlySelectedText = GameObject.Find("currentSelectedOne").GetComponent<Text>();
        Debug.Log("Dotnet version: " + Environment.Version.ToString());
        Highlighting(x,y,x,y);
    }

    void Highlighting(int oldX,int oldY,int newX,int newY) {
        var toBeDehighlighted = menuObjects[new Vector2(oldX, oldY)].gameObject;
        var unHighlightImageComponent = toBeDehighlighted.GetComponent<RawImage>();
        unHighlightImageComponent.color = new Color(1, 1, 1, 0.25f);

        var menuGameItem = menuObjects[new Vector2(newX, newY)];
        var toBeHighlighted = menuGameItem.gameObject;
        var imageComponent = toBeHighlighted.GetComponent<RawImage>();
        imageComponent.color = new Color(1,1,1,1);
        currentlySelectedText.text = menuGameItem.infoText;
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
