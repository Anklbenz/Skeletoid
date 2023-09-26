using System;
using UnityEngine;



    [System.Serializable]
    public class BrickManager  
    {
        [SerializeField] private ParticlesFactory _particlesFactory;
        [SerializeField] private Bricks bricks;
        public event Action AllBricksDestroyedEvent;

        public void Initialize() {
            bricks.Initialize();
        //    particlesPlayer.Initialize();

           // foreach (var brick in bricks)
         //       brick.HitPointsOutEvent += OnBrickDestroy;
        }
        
        private void OnBrickDestroy(Brick sender) {
            _particlesFactory.PlayDestroy(sender.transform.position);
            bricks.Destroy(sender);
            
            if(bricks.count <=0)
                AllBricksDestroyedEvent?.Invoke();
        }
    }
