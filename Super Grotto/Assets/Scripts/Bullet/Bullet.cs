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
        gameObject.SetActive(false);

        GameObject effect = Instantiate(FX,transform.position,Quaternion.identity);

        effect.SetActive(false);
    }
}