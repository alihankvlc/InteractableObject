using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.KVLC
{
    public interface IInteractable
    {
        void Interact();
        void SetOutline(bool isEnable);
        public string DisplayName { get; }
    }

    public class InteractableHandler : MonoBehaviour
    {
        [SerializeField] private float _interactableDistance;
        [SerializeField] private LayerMask _interactableLayer;

        [SerializeField, ReadOnly] private IInteractable _activeInteractableObject = null;

        private Camera _mainCam;
        private Vector3 _raycastSendPosition;

        public delegate void InteractionVisualDelegate(bool isActive, string displayName = "");
        public static event InteractionVisualDelegate OnInteractableObjectChanged;

        private void Start()
        {
            _raycastSendPosition = new(Screen.width / 2, Screen.height / 2);
            _mainCam = Camera.main;
        }

        private void Update()
        {
            SendRay();
        }

        private void SendRay()
        {
            Ray ray = _mainCam.ScreenPointToRay(_raycastSendPosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactableDistance, _interactableLayer))
            {
                HandleInteractableObject(hitInfo.collider.GetComponent<IInteractable>());
            }
            else if (_activeInteractableObject != null)
            {
                ClearActiveInteractableObject();
            }
        }

        private void HandleInteractableObject(IInteractable interactableObject)
        {
            if (interactableObject == null)
                return;

            if (_activeInteractableObject != interactableObject)
            {
                if (_activeInteractableObject != null)
                {
                    _activeInteractableObject.SetOutline(false);
                }

                _activeInteractableObject = interactableObject;
                _activeInteractableObject.SetOutline(true);

                OnInteractableObjectChanged?.Invoke(true, interactableObject.DisplayName);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactableObject.Interact();
            }
        }

        private void ClearActiveInteractableObject()
        {
            _activeInteractableObject.SetOutline(false);
            _activeInteractableObject = null;

            OnInteractableObjectChanged?.Invoke(false);
        }
    }
}
