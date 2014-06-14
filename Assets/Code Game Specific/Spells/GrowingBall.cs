using UnityEngine;
using System.Collections;

public class GrowingBall : SpellBase
{
    public float MinDamage, MaxDamage;
    public float StartupTime, ChargeTime, CooldownTime;

    public Bullet Bullet;

    private bool CanActivate = true;

    private IEnumerator ChargeCoroutine(Transform SpellOrb, Transform Caster, Action action)
    {
        //StartCoroutine(ChargeAnimationCoroutine());

        CanActivate = false;

        float startTime = Time.realtimeSinceStartup;

        // Wait till button is released
        while (action.IsDown())
            yield return null;

        float dt = Time.realtimeSinceStartup - startTime;

        // Wait atleast till startupTime
        if (dt < StartupTime)
            yield return new WaitForSeconds(StartupTime - dt);

        dt = Time.realtimeSinceStartup - startTime;

        float charge = Mathf.Min(1.0f,(dt-ChargeTime+startTime)/ChargeTime);

        Debug.Log(SpellOrb.position + " " + Caster.position);

        Vector3 dir = SpellOrb.position - Caster.position;
        dir.Normalize();
        Debug.Log(SpellOrb.position + " 0 " + Caster.position + " " + dir);

        Bullet.Launch(SpellOrb.position, dir, Mathf.Lerp(MinDamage, MaxDamage, charge));

        Debug.Log(SpellOrb.position +  " 1 " + Caster.position+ " " + dir);

        yield return new WaitForSeconds(CooldownTime);

        Debug.Log("Cd passed");

        CanActivate = true;
    }

    private IEnumerator ChargeAnimationCoroutine()
    {
        return null;
        //throw new System.NotImplementedException();
    }


    

    public override void Activate(Transform SpellOrb, Transform Caster, Action action)
    {
        if(CanActivate)
            StartCoroutine(ChargeCoroutine(SpellOrb, Caster, action));
    }
}
