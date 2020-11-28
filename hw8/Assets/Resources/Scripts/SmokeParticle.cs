using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SmokeParticle : MonoBehaviour
{
    private GameObject car;
    private ParticleSystem exhaust;

    void Start()
    {
        car = transform.parent.gameObject.transform.parent.gameObject;
        exhaust = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        SetEmissionRate();
        SetColor();
    }

    private void SetEmissionRate()
    {
        var K = 1000;
        var emission = exhaust.emission;
        emission.rateOverTime = K * car.GetComponent<CarController>().Revs + 5;
    }

    private void SetColor()
    {
        var color = exhaust.colorOverLifetime;
        var damage = car.GetComponent<CarCollider>().GetDamage() + 2 * car.GetComponent<CarController>().Revs;
        var gradient = new Gradient();
        var colorKeys = new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(214, 189, 151), 0.079f), new GradientColorKey(Color.white, 1.0f) };
        var alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(damage / 255f, 0.061f), new GradientAlphaKey(0.0f, 1.0f) };
        gradient.SetKeys(colorKeys, alphaKeys);
        color.color = gradient;
    }
}
