using kingcrimson.gameplay;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace kingcrimson.ui
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider m_wokenessBar;
        [SerializeField] private TextMeshProUGUI m_wokenessStateLabel;
        [SerializeField] private Player m_player;


        private void Awake()
        {
            
        }

        private void Start()
        {
            m_player.OnWokenessChange.AddListener(UpdateWokenessBar);
            m_wokenessBar.maxValue = m_player.Wokeness;
            m_wokenessBar.value = m_player.Wokeness;
        }

        private void UpdateWokenessBar(float total, float change)
        {
            m_wokenessBar.value = total;
        }
    }
}