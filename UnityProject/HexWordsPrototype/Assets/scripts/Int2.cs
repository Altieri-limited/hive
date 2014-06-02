
public struct Int2 
{
	public int x;
	public int y;

	public Int2(int _x, int _y)
	{
		this.x = _x;
		this.y = _y;
	}

	public override bool Equals (object obj)
	{
		Int2 other = (Int2)obj;
		return other.x == x && other.y == y;
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode();
	}

	static public bool operator==(Int2 i1, Int2 i2)
	{
		return i1.x == i2.x && i1.y == i2.y;
	}

	static public bool operator != (Int2 i1, Int2 i2)
	{
		return !(i1 == i2);
	}
}
