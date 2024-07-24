using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public String value;
    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = value;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
