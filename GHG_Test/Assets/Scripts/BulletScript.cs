using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

	[SerializeField]private Transform Player;

	[SerializeField]private GameObject Door;

	[SerializeField]private GameManager gameManager;

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
		if(GameManager.CurrentGameState == GameState.Shooting)
			transform.localPosition += Vector3.forward * Time.deltaTime * Speed;
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log(col.name);

		if(col.CompareTag("Obstacle") && GameManager.CurrentGameState == GameState.Shooting)
			Explode();
	}

	void Explode()
	{
		gameManager.ChangeState(GameState.DestroyingObstacles);

		var SplashParticle = objectPooler.SpawnFromPool("Splash");

		SplashParticle.transform.position = transform.position;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, BulletSize * ExplosionRadius);
        for(int i = 0; i < hitColliders.Length; i++)
        {
        	if(hitColliders[i].gameObject.CompareTag("Obstacle"))
        		hitColliders[i].GetComponent<ObstacleScript>().StartDeathAnimation();
        }

        ResetBulletPosition();

        transform.localScale = Vector3.zero;

        gameManager.ChangeState(GameState.WaitingForInput);
	}

    void ResetBulletPosition() =>
    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Player.localPosition.z + PlayerToBulletDistance);

}
