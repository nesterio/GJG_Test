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

    public int AmountOfObstaclesToSpawn {get; private set;}

	[Space]

	[SerializeField]private float ObstacleMinSpawnX;
	[SerializeField]private float ObstacleMaxSpawnX;

	[SerializeField]private float ObstacleMinSpawnZ;
	[SerializeField]private float ObstacleMaxSpawnZ;

	[Space]

	[SerializeField]private float ObstacleMinSize;
	[SerializeField]private float ObstacleMaxSize;


    void Awake()
    {
        AmountOfObstaclesToSpawn = Random.Range(MinNumberOfObstacles, MaxNumberOfObstacles);

        ChangeState(GameState.LevelGeneration);
    }

    void GererateLevel()
    {

    	for(int i = 0; i < AmountOfObstaclesToSpawn; i++)
    	{
            /// Randomizing obstacle size ///
    		float obstacleSize = Random.Range(ObstacleMinSize, ObstacleMaxSize);
    		Vector3 obstacleScale = new Vector3(obstacleSize, obstacleSize, obstacleSize);

            /// Randomizing obstacle location ///
    		float xSpawnLocation = Random.Range(ObstacleMinSpawnX, ObstacleMaxSpawnX);
    		float zSpawnLocation = Random.Range(ObstacleMinSpawnZ, ObstacleMaxSpawnZ);

    		Vector3 LocationToSpawn = new Vector3(xSpawnLocation, obstacleSize, zSpawnLocation);

            /// Spawning obstacle and setting its scale ///
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
            Debug.Log("Generating level");
    		GererateLevel();
    		break;

    		case GameState.WaitingForInput:
            Debug.Log("Waiting for input");
    		break;

    		case GameState.PrepearingShot:
            Debug.Log("Prepearing to shoot");
    		break;

    		case GameState.Shooting:
            Debug.Log("Shooting");
    		break;

            case GameState.DestroyingObstacles:
            Debug.Log("Destroying obstacles");
            break;

            case GameState.Exiting:
            Debug.Log("Player is goingto the door");
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
    DestroyingObstacles,
	Exiting,
	Victory,
	Loss
}
