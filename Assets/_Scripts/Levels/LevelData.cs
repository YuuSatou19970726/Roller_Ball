using UnityEngine;

namespace RollerBall
{
    public class LevelData
    {
        public LevelData(string levelName)
        {
            string data = PlayerPrefs.GetString(levelName);
            if (data == "")
                return;

            string[] allData = data.Split('&');
            BestTime = float.Parse(allData[0]);
            SilverTime = float.Parse(allData[1]);
            GoldTime = float.Parse(allData[2]);
        }

        public float BestTime { set; get; }
        public float GoldTime { set; get; }
        public float SilverTime { set; get; }
    }
}