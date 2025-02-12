using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    private Animation anim; // Переименовано, чтобы избежать конфликта имен

    void Start()
    {
        // Получить компонент Animation
        anim = GetComponent<Animation>();

        // Проверка наличия компонента Animation
        if (anim == null)
        {
            Debug.LogError("Animation component is missing on the GameObject.");
            return;
        }

        // Проверка наличия анимационного клипа
        AnimationClip clip = anim.GetClip("walk-kolya-1");
        if (clip == null)
        {
            Debug.LogError("Animation clip 'walk' not found.");
            return;
        }

        // Установить режим зацикливания для анимационного клипа
        anim.wrapMode = WrapMode.Loop;
        anim.Play("walk-kolya-1");
    }
}
