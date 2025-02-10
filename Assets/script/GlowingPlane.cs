using UnityEngine;

public class GlowingPlane : MonoBehaviour
{
    public Color emissionColor = Color.white; // Цвет свечения
    public float emissionIntensity = 1f; // Интенсивность свечения

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Устанавливаем цвет и интенсивность свечения материала
        rend.material.SetColor("_EmissionColor", emissionColor * emissionIntensity);

        // Убедитесь, что шейдер поддерживает Emission
        // Standart Shader поддерживает Emission по умолчанию
    }
}