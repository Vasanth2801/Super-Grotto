using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPooling pooler;
    [SerializeField] private GameObject FX;

    private void Start()
    {
        pooler = FindObjectOfType<ObjectPooling>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        gameObject.SetActive(false);

        Instantiate(FX,transform.position,Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(FX);
    }
}