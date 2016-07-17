using UnityEngine;
using System.Collections;

public class Swim : MonoBehaviour {
    private float swimSpeed = (Random.Range(100f,300f));
    public Rigidbody2D rb;
    Vector2 fishScale;
    float randomSize = Random.Range(1.5f,3.5f);
	// Use this for initialization
	void Start () {
        Vector2 fishScale = new Vector2(randomSize, randomSize);
        this.gameObject.transform.localScale = fishScale;
        rb = GetComponent<Rigidbody2D>();
      
        Swimming();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Swimming() {
        rb.velocity = (new Vector2(-1, 0) * swimSpeed * Time.deltaTime);
    }
}
