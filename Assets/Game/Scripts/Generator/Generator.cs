using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

  [SerializeField] bool overrideSeed = false;
  [SerializeField] int seed;

  void Start() {
    if (overrideSeed) {
      Random.seed = seed;
      Debug.Log("Seed " + seed);
    } else {
      seed = Random.seed;
      Debug.Log("Seed " + seed);
    }

    ProcessGraph(new Node(Type.Level));
  }

  void ProcessGraph(Node root) {
    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(root);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();
      Type currentType = node.type;

      node = Rules.Parse(node);

      if (node.type != currentType) {
        openList.Enqueue(node);
      } else {
        foreach (Node child in node.children) {
          openList.Enqueue(child);
        }
      }
    }

    PrintGraph(root);
  }

  void PrintGraph(Node root) {
    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(root);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();

      Debug.Log(node.getDepth() + "\t" + (node.parent == null ? "NULL" : node.parent.type.ToString()) + "\t" + node.type.ToString());

      foreach (Node child in node.children) {
        openList.Enqueue(child);
      }
    }
  }
}
