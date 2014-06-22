using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrowingOrbAnimation : MonoBehaviour 
{
    public float StartScale, MinScale, MaxScale;
    public float fXScale;

    public float NormalRotationPSec, OverchargeRotationPsec, CoolDownRotationPSec;
    public GameObject GrowingOrbCastObject;

    public Color castColor, cdColor;

    private GrowingBall gb;
    private bool spellStart = false;
    private Transform centerOfSpell;
    private Material mat;

	// Use this for initialization
	void Start () 
    {
        gb = gameObject.GetComponent<GrowingBall>();
        mat = GrowingOrbCastObject.renderer.material;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (spellStart)
            GrowingOrbCastObject.transform.position = centerOfSpell.position;

        if (gb.AnimInfo.AnimStateProgress == 0 && gb.AnimInfo.State == GrowingBall.AnimationInfo.AnimState.Starting && spellStart)
        {
            spellStart = false;
            GrowingOrbCastObject.SetActive(false);
        }
        else if(spellStart)
            GrowingOrbCastObject.SetActive(true);

        float rotation = 0;
        mat.color = castColor;

        switch (gb.AnimInfo.State)
        {
            case GrowingBall.AnimationInfo.AnimState.Starting:
                setSize(StartScale + gb.AnimInfo.AnimStateProgress * (MinScale - StartScale));
                rotation = NormalRotationPSec * Time.deltaTime;
                break;
            case GrowingBall.AnimationInfo.AnimState.Overcharging:
                setSize(MinScale + gb.AnimInfo.AnimStateProgress * (MaxScale - MinScale));
                rotation = OverchargeRotationPsec * Time.deltaTime;
                break;
            case GrowingBall.AnimationInfo.AnimState.Cooldown:
                setSize(GrowingOrbCastObject.transform.localScale.z * 1.1f);

                Color color = cdColor;
                color.a = 1 - gb.AnimInfo.AnimStateProgress;
                mat.color = color;

                rotation = CoolDownRotationPSec * Time.deltaTime;
                break;
        }

        GrowingOrbCastObject.transform.Rotate(0, rotation, 0);
	}

    private void setSize(float p)
    {
        GrowingOrbCastObject.transform.localScale = new Vector3(p,p,p);
    }

    public void StartSpell(Transform centerOfCast)
    {
        this.centerOfSpell = centerOfCast;
        spellStart = true;

        Debug.Log(centerOfSpell.position);
    }
}
