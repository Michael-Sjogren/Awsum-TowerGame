using System;

[AttributeUsage(AttributeTargets.All)]
public class MinMaxRange : Attribute
{
	public MinMaxRange(float min, float max)
	{
		Min = min;
		Max = max;
	}
	public float Min { get; private set; }
	public float Max { get; private set; }
}