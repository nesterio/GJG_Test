using UnityEngine;

public class RoadScript : MonoBehaviour
{

	[SerializeField]private Transform Player;

	[SerializeField]private GameManager gameManager;

	[SerializeField]private bool RoadClear;

	void Update()
    {
    	/// Matches road width with player's ///
        this.transform.localScale = new Vector3(Player.transform.localScale.x / 10, transform.localScale.y, transform.localScale.z);

        /// if the level has generated, player is not shooting and the road is clear, changes game state to finishing level ///
        if(GameManager.CurrentGameState == GameState.WaitingForInput && RoadClear)
    		gameManager.ChangeState(GameState.Exiting);
    }


    	/// Checks if the road is free of obstacles ///
    void FixedUpdate() => RoadClear = true;

    void OnTriggerStay(Collider col)
    {
    	if(col.CompareTag("Obstacle"))
    		RoadClear = false;
    }

}
