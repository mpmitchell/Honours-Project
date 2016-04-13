using System.Collections.Generic;

public class Branch {

  public static Branch goalBranch = null;
  public static int branchNumber = 0;

  public LinkedList<Node> nodes = new LinkedList<Node>();
  public Branch parent = null;
  public int number = 0;
  public bool critical = false;
  public bool locked = false;
  public int locks = 0;
  public int keys = 0;

  public Branch(Node node) {
    number = branchNumber++;

    if (node.type == Type.Lock) {
      locked = true;
      locks++;
    }
    Add(node);
  }

  public Branch(Node node, Branch parent) {
    this.parent = parent;
    locks = parent.locks;

    number = branchNumber++;

    if (node.type == Type.Lock) {
      locked = true;
      locks++;
    }
    Add(node);
  }

  public void Add(Node node) {
    node.branchNumber = number;
    nodes.AddLast(node);

    if (node.type == Type.Goal) {
      goalBranch = this;
    } else if (node.type == Type.Key) {
      keys++;
    }
  }

  public void SetCritical() {
    critical = true;

    foreach (Node node in nodes) {
      node.critical = true;
    }
  }
}
