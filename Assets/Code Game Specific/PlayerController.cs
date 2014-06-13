using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

[RequireComponent(typeof(MovementPhysics))]
public class PlayerController : MonoBehaviour 
{
    private MovementPhysics movPhysics;

	// Use this for initialization
	void Start () 
    {
        movPhysics = this.GetComponent<MovementPhysics>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector2 movInput = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX), XCI.GetAxis(XboxAxis.LeftStickY));
        movPhysics.Move(movInput);
        Debug.Log(movInput);
	}
}
