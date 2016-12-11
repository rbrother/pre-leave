using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


class MenuItem {
    public GameObject gameObject { get; set; }
    public string infoText { get; set; }
    public Action codeblock { get; set; }
}

public class menuContent : MonoBehaviour {

    int x = 1;
    int y = 1;
    Dictionary<Vector2, MenuItem> menuObjects = new Dictionary<Vector2, MenuItem>();
    Text currentlySelectedText;
    GameObject canvas;
    GameObject settingsPanel;
    RectTransform settingsTransform;
    GameObject estimationTxt;
    Text record;
    int msRecord;
    GameObject recElmnts;
    bool windowActive;

    // Use this for initialization
    void Start() {
        //below gameobject initialization
        menuObjects.Add(new Vector2(1, 1), 
            new MenuItem { gameObject = GameObject.Find("expeFileLogo"), infoText = "Experiment Management", codeblock = ExpeCode } );
        menuObjects.Add(new Vector2(2, 1), 
            new MenuItem { gameObject = GameObject.Find("settingsLogo"), infoText = "Settings", codeblock = settingsCode });
        menuObjects.Add(new Vector2(3, 1), 
            new MenuItem { gameObject = GameObject.Find("exitLogo"), infoText = "Shutdown", codeblock = shutdownCode });
        menuObjects.Add(new Vector2(1, 2), 
            new MenuItem { gameObject = GameObject.Find("estimatedRecordLogo"), infoText = "Estimated Record", codeblock = estimation });
        menuObjects.Add(new Vector2(2, 2), 
            new MenuItem { gameObject = GameObject.Find("personalFilesLogo"), infoText = "Personal Files" });
        menuObjects.Add(new Vector2(3, 2),
            new MenuItem { gameObject = GameObject.Find("GCR_Logo"), infoText = "Access GCR" });
        currentlySelectedText = GameObject.Find("currentSelectedOne").GetComponent<Text>();
        canvas = GameObject.Find("Canvas");
        settingsPanel = GameObject.Find("settingsPanel");
        settingsPanel.SetActive(false);
        estimationTxt = GameObject.Find("estimationtext");
        recElmnts = GameObject.Find("recordElements");

        //below beginning function executing
        Highlighting(x, y, x, y);

        //below component initialization
        record = estimationTxt.GetComponent<Text>();
        //unHighlightImageComponent = toBeDehighlighted.GetComponent<RawImage>();
        settingsTransform = settingsPanel.GetComponent<RectTransform>();

        //below int initialization
        msRecord = 750;
        recElmnts.SetActive(false);
        windowActive = false;
    }

    void ExpeCode() {
        //strictlyMenuContent.SetActive(false);
    }

    void settingsCode() {
        settingsPanel.SetActive(settingsPanel.activeSelf ? false : true);
    }

    void shutdownCode() {
        Application.Quit();
    }

    void estimation() {
        if (!recElmnts.activeSelf) {
            recElmnts.SetActive(true);
            record.text = "retrieving information from the current experiment \nexperiment2.overview.estimatedrecord:\n\n" + "\t\t\t\t\t\t" + msRecord;
        }else {
            recElmnts.SetActive(false);
        }
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
            windowActive = windowActive ? false : true;
            menuObjects[new Vector2(x, y)].codeblock();
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("w")) && !windowActive) {
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
