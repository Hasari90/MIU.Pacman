using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private int score;
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public List<Image> lives = new List<Image>(3);

	Text txt_score;

    void Start () 
	{
		txt_score = GetComponentsInChildren<Text>()[0];

	    for (int i = 0; i < 3 - GameManager.lives; i++)
	    {
	        Destroy(lives[lives.Count-1]);
            lives.RemoveAt(lives.Count-1);
	    }
	}
	void Update () 
	{
        Score = GameManager.score;
		txt_score.text = "Score\n" + Score;
	}


}
