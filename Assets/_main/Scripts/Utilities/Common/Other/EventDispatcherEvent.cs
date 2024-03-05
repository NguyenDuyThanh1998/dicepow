using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Common;

public class EDPlayEvent : BaseEvent
{
    public bool isContinue;
}

public class EDUpdateLifeEvent : BaseEvent
{
    public int currentLife;
}

public class EDGameOverEvent : BaseEvent
{
}

public class EDKillEnemy : BaseEvent
{

}


public class EDAddItemData : BaseEvent
{
    public SkillData itemAdd;
    public bool init;
}

public class EDAddBonusData : BaseEvent
{
    public SkillData itemCore;
    public SkillData bonus;
}

public class EDChangeWeapob: BaseEvent
{
    public SkillData data;
}
