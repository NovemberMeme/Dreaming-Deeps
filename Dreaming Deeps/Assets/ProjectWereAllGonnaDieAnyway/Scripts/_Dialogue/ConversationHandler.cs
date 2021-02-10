using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WereAllGonnaDieAnywayNew
{
    [RequireComponent(typeof(Creature))]
    public class ConversationHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected float minInteractionDistance = 5;

        private Creature myCreature;
        
     
        private void Start()
        {
            myCreature = GetComponent<Creature>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CheckPlayerDistance();
        }

        public void CheckPlayerDistance()
        {
            Vector2 currentPlayerPos = Vector2.zero;

            DelegateManager.attemptCreatureInteraction?.Invoke(out currentPlayerPos);

            if(Vector2.Distance(transform.position, currentPlayerPos) < minInteractionDistance)
            {
                BeginInteraction();
            }
        }

        private void BeginInteraction()
        {            
            DelegateManager.beginCreatureInteraction?.Invoke(myCreature);

        }
    }
}