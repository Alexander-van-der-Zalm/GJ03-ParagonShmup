using UnityEngine;
using System.Collections;

public class GrowingBall : SpellBase
{
    public float MinDamage, MaxDamage;
    public float StartupTime, ChargeTime, CooldownTime;
    public float ActivationCost, FullChargeCost;

    public Bullet Bullet;

    private bool CanActivate = true;

    private IEnumerator ChargeCoroutine(Transform SpellOrb, Transform Caster, Stats casterStats, Action action)
    {
        //StartCoroutine(ChargeAnimationCoroutine());

        CanActivate = false;
        float startTime = Time.realtimeSinceStartup;
        float negativeEnergy = casterStats.ConsumeEnergy(ActivationCost);

        // Wait till button is released
        while (action.IsDown() || negativeEnergy > 0)
            yield return null;

        float dt = Time.realtimeSinceStartup - startTime;

        // Wait atleast till startupTime
        if (dt < StartupTime)
            yield return new WaitForSeconds(StartupTime - dt);

        // Dt and charge calculation
        dt = Time.realtimeSinceStartup - startTime;
        float charge = Mathf.Max(0,Mathf.Min(1.0f,(dt-ChargeTime)/ChargeTime));

        // Gather Direction
        Vector3 dir = SpellOrb.position - Caster.position;
        dir.Normalize();

        // Consume energy and launch
        Debug.Log(charge + "  " + (dt - ChargeTime) / ChargeTime);
        casterStats.ConsumeEnergy(FullChargeCost * charge);
        Bullet.Launch(SpellOrb.position, dir, Mathf.Lerp(MinDamage, MaxDamage, charge));

        // Replace by Cooldown Animation Handler
        yield return new WaitForSeconds(CooldownTime);

        CanActivate = true;
    }

    private IEnumerator ChargeAnimationCoroutine()
    {
        return null;
        //throw new System.NotImplementedException();
    }

    public override void Activate(Transform SpellOrb, Transform Caster, Stats casterStats, Action action)
    {
        if(CanActivate&&casterStats.Energy>0)
            StartCoroutine(ChargeCoroutine(SpellOrb, Caster, casterStats, action));
    }
}
