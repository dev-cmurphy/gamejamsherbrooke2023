using kingcrimson.gameplay;
using System.Collections;
using TMPro;
using UnityEngine;

namespace kingcrimson.ui
{
    public class GameTimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_timeTextMesh;

        [SerializeField] private GameTime m_gameTime;

        private void Awake()
        {
            
        }

        private void Start()
        {
            m_gameTime.OnNewMinute.AddListener(UpdateTimeText);
            UpdateTimeText(0);
        }

        private void UpdateTimeText(int ticks)
        {
            const int start_hour = 21;

            // on pose qu'un tick = une minute
            int h = ticks / 60;
            int m = ticks % 60;

            h += start_hour;
            h %= 24;

            m_timeTextMesh.text = $"{h:00}:{m:00}";
        }
    }
}