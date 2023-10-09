using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchange : MonoBehaviour
{
    public GameObject Object;
    public ParticleSystem particleSystem; // Particle system referansı
    [Range(0.6f, 3.2f)] private float magnitude; // Magnitude değişkeni. Min 0.5, Max 2.3

    void Update()
    {
        magnitude = float.Parse(Object.transform.name);
        ChangeColor(); // Her frame'de renk değişim fonksiyonunu çağır.
    }

    void ChangeColor()
    {
        // Magnitude değerini 0 ve 1 arasına normalize et.
        float normalizedMagnitude = Mathf.InverseLerp(0.5f, 2.3f, magnitude);

        // Normalized magnitude değerini kullanarak renk değerini lerp et.
        Color color = Color.Lerp(Color.yellow, Color.red, normalizedMagnitude);

        // Particle systemin başlangıç rengini değiştir.
        var main = particleSystem.main;
        main.startColor = color;
    }
}
