using UnityEngine;
using System.Collections.Generic;

public class ObjetoInteractuable : MonoBehaviour
{
    [Header("Scriptable Object")]
    public ObjSO currentObjectSO;

    [Header("Runtime")]
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();
    private int currentTextureIndex = 0;

    void Start()
    {
        SetupMaterials();
    }

    public void Interactuar()
    {
        UIManager.instance.ShowInteractionCanvas(this);
    }

    void SetupMaterials()
    {
        Renderer rend = GetComponent<Renderer>();
        if (!rend || currentObjectSO == null) return;

        // Clone materials to avoid shared asset modification
        Material[] originalMats = rend.sharedMaterials;
        currentMaterials = new Material[originalMats.Length];

        for (int i = 0; i < originalMats.Length; i++)
        {
            currentMaterials[i] = originalMats[i] ? new Material(originalMats[i]) : null;
        }

        rend.materials = currentMaterials;

        // Select editable materials
        editableMaterials.Clear();

        if (currentObjectSO.customMaterialsToEdit != null && currentObjectSO.customMaterialsToEdit.Count > 0)
        {
            foreach (var matToEdit in currentObjectSO.customMaterialsToEdit)
            {
                foreach (var mat in currentMaterials)
                {
                    if (mat != null && mat.name.StartsWith(matToEdit.name))
                    {
                        editableMaterials.Add(mat);
                    }
                }
            }
        }
        else
        {
            editableMaterials.AddRange(currentMaterials); // Edit all if none specified
        }


        if (currentObjectSO.isTextured && currentObjectSO.texturedMaterial && currentObjectSO.texturedElements.Count > 0)
        {
            ApplyTexture(currentObjectSO.texturedElements[0]);
        }
    }

    public void SetColor(Color newColor)
    {
        foreach (var mat in editableMaterials)
        {
            if (mat != null)
            {
                mat.color = newColor;
            }
        }
    }

    public void SetTexture(int index)
    {
        if (!IsTextureValid(index)) return;
        currentTextureIndex = index;
        ApplyTexture(currentObjectSO.texturedElements[currentTextureIndex]);
    }

    public void NextTexture()
    {
        if (!IsTextureValid()) return;

        currentTextureIndex++;
        if (currentTextureIndex >= currentObjectSO.texturedElements.Count)
            currentTextureIndex = 0;

        ApplyTexture(currentObjectSO.texturedElements[currentTextureIndex]);

    }

    public void PreviousTexture()
    {
        if (!IsTextureValid()) return;

        currentTextureIndex--;
        if (currentTextureIndex < 0)
            currentTextureIndex = currentObjectSO.texturedElements.Count - 1;

        ApplyTexture(currentObjectSO.texturedElements[currentTextureIndex]);

    }

    private void ApplyTexture(TexturedElement element)
    {
        if (!currentObjectSO.texturedMaterial || element == null) return;

        foreach (var mat in currentMaterials)
        {
            if (mat != null && mat.name.StartsWith(currentObjectSO.texturedMaterial.name))
            {
                mat.mainTexture = element.mainTexture;

                if (element.normalMap != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", element.normalMap);
                }
                break;
            }
        }
    }


    private bool IsTextureValid(int index = -1)
    {
        return currentObjectSO.isTextured &&
               currentObjectSO.texturedMaterial != null &&
               currentObjectSO.texturedElements != null &&
               currentObjectSO.texturedElements.Count > 0 &&
               (index == -1 || (index >= 0 && index < currentObjectSO.texturedElements.Count));
    }

    public void SetTiling(Vector2 newTiling)
    {
        foreach (Material mat in editableMaterials)
        {
            if (mat != null)
                mat.mainTextureScale = newTiling;
        }
    }


    public void SetTextureByIndex(int index)
    {
        if (currentObjectSO == null || currentObjectSO.texturedElements == null || index < 0 || index >= currentObjectSO.texturedElements.Count)
            return;

        TexturedElement selectedElement = currentObjectSO.texturedElements[index];

        foreach (Material mat in editableMaterials)
        {
            if (mat != null)
            {
                mat.mainTexture = selectedElement.mainTexture;

                if (selectedElement.normalMap != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", selectedElement.normalMap);
                }
                else
                {
                    mat.DisableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", null); // Clear previous bump
                }
            }
        }

        currentTextureIndex = index;
    }


}