using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_sc : MonoBehaviour
{
    public static GameManager_sc instance;
    [SerializeField]
    Transform[] _Tiles;
    [SerializeField]
    int _Points;
    [SerializeField]
    UnityEngine.UI.Text _TextPoints;
    [SerializeField]
    UnityEngine.UI.Text _TextHelth;
    public Text TextHelth { get => _TextHelth; set => _TextHelth = value; }
    [SerializeField]
    Queue<Transform> _Stack_Tile=new Queue<Transform>();
    private void Awake()
    {
           instance = this;
        foreach (var tile in _Tiles)
            _Stack_Tile.Enqueue(tile);
    }

    public void UpdateLevel()
    {
        _TextPoints.text = _Points++.ToString();
        var tile = _Stack_Tile.Dequeue();
        tile.position +=  Vector3.forward * 6 * _Tiles.Length;
        _Stack_Tile.Enqueue(tile);
    }


}
