using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContoller : MonoBehaviour
{
    public List<Shape> gameShapes;
    public Dictionary<string, Shape> shapeDictionary;
    public Queue<Shape> shapeQueue;
    public Stack<Shape> shapeStack; 
    //oreShapes.Add("rectangle");
    //oreShapes.Insert(2, "triangle");
    //reShapes.Sort(); //alphabetical

    // Start is called before the first frame update
    void Start()
    {
        shapeQueue = new Queue<Shape>();
        shapeDictionary = new Dictionary<string, Shape>();
        shapeStack = new Stack<Shape>();

        foreach(Shape shape in gameShapes)
        {
            shapeDictionary.Add(shape.Name, shape);
        }

        shapeQueue.Enqueue(shapeDictionary["Triangle"]);
        shapeQueue.Enqueue(shapeDictionary["Square"]);
        shapeQueue.Enqueue(shapeDictionary["Octagon"]);
        shapeQueue.Enqueue(shapeDictionary["Circle"]);

        shapeStack.Push(shapeDictionary["Triangle"]);
        shapeStack.Push(shapeDictionary["Square"]);
        shapeStack.Push(shapeDictionary["Octagon"]);
        shapeStack.Push(shapeDictionary["Circle"]);
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shapeStack.Count > 0)
            {
                Shape shape = shapeStack.Pop();
                shape.SetColor(Color.green);
            }
            else
            {
                Debug.Log("the stack is empty");
            }
        }
    } 
    private void DeQueueExample()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shapeQueue.Count > 0)
            {
                Shape shape = shapeQueue.Dequeue();
                shape.SetColor(Color.blue);
            }
            else
            {
                Debug.Log("the queue is empty");
            }
        }
    }
    private void SetRedByName(string shapeName)
    {
        shapeDictionary[shapeName].SetColor(Color.red);
    }

    private void FindExample()
    {
        Shape octagon = gameShapes.Find(s => s.name == "Octagon");
        shapeDictionary["Octagon"].SetColor(Color.red);
    }
    private void DictionaryExample()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetRedByName("Square");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetRedByName("Circle");
        }
    }
    
}
