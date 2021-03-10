using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WereAllGonnaDieAnywayNew
{
    public class ConversationManager : MonoBehaviour
    {
        [SerializeField] protected GameObject interactionMenu;
        [SerializeField] protected GameObject conversationMenu;
        [SerializeField] protected GameObject tradeMenu;

        [SerializeField] protected GameObject loadingBar;

        [SerializeField] protected TextMeshProUGUI randomInfoText;

        //[SerializeField] protected Player player;

        public Creature OtherCreature;

        //pathfinding reference
        //private Pathfinding_Creature otherCreaturePathfinder;
        private void Start()
        {
            interactionMenu.SetActive(false);
            conversationMenu.SetActive(false);
            tradeMenu.SetActive(false);
            loadingBar.SetActive(false);
        }

        private void OnEnable()
        {
            DelegateManager.beginCreatureInteraction += BeginInteractionResponse;
            //DelegateManager.broadcastPlayer += SetPlayer;
            DelegateManager.attemptScavenge += BeginScavenge;
        }

        private void OnDisable()
        {
            DelegateManager.beginCreatureInteraction -= BeginInteractionResponse;
            //DelegateManager.broadcastPlayer -= SetPlayer;
            DelegateManager.attemptScavenge -= BeginScavenge;
        }

        //public void SetPlayer(Player _player)
        //{
        //    player = _player;
        //}

        public void BeginScavenge(HarvestPointMonobehaviour hpmb)
        {

            loadingBar.SetActive(true);
        }

        public void BeginInteractionResponse(Creature otherCreature)
        {
            interactionMenu.SetActive(true);
            OtherCreature = otherCreature;

            //stop
            //otherCreaturePathfinder = otherCreature.GetComponent<Pathfinding_Creature>();
            //otherCreaturePathfinder.Stop();
        }

        public void TransitionToConversation()
        {
            interactionMenu.SetActive(false);
            conversationMenu.SetActive(true);
            //randomInfoText.text = OtherCreature.GetComponent<Communicator>().CommunicateRandomInfo();
            TriggerInfection();
        }

        public void TransitionToTrade()
        {
            interactionMenu.SetActive(false);
            tradeMenu.SetActive(true);
            TriggerInfection();
        }

        public void EndInteraction()
        {
            interactionMenu.SetActive(false);
            conversationMenu.SetActive(false);
            tradeMenu.SetActive(false);

            //go
            //otherCreaturePathfinder.Move();

            DelegateManager.endCreatureInteraction?.Invoke();
            
        }

        public void TriggerInfection()
        {
            List<Creature> infectees = new List<Creature>();

            infectees.Add(OtherCreature);
            //infectees.Add(player.PlayerCreature);

            //Utils.CalculateInfection(infectees);
        }
    }
}