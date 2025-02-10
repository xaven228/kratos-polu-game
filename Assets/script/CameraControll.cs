using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CameraControll : MonoBehaviour
{
    public Transform target; // Ссылка на трансформ игрока 
    public float smoothSpeed = 0.125f; // Скорость сглаживания 
    public Vector3 offset; // Смещение камеры относительно игрока 

    void LateUpdate() 
    { 
        // Желаемая позиция камеры с учетом смещения 
        Vector3 desiredPosition = target.position + offset; 
        
        // Интерполяция позиции камеры для плавного следования 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
        
        // Обновление позиции камеры 
        transform.position = smoothedPosition; 
    } 
}
