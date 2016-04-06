using UnityEngine;

public class MovingEntity : Entity {

  protected bool moving = false;
  new protected Direction direction = Direction.Down;

  Vector2 colliderExtents;

  const float BOX_CAST_DISTANCE = 0.1f;

  void Awake() {
    animator = GetComponent<Animator>();
    animator.GetBehaviour<DeathAnimator>().target = gameObject;
    colliderExtents = GetComponent<BoxCollider2D>().bounds.extents;
  }

  protected void SetDirection(float dx, float dy) {
    if (dx < 0.0f) {
      direction = Direction.Left;
      animator.SetInteger("Direction", (int)direction);
    } else if (dx > 0.0f) {
      direction = Direction.Right;
      animator.SetInteger("Direction", (int)direction);
    }

    if (dy < 0.0f) {
      direction = Direction.Down;
      animator.SetInteger("Direction", (int)direction);
    } else if (dy > 0.0f) {
      direction = Direction.Up;
      animator.SetInteger("Direction", (int)direction);
    }
  }

  protected void UpdateDirection() {
    animator.SetInteger("Direction", (int)direction);
  }

  public override Vector3 GetDirectionVector() {
    switch (direction) {
      case Direction.Left: {
        return new Vector3(-1.0f, 0.0f, 0.0f);
      }

      case Direction.Right: {
        return new Vector3(1.0f, 0.0f, 0.0f);
      }

      case Direction.Down: {
        return new Vector3(0.0f, -1.0f, 0.0f);
      }

      case Direction.Up: {
        return new Vector3(0.0f, 1.0f, 0.0f);
      }
    }

    return Vector3.zero;
  }

  public bool CollisionCheck(ref float dx, ref float dy) {
    float left =   transform.position.x + dx - colliderExtents.x;
    float right =  transform.position.x + dx + colliderExtents.x;
    float bottom = transform.position.y + dy - colliderExtents.y;
    float top =    transform.position.y + dy + colliderExtents.y;

    bool hit = false;

    if (dx < 0.0f) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(left, bottom), Vector2.left, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(left, top), Vector2.left, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitBottom.collider != null) {
        dx = 0.0f;
        hitBottom.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
        hit = true;
      } else if (hitTop.collider != null) {
        dx = 0.0f;
        hitTop.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
        hit = true;
      }
    } else if (dx > 0.0f) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(right, bottom), Vector2.right, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(right, top), Vector2.right, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitBottom.collider != null) {
        dx = 0.0f;
        hitBottom.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
        hit = true;
      } else if (hitTop.collider != null) {
        dx = 0.0f;
        hitTop.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
        hit = true;
      }
    }

    if (dy < 0.0f) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, bottom), Vector2.down, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, bottom), Vector2.down, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitLeft.collider != null) {
        dy = 0.0f;
        hitLeft.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
        hit = true;
      } else if (hitRight.collider != null) {
        dy = 0.0f;
        hitRight.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
        hit = true;
      }
    } else if (dy > 0.0f) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, top), Vector2.up, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, top), Vector2.up, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitLeft.collider != null) {
        dy = 0.0f;
        hitLeft.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
        hit = true;
      } else if (hitRight.collider != null) {
        dy = 0.0f;
        hitRight.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
        hit = true;
      }
    }

    return hit;
  }

  public bool CollisionCheck(ref float dx, ref float dy, out GameObject hit) {
    float left =   transform.position.x + dx - colliderExtents.x;
    float right =  transform.position.x + dx + colliderExtents.x;
    float bottom = transform.position.y + dy - colliderExtents.y;
    float top =    transform.position.y + dy + colliderExtents.y;

    hit = null;

    if (dx < 0.0f) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(left, bottom), Vector2.left, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(left, top), Vector2.left, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitBottom.collider != null) {
        dx = 0.0f;
        hitBottom.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
        hit = hitBottom.collider.gameObject;
      } else if (hitTop.collider != null) {
        dx = 0.0f;
        hitTop.collider.SendMessage("PushLeft", SendMessageOptions.DontRequireReceiver);
        hit = hitTop.collider.gameObject;
      }
    } else if (dx > 0.0f) {
      RaycastHit2D hitBottom = Physics2D.Raycast(new Vector2(right, bottom), Vector2.right, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitTop = Physics2D.Raycast(new Vector2(right, top), Vector2.right, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitBottom.collider != null) {
        dx = 0.0f;
        hitBottom.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
        hit = hitBottom.collider.gameObject;
      } else if (hitTop.collider != null) {
        dx = 0.0f;
        hitTop.collider.SendMessage("PushRight", SendMessageOptions.DontRequireReceiver);
        hit = hitTop.collider.gameObject;
      }
    }

    if (dy < 0.0f) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, bottom), Vector2.down, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, bottom), Vector2.down, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitLeft.collider != null) {
        dy = 0.0f;
        hitLeft.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
        hit = hitLeft.collider.gameObject;
      } else if (hitRight.collider != null) {
        dy = 0.0f;
        hitRight.collider.SendMessage("PushDown", SendMessageOptions.DontRequireReceiver);
        hit = hitRight.collider.gameObject;
      }
    } else if (dy > 0.0f) {
      RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(left, top), Vector2.up, BOX_CAST_DISTANCE, obstacleLayerMask);
      RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(right, top), Vector2.up, BOX_CAST_DISTANCE, obstacleLayerMask);
      if (hitLeft.collider != null) {
        dy = 0.0f;
        hitLeft.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
        hit = hitLeft.collider.gameObject;
      } else if (hitRight.collider != null) {
        dy = 0.0f;
        hitRight.collider.SendMessage("PushUp", SendMessageOptions.DontRequireReceiver);
        hit = hitRight.collider.gameObject;
      }
    }

    return hit != null;
  }

  protected void SetMoving(float dx, float dy) {
    if (dx == 0.0f && dy == 0.0f) {
      if (moving) {
        animator.SetTrigger("StoppedMoving");
        moving = false;
      }
    } else {
      if (!moving) {
        animator.SetTrigger("StartedMoving");
        moving = true;
      }
    }
  }
}
