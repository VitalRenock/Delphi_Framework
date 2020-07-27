using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class Plane_Generator : MonoBehaviour
{
	public Vector2 Size = Vector3.one * 10;
	[Min(1)]
	public int Resolution = 1;
	public Axes Axe = Axes.XZ;
	public Material Material;

	[SerializeReference]
	public List<Effects_Terrain> ListOfEffects = new List<Effects_Terrain>();

	Mesh mesh;
	Vector3Int meshResolution = Vector3Int.zero;
	float[,] heights;


	private void Update()
    {
		mesh = GenerateMesh(Size, Resolution, Axe);
		meshResolution = GetMeshResolution(mesh);
		heights = GetHeights(mesh, Axe);

		//SetHeights(mesh);

		Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, Material, 0);
    }

    #region Manipulate mesh

    Vector3Int GetMeshResolution(Mesh mesh)
    {
		Vector3Int meshResolution = Vector3Int.zero;
		Vector3 farestVertice = Vector3.zero;
		foreach (Vector3 vertice in mesh.vertices)
		{
			if (vertice.x > farestVertice.x)
			{
				farestVertice.x = vertice.x;
				meshResolution.x++;
			}
			else if (vertice.y > farestVertice.y)
			{
				farestVertice.y = vertice.y;
				meshResolution.y++;
			}
			else if (vertice.z > farestVertice.z)
			{
				farestVertice.z = vertice.z;
				meshResolution.z++;
			}
		}

		return meshResolution;
	}
	float[,] GetHeights(Mesh mesh, Axes axes)
    {
		float[,] heights;
        switch (axes)
        {
            case Axes.XY:
				heights = new float[meshResolution.x, meshResolution.y];
				return heights;
            case Axes.XZ:
				heights = new float[meshResolution.x, meshResolution.z];
				return heights;
            case Axes.YZ:
				heights = new float[meshResolution.y, meshResolution.z];
				return heights;
            default:
				return null;
        }
    }
	void SetHeights(Mesh mesh)
    {
        switch (Axe)
        {
            case Axes.XY:
				for (int x = 0, i = 0; x < meshResolution.x; x++)
					for (int y = 0; y < meshResolution.y; y++, i++)
						mesh.vertices[i].z = heights[x, y];
				break;
            case Axes.XZ:
				for (int x = 0, i = 0; x < meshResolution.x; x++)
					for (int z = 0; z < meshResolution.z; z++, i++)
						mesh.vertices[i].y = heights[x, z];
				break;
            case Axes.YZ:
				for (int y = 0, i = 0; y < meshResolution.y; y++)
					for (int z = 0; z < meshResolution.z; z++, i++)
						mesh.vertices[i].x = heights[y, z];
				break;
            default:
                break;
        }
    }

    #endregion

    #region Generate Mesh

    Vector3[] GenerateVertices(Vector2 size, int subdivisions, Axes axes)
	{
		// Size.x +1 * Size.y +1! because adjacent quads can share the same vertex.
		Vector3[] vertices = new Vector3[(subdivisions + 1) * (subdivisions + 1)];

		float minX = size.x / subdivisions;
		float minY = size.y / subdivisions;

		// Génère le mesh selon l'axe et en appliquant les effets.
        switch (axes)
        {
            case Axes.XY:
				for (int i = 0, y = 0; y <= subdivisions; y++)
					for (int x = 0; x <= subdivisions; x++, i++)
						vertices[i] = new Vector3(x * minX, y * minY, ApplyEffects(new Vector2(x, y), i));
				break;
            case Axes.XZ:
				for (int i = 0, y = 0; y <= subdivisions; y++)
					for (int x = 0; x <= subdivisions; x++, i++)
						vertices[i] = new Vector3(x * minX, ApplyEffects(new Vector2(x, y), i), y * minY);
				break;
            case Axes.YZ:
				for (int i = 0, y = 0; y <= subdivisions; y++)
					for (int x = 0; x <= subdivisions; x++, i++)
						vertices[i] = new Vector3(ApplyEffects(new Vector2(x, y), i), x * minX, y * minY);
				break;
            default:
                break;
        }

        return vertices;
    }
	int[] GenerateTriangles(int subdivisions)
    {
        int[] triangles = new int[subdivisions * subdivisions * 6];

        for (int ti = 0, vi = 0, y = 0; y < subdivisions; y++, vi++)
        {
			for (int x = 0; x < subdivisions; x++, ti += 6, vi++)
			{
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + subdivisions + 1;
				triangles[ti + 5] = vi + subdivisions + 2;
			}
        }
		return triangles;
    }
	Vector3[] GenerateNormals(Vector3[] vertices, Axes axes)
    {
		// Also possible: Mesh.RecalculateNormals();

		Vector3[] normals = new Vector3[vertices.Length];

        switch (axes)
        {
            case Axes.XY:
				for (int i = 0; i < vertices.Length; i++)
					normals[i] = Vector3.back;
				break;
            case Axes.XZ:
				for (int i = 0; i < vertices.Length; i++)
					normals[i] = Vector3.up;
				break;
            case Axes.YZ:
				for (int i = 0; i < vertices.Length; i++)
					normals[i] = Vector3.left;
				break;
            default:
                break;
        }

        return normals;
    }
	Vector2[] GenerateUVs(Vector3[] vertices, int subdivisions)
    {
		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i = 0, y = 0; y <= subdivisions; y++)
			for (int x = 0; x <= subdivisions; x++, i++)
				uvs[i] = new Vector2((float)x / subdivisions, (float)y / subdivisions);

		return uvs;
    }
	Vector4[] GenererateTangents(Vector3[] vertices)
    {
		Vector4[] tangents = new Vector4[vertices.Length];

		Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0; i < tangents.Length; i++)
			tangents[i] = tangent;

		return tangents;
    }
	Mesh GenerateMesh(Vector2 size, int subdivisions, Axes axes = Axes.XY)
    {
		Mesh mesh = new Mesh();

		mesh.vertices = GenerateVertices(size, subdivisions, axes);
		mesh.triangles = GenerateTriangles(subdivisions);
		//mesh.normals = GenerateNormals(mesh.vertices, axes);
		mesh.RecalculateNormals();
		mesh.uv = GenerateUVs(mesh.vertices, subdivisions);
		mesh.tangents = GenererateTangents(mesh.vertices);

		return mesh;
    }

    #endregion

	float ApplyEffects(Vector2 coordinate, float i)
    {
		float height = 0f;
        foreach (Effects_Terrain effect in ListOfEffects)
			height += effect.ComputeEffect(coordinate, height, i);

        return height;
    }
}

public enum Axes
{
	XY,
	XZ,
	YZ
}