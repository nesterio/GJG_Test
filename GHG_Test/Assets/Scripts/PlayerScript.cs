using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField]private GameManager gameManager;

	[Header("Player config")]
	[SerializeField]private float StartPlayerSize;
	private float CurrentPlayerSize
	{
		get{return this.transform.localScale.x;}
	}

	[Header("Shooting config")]
	[SerializeField]private Transform Bullet;

	[SerializeField]private float BulletGrowthSpeed;

	[Header("Loss conditions")]
	[SerializeField]private float MinimalSize;


    void Start()
    {
        this.transform.localScale = new Vector3(StartPlayerSize, StartPlayerSize, StartPlayerSize);
    }


    void Update()
    {
    	if(CurrentPlayerSize <= MinimalSize)
    	{
    		gameManager.ChangeState(GameState.Loss);
    	}
    }

}
