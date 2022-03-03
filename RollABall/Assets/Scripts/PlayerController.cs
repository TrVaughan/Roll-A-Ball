using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText; 
    public TextMeshProUGUI keyText;
    public GameObject winTextObject; 

    private Rigidbody rb;
    private int count; 
    private int keys;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        keys = 0;
        SetCountText();
             winTextObject.SetActive(false);
        SetKeyText();
       
    }

    private void Update()
    {
        if(Input.GetButtonDown("jump") )
        {
            rb.AddForce(new Vector3(0f,300f,0f));
        }
    }
    private void OnJump(InputValue iv)
     {
        rb.AddForce(new Vector3(0f, 200f, 0f));
    }
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count + " + count.ToString();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
          other.gameObject.SetActive(false);
          count = count +1;  

          SetCountText();
        }
        if (other.gameObject.CompareTag ("Key"))
		{
			other.gameObject.SetActive (false);
			keys = keys + 1;
			SetKeyText ();
        }
        if (other.gameObject.CompareTag ("Door"))
		{
			if (keys > 0)	
			{	other.gameObject.SetActive (false);
				keys = keys - 1;
				SetKeyText ();
            }
        }
        if (other.gameObject.CompareTag ("Win"))
		{
			winTextObject.SetActive(true);
		}
        
    }
    void SetKeyText()
    {
        keyText.text = "Keys: " + keys.ToString();
    }
    
}