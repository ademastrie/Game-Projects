﻿using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        Destroy (other.gameObject);
    }
}
