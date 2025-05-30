using System.Collections.Generic;
using UnityEngine;

public class NextSwitcher : MonoBehaviour
{
    public List<GameObject> objectsToSwitch;
    private int currentIndex = 0;

    private void Start()
    {
        // Make sure only the first object is active at start
        for (int i = 0; i < objectsToSwitch.Count; i++)
        {
            objectsToSwitch[i].SetActive(i == currentIndex);
        }
    }

    // Activate next object in the list (loop around)
    public void SwitchNext()
    {
        if (objectsToSwitch.Count == 0)
            return;

        // Deactivate current
        objectsToSwitch[currentIndex].SetActive(false);

        currentIndex++;
        if (currentIndex >= objectsToSwitch.Count)
            currentIndex = 0;

        // Activate new
        objectsToSwitch[currentIndex].SetActive(true);
    }

    // Activate previous object in the list (loop around)
    public void SwitchPrevious()
    {
        if (objectsToSwitch.Count == 0)
            return;

        // Deactivate current
        objectsToSwitch[currentIndex].SetActive(false);

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = objectsToSwitch.Count - 1;

        // Activate new
        objectsToSwitch[currentIndex].SetActive(true);
    }
}

