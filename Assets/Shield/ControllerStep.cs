using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControllerStep : MonoBehaviour {

	[Range (0,1f)]
	private float factor;

	[Range (0,1f)]
	public float limitFactor = 0.98f;

	public Shader shader;
    private RenderTexture buffer;
    private Material materialSelected;

	public float MaxTimeBeforeChange = 5f;
	private float timeBeforeChange;



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

		CheckIfMoovement.OnSomethingMoved += ChangeFactor;
    }

	void LateUpdate()
	{
		if (timeBeforeChange > 0)
		{
			timeBeforeChange -= Time.deltaTime;
		}

		if (factor > 0 && timeBeforeChange <= 0)
		{
			factor -= Time.deltaTime;
		}

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

		CheckIfMoovement.OnSomethingMoved -= ChangeFactor;
    }


	private void ChangeFactor()
	{
		timeBeforeChange = MaxTimeBeforeChange;
		factor = limitFactor;
	}
}
