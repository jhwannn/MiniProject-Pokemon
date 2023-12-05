﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Attack : SkillType
{
    public BattleProcess battleProcess;
    public enum SkillList
    {
        QuickAttack,
        Tackle,
        Cut,
        Slash,
        Leer,
        Peck,
        PoisonSting,
        SandAttack,
        Growl,
        Gust,
        VineWhip,
        HitTheJop,
        DynamicPunch,
        IcePunch,
        Kinesis

    }
    [SerializeField]
    public SkillList _nowSkill;

    private void Awake()
    {
        battleProcess = GameObject.Find("BattleProcess").GetComponent<BattleProcess>();

    }
    private void Start()
    {
        base.nowSkill = _nowSkill.ToString();

    }

    public void UseSkill()
    {

        Debug.Log(_nowSkill.ToString() + "_Hit");
        string _skillName = _nowSkill.ToString() + "_Hit";
        GameObject.Find("BattleProcess").GetComponent<BattleProcess>().EnemyAnim(_skillName, base.NameKR, base.Damage);

    }





}
