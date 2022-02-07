using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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


	[Header("Exiting animation")]

    [SerializeField]private Transform JumpingDestination;

    [SerializeField]private float JumpingAnimationDuration;

    [SerializeField]private int NumberOfJumps;
    [SerializeField]private float JumpForce;

    private bool Exiting;


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

    		/// Shoot the bullet if player lifts the finger and the bullet matches or is bigger than required minimum ///
    		if(GameManager.CurrentGameState == GameState.PrepearingShot && Input.GetButtonUp("Fire1") && Bullet.localScale.x >= MinBulletSize)
    			ReleaseBullet();

    		/// Start exiting animation if the game state is "exiting" and player is not exiting yet ///
    		if(GameManager.CurrentGameState == GameState.Exiting && Exiting == false)
    			ExitLevel();

    }

    void GrowBullet()
    {
    	if(GameManager.CurrentGameState == GameState.WaitingForInput)
    		gameManager.ChangeState(GameState.PrepearingShot);

    	transform.localScale -= new Vector3(SizeChangeSpeed, SizeChangeSpeed, SizeChangeSpeed);
    	Bullet.localScale += new Vector3(SizeChangeSpeed * BulletGrowthMultiplier, SizeChangeSpeed * BulletGrowthMultiplier, SizeChangeSpeed * BulletGrowthMultiplier);

    	/// Draws bullet back for visual recoil effect ///
    	Bullet.localPosition += Vector3.back * Time.deltaTime * BulletGrowthRecoil;
    }

    void ReleaseBullet() => gameManager.ChangeState(GameState.Shooting);


    void ExitLevel()
    {
    	Exiting = true;

    	/// Calculates the position to jump to ///
    	Vector3 JumpingVector = new Vector3(JumpingDestination.position.x, transform.position.y, JumpingDestination.position.z);

    	/// Starts jumping and changes the game state when completed ///
        transform.DOJump(JumpingVector, JumpForce, NumberOfJumps, JumpingAnimationDuration, false).
        OnComplete(() => gameManager.ChangeState(GameState.Victory));
    }

}
