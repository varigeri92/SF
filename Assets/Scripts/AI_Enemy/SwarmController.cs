using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmController : MonoBehaviour
{
    [SerializeField]
    GameObject swarmEnemy;
    [SerializeField]
    int swarmSize;
    [SerializeField]
    float spawnRate;
    [SerializeField]
    float speed;
    [SerializeField]
    float rotateSpeed;
    bool playerdead = false;

    bool spawnDone;
    int livingSwarms;


    List<GameObject> swarm = new List<GameObject>();

    Transform player;

    List<Rigidbody2D> rigidbodys = new List<Rigidbody2D>();

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Spawn());
        Player.OnPlayerDeath += PlayerDead;
    }
    private void OnDestroy()
    {
        Player.OnPlayerDeath -= PlayerDead;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spawnDone)
            return;

        if (rigidbodys.Count == 0)
            return;

        for (int i = 0; i < swarmSize; i++)
        {
            if (i == 0)
            {
                
                if (playerdead)
                {
                    rigidbodys[i].velocity = rigidbodys[i].transform.up * speed;
                }
                else
                {
                    Vector2 direction = (Vector2)player.position - rigidbodys[i].position;
                    direction.Normalize();
                    float rotateamount = Vector3.Cross(direction, rigidbodys[i].transform.up).z;
                    rigidbodys[i].angularVelocity = -rotateamount * rotateSpeed;
                    rigidbodys[i].velocity = rigidbodys[i].transform.up * speed;
                }
            }
            else
            {
                Vector2 direction = (Vector2)rigidbodys[i-1].position - rigidbodys[i].position;
                direction.Normalize();
                float rotateamount = Vector3.Cross(direction, rigidbodys[i].transform.up).z;
                rigidbodys[i].angularVelocity = -rotateamount * rotateSpeed;
                rigidbodys[i].velocity = rigidbodys[i].transform.up * speed;
            }
        }
    }

    IEnumerator Spawn()
    {
        livingSwarms = swarmSize;
        for (int i = 0; i < swarmSize; i++)
        {
            GameObject _swarmEnemy = Instantiate(swarmEnemy, transform.position, Quaternion.identity);
            swarm.Add(_swarmEnemy);
            _swarmEnemy.GetComponentInChildren<SwarmObject>().myController = this;
            Rigidbody2D rb = _swarmEnemy.GetComponent<Rigidbody2D>();
            rigidbodys.Add(rb);
            rb.velocity = transform.up * speed;
            yield return new WaitForSeconds(spawnRate);
        }
        spawnDone = true;
        
        yield break;
    }
    void PlayerDead()
    {
        playerdead = true;
    }

    public void SwarmObjectDied()
    {
        livingSwarms--;
        if(livingSwarms == 0)
        {
            rigidbodys = new List<Rigidbody2D>();
            for (int i = 0; i < swarm.Count; i++)
            {
                Destroy(swarm[i]);
            }
            Destroy(gameObject);
        }
    }
}
