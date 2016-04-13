using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

  [Header("Seed")]
  [SerializeField] bool overrideSeed = false;
  [SerializeField] int seed;

  [Header("Hierarchy")]
  [SerializeField] Transform rooms;
  [SerializeField] Transform doors;
  [SerializeField] Transform bombableWalls;

  [Header("Room Prefabs")]
  [SerializeField] GameObject roomPrefab;
  [SerializeField] GameObject entrancePrefab;
  [SerializeField] GameObject bossPrefab;
  [SerializeField] GameObject goalPrefab;

  [Header("Wall Prefabs")]
  [SerializeField] GameObject leftWall;
  [SerializeField] GameObject rightWall;
  [SerializeField] GameObject downWall;
  [SerializeField] GameObject upWall;

  [Header("Door Prefabs")]
  [SerializeField] GameObject leftDoor;
  [SerializeField] GameObject rightDoor;
  [SerializeField] GameObject downDoor;
  [SerializeField] GameObject upDoor;

  [Header("Object Prefabs")]
  [SerializeField] Obstacle[] standardRoomPrefabs;
  [SerializeField] Obstacle[] bonusItemRoomPrefabs;
  [SerializeField] Obstacle[] itemRoomPrefabs;
  [SerializeField] Enemy[] enemyPrefabs;
  [SerializeField] Enemy[] itemEnemyPrefabs;
  [SerializeField] GameObject keyPrefab;
  [SerializeField] GameObject mapPrefab;
  [SerializeField] GameObject compassPrefab;
  [SerializeField] GameObject bowPrefab;
  [SerializeField] GameObject stairPrefab;
  [SerializeField] GameObject lockedStairPrefab;

  Node graph;
  Dictionary<int, Branch> branches = new Dictionary<int, Branch>();
  LinkedList<Node> criticalPath = new LinkedList<Node>();
  Dictionary<int, Node[,]> maps = new Dictionary<int, Node[,]>();

  int currentMap = 0;

  void Start() {
    Branch.branchNumber = 0;

    if (overrideSeed) {
      Random.seed = seed;
    }

    Logger.Log("Seed," + Seed.seed.ToString());

    foreach (Obstacle obstacle in standardRoomPrefabs) {
      obstacle.used = 0;
    }
    foreach (Obstacle obstacle in bonusItemRoomPrefabs) {
      obstacle.used = 0;
    }
    foreach (Obstacle obstacle in itemRoomPrefabs) {
      obstacle.used = 0;
    }

    GenerateGraph();
    AnnotateGraph();
    CalculateChallengeRatings();

    NewMap();
    GenerateRooms();
    FillWalls();
    FillRooms();
  }

  KeyValuePair<int, Node[,]> NewMap() {
    Node[,] map = new Node[7,8];
    for (int x = 0; x < 7; x++) {
      for (int y = 0; y < 8; y++) {
        map[x, y] = null;
      }
    }
    maps.Add(currentMap, map);
    return new KeyValuePair<int, Node[,]>(currentMap++, map);
  }

  void GenerateGraph() {
    graph = new Node(Type.Level);

    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(graph);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();
      Type currentType = node.type;

      switch (node.type) {
        case Type.Level: {
          node.type = Type.Entrance;
          node.AddChild(Type.Lock)
              .AddChild(Type.Hook)
              .AddChild(Type.CriticalPath)
              .AddChild(Type.KeyPath)
              .AddChild(Type.Lock)
              .AddChild(Type.CriticalPath)
              .AddChild(Type.ItemPath)
              .AddChild(Type.Hook)
              .AddChild(Type.Boss)
              .AddChild(Type.Goal);
          break;
        }

        case Type.Entrance: {
          node.AddChild(Type.Key);

          if (Random.value <= 0.60f) {
            node.AddChild(Type.Room);
          }
          break;
        }

        case Type.CriticalPath: {
          node.type = Type.CriticalRoom;

          if (Random.value <= 0.7f) {
            Node criticalRoom = node.InsertChild(Type.CriticalRoom);

            if (Random.value <= 0.3f) {
              criticalRoom.InsertChild(Type.CriticalRoom);
            }
          }
          break;
        }

        case Type.ItemPath: {
          if (Random.value <= 0.5f) {
            node.type = Type.Room;

            if (Random.value <= 0.4f) {
              node.AddChild(Type.Room)
                  .AddChild(Type.Item);
            } else {
              node.AddChild(Type.Item);
            }
          } else {
            node.type = Type.Item;
          }
          break;
        }

        case Type.KeyPath: {
          if (Random.value <= 0.3f) {
            node.type = Type.Key;
          } else {
            node.type = Type.Room;

            if (Random.value <= 0.5f) {
              node.AddChild(Type.Key);
            } else {
              node.AddChild(Type.Room)
                  .AddChild(Type.Key);
            }
          }
          break;
        }

        case Type.Hook: {
          node.type = Type.Room;

          if (Random.value <= 0.6f) {
            node.InsertChild(Type.Lock);
            node.AddChild(Type.KeyPath);
          }

          node.AddChild(Type.BonusItem);
          break;
        }

        // case Type.CriticalRoom: {
        //   if (Random.value <= 0.1f) {
        //     if (Random.value <= 0.5f) {
        //       node.AddChild(Type.KeyPath);
        //       node.AddChild(Type.Lock)
        //           .AddChild(Type.BonusItem);
        //     } else {
        //       node.AddChild(Type.BonusItem);
        //     }
        //   }
        //   break;
        // }
      }

      if (node.type != currentType) {
        openList.Enqueue(node);
      } else {
        foreach (Node child in node.children) {
          openList.Enqueue(child);
        }
      }
    }
  }

  void AnnotateGraph() {
    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(graph);

    branches.Add(0, new Branch(graph));

    while (openList.Count > 0) {
      Node node = openList.Dequeue();

      foreach (Node child in node.children) {
        if (child == node.children.First.Value && child.type != Type.Lock) {
          branches[node.branchNumber].Add(child);
        } else {
          Branch newBranch = new Branch(child, branches[node.branchNumber]);
          branches.Add(newBranch.number, newBranch);
        }
        openList.Enqueue(child);
      }
    }

    int locks = 0;
    int keys = 0;
    Branch branch = Branch.goalBranch;
    while (branch != null) {
      for (LinkedListNode<Node> node = branch.nodes.Last; node != null; node = node.Previous) {
        criticalPath.AddFirst(node.Value);
      }

      branch.SetCritical();

      if (branch.locked) {
        locks++;
      }

      keys += branch.keys;
      branch = branch.parent;
    }

    Queue<Branch> branchList = new Queue<Branch>();
    branchList.Enqueue(branches[0]);

    while (locks > keys) {
      branch = branchList.Dequeue();

      foreach (Node node in branch.nodes) {
        foreach (Node child in node.children) {
          if (child.branchNumber != branch.number) {
            Branch childBranch = branches[child.branchNumber];

            if (!child.critical) {
              if (childBranch.keys > 0) {
                // Find closest parent on critical path
                LinkedList<Branch> chain = new LinkedList<Branch>();
                chain.AddFirst(childBranch);
                Branch parentBranch = branch;
                while (!parentBranch.critical) {
                  chain.AddFirst(parentBranch);
                  parentBranch = parentBranch.parent;
                }

                // Insert branches into critical path
                LinkedListNode<Node> branchNode = criticalPath.Find(node);
                foreach (Branch link in chain) {
                  foreach (Node linkNode in link.nodes) {
                    branchNode = criticalPath.AddAfter(branchNode, linkNode);
                  }

                  link.SetCritical();
                }

                keys += childBranch.keys;
              }
            }

            branchList.Enqueue(childBranch);
          }
        }
      }
    }
  }

  void CalculateChallengeRatings() {
    int maxDepth = 0;
    foreach (Node node in criticalPath) {
      if (node.type != Type.Lock) {
        node.criticalDepth = maxDepth++;
      } else {
        node.criticalDepth = node.parent.criticalDepth;
      }
    }

    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(graph);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();

      if (!node.critical) {
        node.criticalDepth = node.parent.criticalDepth;
      }

      node.challengeRating = (100 / maxDepth) * node.criticalDepth;

      foreach (Node child in node.children) {
        openList.Enqueue(child);
      }
    }
  }

  void GenerateRooms() {
    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(graph);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();

      switch (node.type) {
        case Type.Entrance: {
          GameObject room = Instantiate(entrancePrefab) as GameObject;
          room.GetComponent<Room>().node = node;
          room.transform.parent = rooms;

          node.position = new MapVector(3, 0, 0);
          maps[node.position.mapNumber][3, 0] = node;
          node.room = room.GetComponent<Room>();
          break;
        }

        case Type.Key:
        case Type.Item:
        case Type.BonusItem:
        case Type.CriticalRoom:
        case Type.Room: {
          GenerateRoom(roomPrefab, ref node);
          break;
        }

        case Type.Lock: {
          node.position = node.parent.position;
          break;
        }

        case Type.Boss: {
          GenerateRoom(bossPrefab, ref node);
          break;
        }

        case Type.Goal: {
          GenerateRoom(goalPrefab, ref node);
          break;
        }
      }

      if (node.parent != null && node.position.mapNumber != node.parent.position.mapNumber) {
        if (node.parent.type == Type.Lock) {
          node.parent.parent.stairs++;
        } else {
          node.parent.stairs++;
        }

        node.stairs++;
      }

      foreach (Node child in node.children) {
        openList.Enqueue(child);
      }
    }
  }

  void GenerateRoom(GameObject prefab, ref Node node) {
    GameObject room = Instantiate(prefab) as GameObject;
    room.GetComponent<Room>().node = node;
    room.transform.parent = rooms;

    node.position = GetEmptyMapSquare(node);
    maps[node.position.mapNumber][node.position.x, node.position.y] = node;
    node.room = room.GetComponent<Room>();

    if (node.position.mapNumber != node.parent.position.mapNumber) {
      if (node.parent.type == Type.Lock) {
        node.parent.parent.room.name = "BREAK " + node.parent.parent.room.name;
      } else {
        node.parent.room.name = "BREAK " + node.parent.room.name;
      }
    }

    room.name = node.type.ToString() + "\t" + (node.critical ? "c " : "nc ") + node.criticalDepth + "\t" + node.position.mapNumber + "," + node.position.x + "," + node.position.y;
    room.transform.Translate(new Vector2((node.position.mapNumber * 105) + (node.position.x - 3) * 15, node.position.y * 11));
  }

  MapVector GetEmptyMapSquare(Node node) {
    LinkedList<MapVector> openList = new LinkedList<MapVector>();

    if (node.parent.position.x - 1 >= 0 && maps[node.parent.position.mapNumber][node.parent.position.x - 1, node.parent.position.y] == null) {
      openList.AddLast(new MapVector(node.parent.position.x - 1, node.parent.position.y, node.parent.position.mapNumber));
    }
    if (node.parent.position.x + 1 < 7 && maps[node.parent.position.mapNumber][node.parent.position.x + 1, node.parent.position.y] == null) {
      openList.AddLast(new MapVector(node.parent.position.x + 1, node.parent.position.y, node.parent.position.mapNumber));
    }
    if (node.parent.position.y - 1 >= 0 && maps[node.parent.position.mapNumber][node.parent.position.x, node.parent.position.y - 1] == null) {
      openList.AddLast(new MapVector(node.parent.position.x, node.parent.position.y - 1, node.parent.position.mapNumber));
    }
    if (node.parent.position.y + 1 < 8 && maps[node.parent.position.mapNumber][node.parent.position.x, node.parent.position.y + 1] == null) {
      openList.AddLast(new MapVector(node.parent.position.x, node.parent.position.y + 1, node.parent.position.mapNumber));
    }

    if (openList.Count > 0) {
      int index = Random.Range(0, openList.Count);
      LinkedListNode<MapVector> square = openList.First;
      for (int i = 0; i != index; i++, square = square.Next);
      return square.Value;
    }

    foreach (KeyValuePair<int, Node[,]> map in maps) {
      if (map.Key != node.parent.position.mapNumber) {
        for (int x = 0; x < 7; x++) {
          for (int y = 0; y < 8; y++) {
            if (map.Value[x, y] == null) {
              return new MapVector(x, y, map.Key);
            }
          }
        }
      }
    }

    KeyValuePair<int, Node[,]> newMap = NewMap();
    return new MapVector(node.parent.position.x, node.parent.position.y, newMap.Key);
  }

  void FillWalls() {
    Queue<Node> openList =  new Queue<Node>();
    openList.Enqueue(graph);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();

      if (node.type != Type.Lock) {
        bool[] connections = { false, false, false, false };

        // Get parent on same map
        if (node.parent != null && node.parent.position.mapNumber == node.position.mapNumber) {
          CheckAdjacency(node, node.parent, ref connections);
        }

        // Get children on same map
        foreach (Node child in node.children) {
          if (child.position.mapNumber == node.position.mapNumber) {
            if (child.type == Type.Lock) {
              foreach (Node lockChild in child.children) {
                if (lockChild.position.mapNumber == node.position.mapNumber) {
                  CheckAdjacency(node, lockChild, ref connections);
                }
              }
            } else {
              CheckAdjacency(node, child, ref connections);
            }
          }
        }

        // Place walls
        if (!connections[0]) {
          GameObject wall = Instantiate(leftWall) as GameObject;
          wall.transform.SetParent(node.room.transform, false);
        }

        if (!connections[1]) {
          GameObject wall = Instantiate(rightWall) as GameObject;
          wall.transform.SetParent(node.room.transform, false);
        }

        if (!connections[2]) {
          GameObject wall = Instantiate(downWall) as GameObject;
          wall.transform.SetParent(node.room.transform, false);
        }

        if (!connections[3]) {
          GameObject wall = Instantiate(upWall) as GameObject;
          wall.transform.SetParent(node.room.transform, false);
        }
      }

      foreach (Node child in node.children) {
        openList.Enqueue(child);
      }
    }
  }

  void CheckAdjacency(Node node, Node other, ref bool[] connections) {
    if (other.position.x < node.position.x) {
      connections[0] = true;
    } else if (other.position.x > node.position.x) {
      connections[1] = true;
    }

    if (other.position.y < node.position.y) {
      connections[2] = true;
    } else if (other.position.y > node.position.y) {
      connections[3] = true;
    }
  }

  void FillRooms() {
    Queue<Node> openList = new Queue<Node>();
    openList.Enqueue(graph);

    while (openList.Count > 0) {
      Node node = openList.Dequeue();

      if (node.type == Type.Lock) {
        if (node.position.mapNumber == node.children.First.Value.position.mapNumber) {
          bool[] connections = { false, false, false, false };
          CheckAdjacency(node, node.children.First.Value, ref connections);

          if (connections[0]) {
            GameObject door = Instantiate(leftDoor) as GameObject;
            door.transform.SetParent(node.parent.room.transform, false);
          } else if (connections[1]) {
            GameObject door = Instantiate(rightDoor) as GameObject;
            door.transform.SetParent(node.parent.room.transform, false);
          } else if (connections[2]) {
            GameObject door = Instantiate(downDoor) as GameObject;
            door.transform.SetParent(node.parent.room.transform, false);
          } else {
            GameObject door = Instantiate(upDoor) as GameObject;
            door.transform.SetParent(node.parent.room.transform, false);
          }
        }
      } else if (node.type != Type.Entrance) {
        AddObstacles(node);
        AddStairs(node);

        if (node.type != Type.Boss && node.type != Type.Goal) {
          AddItems(node);

          if (node.type != Type.Item) {
            AddEnemies(node);
          }
        }
      }

      foreach (Node child in node.children) {
        openList.Enqueue(child);
      }
    }
  }

  void AddObstacles(Node node) {
    if (node.type == Type.Item) {
      Obstacle[][] obstacleSets = { itemRoomPrefabs };
      GameObject obstacle = Instantiate(GetObstacle(node, obstacleSets).gameObject) as GameObject;
      obstacle.transform.SetParent(node.room.transform, false);
      node.obstacles = obstacle.GetComponent<Obstacle>();
    } else if (node.type == Type.BonusItem || node.type == Type.Key) {
      Obstacle[][] obstacleSets = { bonusItemRoomPrefabs };
      node.obstacles = GetObstacle(node, obstacleSets);
      GameObject obstacle = Instantiate(node.obstacles.gameObject) as GameObject;
      obstacle.transform.SetParent(node.room.transform, false);
    } else if (node.type == Type.Boss || node.type == Type.Goal) {
      foreach (Transform child in node.room.transform) {
        if (child.tag == "Obstacle") {
          node.obstacles = child.GetComponent<Obstacle>();
        }
      }
    } else {
      Obstacle[][] obstacleSets = { standardRoomPrefabs, bonusItemRoomPrefabs };
      GameObject obstacle = Instantiate(GetObstacle(node, obstacleSets).gameObject) as GameObject;
      obstacle.transform.SetParent(node.room.transform, false);
      node.obstacles = obstacle.GetComponent<Obstacle>();
    }
  }

  Obstacle GetObstacle(Node node, Obstacle[][] obstacleSets) {
    LinkedList<Obstacle> obstacles = new LinkedList<Obstacle>();
    int range = 10;

    while (obstacles.Count == 0) {
      foreach (Obstacle[] obstacleSet in obstacleSets) {
        foreach (Obstacle obstacle in obstacleSet) {
          if (obstacle.challengeRating - obstacle.used * 10 <= node.challengeRating + range && obstacle.challengeRating - obstacle.used * 10 >= node.challengeRating - range) {
            obstacles.AddLast(obstacle);
          }
        }
      }

      range += 10;
    }

    int index = Random.Range(0, obstacles.Count - 1);
    LinkedListNode<Obstacle> obstacleNode = obstacles.First;
    for (int i = 0; i != index; i++, obstacleNode = obstacleNode.Next);

    obstacleNode.Value.used++;
    return obstacleNode.Value;
  }

  void AddStairs(Node node) {
    if (node.parent.position.mapNumber != node.position.mapNumber) {
      Node parent = node.parent;
      GameObject prefab = stairPrefab;

      if (node.parent.type == Type.Lock) {
        parent = node.parent.parent;
        prefab = lockedStairPrefab;
      }

      GameObject stairs = Instantiate(prefab) as GameObject;
      StairPair pair = stairs.GetComponent<StairPair>();

      GameObject upStairs = pair.up;
      GameObject downStairs = pair.down;

      if (parent.position.mapNumber < node.position.mapNumber) {
        upStairs.GetComponent<Stairs>().room = parent.room;
        upStairs.transform.SetParent(parent.room.stairContainer.transform, false);
        upStairs.transform.localPosition = parent.obstacles.stairSpaces[--parent.stairs].localPosition;

        downStairs.GetComponent<Stairs>().room = node.room;
        downStairs.transform.SetParent(node.room.stairContainer.transform, false);
        downStairs.transform.localPosition = node.obstacles.stairSpaces[--node.stairs].localPosition;
      } else {
        upStairs.GetComponent<Stairs>().room = node.room;
        upStairs.transform.SetParent(node.room.stairContainer.transform, false);
        upStairs.transform.localPosition = node.obstacles.stairSpaces[--node.stairs].localPosition;

        downStairs.GetComponent<Stairs>().room = parent.room;
        downStairs.transform.SetParent(parent.room.stairContainer.transform, false);
        downStairs.transform.localPosition = parent.obstacles.stairSpaces[--parent.stairs].localPosition;
      }
    }
  }

  void AddItems(Node node) {
    if (node.type == Type.BonusItem) {
      if (Random.value <= 0.5f && compassPrefab != null || mapPrefab == null) {
        GameObject compass = Instantiate(compassPrefab) as GameObject;
        compass.transform.SetParent(node.room.transform, false);
        compass.transform.localPosition = node.obstacles.itemSpaces[Random.Range(0, node.obstacles.itemSpaces.Length - 1)].localPosition;
        compassPrefab = null;
      } else {
        GameObject map = Instantiate(mapPrefab) as GameObject;
        map.transform.SetParent(node.room.transform, false);
        map.transform.localPosition = node.obstacles.itemSpaces[Random.Range(0, node.obstacles.itemSpaces.Length - 1)].localPosition;
        mapPrefab = null;
      }
    } else if (node.type == Type.Item) {
      GameObject bow = Instantiate(bowPrefab) as GameObject;
      bow.transform.SetParent(node.room.transform, false);
      bow.transform.localPosition = node.obstacles.itemSpaces[Random.Range(0, node.obstacles.itemSpaces.Length - 1)].localPosition;
    } else if (node.type == Type.Key) {
      if (Random.value <= 0.8f) {
        GameObject key = Instantiate(keyPrefab) as GameObject;

        if (Random.value <= 0.7f) {
          key.transform.SetParent(node.room.rewardContainer.transform, false);
        } else {
          key.transform.SetParent(node.room.transform, false);
        }

        key.transform.localPosition = node.obstacles.itemSpaces[Random.Range(0, node.obstacles.itemSpaces.Length - 1)].localPosition;
      } else {
        GameObject itemEnemy = Instantiate(itemEnemyPrefabs[Random.Range(0, itemEnemyPrefabs.Length - 1)].gameObject) as GameObject;
        itemEnemy.GetComponent<Enemy>().room = node.room;
        itemEnemy.transform.SetParent(node.room.enemyContainer.transform, false);
        itemEnemy.transform.localPosition = GetOpenSpace(node);
      }
    }
  }

  void AddEnemies(Node node) {
    LinkedList<Enemy> enemies = new LinkedList<Enemy>();
    int enemyCount = (node.challengeRating - 3) * 7 / 100 + 3;
    int range = 10;

    while (enemies.Count < enemyCount) {
      foreach (Enemy enemy in enemyPrefabs) {
        if (enemy.challengeRating <= node.challengeRating + range && enemy.challengeRating >= node.challengeRating - range) {
          enemies.AddLast(enemy);
        }
      }

      range += 10;
    }

    LinkedListNode<Enemy> enemyNode = enemies.First;

    for (int i = 0; i < enemyCount; i++) {
      GameObject enemy = Instantiate(enemyNode.Value.gameObject) as GameObject;
      enemy.GetComponent<Enemy>().room = node.room;
      enemy.transform.SetParent(node.room.enemyContainer.transform, false);
      enemy.transform.localPosition = GetOpenSpace(node);
      enemyNode = enemyNode.Next;
      if (enemyNode == null) {
        enemyNode = enemies.First;
      }
    }
  }

  Vector3 GetOpenSpace(Node node) {
    Vector3 position = new Vector3(Random.Range(-6, 6), Random.Range(-4, 4), 0.0f);

    while (Physics2D.OverlapPoint(position + node.room.transform.position)) {
      position = new Vector3(Random.Range(-6, 6), Random.Range(-4, 4), 0.0f);
    }

    return position;
  }
}
