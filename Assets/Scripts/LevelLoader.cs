
using System.IO;
using UnityEngine;

namespace Travancore.ROBY
{
    public class LevelLoader : MonoBehaviour
    {

        [SerializeField] GameObject LevelCard;
        [SerializeField] Transform LevelCardParent;
        TotalLevels levels;


        // Start is called before the first frame update
        void Start()
        {
            string filepath = Path.Combine(Application.dataPath, "LevelData.json");
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                levels = JsonUtility.FromJson<TotalLevels>(json);

                foreach (var level in levels.Levels)
                {
                    var newLevelCard = Instantiate(LevelCard, LevelCardParent);
                    LevelCard levelCard = newLevelCard.GetComponent<LevelCard>();
                    levelCard.setLevelName(level.LevelNumber);
                    levelCard.buttonClick(enableNode, level);
                }

            }
        }

        void enableNode(Level SelectedLevel)
        {
            GamePlayManager.instance.SelectedLevel = SelectedLevel;
            GamePlayManager.instance.SceneChange(1);
        }
    }
}
