using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private bool hasDoubleJump = true;
    private float movementX;
    private float movementY;
    private float movementZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementZ, movementY);

        rb.AddForce(movement * speed);

        if (movementZ > 0)
        {
            movementZ--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void OnJump()
    {
        if (transform.position.y == 0.5f)
        {
            movementZ = 8f;
            hasDoubleJump = true;
            Debug.Log("Jump");
        }
        else if (hasDoubleJump && transform.position.y > 0.5f)
        {
            movementZ = 8f;
            hasDoubleJump = false;
            Debug.Log("Jump");
        }
    }
}
