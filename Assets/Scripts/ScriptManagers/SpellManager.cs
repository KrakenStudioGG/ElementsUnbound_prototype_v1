using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpellManager : MonoBehaviour
{
    public List<SpellSO> spellList = new List<SpellSO>();
    private List<GameObject> spawnSpells;
    private Vector2 playerPos;
    private Vector2 rayPos;

    private void Awake()
    {
        CreateSpells();
    }

    private void Update()
    {
        spawnSpells[0].transform.position = Vector2.Lerp(playerPos, rayPos, spellList[0].speed);
    }

    private void CreateSpells()
    {
        spawnSpells = new List<GameObject>(spellList.Count);
        for (int i = 0; i < spellList.Count; i++)
        {
            spawnSpells.Add(Instantiate(spellList[i].prefab, Vector2.zero, Quaternion.identity));
            spawnSpells[i].SetActive(false);
        }
    }

    public void MoveData(Vector2 playerPos, Vector2 rayPos)
    {
        this.playerPos = playerPos;
        this.rayPos = rayPos;
    }

    IEnumerator MoveSpell(Vector2 player, Vector2 ray)
    {
        spawnSpells[0].SetActive(true);
        while (Vector2.Distance(player, ray) > 0.1f)
        {
            spawnSpells[0].transform.position = Vector2.Lerp(player, ray, spellList[0].speed);
            yield return null;
        }
        spawnSpells[0].SetActive(false);
    }
}
