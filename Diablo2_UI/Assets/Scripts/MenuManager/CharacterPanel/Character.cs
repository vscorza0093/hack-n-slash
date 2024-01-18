using UnityEngine;

namespace CharacterPanel
{
    public class Character : MonoBehaviour
    {
        public GameObject inventoryObject;

        public void Open()
        {
            inventoryObject.SetActive(true);
        }

        public void Close()
        {
            inventoryObject.SetActive(false);
        }
    }
}
