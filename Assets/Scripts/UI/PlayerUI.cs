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

        [SerializeField] private Image m_barFill;
        [SerializeField] private Animator m_barAnimator;


        private void Awake()
        {
        }

        private void Start()
        {
            m_player.OnWokenessChange.AddListener(UpdateWokenessBar);
            m_wokenessBar.maxValue = m_player.Wokeness;
            m_wokenessBar.value = m_player.Wokeness;

            StartCoroutine(WokebarFlash());
        }

        private void UpdateWokenessBar(float total, float change)
        {
            m_wokenessBar.value = total;

            if (change > 0.1f)
            {
                m_barAnimator.SetTrigger("Flash");
            }
        }

        private IEnumerator WokebarFlash()
        {
            while (true)
            {
                float flashDuration = m_wokenessBar.value / (m_wokenessBar.maxValue - 1);
                if (flashDuration < 1)
                {
                    flashDuration = Mathf.Clamp(flashDuration, 0.2f, 1f) * 4f;

                    Color c = m_barFill.color;
                    c.a = 0;
                    m_barFill.color = c;
                    yield return new WaitForSeconds(flashDuration * 0.1f);

                    c.a = 1;
                    m_barFill.color = c;
                    yield return new WaitForSeconds(flashDuration * 0.9f);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}