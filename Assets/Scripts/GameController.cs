using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Camera cam;
    public GameObject startButton;
    public PlayerController playerController;

    public BackgroundMover bm;

    public GameObject triangle;
    public GameObject square;
    public GameObject pentagon;
    public GameObject hexagon;

    private float maxWidth;
    private float minHeight;
    public static bool running;
    private float startTime;

    public float mult = 1f;
    public float levelSec = 15f; //seconds before proceeding to next level state
    public float spawnSpeed = 5f;
    public float lastSpawned = -1f;

    // Use this for initialization
    void Start () {
        if (cam == null) {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float w = triangle.GetComponent<Renderer>().bounds.extents.x;
        float h = triangle.GetComponent<Renderer>().bounds.extents.y;
        maxWidth = targetWidth.x - w;
        minHeight = targetWidth.y + h;
        
        startButton.SetActive(true);
        GameController.running = false;
        startTime = -1f;

        bm.randomize();
        gameObject.GetComponent<Renderer>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (GameController.running) {
            mult = 1 + (int)((Time.time - startTime) / levelSec);
            if ((Time.time - lastSpawned) > (levelSec / (spawnSpeed * mult))) {
                Vector3 spawnPos = new Vector3(Random.Range(-maxWidth, maxWidth), minHeight, 0); //rand cord selection
                Quaternion spawnRotation = Quaternion.identity;
                switch (Random.Range(0, 4)) { //rand shape selection
                    case 0:
                        Instantiate(triangle, spawnPos, spawnRotation);
                        break;
                    case 1:
                        Instantiate(square, spawnPos, spawnRotation);
                        break;
                    case 2:
                        Instantiate(pentagon, spawnPos, spawnRotation);
                        break;
                    case 3:
                        Instantiate(hexagon, spawnPos, spawnRotation);
                        break;
                    default: //wtf
                        break;
                }
                lastSpawned = Time.time;
            }
        } else {
            GameController.running = false;
            startButton.SetActive(true);
            playerController.ToggleControl(false);
        }
	}

    public void StartGame() {
        gameObject.GetComponent<Renderer>().enabled = false;
        startTime = Time.time;
        GameController.running = true;
        startButton.SetActive(false);
        playerController.Reset();
        playerController.ToggleControl(true);
    }
}
