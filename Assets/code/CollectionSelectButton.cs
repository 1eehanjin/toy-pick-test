using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSelectButton : MonoBehaviour
{
    public int collectionNumber;




    void Start()
    {

    }

    private void OnMouseDown()
    {

        CollectionManager.OnCollectionSelected?.Invoke(collectionNumber);

    }
}