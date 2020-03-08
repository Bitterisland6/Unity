using UnityEngine;
public class ArrowDirection : MonoBehaviour
{
    public GameObject ground;
    void Start()
    {
        System.Random rnd = new System.Random();
        ground.transform.Rotate(0, rnd.Next(0, 3) * 90, 0);
    }
}
