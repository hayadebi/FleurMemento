using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thisadmob_objclick : MonoBehaviour
{
    private GameObject clickedGameObject;
    public string OpenURLText = "";
    public Camera ray_cm = null;
    private void Start()
    {
        if (ray_cm == null)
            ray_cm = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            clickedGameObject = null;
            Ray ray = ray_cm.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "noactive")
                clickedGameObject = hit.collider.gameObject;
            if (clickedGameObject != null && clickedGameObject == this.gameObject)
                Application.OpenURL(OpenURLText);
        }
    }
}
