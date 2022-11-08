using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsClass : SpellManager
{
    private Vector2 playerPosition;
    private Vector2 rayHitPosition;
    
    public override void Update()
    {
        base.Update();
        //transform.position = Vector2.Lerp(playerPosition, rayHitPosition, spawnSpells.FindIndex(0));
    }
}
