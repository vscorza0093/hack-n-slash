using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillTreePanel
{
    public class SkillTree : MonoBehaviour
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
