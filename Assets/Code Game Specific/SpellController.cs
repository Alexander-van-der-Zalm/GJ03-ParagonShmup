using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpellOrb;

    private float radius;
    private Transform tr;
    private Vector3 newDir;

	// Use this for initialization
	void Start () 
    {
        tr = transform;
        Vector3 offset = SpellOrb.position - tr.position;
        Vector2 xyplane = new Vector2(offset.x, offset.z);
        radius = xyplane.magnitude;
	}
	
	// Update is called once per frame
	void Update () 
    {
        updateOrbLocation();
	}

    private void updateOrbLocation()
    {
        SpellOrb.position = newDir + tr.position;
    }

    public void MoveOrb(Vector2 dir)
    {
        newDir = dir.normalized * radius;
        newDir.z = newDir.y;
        newDir.y = 0;
    }

    public void Launch(SpellBase spell, Transform caster, Action action)
    {
        Debug.Log("Launch");
        spell.Activate(SpellOrb, caster, action);
    }
}
