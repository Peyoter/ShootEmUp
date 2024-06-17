using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterManager : MonoBehaviour
    {
        [SerializeField] private GameManager.GameManager GameManager;


        private void OnEnable()
        {
            GetComponent<DeathObserverComponent>().OnDeath += OnCharacterDeath;
        }

        private void OnDisable()
        {
            GetComponent<DeathObserverComponent>().OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => GameManager.FinishGame();
    }
}