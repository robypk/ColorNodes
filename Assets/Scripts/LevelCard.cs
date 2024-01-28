using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Travancore.ROBY
{
    public class LevelCard : MonoBehaviour
    {

        [SerializeField] TMP_Text LevelName;
        [SerializeField] TMP_Text TimeText;
        [SerializeField] Button LevelButton;

        public delegate void OnLevelChange(Level SelectedLevel);


        private void Start()
        {
            setLevelTime(DateTime.Now);
        }


        public void setLevelName(int levelNum)
        {
            LevelName.text = "Level: " + levelNum;
        }


        public void setLevelTime(DateTime TimeTaken)
        {
            TimeText.text = "Finished on: " + TimeTaken;
        }


        public void buttonClick(OnLevelChange onLevelChange,  Level SelectedLevel)
        {
            LevelButton.onClick.AddListener(() => onLevelChange(SelectedLevel));
        }


    }
}
