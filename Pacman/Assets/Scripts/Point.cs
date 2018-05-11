using UnityEngine;

public class Point : MonoBehaviour
{

    public float _speed;

	void Start () 
    {
	    Destroy(gameObject, 1.5f);
	}
	
	void Update () 
    {
	    transform.Translate(new Vector3(0, _speed * Time.deltaTime, 0));
	    _speed -= 0.01f;
    }
}
