//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////libreria sobre cambios de escena
//using UnityEngine.SceneManagement;
//public class MenuFinal : MonoBehaviour
//{
//    [SerializeField] private GameObject _creditsObject;
//    public GameObject _menuObject;
//    [SerializeField] private GameObject[] imagess;
//    int _random;
//    private void Start()
//    {
//        //_random = Random.Range(0, imagess.Length);
//    }
//    public void Jugar()
//    {
//        SceneManager.LoadScene(1);
//    }
//    public void Salir()
//    {
//        //codigo para cerrar la aplicacion
//        Application.Quit();
//    }
//    public void ActivarOptions()
//    {
//        _menuObject.SetActive(false);
//        _creditsObject.SetActive(true);
//    }
//    public void DesactivarOptions()
//    {
//        _creditsObject.SetActive(false);
//        _menuObject.SetActive(true);
//    }
//    //public void metodoImagnes()
//    //{
//    // // Primero desactivamos todos los objetos del array
//    // foreach (var item in imagess)
//    // {
//    // item.SetActive(false);
//    // }
//    // // Elegimos uno aleatoriamente
//    // int randomIndex = Random.Range(0, imagess.Length);
//    // // Activamos solo el objeto aleatorio
//    // imagess[randomIndex].SetActive(true);
//    //}
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class CambioEscena : MonoBehaviour
{

    public void CargarNuevaEscena()
    {
        SceneManager.LoadScene(1);
    }

    public void CargarEscenaFinal()
    {
        SceneManager.LoadScene(2); 
    }

    public void QuitApplication()
    {
        Debug.Log("Cerrando la aplicación...");
        Application.Quit();
    }

}