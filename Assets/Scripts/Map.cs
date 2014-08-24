using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public struct Tiles {

        public Transform[][] rows;
        public Transform[][] cols;

        public int indexRows;
        public int indexCols;
        
        private float size;
        
        public Tiles(Transform[] tiles, int width, int height, float tileSize) {
            rows = new Transform[height][];
            for (int i = 0; i < height; ++i) {
                rows[i] = new Transform[width];
                for (int j = 0; j < width; ++j) {
                    rows[i][j] = tiles[j * height + i];
                }
            }

            cols = new Transform[width][];
            for (int i = 0; i < width; ++i) {
                cols[i] = new Transform[height];
                for (int j = 0; j < height; ++j) {
                    cols[i][j] = tiles[i * height + j];
                }
            }

            size = tileSize;
            indexRows = 0;
            indexCols = 0;
        }
        
        public void Up() {
            for (var i = 0; i < firstRow.Length; i++) {
                firstRow[i].position = new Vector3(firstRow[i].position.x, firstRow[i].position.y + size * rows.Length, 0.0f);
            }
            indexRows = (indexRows + 1) % rows.Length;
        }

        public void Down() {
            for (var i = 0; i < lastRow.Length; i++) {
                lastRow[i].position = new Vector3(lastRow[i].position.x, lastRow[i].position.y - size * rows.Length, 0.0f);
            }
            indexRows = (indexRows - 1) % rows.Length;
            indexRows = indexRows < 0 ? rows.Length - 1 : indexRows;
        }

        public void Right() {
            for (var i = 0; i < firstCol.Length; i++) {
                firstCol[i].position = new Vector3(firstCol[i].position.x + size * cols.Length, firstCol[i].position.y, 0.0f);
            }
            indexCols = (indexCols + 1) % cols.Length;
        }

        public void Left() {
            for (var i = 0; i < lastCol.Length; i++) {
                lastCol[i].position = new Vector3(lastCol[i].position.x - size * cols.Length, lastCol[i].position.y, 0.0f);
            }
            indexCols = (indexCols - 1) % cols.Length;
            indexCols = indexCols < 0 ? cols.Length - 1 : indexCols;
        }
        
        public Transform[] firstRow {
            get { return rows[indexRows]; }
        }

        public Transform[] lastRow {
            get { return rows[(indexRows - 1) < 0 ? rows.Length - 1 : indexRows - 1]; }
        }

        public Transform[] firstCol {
            get { return cols[indexCols]; }
        }

        public Transform[] lastCol {
            get { return cols[(indexCols - 1) < 0 ? cols.Length - 1 : indexCols - 1]; }
        }
    }

    public GameObject prefab;
    
    public float cameraFactor = 1.4f;
    public float tilesSize = 4;
    public float tileSize = 2.0f;
    
    private Tiles tiles;
    
    private Transform cachedTransform;
    private Transform cachedTransformCamera;
    private Rigidbody2D cachedPlayerRigidbody2D;

    private void Awake() {
        tileSize = prefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        cachedTransform = transform;
        cachedTransformCamera = Camera.main.transform;
        cachedPlayerRigidbody2D = Camera.main.GetComponent<CameraFollow>().target;
        
        var cameraRect = CameraRect(tilesSize);
        
        var tempMaps = new List<Transform>();
        
        int i = 0, j = 0;
        for (var x = cameraRect.xMin; x <= cameraRect.xMax; x += tileSize) {
            for (var y = cameraRect.yMin; y <= cameraRect.yMax; y += tileSize) {
                var tile = Instantiate(prefab, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                tile.transform.parent = cachedTransform;
                tempMaps.Add(tile.transform);
                j++;
            }
            i++;
        }
    
        tiles = new Tiles(tempMaps.ToArray(), i, j / i, tileSize);
    }

    private void Update() {
        var cameraRect = CameraRect(cameraFactor);

        if (tiles.firstCol[0].position.x > cameraRect.xMin)
            tiles.Left();
        if (tiles.firstRow[0].position.y > cameraRect.yMin)
            tiles.Down();
        
        if (tiles.lastCol[0].position.x < cameraRect.xMax)
            tiles.Right();
        if (tiles.lastRow[0].position.y < cameraRect.yMax)
            tiles.Up();
    }
    
    private Rect CameraRect(float factor) {
        var camHeight = 2f * Camera.main.orthographicSize;
        var camWidth = camHeight * Camera.main.aspect;
        var velocity = cachedPlayerRigidbody2D.velocity;
        var position = new Vector2(cachedTransformCamera.position.x, cachedTransformCamera.position.y);
        var cameraRect = new Rect(-camWidth / 2.0f, -camHeight / 2.0f, camWidth, camHeight);
        cameraRect.size = new Vector2(camWidth * factor, camHeight * factor);
        cameraRect.position = position - cameraRect.size / 2.0f;
        
        return cameraRect;
    }
}
