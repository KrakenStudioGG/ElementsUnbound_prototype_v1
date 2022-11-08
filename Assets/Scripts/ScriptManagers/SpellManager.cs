using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpellManager : MonoBehaviour
{
    public PlayerController playerController;
    public List<SpellSO> spellList = new List<SpellSO>();
    public List<GameObject> spawnSpells;
    private Vector3 playerPos;
    private Vector3 rayPos;
    private bool isCastEventTriggered;
    private bool isCoroutineStarted;
    private Transform activeTransform;
    private int listIndex;

    private void Awake()
    {
        CreateSpells();
    }

    private void OnEnable()
    {
        PlayerController.castEvent += MoveData;
    }

    private void OnDisable()
    {
        PlayerController.castEvent -= MoveData;
    }

    private void MoveData(Vector2 player, Vector2 rayPointPos, int index)
    {
        listIndex = index;
        activeTransform= spawnSpells[index].GetComponent<Transform>();
        activeTransform.position = player;
        rayPos = rayPointPos;
        spawnSpells[index].SetActive(true);
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

    public virtual void Update()
    {
        if (spawnSpells[listIndex].activeInHierarchy)
        {
            MoveSpell();
            RotateSpell();
        }
    }

    private void MoveSpell()
    {
        Vector3 moveDir = rayPos - (Vector3)activeTransform.position;
        moveDir = moveDir.normalized;
        if (Vector2.Distance(activeTransform.position, rayPos) > 0.1f)
        {
            activeTransform.position += moveDir * Time.deltaTime * 0.5f;
            RotateSpell();
        }
        else
        {
            spawnSpells[listIndex].SetActive(false);
        }
    }

    private void RotateSpell()
    {
        Vector3 moveDir = rayPos - (Vector3)activeTransform.position;
        moveDir = moveDir.normalized;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        Quaternion rotateValue = Quaternion.AngleAxis(angle, Vector3.forward);
        activeTransform.rotation = Quaternion.Slerp(activeTransform.rotation, rotateValue, Time.deltaTime * 100f);
    }
}
