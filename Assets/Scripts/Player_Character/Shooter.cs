using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    private GameObject _missile;
    private Camera _camera;
    
    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            HitDetector target = hitObject.GetComponent<HitDetector>();
            if (target != null)
            {
                ProjectileLaunch();
                target.HitReaction();
            }
            else
            {
                ProjectileLaunch();
                //StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    private void ProjectileLaunch()
    {
        _missile = Instantiate(missilePrefab) as GameObject;
        _missile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _missile.transform.rotation = transform.rotation;
    }

    /*private IEnumerator SphereIndicator(Vector3 pos)
    {

        ///implement damage on the walls instead///

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }*/
}
