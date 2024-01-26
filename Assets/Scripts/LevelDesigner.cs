using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Travancore.ROBY
{

    public class LevelDesigner : MonoBehaviour
    {
        public TotalLevels totalLevels;
        // Start is called before the first frame update
        void Start()
        {
            string levelData = JsonUtility.ToJson(totalLevels,true);
            string filepath = Path.Combine(Application.dataPath, "LevelData.json");
            File.WriteAllText(filepath, levelData);
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
