using System.Collections.Generic;
using UnityEngine;

public class PlatformCanon : MonoBehaviour
{

    private List<GameObject> _bullets;
    private GameObject _spawner;
    private Vector3 _bulletStartPoint;
    private int _spawnCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name != "Shark Generator")
        {
            _bullets = new List<GameObject>();
            /*
            _bullets.Add(gameObject.transform.Find("/" + gameObject.name + " / Bullet1").gameObject);
            _bullets.Add(gameObject.transform.Find("/" + gameObject.name + " / Bullet2").gameObject);
            _bullets.Add(gameObject.transform.Find("/" + gameObject.name + " / Bullet3").gameObject);
            _bullets.Add(gameObject.transform.Find("/" + gameObject.name + " / Bullet4").gameObject);
            _bullets.Add(gameObject.transform.Find("/" + gameObject.name + " / Bullet5").gameObject);
            */



            foreach (Transform _bullet in gameObject.transform)
            {

              

                if (_bullet.name == "Bullet1" || _bullet.name == "Bullet2" || _bullet.name == "Bullet3" || _bullet.name == "Bullet4" || _bullet.name == "Bullet5")
                {
                    _bullet.tag = "LiveBullet";
                    _bullet.GetComponent<Bullet>().home = _bullet.position;


                    _bullets.Add(_bullet.gameObject);
                    

                    
                        _bulletStartPoint = _bullet.position;
                }

            }

            _spawner = gameObject.transform.Find("CanonSpawn").gameObject;
            
            InvokeRepeating("Spawn", 0, 2);
        }
    }

    private void Update()
    {
        if(GameState.Reset == GameManager.gm.State)
        {
            foreach (GameObject _bullet in _bullets)
            {

                _bullet.transform.position = _bulletStartPoint;
            
            }


        }
    }

    private void Spawn()
    {

        if (5 == _spawnCount) _spawnCount = 0;


        _bullets[_spawnCount].transform.position = new Vector3(_spawner.transform.position.x, _spawner.transform.position.y, _spawner.transform.position.z);
        _bullets[_spawnCount].GetComponent<Bullet>().on = true;
        _spawnCount++;

    }
}
