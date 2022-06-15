using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    private void Start()
    {
        int dif = PlayerPrefs.GetInt("Zorluk", 2);

        switch (dif) {
            case 1:
                speed = 3.0f;
                break;
            case 2:
                speed = 5.0f;
                break;
            case 3:
                speed = 7.5f;
                break;
        }
        speed += ScoreText.scoreValue/25;

        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
    }
    void Update()
    {

        if (transform.position.y < screenBounds.y *2) {
            Destroy(this.gameObject);
        }
        if (transform.position.x < screenBounds.x) {
            Destroy(this.gameObject);
        }
    }
}
