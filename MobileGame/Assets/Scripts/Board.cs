using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Board : MonoBehaviour
{

    public static Board Instance { get; private set; }
    public GameObject particle;
    public Row[] rows;
    public Tile[,] Tiles { get; private set; }
    public int Width => Tiles.GetLength(0);
    [SerializeField] private AudioClip matchSound;
    [SerializeField] private AudioSource audioSource;
    public int Height => Tiles.GetLength(1);
    private readonly List<Tile> _selection = new List<Tile>();
    private const float TweenDuration = 0.25f;
  private ParticleSystem _particleSystem;
    private void Awake () => Instance = this;

    private void Start()
    {
        DOTween.KillAll();
        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];
        for( var y = 0; y < Height; y++)
        {
            for(var x =0; x < Width; x++) 
            {
                var tile = rows[y].tiles[x];
                tile.x = x;
                tile.y = y;
                Tiles[x, y] = tile;
                tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];
            }
        }
    }
    
    public  async void Select(Tile tile)
    {
        if (!_selection.Contains(tile)) _selection.Add(tile);
        if (_selection.Count < 2) return;
        Debug.Log($"Selected tiles at ({_selection[0].x},{_selection[0].y}) and ({_selection[1].x}, {_selection[1].y})");

        await Swap(_selection[0], _selection[1]);
        if (CanMatch())
        {
            Match();
            Moves.Instance.MoveCount++;
        }
        else
        {
            Debug.Log("Swapped");

            Moves.Instance.MoveCount++;
            await Swap(_selection[0], _selection[1]);
        }
        _selection.Clear();
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
       
            var icon1 = tile1.icon;
            var icon2 = tile2.icon;
            var icon1Transform = icon1.transform;
            var icon2Transform = icon2.transform;


            var moveSequence = DOTween.Sequence();
            moveSequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                     .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));
            
            await moveSequence.Play().AsyncWaitForCompletion();

            icon1Transform.SetParent(tile2.transform);
            icon2Transform.SetParent(tile1.transform);
            tile1.icon = icon2;
            tile2.icon = icon1;
            var tile1Item = tile1.Item;
            tile1.Item = tile2.Item;
            tile2.Item = tile1Item;
            
        
    }
   
    

  // checks if objects can match
    private bool CanMatch()
    {
        for(var y = 0; y < Height; y++)
        
            for(var x =0; x< Width; x++)
            
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2)
                    return true;

                return false;
    }
   
    private async void Match()
    {
        //checks for match of 3 or more
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = Tiles[x, y];
                var connectedTiles = tile.GetConnectedTiles();
                if (connectedTiles.Skip(1).Count() < 2) continue;


                var shrinkSequence = DOTween.Sequence();
                foreach (var connectedTile in connectedTiles) {
              
                    
                    
                        shrinkSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));
                        Instantiate(particle, new Vector3(connectedTile.transform.position.x, connectedTile.transform.position.y, -10), Quaternion.identity);
                    
                }
                Score.Instance.ScoreCount += tile.Item.value * connectedTiles.Count;
                audioSource.PlayOneShot(matchSound);
                await shrinkSequence.Play().AsyncWaitForCompletion();
               
                Debug.Log($"Partile placed)");
                
                

                var growSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    
                    connectedTile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

                    growSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
                }
                await growSequence.Play().AsyncWaitForCompletion();
                x = 0;
                y = 0;
            }
        }
    }
   
}
