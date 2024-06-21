using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SawScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RotateLeft();
    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward * -360 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Game over");
    }
}
