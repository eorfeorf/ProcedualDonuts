using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralDonuts : MonoBehaviour
{
    [SerializeField]
    private Transform piecePrefab;

    [SerializeField, Range(3, 50)]
    private int count = 3;

    [SerializeField, Range(1f, 10f)]
    private float radius = 1f;

    [SerializeField, Range(1f, 10f)]
    private float scale = 1f;

    private List<Transform> objs = new List<Transform>();

    private int prevCount = 3;
    private float prevRadius = 1f;
    private float prevScale = 1f;

    private float oneAngle
    {
        get
        {
            var pi2 = Mathf.PI * 2f;
            var oneAngle = pi2 / count;
            return oneAngle;
        }
    }

    private void Start()
    {
        //count = 3;
        Remake(count);
        prevCount = count;
        prevRadius = radius;
        prevScale = scale;
    }

    private void Update()
    {
        if (count != prevCount)
        {
            DestroyDonuts();
            Remake(count);
            prevCount = count;
        }
        if (Math.Abs(radius - prevRadius) > float.Epsilon)
        {
            UpdateRadius();
            prevRadius = radius;
        }
        if (Math.Abs(scale - prevScale) > float.Epsilon)
        {
            UpdateScale();
            prevScale = scale;
        }
    }

    private void Remake(int c)
    {
        var angle = 0f;
        for (int i = 0; i < c; ++i, angle = oneAngle * i)
        {
            var t = Instantiate(piecePrefab, Vector3.zero, Quaternion.identity, transform);
            objs.Add(t);
        }
        UpdateRadius();
        UpdateScale();
    }

    private void UpdateRadius()
    {
        var angle = 0f;
        for (int i = 0; i < count; ++i, angle = oneAngle * i)
        {
            var t = objs[i];
            t.position = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
        }
    }

    private void UpdateScale()
    {
        foreach (var t in objs)
        {
            t.localScale = new Vector3(scale, scale, scale);
        }
    }
    
    private void DestroyDonuts()
    {
        foreach (var t in objs)
        {
            Destroy(t.gameObject);
        }
        objs.Clear();
    }
}
