﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public interface ITargetType 
    {
        PartyCharacter GetByTargetType(PartyCharacter _user);
    }
}