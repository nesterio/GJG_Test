using UnityEngine;

public class RoadScript : MonoBehaviour
{

	[SerializeField]private Transform Player;

	void Update()
    {
        this.transform.localScale = new Vector3(Player.transform.localScale.x / 10, transform.localScale.y, transform.localScale.z);
    }

}
