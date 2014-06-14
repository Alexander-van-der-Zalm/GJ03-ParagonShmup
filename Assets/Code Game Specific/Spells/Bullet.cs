using UnityEngine;
using System.Collections;

public class Bullet : ManagedObject 
{
    public float Damage;
    public float Speed;
    public Vector3 Velocity;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    this.transform.position += Velocity * Time.fixedDeltaTime;
	}

    public void Launch(Vector3 origin, Vector3 Direction, float damage = -1, float speed = -1)
    {
        if (damage >= 0)
            Damage = damage;
        if (speed >= 0)
            Speed = speed;
        
        Direction.y = 0;
        Velocity = Direction * Speed;

        GameObject bullet = this.Create();
        bullet.transform.position = origin;

        
    }

    //public 
}
