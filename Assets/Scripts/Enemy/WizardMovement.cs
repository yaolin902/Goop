using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
	[SerializeField] internal float moveSpeed = 5;
	[SerializeField] internal float desiredDistance = 2f;
	private bool is_facing_right = false;

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
		float currentDistance = Vector2.Distance(player.transform.position, this.transform.position);
		float x_diff = player.transform.position.x - this.transform.position.x;

		if ((x_diff < 0f && is_facing_right) ||
            (x_diff > 0f && !is_facing_right))
            flip();
		
		// This will try to keep the player in the desired distance by moving towards or away.
        if(currentDistance <= desiredDistance)
			rigidBody.velocity = new Vector2(moveSpeed * Mathf.Sign(desiredDistance - currentDistance), 0);
		else
			rigidBody.velocity = new Vector2(0,0);
    }

	private void flip() {
        is_facing_right = !is_facing_right;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}