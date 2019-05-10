using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourSideEnemy : BasicEnemy
{
    [SerializeField]
    public List<Side> Sides = new List<Side>();

    public GameObject center;
    public float centralRotationSpeed;

    public override void Shoot()
    {
        Debug.Log("The Boss is Shooting Now!");
        foreach (Side side in Sides)
        {
            if (side != null)
            {
                side.Shoot();
            }
        }
    }
    void RotateCenter()
    {
        center.transform.Rotate(Vector3.forward, centralRotationSpeed * Time.deltaTime);
    }

    public override void Follow()
    {
        RotateCenter();

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateamount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateamount * enemyObject.rotationSpeed;
        float _speed = enemyObject.mooveSpeed;
        distance = Vector2.Distance((Vector2)transform.position, (Vector2)target.position);

        if (distance < 15)
        {
            Shoot();
        }

        if (distance < 10)
        {
            _speed = 0;
            rb.velocity = transform.right * enemyObject.mooveSpeed;
        }
        else
        {
            _speed = enemyObject.mooveSpeed;
            rb.velocity = transform.up * _speed;
        }

        if (distance < 9)
        {
            _speed = -enemyObject.mooveSpeed;
            rb.velocity = transform.up * _speed;
        }
    }
    public override void Die()
    {
        foreach (Side side in Sides)
        {
            if (side != null)
            {
                side.rb  = side.gameObject.AddComponent<Rigidbody2D>();
                side.rb.gravityScale = 0;
                side.baseDead = true;
                side.transform.parent = null;
                side.transform.Translate(side.transform.up * 6 * Time.deltaTime);
            }
        }
        base.Die();
    }
}
