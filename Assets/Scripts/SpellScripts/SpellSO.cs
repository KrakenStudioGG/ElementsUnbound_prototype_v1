using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "SpellSO", menuName = "Create Spell")]
public class SpellSO : ScriptableObject
{
    public GameObject prefab;
    public float speed;
    public float damage;
    private Vector2 startingPos;
    private Vector2 endingPos;

    /*public static SpellSO CreateInstance(GameObject prefab, float speed, float damage)
    {
        var data = ScriptableObject.CreateInstance<SpellSO>();
        data.prefab = prefab;
        data.speed = speed;
        data.damage = damage;
        return data;
    }*/
}
