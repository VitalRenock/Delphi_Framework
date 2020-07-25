using System;
using UnityEngine;

[Serializable] public abstract class TerrainEffect 
{
	/// <summary>
	/// Calcule l'effet sur une hauteur à partir des coordonnées fournies.
	/// </summary>
	/// <param name="coordinate">coordonnées</param>
	/// <param name="height">hauteur courante</param>
	/// <param name="i">index dans la boucle</param>
	/// <returns></returns>
	public abstract float ComputeEffect(Vector2 coordinate, float height, float i);

	protected Vector2 RotateCoordinate(Vector2 coordinate, float angle)
    {
		float newX = (coordinate.x * Mathf.Cos(angle)) + (coordinate.y * Mathf.Sin(angle));
		float newY = (coordinate.x * Mathf.Sin(angle)) + (coordinate.y * Mathf.Cos(angle));
		return new Vector2(newX, newY);
	}
	protected Vector2 TranslateCoordinate(Vector2 coordinate, Vector2 offset)
    {
		return new Vector2((coordinate.x + offset.x), (coordinate.y + offset.y));
    }
}

[Serializable] public class AjustHeight : TerrainEffect
{
	public float HeightOffset = 0f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		return HeightOffset;
	}
}
[Serializable] public class Perlin : TerrainEffect
{
	[Min(0)]
	public float NoiseFactor = 0f;
	[Range(0f, 1f)]
	public float NoiseX = 0f;
	[Range(0f, 1f)]
	public float NoiseY = 0f;

	public Vector2 TranslateEffect = Vector2.zero;
	[Range(0f, Mathf.PI)]
	public float RotateEffect = 0f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		// Translation de l'effet.
		if (TranslateEffect != Vector2.zero)
			coordinate = TranslateCoordinate(coordinate, TranslateEffect);
		// Rotation de l'effet.
		if (RotateEffect != 0f)
			coordinate = RotateCoordinate(coordinate, RotateEffect);

		return Mathf.PerlinNoise(coordinate.x * NoiseX, coordinate.y * NoiseY) * NoiseFactor;
	}
}
[Serializable] public class Waves : TerrainEffect
{
	[Min(0f)]
	public float WavesLenght = 1f;
	[Min(0f)]
	public float WavesAmplitude = 1f;

	public Vector2 TranslateEffect = Vector2.zero;
	[Range(0f, Mathf.PI)]
	public float RotateEffect = 0f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		// Translation de l'effet.
		if (TranslateEffect != Vector2.zero)
			coordinate = TranslateCoordinate(coordinate, TranslateEffect);
		// Rotation de l'effet.
		if (RotateEffect != 0f)
			coordinate = RotateCoordinate(coordinate, RotateEffect);

		return Mathf.Cos(coordinate.x * WavesLenght) * WavesAmplitude;
	}
}
[Serializable] public class Clamp : TerrainEffect
{
	public float MinimumHeight = 0f;
	public float MaximumHeight = 1f;
	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		return Mathf.Clamp(height, MinimumHeight, MaximumHeight);
	}
}
[Serializable] public class Bumpy : TerrainEffect
{
	[Range(0.01f, 100f)]
	public float Size = 0.01f;
	[Min(1f)]
	public float Strenght = 1f;

	public Vector2 TranslateEffect = Vector2.zero;
	[Range(0f, Mathf.PI)]
	public float RotateEffect = 0f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		// Translation de l'effet.
		if (TranslateEffect != Vector2.zero)
			coordinate = TranslateCoordinate(coordinate, TranslateEffect);
		// Rotation de l'effet.
		if (RotateEffect != 0f)
			coordinate = RotateCoordinate(coordinate, RotateEffect);

		return Mathf.Sin(coordinate.x / Size) * Mathf.Sin(coordinate.y / Size) * Strenght;
	}
}
[Serializable] public class RandomBump : TerrainEffect
{
	public float BumpFactor = 0.1f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		return UnityEngine.Random.Range(0, BumpFactor);
	}
}
[Serializable] public class Slope : TerrainEffect
{


	public Vector2 TranslateEffect = Vector2.zero;
	[Range(0f, Mathf.PI)]
	public float RotateEffect = 0f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		// Translation de l'effet.
		if (TranslateEffect != Vector2.zero)
			coordinate = TranslateCoordinate(coordinate, TranslateEffect);
		// Rotation de l'effet.
		if (RotateEffect != 0f)
			coordinate = RotateCoordinate(coordinate, RotateEffect);

		// à remplacer.
		return Mathf.Sqrt((coordinate.x + coordinate.y));
	}
}
[Serializable] public class Test : TerrainEffect
{
	public float OffsetA = 0f;
	public float OffsetB = 0f;
	public float OffsetC = 0f;
	public float OffsetD = 0f;
	[Range(0f, Mathf.PI)]
	public float RotateEffect = 0f;

	public override float ComputeEffect(Vector2 coordinate, float height, float i)
	{
		if (RotateEffect != 0f)
			coordinate = RotateCoordinate(coordinate, RotateEffect);

		//return (coordinate.x + coordinate.y) * i;

		//return Mathf.InverseLerp(OffsetA, OffsetB, height);
		//return Mathf.InverseLerp(x, height, y);
		//return Mathf.InverseLerp(x * OffsetA, y * OffsetB, i);
		//return Mathf.InverseLerp(x + OffsetA, y + OffsetB, i * OffsetC) * OffsetD;

		//return Mathf.Sqrt((coordinate.x + OffsetA) * (coordinate.y + OffsetB));
		//return Mathf.Sqrt((coordinate.x + coordinate.y));

		return 0f;
	}

}