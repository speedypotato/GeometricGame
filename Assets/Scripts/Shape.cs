using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    private float spawnTime = 0;
    private int lifespan = 5;

    // Use this for initialization
    void Start() {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - spawnTime > lifespan)
            Destroy(gameObject);
    }
}
