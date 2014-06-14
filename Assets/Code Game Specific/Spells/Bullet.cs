using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : ManagedObject 
{
    public float Damage;
    public float Speed;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Launch(Vector3 origin, Vector3 Direction, float damage = -1, float speed = -1)
    {
        if (damage >= 0)
            Damage = damage;
        if (speed >= 0)
            Speed = speed;

        GameObject bullet = this.Create();
        bullet.rigidbody.position = origin;

        Direction.y = 0;
        bullet.rigidbody.velocity = Direction;
    }

    //public 
}
