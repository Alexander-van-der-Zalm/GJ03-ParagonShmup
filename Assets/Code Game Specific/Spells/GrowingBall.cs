using UnityEngine;
using System.Collections;

public class GrowingBall : MonoBehaviour,ISpellBase
{
    public float MinDamage, MaxDamage;
    public float StartupTime, ChargeTime, CooldownTime;

    public Bullet Orb;

    private Action action;
    private Transform spellOrb, caster;

    private IEnumerator ChargeCoroutine(Transform SpellOrb, Transform Caster, Action action)
    {
        StartCoroutine(ChargeAnimationCoroutine());

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

        Orb.Launch(spellOrb.position, spellOrb.position - Caster.position, Mathf.Lerp(MinDamage, MaxDamage, charge));
    }

    private string ChargeAnimationCoroutine()
    {
        throw new System.NotImplementedException();
    }


    

    public void Activate(Transform SpellOrb, Transform Caster, Action action)
    {
        this.action = action;

        StartCoroutine(ChargeCoroutine(spellOrb,Caster,action));
    }
}
