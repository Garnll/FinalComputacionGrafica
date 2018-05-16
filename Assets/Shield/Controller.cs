using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Controller : MonoBehaviour {

	public Shader shader;

    public Texture2D mascaraVignette;

    public Color terrain, objects, border;

    public RenderTexture mascara;

	[Range(-1, 3)]
	public float factor;

    [Range(0, 1)]
    public float M;

    public float brillo, contraste, grosor;

	private Material materialSelected;

	Material material
	{
		get {
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
	void Start () {

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
		material.SetFloat("_Factor", factor);

        material.SetFloat("_Mfactor", M);

        material.SetTexture("_Vignette", mascaraVignette);

        material.SetTexture("_Mascara", mascara);

		material.SetFloat("_Brillo", brillo);

		material.SetFloat("_Grosor", grosor);

		material.SetFloat("_Contraste", contraste);

        material.SetColor("_TerrainColor", terrain);

        material.SetColor("_ObjectColor", objects);

        material.SetColor("_BorderColor", border);



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
