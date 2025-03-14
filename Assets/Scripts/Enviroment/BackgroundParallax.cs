using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundParallax : MonoBehaviour {

    public Transform[] layer01;
    public float ParallaxReductionFactor_layer01 = 50;

    public Transform[] layer02;
    public float ParallaxReductionFactor_layer02 = 40;

    public Transform[] layer03;
    public float ParallaxReductionFactor_layer03 = 30;

    public Transform[] layer04;
    public float ParallaxReductionFactor_layer04 = 20;

    public Transform[] layer05;
    public float ParallaxReductionFactor_layer05 = 10;

    public float ParallaxScale = 1;
    public float Smoothing = 0.5f;

    private Vector3 _lastPosition;
    private float parallax;

    public void Start()
    {
        _lastPosition = transform.position;
    }

    public void Update()
    {
        parallax = (_lastPosition.x - transform.position.x) * ParallaxScale;

        ParallaxLayerMove(layer01, ParallaxReductionFactor_layer01);
        ParallaxLayerMove(layer02, ParallaxReductionFactor_layer02);
        ParallaxLayerMove(layer03, ParallaxReductionFactor_layer03);
        ParallaxLayerMove(layer04, ParallaxReductionFactor_layer04);
        ParallaxLayerMove(layer05, ParallaxReductionFactor_layer05);

        _lastPosition = transform.position; 
    }

    void ParallaxLayerMove(Transform[] Layer,float ParallaxReductionFactor)
    {
        if (Layer.Length == 0)
            return;

        foreach (Transform layer in Layer)
        {
            var backgroundTargetPosition = layer.position.x + parallax * (ParallaxReductionFactor + 1);
            layer.position = Vector3.Lerp(
                layer.position, new Vector3(backgroundTargetPosition, layer.position.y, layer.position.z),
                Smoothing * Time.deltaTime);
        }
    }

}
