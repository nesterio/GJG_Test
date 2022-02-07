using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField]private GameManager gameManager;

	private float StartPlayerSize;
	private float CurrentPlayerSize
	{
		get{return this.transform.localScale.x;}
	}


	[Header("Bullet config")]
	[SerializeField]private Transform Bullet;

	[SerializeField]private float SizeChangeSpeed;
	[SerializeField]private float BulletGrowthMultiplier;

	[Range(0.1f, 0.5f)]
	[SerializeField]private float BulletGrowthRecoil;

	[SerializeField]private float MinBulletSize;

	[Header("Loss conditions")]

	[SerializeField]private int Difficulty;

	[SerializeField]private float MinimalSize;


    void Start()
    {
    	StartPlayerSize = gameManager.AmountOfObstaclesToSpawn / Difficulty;

        this.transform.localScale = new Vector3(StartPlayerSize, StartPlayerSize, StartPlayerSize);
    }


    void Update()
    {
    	/// Controlling player object Y coordinate, so it touches the ground ///
    	this.transform.localPosition = new Vector3(this.transform.localPosition.x, CurrentPlayerSize / 2, this.transform.localPosition.z);

    	/// Checking if the player is not too small ///
    	if(CurrentPlayerSize <= MinimalSize)
    	{
    		ReleaseBullet();
    		gameManager.ChangeState(GameState.Loss);
    	}

    		/// Growing a bullet if player holds finger ///
    		if(GameManager.CurrentGameState == GameState.WaitingForInput && Input.GetButtonDown("Fire1") || GameManager.CurrentGameState == GameState.PrepearingShot && Input.GetButton("Fire1"))
    			GrowBullet();

    		if(GameManager.CurrentGameState == GameState.PrepearingShot && Input.GetButtonUp("Fire1") && Bullet.localScale.x >= MinBulletSize)
    			ReleaseBullet();

    }

    void GrowBullet()
    {
    	if(GameManager.CurrentGameState == GameState.WaitingForInput)
    		gameManager.ChangeState(GameState.PrepearingShot);

    	transform.localScale -= new Vector3(SizeChangeSpeed, SizeChangeSpeed, SizeChangeSpeed);
    	Bullet.localScale += new Vector3(SizeChangeSpeed * BulletGrowthMultiplier, SizeChangeSpeed * BulletGrowthMultiplier, SizeChangeSpeed * BulletGrowthMultiplier);

    	Bullet.localPosition += Vector3.back * Time.deltaTime * BulletGrowthRecoil;
    }

    void ReleaseBullet() => gameManager.ChangeState(GameState.Shooting);

}
