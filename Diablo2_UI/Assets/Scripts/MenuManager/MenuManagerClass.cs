using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryPanel;
using CharacterPanel;
using SkillTreePanel;

namespace MenuManager
{
    public class MenuManagerClass : MonoBehaviour
    {
        private bool rightPanelIsOpen = false;
        private bool leftPanelIsOpen = false;

        private GameObject? currentOpenedRightMenu;
        private GameObject? currentOpenedLeftMenu;

        public List<GameObject> rightMenuList;
        public List<GameObject> leftMenuList;

        public void ManageInventory()
        {
            if (currentOpenedRightMenu is null && !rightPanelIsOpen)
            {
                rightPanelIsOpen = true;
                currentOpenedRightMenu = rightMenuList[0];
                currentOpenedRightMenu.gameObject.GetComponent<Inventory>().Open();
            }
            else if (currentOpenedRightMenu is not null && currentOpenedRightMenu == rightMenuList[0] && rightPanelIsOpen)
            {
                rightPanelIsOpen = false;
                currentOpenedRightMenu.gameObject.GetComponent<Inventory>().Close();
                currentOpenedRightMenu = null;
            }
            else if (currentOpenedRightMenu is not null && currentOpenedRightMenu != rightMenuList[0] && rightPanelIsOpen)
            {
                foreach (var menu in rightMenuList)
                    menu.SetActive(false);
                currentOpenedRightMenu = null;
                rightPanelIsOpen = false;
                ManageInventory();
            }
        }

        public void ManageCharacter()
        {
            if (currentOpenedLeftMenu is null && !leftPanelIsOpen)
            {
                leftPanelIsOpen = true;
                currentOpenedLeftMenu = leftMenuList[0];
                leftMenuList[0].gameObject.GetComponent<Character>().Open();
            }
            else if (currentOpenedLeftMenu is not null && currentOpenedLeftMenu == leftMenuList[0] && leftPanelIsOpen)
            {
                leftPanelIsOpen = false;
                leftMenuList[0].gameObject.GetComponent<Character>().Close();
                currentOpenedLeftMenu = null;
            }
            else if (currentOpenedLeftMenu is not null && currentOpenedLeftMenu != leftMenuList[0] && leftPanelIsOpen)
            {
                foreach (var menu in leftMenuList)
                    menu.SetActive(false);
                currentOpenedLeftMenu = null;
                leftPanelIsOpen = false;
                ManageCharacter();
            }
        }

        public void ManageSkillTree()
        {
            if (currentOpenedRightMenu is null && !rightPanelIsOpen)
            {
                rightPanelIsOpen = true;
                currentOpenedRightMenu = rightMenuList[1];
                currentOpenedRightMenu.gameObject.GetComponent<SkillTree>().Open();
            }
            else if (currentOpenedRightMenu is not null && currentOpenedRightMenu == rightMenuList[1] && rightPanelIsOpen)
            {
                rightPanelIsOpen = false;
                currentOpenedRightMenu.gameObject.GetComponent<SkillTree>().Close();
                currentOpenedRightMenu = null;
            }
            else if (currentOpenedRightMenu is not null && currentOpenedRightMenu != rightMenuList[1] && rightPanelIsOpen)
            {
                foreach (var menu in rightMenuList)
                    menu.SetActive(false);
                currentOpenedRightMenu = null;
                rightPanelIsOpen = false;
                ManageSkillTree();
            }
        }
    }
}
