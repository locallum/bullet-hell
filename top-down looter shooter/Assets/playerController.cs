using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim; 

    private Vector2 movement;
    private Vector2 mousePosition;

    private List<Vector2> positions;

    rewindAbility rewind;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rewind = GetComponent<rewindAbility>();
        positions = new List<Vector2>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0) anim.SetFloat("x input", movement.x);

        // bind rewind ability
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            rewind.startRewind();
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            rewind.stopRewind();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void fallOffPlatform() {
        Debug.Log("PLAYER FELL!");
        Destroy(this.gameObject);
    }
}
