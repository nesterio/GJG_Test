using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

	Animator animator 
	{get
		{ return GetComponent<Animator>(); }
	}

	ObjectPooler objectPooler
	{get
		{ return ObjectPooler.Instance; }
	}

    
    public void StartDeathAnimation() =>  animator.SetTrigger("Destroy");


    public void PlayParticles()
    {
    	var SplashParticle = objectPooler.SpawnFromPool("Splash");

		SplashParticle.transform.position = transform.position;
    }
    

    public void DisableObstacle() => gameObject.SetActive(false);

}
