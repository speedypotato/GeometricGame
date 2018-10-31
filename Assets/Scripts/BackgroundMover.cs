using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour {
    public Camera cam;
    public GameObject bg;
    public Sprite bg1;
    public Sprite bg2;
    public Sprite bg3;
    public Sprite bg4;

    private Vector3 startPos;
    private float reducer = 10;

	// Use this for initialization
	void Start () {
        if (cam == null) {
            cam = Camera.main;
        }
        randomize();
        startPos = bg.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float diff = startPos.x - rawPosition.x;
        Vector3 targetPosition = new Vector3(startPos.x + (diff / reducer), startPos.y, startPos.z);
        bg.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
    }

    public void randomize() {
        switch (Random.Range(0, 4)) { //random bg
            case 0:
                bg.GetComponent<SpriteRenderer>().sprite = bg1;
                break;
            case 1:
                bg.GetComponent<SpriteRenderer>().sprite = bg2;
                break;
            case 2:
                bg.GetComponent<SpriteRenderer>().sprite = bg3;
                break;
            case 3:
                bg.GetComponent<SpriteRenderer>().sprite = bg4;
                break;
            default:
                bg.GetComponent<SpriteRenderer>().sprite = bg1;
                break;
        }
    }
}
