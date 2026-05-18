using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _03.Scripts._Core
{
    public class HUD : MonoBehaviour
    {
        public enum InfoType { Exp, Level, Kill, Time, Health }
        public InfoType type;
        Text myText;
        Slider mySlider;

        private void Awake()
        {
            myText = GetComponent<Text>();
            mySlider = GetComponent<Slider>();
        }

        private void LateUpdate()
        {
            switch (type)
            {
                case InfoType.Exp:
                    float curExp = GameManager.instance.exp;
                    float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                    mySlider.value = curExp / maxExp;
                    break;
                case InfoType.Level:
                    myText.text = String.Format("Lv.{0:F0}", GameManager.instance.level);
                    break;
                case InfoType.Kill:
                    myText.text = String.Format("{0:F0}", GameManager.instance.kill);
                    break;
                case InfoType.Time:
                    float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                    int min = Mathf.FloorToInt(remainTime / 60);
                    int sec = Mathf.FloorToInt(remainTime % 60);
                    myText.text = String.Format("{0:D2}:{1:D2}", min, sec);
                    break;
                case InfoType.Health:
                    float curHealth = GameManager.instance.health;
                    float maxHealth = GameManager.instance.maxHealth;
                    mySlider.value = curHealth / maxHealth;
                    break;
            }
        }
    }
}