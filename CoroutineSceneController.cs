using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSceneController : MonoBehaviour
{
    public List<Shape> gameShapes;

    private Coroutine countToNumberRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            countToNumberRoutine = StartCoroutine(CountToNumber(2000));
            //StartCoroutine("CountToNumber", 2000);
            //StartCoroutine(CountToNumber(1000));
            StartCoroutine(SetShapesBlue());
            Debug.Log("Keypress Complete");
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            //StopAllCoroutines();
            StopCoroutine("CountToNumber");
        }
    }

    private IEnumerator CountToNumber(int NumberToCountTo)
    {
        for(int i = 0; i<= NumberToCountTo; i++)
        {
            Debug.Log(i);
            yield return null;
        }
    }
    private IEnumerator SetShapesBlue()
    {
        foreach (Shape shape in gameShapes)
        {
            shape.SetColor(Color.blue);
            yield return new WaitForSeconds(2);
            //waitUntil(true?)
            //WaitWhile(false?)
            //WaitForEndOfFrame()
            shape.SetColor(Color.white);
        }

        yield return new WaitForSecondsRealtime(1);
        Debug.Log("I just wasted a second");

        yield return StartCoroutine(SetShapesBlue());
    }
    private void SetShapesRed()
    {
        foreach(Shape shape in gameShapes)
        {
            shape.SetColor(Color.red);
        }
    }
}
