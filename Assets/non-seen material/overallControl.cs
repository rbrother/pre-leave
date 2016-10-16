using UnityEngine;
using System.Collections;
using System;

public class overallControl : MonoBehaviour {

    GameObject canvas;
    GameObject settings;
    bool audioOn;
    GameObject inGameGuide;
    System.Random random;
    float startTime;
    float timeUntilChange;
    GameObject changingDisplay;
    SpriteRenderer renderer;
    TextMesh displaytext;
    String state;
    float reactionTimeS;
    int screenWidth;
    int screenHeight;
    GameObject camera;
    Camera camComponent;

    // Use this for initialization
    void Start () {
        camera = GameObject.Find("Camera");
        settings = GameObject.Find("settingsElements");
        settings.SetActive(false);
        audioOn = true;
        inGameGuide = GameObject.Find("gameInstructions");
        random = new System.Random();
        changingDisplay = GameObject.Find("display");
        renderer = changingDisplay.GetComponent<SpriteRenderer>();
        timeUntilChange = 2;
        displaytext = inGameGuide.GetComponent<TextMesh>();
        state = "start";
        reactionTimeS = 0.5f;
        startTime = 0.1f;
        renderer.color = new Color(0, 0, 1, 1);
        camComponent = camera.GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        var screenRatio = Convert.ToSingle(screenWidth) / Convert.ToSingle(screenHeight);
        var worldRatio = 2.8f;
        camComponent.orthographicSize = screenRatio > worldRatio ? 4.2f : 4.2f * worldRatio / screenRatio;
    }

    // Update is called once per frame
    void Update () {
            /*
            if (waitingForTurn && Time.time - timeAtPlayStart >= timeUntilChange) {
                renderer.color = new Color(1, 0, 0, 1);
                typeFast = true;
                timeAtPlayStart = Time.time;
                waitingForTurn = false;
            }

            if(waitingForTurn && Time.time - timeAtPlayStart < timeUntilChange && Input.GetKeyDown("3") && !gameOver) {
                timeAtPlayStart = Time.time;
                Debug.Log("Timer reset!");
            }

            if (Input.GetKeyDown("2") && atTitle && settingsDisplayed == false) {
                settings.SetActive(true);
                settingsDisplayed = true;
            }else if(Input.GetKeyDown("2") && atTitle && settingsDisplayed == true) {
                settings.SetActive(false);
                settingsDisplayed = false;
            }

            if(settingsDisplayed && Input.GetKeyDown("3") && atTitle) {
                audioOn = !audioOn;
            }

            if(!atTitle && Input.GetKeyDown("3") && !waitingForTurn && Time.time > timeAtPlayStart + 2) {
                inGameGuide.SetActive(false);
                changeSetup();
            }
            */

            if (state == "start") {
            titleCycle();
        }else if(state == "settings") {
            settingsCycle();
        }else if (state == "guideToPlay") {
            guideCycle();
        }else if(state == "beforeTurningToRed") {
            beforeRedCycle();
        }else if(state == "gottaPressFast") {
            actionRedCycle();
        }else if (state == "failure") {
            retryCycle();
        }
        
        if (Input.GetKeyDown("up")) {
            canvas.SetActive(false);
            renderer.color = new Color(1, 0, 0, 1);
            successText();
        }

    }

    void titleCycle() {

        /*
        if (Input.GetKeyDown("3")) {
            canvas.SetActive(false);
            state = "guideToPlay";
        }
        if (Input.GetKeyDown("2")) {
            state = "settings";
            settings.SetActive(true);
        }
        if (Input.GetKeyDown("1")) {
            Application.Quit();
        }
        */
    }

    void settingsCycle() {
        if (Input.GetKeyDown("3")) {
            audioOn = !audioOn;
            Debug.Log("The state of audio being on is: " + audioOn);
        }else if (Input.GetKeyDown("2")) {
            state = "start";
            settings.SetActive(false);
        }
    }

    void guideCycle() {
        if(Time.time > startTime + 0.25) {
            if (Input.GetKeyDown("3")) {
                state = "beforeTurningToRed";
                var randomDouble = random.NextDouble();                
                timeUntilChange = 2 + Convert.ToSingle(randomDouble) * 2;
                startTime = Time.time;
                //Debug.Log("The timeUntilChange is: " + timeUntilChange);
                inGameGuide.SetActive(false);
            }
        }
    }

    void beforeRedCycle() {
        if(Time.time >= startTime + timeUntilChange) {
            renderer.color = new Color(1, 0, 0, 1);
            state = "gottaPressFast";
            startTime = Time.time;
        }
        if (Input.GetKeyDown("3")) {
            startTime = Time.time;
        }
    }

    void actionRedCycle() {
        if (Time.time > startTime + reactionTimeS) {
            state = "failure";
            displaytext.text = "experiment failed. press 3 to" + "\n" + "try again.";
            inGameGuide.SetActive(true);
        }else if (Input.GetKeyDown("3")) {
            state = "success";
            // " + "\n" +  "
            successText();            
            inGameGuide.SetActive(true);
        }
    }

    void successText() {
        displaytext.text = "experiment succeeded! The" + "\n" + "time you needed to react to " + "\n" + "the color changing was" + "\n" + (Time.time - startTime) + " seconds!";
    }

    void retryCycle() {
        if (Input.GetKeyDown("3")) {
            renderer.color = new Color(0, 0, 1, 1);
            var randomDouble = random.NextDouble();
            timeUntilChange = 2 + Convert.ToSingle(randomDouble) * 2;
            startTime = Time.time;
            inGameGuide.SetActive(false);
            state = "beforeTurningToRed";
        }
    }

    //For changing color: https://docs.unity3d.com/ScriptReference/Color.html

}
