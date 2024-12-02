using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewindAbility : MonoBehaviour
{
    private bool isRewinding = false;
    private float cooldownTimer = 0f;

    private List<Vector2> positions;
    private Rigidbody2D rb;

    public float maxRewind = 3f;
    public float cooldown = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        positions = new List<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRewinding) {
            rewind();
        } else {
            record();
        }

        if (cooldownTimer > 0) {
            cooldownTimer -= Time.fixedDeltaTime;
        } else {
            cooldownTimer = 0;
        }
    }

    public void startRewind() {
        if (cooldownTimer == 0) {
            isRewinding = true;
            rb.isKinematic = true;
        }
    }

    public void stopRewind() {
        if (isRewinding) {
           isRewinding = false;
            rb.isKinematic = false;
            cooldownTimer = cooldown; 
        }
    }

    private void rewind() {
        if (positions.Count > 0) {
            transform.position = positions[0];
            positions.RemoveAt(0);
            if (positions.Count > 0) positions.RemoveAt(0);
        } else {
            stopRewind();
        }
        
    }

    private void record() {
        if (positions.Count > Mathf.Round(maxRewind / Time.fixedDeltaTime)) {
            positions.RemoveAt(positions.Count - 1);
        }

        positions.Insert(0, transform.position);
    }
}
