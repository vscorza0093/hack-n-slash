using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PlayerAttributes
{
    public class PlayerAttributes : MonoBehaviour
    {
        public float attackDamage = 13;

        public int maxHealthPoints = 40;
        public int healthPoints = 40;
        public int attackRange = 1;
        public int armorDefense = 0;
        public int attackAccuracy = 0;
        public int magicDamage = 0;

        public TMP_Text availablePointsText;

        public TMP_Text strenghtPointsText;
        public TMP_Text dexterityPointsText;
        public TMP_Text vitalityPointsText;
        public TMP_Text energyPointsText;

        public TMP_Text attackDamageText;
        public TMP_Text accuracyText;
        public TMP_Text defenseText;
        public TMP_Text healthText;

        public int availablePoints = 0;

        private int strenghtPoints = 12;
        private int dexterityPoints = 10;
        private int vitalityPoints = 10;
        private int energyPoints = 8;


        void Update()
        {
            availablePointsText.text = availablePoints.ToString();
            strenghtPointsText.text = strenghtPoints.ToString();
            dexterityPointsText.text = dexterityPoints.ToString();
            vitalityPointsText.text = vitalityPoints.ToString();
            energyPointsText.text = energyPoints.ToString();
            attackDamageText.text = attackDamage.ToString("0.00");
            accuracyText.text = attackAccuracy.ToString();
            healthText.text = maxHealthPoints.ToString();
        }

        public void AddStrenghtPoint()
        {
            if (availablePoints > 0)
            {
                strenghtPoints++;
                availablePoints--;
                ChangeAttackDamage();
            }
        }

        public void AddDexterityPoint()
        {
            if (availablePoints > 0)
            {
                dexterityPoints++;
                availablePoints--;
            }
        }

        public void AddVitalityPoint()
        {
            if (availablePoints > 0)
            {
                vitalityPoints++;
                availablePoints--;
                ChangeHealthStats();
            }
        }

        public void AddEnergyPoint()
        {
            if (availablePoints > 0)
            {
                energyPoints++;
                availablePoints--;
            }
        }

        void ChangeAttackDamage()
        {
            attackDamage += (attackDamage * 0.04f);
        }

        void ChangeAccuracy()
        {

        }

        void ChangeDefense()
        {

        }

        void ChangeHealthStats()
        {
            maxHealthPoints += (int)(maxHealthPoints * 0.04f);

        }
    }
}
