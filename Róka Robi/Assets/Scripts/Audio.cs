using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Audio : MonoBehaviour
    {

        [SerializeField] private AudioSource landing;
        [SerializeField] private AudioSource hurt;
        [SerializeField] private AudioSource player_jump;
        [SerializeField] private AudioSource enemy_death;
        [SerializeField] private AudioSource music;
        [SerializeField] private LayerMask ground;
        [SerializeField] private AudioSource cherry;
        private Collider2D coll;

        private void Start()
        {
            coll = GameObject.Find("Player").GetComponent<Collider2D>();
        }



        public void Landing()
        {
            landing.Play();
        }

        public void Hurt()
        {
            hurt.Play();
        }

        public void JumpSound()
        {
            player_jump.Play();
        }

        public void EnemyDeath()
        {
            enemy_death.Play();
        }

        public void Music()
        {
            music.Play();
        }

        public void CherrySound()
        {
            cherry.Play();
        }

    }