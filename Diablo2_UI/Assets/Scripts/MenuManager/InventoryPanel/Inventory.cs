using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryPanel
{
    public class Inventory : MonoBehaviour
    {
        public GameObject inventoryObject;

        private int availableSlots = 40;
        private List<int> inventorySlot = new List<int>();

        public void Open()
        {
            inventoryObject.SetActive(true);
        }

        public void Close()
        {
            inventoryObject.SetActive(false);
        }
    }

    public class Equipment_Slot_I
    {
        public GameObject helmet;
        public GameObject armor;
        public GameObject leftHandWeapon;
        public GameObject rightHandWeapon;
        public GameObject gloves;
        public GameObject boots;
    }
}
