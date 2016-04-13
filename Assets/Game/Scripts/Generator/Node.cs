using UnityEngine;
using System.Collections.Generic;

public class Node {

  public Node parent = null;
  public LinkedList<Node> children = new LinkedList<Node>();
  public Type type;
  public int branchNumber;
  public bool critical;
  public int criticalDepth;
  public int challengeRating;
  public MapVector position;
  public Room room;
  public Obstacle obstacles;
  public int stairs = 0;
  public Node[] bombableConnections = { null, null, null, null };
  public GameObject[] walls = { null, null, null, null };

  public Node(Type type) {
    this.type = type;
  }

  public Node AddChild(Type type) {
    Node node = new Node(type);
    children.AddLast(node);
    node.parent = this;
    return node;
  }

  public Node InsertChild(Type type) {
    Node node = new Node(type);

    foreach (Node child in children) {
      node.children.AddLast(child);
      child.parent = node;
    }

    children.Clear();
    children.AddLast(node);
    node.parent = this;
    return node;
  }
}
