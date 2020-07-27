using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Terrain))]
public class Modifier_Terrain : MonoBehaviour
{
    [SerializeReference]
    public List<Effects_Terrain> ListOfEffects = new List<Effects_Terrain>();

    [ShowInInspector][ReadOnly]
    Terrain terrain;
    [ShowInInspector][ReadOnly]
    TerrainData terrainData;


    private void Reset()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        //terrainCollider = GetComponent<TerrainCollider>();
        //resolution = new Vector2Int(terrainData.heightmapResolution, terrainData.heightmapResolution);
        //terrainSize = terrainData.size;
    }

    private void Update()
    {
        //if (terrainData != null)
        //{
        //    //resolution = new Vector2Int(terrainData.heightmapResolution, terrainData.heightmapResolution);
        //    terrainData.size = terrainSize;
        //}
        UpdateTerrain();
    }
    //private void OnValidate()
    //{
    //    //if (terrainData != null)
    //    //{
    //    //    //resolution = new Vector2Int(terrainData.heightmapResolution, terrainData.heightmapResolution);
    //    //    terrainData.size = terrainSize;
    //    //}
    //    UpdateTerrain();
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0.2f, 0f);
        Gizmos.DrawWireCube(terrain.terrainData.bounds.center, terrain.terrainData.bounds.size);
    }

    void UpdateTerrain()
    {
        //_terrainData.SetHeights(0, 0, GenerateHeights());
        terrainData.SetHeightsDelayLOD(0, 0, GenerateHeights());
        terrainData.SyncHeightmap();
    }

    float[,] GenerateHeights()
    {
        int heightmapResolution = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, heightmapResolution, heightmapResolution);

        for (int x = 0, i = 0; x < heightmapResolution; x++)
            for (int y = 0; y < heightmapResolution; y++, i++)
                heights[x, y] = ApplyEffects(new Vector2(x, y), i);

        return heights;
    }

    float ApplyEffects(Vector2 coordinate, float i)
    {
        float height = 0f;
        foreach (Effects_Terrain effect in ListOfEffects)
            height += effect.ComputeEffect(coordinate, height, i);

        return height;
    }

    //[Button("Generate Terrain")]
    //void GenerateTerrain()
    //{
    //	terrainData = new TerrainData();
    //	terrainData.name = _terrainName;
    //	terrainData.heightmapResolution = 513;
    //	terrainData.size = _terrainSize;

    //	goTerrain = Terrain.CreateTerrainGameObject(terrainData);
    //	goTerrain.name = _terrainName;

    //	terrain = goTerrain.GetComponent<Terrain>();
    //	terrain.terrainData = terrainData;

    //	terrainCollider = goTerrain.GetComponent<TerrainCollider>();
    //	terrainCollider.terrainData = terrainData;
    //}

    //[Button("Reset Terrain")]
    //void ResetTerrain()
    //{
    //	terrainData.SetHeights(0, 0, new float[_terrainResolution.x, _terrainResolution.y]);
    //}

    //[Button("Save Terrain")]
    //void SaveTerrain()
    //{
    //	if (!AssetDatabase.IsValidFolder("Assets/Terrains"))
    //		AssetDatabase.CreateFolder("Assets", "Terrains");

    //	AssetDatabase.CreateAsset(terrain.terrainData, "Assets/Terrains/" + _terrainName + ".preset");
    //	PrefabUtility.SaveAsPrefabAsset(goTerrain, "Assets/Terrains/" + _terrainName + ".prefab");

    //	AssetDatabase.Refresh();
    //}
}
