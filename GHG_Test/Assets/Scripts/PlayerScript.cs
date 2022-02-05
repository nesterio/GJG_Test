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

	[Header("Bullet config")]
	[SerializeField]private Transform Bullet;

	[SerializeField]private float PlayerToBulletDistance;

	[SerializeField]private float BulletGrowthSpeed;

	[Header("Loss conditions")]
	[SerializeField]private float MinimalSize;


    void Start()
    {
        this.transform.localScale = new Vector3(StartPlayerSize, StartPlayerSize, StartPlayerSize);

        ResetBulletDistance();
    }


    void Update()
    {
    	/// Controlling player object Y coordinate, so it touches the ground ///
    	this.transform.localPosition = new Vector3(this.transform.localPosition.x, CurrentPlayerSize / 2, this.transform.localPosition.z);

    	/// Setting bullet y, so it matches player center ///
    	Bullet.transform.localPosition = new Vector3(Bullet.transform.localPosition.x, this.transform.localPosition.y, Bullet.transform.localPosition.z);

    	/// Checking if the player is not too small ///
    	if(CurrentPlayerSize <= MinimalSize)
    	{
    		gameManager.ChangeState(GameState.Loss);
    	}

    	if (Input.touchCount > 0)
        {
        	Debug.Log("test1");
    		/// Growing a bullet if player holds finger ///
    		if(GameManager.CurrentGameState == GameState.WaitingForInput && Input.GetTouch(0).phase == TouchPhase.Began)
    			GrowBullet();

    		if(GameManager.CurrentGameState == GameState.PrepearingShot && Input.GetTouch(0).phase == TouchPhase.Ended)
    			ReleaseBullet();
    	}
    }

    void ResetBulletDistance() => Bullet.transform.localPosition = new Vector3(Bullet.transform.localPosition.x, Bullet.transform.position.y, this.transform.localPosition.z - PlayerToBulletDistance);

    void GrowBullet()
    {
    	gameManager.ChangeState(GameState.PrepearingShot);

    	transform.localScale -= new Vector3(BulletGrowthSpeed, BulletGrowthSpeed, BulletGrowthSpeed);
    	Bullet.localScale += new Vector3(BulletGrowthSpeed, BulletGrowthSpeed, BulletGrowthSpeed);
    }

    void ReleaseBullet()
    {
    	gameManager.ChangeState(GameState.Shooting);
    }

}
