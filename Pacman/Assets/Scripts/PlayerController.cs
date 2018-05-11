using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 _dest = Vector2.zero;
    Vector2 _dir = Vector2.zero;
    public PointSprites points;
    public static int killstreak = 0;
    private GameGUINavigation _gui;
    private GameManager _gm;
    private bool _deadPlaying = false;

    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    void Start()
    {
        _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _gui = GameObject.Find("UI Manager").GetComponent<GameGUINavigation>();
        _dest = transform.position;
    }

    void FixedUpdate()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                ReadInputAndMove();
                Animate();
                break;

            case GameManager.GameState.Dead:
                if (!_deadPlaying)
                    StartCoroutine("PlayDeadAnimation");
                break;
        }
    }

    IEnumerator PlayDeadAnimation()
    {
        _deadPlaying = true;
        GetComponent<Animator>().SetBool("Die", true);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Die", false);
        _deadPlaying = false;

        if (GameManager.lives <= 0)
        {
                _gui.ShowGameOver();
        }
        else
            _gm.ResetScene();
    }

    void Animate()
    {
        Vector2 dir = _dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 direction)
    {
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        _dest = new Vector2(15f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void ReadInputAndMove()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);


        if ((Vector2)transform.position == _dest)
        {
            if (Input.GetKey(KeyCode.W) && Valid(Vector2.up))
                _dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.D) && Valid(Vector2.right))
                _dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.S) && Valid(-Vector2.up))
                _dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.A) && Valid(-Vector2.right))
                _dest = (Vector2)transform.position - Vector2.right;
        }
    }

    public Vector2 getDir()
    {
        return _dir;
    }

    public void UpdateScore()
    {
        killstreak++;

        if (killstreak > 4) killstreak = 4;

        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;
    }
}
