using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControllerWarudo : MonoBehaviour {


    public Shader shader;

    private Material materialSelected;

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
