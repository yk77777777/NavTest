using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace kinjo{
    public class EnemyController : MonoBehaviour
    {
        public Transform player;
        //索敵範囲
        public float traceDist = 10.0f;
        NavMeshAgent nav;
        // Start is called before the first frame update
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            //毎フレーム距離の計測をする必要はないのでコルーチンで行う
            StartCoroutine(CheckDist());
        }

        IEnumerator CheckDist()
        {
            while (true)
            {
                //1秒間に５回距離を計測する
                yield return new WaitForSeconds(0.2f);
                //プレイヤーとの距離を計測
                float dist = Vector3.Distance(player.position, transform.position);
                //索敵範囲に入ったか？
                if (dist < traceDist)
                {
                    //プレイヤーの位置を目的地に設定
                    nav.SetDestination(player.position);
                    //追跡再開
                    nav.isStopped = false;
                }
                else
                {
                    //索敵範囲から出たら追跡終了
                    nav.isStopped = true;
                }
            }
        }
    }
}