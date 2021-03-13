using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class PassiveEntity : MonoBehaviour
    {
        public Passive MyPassiveEffect;

        public List<PartyCharacter> AffectedTargets = new List<PartyCharacter>();
    }
}