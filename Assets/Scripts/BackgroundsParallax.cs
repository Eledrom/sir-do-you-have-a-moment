using UnityEngine;

public class BackgroundsParallax : MonoBehaviour
{
    private Material mat;
    private float distance;

    [Range(0f, .5f)]
    public float speed;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.up * distance);
    }
}
