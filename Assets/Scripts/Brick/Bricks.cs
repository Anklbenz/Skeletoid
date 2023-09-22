using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Bricks : MonoBehaviour, IEnumerable<Brick>
    {
        public int count => _bricks.Count;
        private List<Brick> _bricks = new();

        public void Initialize() {
            GetBricks();
        }

        private void GetBricks() {
            foreach (Transform child in transform) {
                var brick = child.GetComponent<Brick>();
                if (brick)
                    _bricks.Add(brick);
            }
        }

        public IEnumerator<Brick> GetEnumerator() {
            return _bricks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Destroy(Brick brick) {
            _bricks.Remove(brick);
            Destroy(brick.gameObject);
        }
    }
