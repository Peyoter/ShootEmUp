using Bullets;
using Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject Character;
        [SerializeField] private GameManager.GameManager GameManager;
     
        private void OnEnable()
        {
            Character.GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            Character.GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => GameManager.FinishGame();
  
    }
}