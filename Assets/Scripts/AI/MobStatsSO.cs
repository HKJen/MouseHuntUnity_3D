using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMobStats", menuName = "Survival/MobStats")]

public class MobStatsSO : ScriptableObject
{
    public Vector2 MaxStayTimeBorders;
    public float FollowRange;
    public float AttackRange;
    public float Power;
    public float MaxHealth;

    public ItemSO[] Drop;
    public int[] DropCount;

}
