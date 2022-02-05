using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameState CurrentGameState;

	[Header("Obstacle generation config")]

	[SerializeField]private GameObject ObstaclePrefab;

	[Space]

	[SerializeField]private int MinNumberOfObstacles;

	[SerializeField]private int MaxNumberOfObstacles;

	[Space]

	[SerializeField]private float ObstacleMinSpawnX;
	[SerializeField]private float ObstacleMaxSpawnX;

	[SerializeField]private float ObstacleMinSpawnZ;
	[SerializeField]private float ObstacleMaxSpawnZ;

	[Space]

	[SerializeField]private float ObstacleMinSize;
	[SerializeField]private float ObstacleMaxSize;


    void Start()
    {
        ChangeState(GameState.LevelGeneration);
    }

    void GererateLevel()
    {
    	int AmountOfObstaclesToSpawn = Random.Range(MinNumberOfObstacles, MaxNumberOfObstacles);

    	for(int i = 0; i < AmountOfObstaclesToSpawn; i++)
    	{
    		float obstacleSize = Random.Range(ObstacleMinSize, ObstacleMaxSize);

    		Vector3 obstacleScale = new Vector3(obstacleSize, obstacleSize, obstacleSize);

    		float xSpawnLocation = Random.Range(ObstacleMinSpawnX, ObstacleMaxSpawnX);
    		float zSpawnLocation = Random.Range(ObstacleMinSpawnZ, ObstacleMaxSpawnZ);


    		Vector3 LocationToSpawn = new Vector3(xSpawnLocation, obstacleSize / 2, zSpawnLocation);

    		var _obstacle = Instantiate(ObstaclePrefab, LocationToSpawn, Quaternion.identity);

    		_obstacle.transform.localScale = obstacleScale;

    	}

    	ChangeState(GameState.WaitingForInput);
    }

    public void ChangeState(GameState newState)
    {
    	CurrentGameState = newState;
    	switch(newState)
    	{
    		case GameState.LevelGeneration:
    		GererateLevel();
    		break;

    		case GameState.WaitingForInput:
    		
    		break;

    		case GameState.PrepearingShot:
    		break;

    		case GameState.Shooting:
    		break;

    		case GameState.Victory:
            Debug.Log("Victory");
    		break;

    		case GameState.Loss:
            Debug.Log("Loss");
    		break;
    	}
    }

}


public enum GameState
{
	LevelGeneration,
	WaitingForInput,
	PrepearingShot,
	Shooting,
	Exiting,
	Victory,
	Loss
}
