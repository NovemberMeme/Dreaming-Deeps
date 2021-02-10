using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [System.Serializable]
    public class Residentdata
    {   [SerializeField]
        private CreatureFactoryData creatureData;
        [SerializeField]
        private string _name;
        [SerializeField]
        private HOUSEHOLD_TASKS assignedTask;
        [SerializeField]
        private HOUSEHOLD_TASKS wantToDoTask;
        [SerializeField]
        private HOUSEHOLD_TASKS goodAtTask;
        [SerializeField]
        private int dayTaskAssigned = 0;
        
        public CreatureFactoryData CreatureData { get => creatureData; set => creatureData = value; }
        public string Name { get => _name; set => _name = value; }
        public HOUSEHOLD_TASKS AssignedTask { get => assignedTask; set => assignedTask = value; }
        public int DayTaskAssigned { get => dayTaskAssigned; set => dayTaskAssigned = value; }
        public HOUSEHOLD_TASKS WantToDoTask { get => wantToDoTask; set => wantToDoTask = value; }
        public HOUSEHOLD_TASKS GoodAtTask { get => goodAtTask; set => goodAtTask = value; }

        public Residentdata(CreatureFactoryData actorData, HOUSEHOLD_TASKS assignedTask)
        {
            CreatureData = actorData;
            AssignedTask = assignedTask;
            Name = actorData.Name;
            wantToDoTask = HOUSEHOLD_TASKS.NONE;
        }

        public Residentdata(CreatureFactoryData actorData)
        {
            CreatureData = actorData;
            AssignedTask = HOUSEHOLD_TASKS.NONE;
            Name = actorData.Name;
            wantToDoTask = HOUSEHOLD_TASKS.NONE;
        }

        public void SetWantTodo(HOUSEHOLD_TASKS _wantToDoTask)
        {
            wantToDoTask = _wantToDoTask;
        }

    }

}



