using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[Header("Object referenses")]

	[SerializeField]private Transform Player;

	[SerializeField]private GameManager gameManager;


	[Header("Bullet config")]

	[SerializeField]private float PlayerToBulletDistance;

	[SerializeField]private float Speed;

	[SerializeField]private float ExplosionRadius;


	private float BulletSize
	{
		get {return this.transform.localScale.x;}
	}

	ObjectPooler objectPooler;


	void Start()
	{
		ResetBulletPosition();

		transform.localScale = Vector3.zero;

		objectPooler = ObjectPooler.Instance;
	}

	void Update()
	{
		/// if bullet shot, then move forward ///
		if(GameManager.CurrentGameState == GameState.Shooting)
			transform.localPosition += Vector3.forward * Time.deltaTime * Speed;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Obstacle") && GameManager.CurrentGameState == GameState.Shooting)
			Explode();

		if(col.CompareTag("Door") && GameManager.CurrentGameState == GameState.Shooting)
			Explode();
	}

	void Explode()
	{
		gameManager.ChangeState(GameState.DestroyingObstacles);

		/// Spawn explosion particles form object pooler and set their position ///
		var SplashParticle = objectPooler.SpawnFromPool("Splash");
		SplashParticle.transform.position = transform.position;

		/// Infest all the obstacles in explosion radius ///
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, BulletSize * ExplosionRadius);
        for(int i = 0; i < hitColliders.Length; i++)
        {
        	if(hitColliders[i].gameObject.CompareTag("Obstacle"))
        		hitColliders[i].GetComponent<ObstacleScript>().StartDeathAnimation();
        }

        /// Reset bullet size and position ///
        ResetBulletPosition();
        transform.localScale = Vector3.zero;

        gameManager.ChangeState(GameState.WaitingForInput);
	}

    void ResetBulletPosition() =>
    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Player.localPosition.z + PlayerToBulletDistance);

}
