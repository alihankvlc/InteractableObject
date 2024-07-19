using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.KVLC
{
    [System.Serializable]
    public class InteractionVisualHandler
    {
        [SerializeField] private GameObject InteractionParentObject;
        [SerializeField] private TextMeshProUGUI InteractionDisplayText;

        public void ShowDisplayInfo(bool isShow, string displayName = "")
        {
            InteractionParentObject.SetActive(isShow);
            InteractionDisplayText.SetText(displayName);
        }
    }
}