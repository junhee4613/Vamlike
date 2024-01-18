using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] public GameObject missile;
    [SerializeField] public GameObject target;
    [SerializeField] public float spd;
    [SerializeField] public int shot = 12;

    public void Shot()
    {
        StartCoroutine(CreateMissile());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            Shot();
        }
    }

    IEnumerator CreateMissile()
    {
        int _shot = shot;
        while (_shot > 0)
        {
            _shot--;
            GameObject temp = Instantiate(missile, transform.position, Quaternion.identity);
            temp.GetComponent<BezieMissile>().master = gameObject;
            temp.GetComponent<BezieMissile>().enemy = target;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
