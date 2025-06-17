using UnityEngine;

public class SkillUIUpdater : MonoBehaviour
{
    [SerializeField] private VampirismUI _ui;

    public void UpdateActivation(float fillAmount, float remainingTime)
    {
        if (_ui != null)
            _ui.UpdateTimer(fillAmount, remainingTime);
    }

    public void UpdateCooldown(float fillAmount, float cooldownRemaining)
    {
        if (_ui != null)
            _ui.UpdateCooldown(fillAmount, cooldownRemaining);
    }

    public void Show()
    {
        if (_ui != null)
            _ui.Show();
    }

    public void Hide()
    {
        if (_ui != null)
            _ui.Hide();
    }
}
