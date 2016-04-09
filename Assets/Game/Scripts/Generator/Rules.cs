using UnityEngine;
using System.Collections.Generic;

public class Rules {

  public static Node Parse(Node node) {
    switch (node.type) {
      case Type.Level: {
        node.type = Type.CriticalPath;
        node.addChild(Type.Lock)
            .addChild(Type.Item)
            .addChild(Type.Hook)
            .addChild(Type.Boss)
            .addChild(Type.Goal);
        break;
      }

      case Type.CriticalPath: {
        node.type = Type.Entrance;
        node.insertChild(Type.LinearChain);

        node.addChild(Type.Room);

        if (Random.value <= 0.60f) {
          node.addChild(Type.Room);
        }
        break;
      }

      case Type.LinearChain: {
        node.type = Type.Room;

        float random = Random.value;
        if (random <= 0.3f) {
          node.addChild(Type.Room);
        } else if (random <= 0.6f) {
          node.addChild(Type.Room);
          node.addChild(Type.Room);
        } else {
          node.addChild(Type.Room);
          node.addChild(Type.Room);
          node.addChild(Type.Room);
        }
        break;
      }
    }

    return node;
  }
}
