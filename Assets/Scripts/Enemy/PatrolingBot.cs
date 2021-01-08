using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingBot : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    private GameObject _missile;

    public float movingSpeed = 3.0f;
    public float obstacleRange = 5.0f;

    private bool _alive;
    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, movingSpeed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if(hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_missile == null)
                    {
                        _missile = Instantiate(missilePrefab) as GameObject;
                        _missile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _missile.transform.rotation = transform.rotation;
                    }
                }
                if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }    
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
