using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public Camera cam;
    private float maxWidth;
    private float maxHeight;
    private bool canControl;

    public Sprite triangle;
    public Sprite square;
    public Sprite pentagon;
    public Sprite hexagon;
    private SpriteRenderer sr;

    public PolygonCollider2D triangleCollider;
    public BoxCollider2D squareCollider;
    public PolygonCollider2D pentagonCollider;
    public PolygonCollider2D hexagonCollider;

    public Text scoreText;

    //0triangle - 1square - 2pentagon - 3hexagon
    public int transformed;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == null)
            sr.sprite = triangle;

        if (cam == null) {
            cam = Camera.main;
        }
        Vector3 targetWidth = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        Vector3 targetHeight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0.0f));

        float width = GetComponent<Renderer>().bounds.extents.x;
        float height = GetComponent<Renderer>().bounds.extents.y;
        maxWidth = targetWidth.x - width;
        maxHeight = targetHeight.y + height;
        canControl = false;

        foreach (Transform child in transform) {
            print(child.name);
            if (child.name == "TriangleCollider") {
                triangleCollider = child.GetComponent<PolygonCollider2D>();
                triangleCollider.enabled = true;
            } else if (child.name == "SquareCollider") {
                squareCollider = child.GetComponent<BoxCollider2D>();
                squareCollider.enabled = false;
            } else if (child.name == "PentagonCollider") {
                pentagonCollider = child.GetComponent<PolygonCollider2D>();
                pentagonCollider.enabled = false;
            } else if (child.name == "HexagonCollider") {
                hexagonCollider = child.GetComponent<PolygonCollider2D>();
                hexagonCollider.enabled = false;
            }
        }

        transformed = 0;

        scoreText.text = "Score: " + transformed;
    }

    public void Reset() {
        sr.sprite = triangle;
        triangleCollider.enabled = true;
        squareCollider.enabled = false;
        pentagonCollider.enabled = false;
        hexagonCollider.enabled = false;
        transformed = 0;
        scoreText.text = "Score: " + transformed;
    }

    // Update is called once per physics timestep
    void FixedUpdate() {
        if (canControl) {
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(rawPosition.x, maxHeight, 0.0f);
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        }
    }

    public void ToggleControl(bool toggle) {
        canControl = toggle;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col) {
        print("Collision with " + col.gameObject.tag);
        int curShape = transformed % 4;
        switch (curShape) {
            case 0: //triangle
                if (col.gameObject.tag == "Triangle") {
                    triangleCollider.enabled = false;
                    squareCollider.enabled = true;
                    Destroy(col.gameObject);
                    transformed++;
                    sr.sprite = square;
                    scoreText.text = "Score: " + transformed;
                }
                else
                    GameController.running = false;
                break;
            case 1: //square
                if (col.gameObject.tag == "Square") {
                    squareCollider.enabled = false;
                    pentagonCollider.enabled = true;
                    Destroy(col.gameObject);
                    transformed++;
                    sr.sprite = pentagon;
                    scoreText.text = "Score: " + transformed;
                }
                else
                    GameController.running = false;
                break;
            case 2: //pentagon
                if (col.gameObject.tag == "Pentagon") {
                    pentagonCollider.enabled = false;
                    hexagonCollider.enabled = true;
                    Destroy(col.gameObject);
                    transformed++;
                    sr.sprite = hexagon;
                    scoreText.text = "Score: " + transformed;
                }
                else
                    GameController.running = false;
                break;
            case 3: //hexagon
                if (col.gameObject.tag == "Hexagon") {
                    hexagonCollider.enabled = false;
                    triangleCollider.enabled = true;
                    Destroy(col.gameObject);
                    transformed++;
                    sr.sprite = triangle;
                    scoreText.text = "Score: " + transformed;
                }
                else
                    GameController.running = false;
                break;
            default: //nothing
                break;
        }
    }
}
