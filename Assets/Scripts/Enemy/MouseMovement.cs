using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
	[SerializeField] internal float moveSpeed = 5;
	[SerializeField] internal float desiredDistance = 0.5f;

	internal Rigidbody2D rigidBody;
	internal GameObject player;
	
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
		rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		// This stores the distance from the current object and the player. This updates every frame.
		internal float currentDistance = Vector2.Distance(player.transform.position, this.transform.position);
		
		// This will try to keep the player in the desired distance by moving towards or away.
        if(currentDistance != desiredDistance)
			rigidBody.velocity = new Vector2(moveSpeed * Mathf.Sign(currentDistance - desiredDistance), 0);
		else
			rigidBody.velocity = new Vector2(0,0);
    }
}
