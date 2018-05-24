using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControllerStep : MonoBehaviour {

	[Range (0,1f)]
	public float factor;
	public Shader shader;
    private RenderTexture buffer;
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

		buffer = new RenderTexture (Screen.width, Screen.height, 16);

    }

    void OnRenderImage(RenderTexture entry, RenderTexture exit)
    {

		material.SetFloat ("_Factor", factor);
		material.SetTexture ("_LastFrame", buffer);

		Graphics.Blit (entry, exit, material);
		Graphics.Blit(RenderTexture.active, buffer);
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
