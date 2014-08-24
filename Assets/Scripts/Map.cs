using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public struct Tiles {

        public Transform[][] rows;
        public Transform[][] cols;

        public int indexRows;
        public int indexCols;
        
        private float tileSize;
        
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

            this.tileSize = tileSize;
            indexRows = 0;
            indexCols = 0;
        }
        
        public void Up() {
            for (var i = 0; i < firstRow.Length; i++)
                firstRow[i].position = new Vector3(firstRow[i].position.x, firstRow[i].position.y + tileSize * rows.Length, 0.0f);
            indexRows = (indexRows + 1) % rows.Length;
        }

        public void Down() {
            for (var i = 0; i < lastRow.Length; i++)
                lastRow[i].position = new Vector3(lastRow[i].position.x, lastRow[i].position.y - tileSize * rows.Length, 0.0f);
            indexRows = (indexRows - 1) % rows.Length;
            indexRows = indexRows < 0 ? rows.Length - 1 : indexRows;
        }

        public void Right() {
            for (var i = 0; i < firstCol.Length; i++)
                firstCol[i].position = new Vector3(firstCol[i].position.x + tileSize * cols.Length, firstCol[i].position.y, 0.0f);
            indexCols = (indexCols + 1) % cols.Length;
        }

        public void Left() {
            for (var i = 0; i < lastCol.Length; i++)
                lastCol[i].position = new Vector3(lastCol[i].position.x - tileSize * cols.Length, lastCol[i].position.y, 0.0f);
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
    
    private Tiles tiles;
    
    private Transform cachedTransform;
    private Transform cachedTransformCamera;
    private Rigidbody2D cachedPlayerRigidbody2D;

    private void Start() {
        cachedTransform = transform;

        var size = prefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        
        var tileRect = CameraUtility.instance.ScaleRect(tilesSize);
        
        var tempMaps = new List<Transform>();
        
        int i = 0, j = 0;
        for (var x = tileRect.xMin; x <= tileRect.xMax; x += size) {
            for (var y = tileRect.yMin; y <= tileRect.yMax; y += size) {
                var tile = Instantiate(prefab, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                tile.transform.parent = cachedTransform;
                tempMaps.Add(tile.transform);
                j++;
            }
            i++;
        }
    
        tiles = new Tiles(tempMaps.ToArray(), i, j / i, size);
    }

    private void Update() {
        var rect = CameraUtility.instance.ScaleRect(cameraFactor);

        if (tiles.firstCol[0].position.x > rect.xMin)
            tiles.Left();
        if (tiles.firstRow[0].position.y > rect.yMin)
            tiles.Down();
        
        if (tiles.lastCol[0].position.x < rect.xMax)
            tiles.Right();
        if (tiles.lastRow[0].position.y < rect.yMax)
            tiles.Up();
    }
}
