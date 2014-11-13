public struct FPoint
{
	public FInt X;
	public FInt Y;
	
	public static FPoint Create( FInt X, FInt Y )
	{
		FPoint fp;
		fp.X = X;
		fp.Y = Y;
		return fp;
	}
	
	public static FPoint FromPoint( Point p )
	{
		FPoint f;
		f.X = (FInt)p.X;
		f.Y = (FInt)p.Y;
		return f;
	}
	
	public static Point ToPoint( FPoint f )
	{
		return new Point( f.X.IntValue, f.Y.IntValue );
	}
	
	#region Vector Operations
	public static FPoint VectorAdd( FPoint F1, FPoint F2 )
	{
		FPoint result;
		result.X = F1.X + F2.X;
		result.Y = F1.Y + F2.Y;
		return result;
	}
	
	public static FPoint VectorSubtract( FPoint F1, FPoint F2 )
	{
		FPoint result;
		result.X = F1.X - F2.X;
		result.Y = F1.Y - F2.Y;
		return result;
	}
	
	public static FPoint VectorDivide( FPoint F1, int Divisor )
	{
		FPoint result;
		result.X = F1.X / Divisor;
		result.Y = F1.Y / Divisor;
		return result;
	}
	#endregion
}
