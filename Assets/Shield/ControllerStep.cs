using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControllerStep : MonoBehaviour {

    private RenderTexture buffer;

    public Shader shader;

    private Material materialSelected;
    private int a;

    Material material
    {
        get
        {
            if (materialSelected == null)
            {
                Debug.Log("Creando material");
                materialSelected = new Material(shader);
                materialSelected.hideFlags = HideFlags.HideAndDontSave;

            }
            return materialSelected;
        }
    }

    // Use this for initialization
    void Start()
    {

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!shader || !shader.isSupported)
        {
            enabled = false;
        }

    }

    void OnRenderImage(RenderTexture entry, RenderTexture exit)
    {

        if (a <= 0)
        {
            buffer = entry;
        }

        a++;

        material.SetTexture("_LastFrame", buffer);

        if (a >= 1000)
        {
            a = 0;
        }

        Graphics.Blit(entry, exit, material);
    }

    // Update is called once per frame

    private void OnDisable()
    {
        if (materialSelected)
        {
            DestroyImmediate(materialSelected);
        }
    }
}
