using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovementPhysics : MonoBehaviour 
{
    
    public float Speed;
    public float TurningSpeed;
    private Rigidbody rigid;
    private Vector3 addedVelocity;

	// Use this for initialization
	void Start () 
    {
        rigid = this.rigidbody;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        rigid.velocity += addedVelocity * Time.fixedDeltaTime;
        Debug.Log(rigid.velocity);
        addedVelocity = Vector3.zero;
	}

    public void Move(Vector2 dir)
    {
        addedVelocity = dir.normalized * Speed;
        addedVelocity.z = addedVelocity.y;
        addedVelocity.y = 0;
    }

    public void LookAt(Vector3 point)
    {

    }

}
