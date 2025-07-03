using UnityEngine;
using UnityEngine.UI;
public class EstiloRandom : MonoBehaviour
{
    [SerializeField] private Image imagenDevolucion;
    [SerializeField] private Sprite[] imagenesPositivas;

    private void Start()
    {
        if (imagenesPositivas.Length == 0) return;

        int indice = Random.Range(0, imagenesPositivas.Length);
        imagenDevolucion.sprite = imagenesPositivas[indice];
        imagenDevolucion.gameObject.SetActive(true);
    }
}
