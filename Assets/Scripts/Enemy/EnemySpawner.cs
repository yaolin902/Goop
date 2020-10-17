using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	// Spawnpool will hold all possible creatures that will
	// be possible chosen. This is here in case anybody
	// wants to create a new prefab for an enemy.
	[SerializeField] internal GameObject[] spawnPool;

	// Following dictates how fast enemy spawns
	[SerializeField] internal float spawnRate = 10.0f;
	[SerializeField] internal float spawnTimer;

	// "safeDistance" determines how far a player must be
	// away from the spawner for the commands to run.
	// This is to prevent contact enemies from spawning
	// on top of player.
	[SerializeField] internal float safeDistance = 2.5f;
	internal GameObject player;

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		spawnTimer = spawnRate;
	}

	private void Update()
	{
		// Cancels spawn timer countdown if player is within
		// specified "safeDistance"
		if (Vector2.Distance(player.transform.position, this.transform.position) < safeDistance) return;

		// Timer countdown
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0)
		{
			// After timer finishes, the "TimerEnds" function
			// is called and the timer resets.
			TimerEnds();
			spawnTimer = spawnRate;
		}
	}
	
	private void TimerEnds()
	{
		// Following variables determine which enemy from the
		// spawnPool will be created and where in the spawnRadius
		// it will be created.
		GameObject obj = Instantiate(spawnPool[Random.Range(0, spawnPool.Length)], this.transform, true);
	}
}