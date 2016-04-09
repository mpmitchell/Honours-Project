using System.Collections.Generic;

public class Node {

  public Node parent = null;
  public LinkedList<Node> children = new LinkedList<Node>();
  public Type type;

  public Node(Type type) {
    this.type = type;
  }

  public int getDepth() {
    Node node = this;
    int depth = 0;

    while (node != null) {
      node = node.parent;
      depth++;
    }

    return depth;
  }

  public Node addChild(Type type) {
    Node node = new Node(type);
    children.AddLast(node);
    node.parent = this;
    return node;
  }

  public Node insertChild(Type type) {
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
