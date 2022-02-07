using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorScript : MonoBehaviour
{

	[Header("Exiting animation")]

    [SerializeField]private GameObject DoorObj;

    [SerializeField]private int DoorOpenAngle;

    [SerializeField]private float DoorOpenDuration;

    [Space]

    [SerializeField]private float PlayerDetectionDistance;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, PlayerDetectionDistance);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player") && GameManager.CurrentGameState == GameState.Exiting)
        	OpenDoor();
    }

    void OpenDoor()
    {
    	Vector3 RotationDestination = new Vector3(0, DoorOpenAngle, 0);
    	DoorObj.transform.DORotate(RotationDestination, DoorOpenDuration, RotateMode.Fast);
    }
}
