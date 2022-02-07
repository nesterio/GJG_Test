using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerScript : MonoBehaviour
{
    [SerializeField]private float TimeBeforeDisable;
    private float TimeLeft;
    void Awake()
    {
        TimeLeft = TimeBeforeDisable;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        
        if(TimeLeft <= 0)
        	gameObject.SetActive(false);
    }
}
