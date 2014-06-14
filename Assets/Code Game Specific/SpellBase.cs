using UnityEngine;
using System.Collections;

public interface ISpellBase 
{
    void Activate(Transform SpellOrb, Transform Caster, Action action);
}
