using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Travancore.ROBY
{

    public class LevelDesigner : MonoBehaviour
    {
        public TotalLevels totalLevels;

       public void onSaveButtonClick()
        {
            string levelData = JsonUtility.ToJson(totalLevels, true);
            string filepath = Path.Combine(Application.dataPath, "LevelData.json");
            File.WriteAllText(filepath, levelData);
        }
    }
}
