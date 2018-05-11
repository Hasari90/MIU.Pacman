using UnityEngine;

public class Energizer : MonoBehaviour {

    private GameManager _gm;

	void Start ()
	{
	    _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "pacman")
        {
            _gm.ScareGhosts();
            Destroy(gameObject);
        }
    }
}
