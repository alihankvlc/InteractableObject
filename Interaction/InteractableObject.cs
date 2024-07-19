using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.KVLC
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _displayName;
        [SerializeField] private UnityEvent _interactionEvent;

        private Outline _outline;
        public string DisplayName => _displayName;

        private void Start() => _outline = GetComponent<Outline>();

        public void SetOutline(bool isEnable) => _outline.enabled = isEnable;
        public void Interact() => _interactionEvent?.Invoke();
    }
}