using System;
using UnityEngine;



    [System.Serializable]
    public class BrickManager  
    {
        [SerializeField] private ParticlesPlayer particlesPlayer;
        [SerializeField] private Bricks bricks;
        public event Action AllBricksDestroyedEvent;

        public void Initialize() {
            bricks.Initialize();
            particlesPlayer.Initialize();

           // foreach (var brick in bricks)
         //       brick.HitPointsOutEvent += OnBrickDestroy;
        }
        
        private void OnBrickDestroy(Brick sender) {
            particlesPlayer.PlayDestroy(sender.transform.position);
            bricks.Destroy(sender);
            
            if(bricks.count <=0)
                AllBricksDestroyedEvent?.Invoke();
        }
    }
