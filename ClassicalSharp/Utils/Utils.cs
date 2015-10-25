﻿using System;
using System.Drawing;
using OpenTK;

namespace ClassicalSharp {

	// NOTE: These delegates should be removed when using versions later than NET 2.0.
	// ################################################################
	public delegate void Action();
	public delegate void Action<T1, T2>( T1 arg1, T2 arg2 );
	public delegate void Action<T1, T2, T3>( T1 arg1, T2 arg2, T3 arg3 );
	public delegate void Action<T1, T2, T3, T4>( T1 arg1, T2 arg2, T3 arg3, T4 arg4 );
	public delegate TResult Func<TResult>();
	public delegate TResult Func<T1, TResult>( T1 arg1 );
	public delegate TResult Func<T1, T2, TResult>( T1 arg1, T2 arg2 );
	public delegate TResult Func<T1, T2, T3, TResult>( T1 arg1, T2 arg2, T3 arg3 );
	public delegate bool TryParseFunc<T>( string s, out T value );
	// ################################################################
	
	public static class Utils {
		
		
		
		/// <summary> Clamps that specified value such that min ≤ value ≤ max </summary>
		public static void Clamp( ref float value, float min, float max ) {
			if( value < min ) value = min;
			if( value > max ) value = max;
		}
		
		/// <summary> Clamps that specified value such that min ≤ value ≤ max </summary>
		public static void Clamp( ref int value, int min, int max ) {
			if( value < min ) value = min;
			if( value > max ) value = max;
		}
		
		/// <summary> Returns the next highest power of 2 that is ≥ to the given value. </summary>
		public static int NextPowerOf2( int value ) {
			int next = 1;
			while( value > next ) {
				next <<= 1;
			}
			return next;
		}
		
		/// <summary> Returns whether the given value is a power of 2. </summary>
		public static bool IsPowerOf2( int value ) {
			return value != 0 && (value & (value - 1)) == 0;
		}
		
		/// <summary> Returns a string with all the colour codes stripped from it. </summary>
		public static string StripColours( string value ) {
			if( value.IndexOf( '&' ) == -1 ) {
				return value;
			}
			
			char[] output = new char[value.Length];
			int usedChars = 0;
			
			for( int i = 0; i < value.Length; i++ ) {
				char token = value[i];
				if( token == '&' ) {
					i++; // Skip over the following colour code.
				} else {
					output[usedChars++] = token;
				}
			}
			return new String( output, 0, usedChars );
		}
		
		public static bool CaselessEquals( string a, string b ) {
			return a.Equals( b, StringComparison.OrdinalIgnoreCase );
		}
		
		public static bool CaselessStarts( string a, string b ) {
			return a.StartsWith( b, StringComparison.OrdinalIgnoreCase );
		}
		
		public static string ToHexString( byte[] array ) {
			int len = array.Length;
			char[] hexadecimal = new char[len * 2];
			for( int i = 0; i < array.Length; i++ ) {
				int value = array[i];
				int upper = value >> 4;
				int lower = value & 0x0F;
				
				// 48 = index of 0, 55 = index of (A - 10).
				hexadecimal[i * 2] = upper < 10 ? (char)(upper + 48) : (char)(upper + 55);
				hexadecimal[i * 2 + 1] = lower < 10 ? (char)(lower + 48) : (char)(lower + 55);
			}
			return new String( hexadecimal );
		}
		
		/// <summary> Returns the hex code represented by the given character.
		/// Throws FormatException if the input character isn't a hex code. </summary>
		public static int ParseHex( char value ) {
			if( value >= '0' && value <= '9' ) {
				return (int)( value - '0' );
			} else if( value >= 'a' && value <= 'f' ) {
				return (int)( value - 'a' ) + 10;
			} else if( value >= 'A' && value <= 'F' ) {
				return (int)( value - 'A' ) + 10;
			} else {
				throw new FormatException( "Invalid hex code given: " + value );
			}
		}
		
		/// <summary> Multiply a value in degrees by this to get its value in radians. </summary>
		public const float Deg2Rad = (float)(Math.PI / 180);
		/// <summary> Multiply a value in radians by this to get its value in degrees. </summary>
		public const float Rad2Deg = (float)(180 / Math.PI);
		
		public static int DegreesToPacked( double degrees, int period ) {
			return (int)(degrees * period / 360.0) % period;
		}
		
		public static double PackedToDegrees( byte packed ) {
			return packed * 360.0 / 256.0;
		}
		
		public static Vector3 RotateY( Vector3 v, float angle ) {
			float cosA = (float)Math.Cos( angle );
			float sinA = (float)Math.Sin( angle );
			return new Vector3( cosA * v.X - sinA * v.Z, v.Y, sinA * v.X + cosA * v.Z );
		}
		
		public static Vector3 RotateY( float x, float y, float z, float angle ) {
			float cosA = (float)Math.Cos( angle );
			float sinA = (float)Math.Sin( angle );
			return new Vector3( cosA * x - sinA * z, y, sinA * x + cosA * z );
		}
		
		public static Vector3 RotateX( Vector3 p, float cosA, float sinA ) {
			return new Vector3( p.X, cosA * p.Y + sinA * p.Z, -sinA * p.Y + cosA * p.Z );
		}
		
		public static Vector3 RotateY( Vector3 p, float cosA, float sinA ) {
			return new Vector3( cosA * p.X - sinA * p.Z, p.Y, sinA * p.X + cosA * p.Z );
		}
		
		public static Vector3 RotateZ( Vector3 p, float cosA, float sinA ) {
			return new Vector3( cosA * p.X + sinA * p.Y, -sinA * p.X + cosA * p.Y, p.Z );
		}
		
		public static Vector3 RotateX( float x, float y, float z, float cosA, float sinA ) {
			return new Vector3( x, cosA * y + sinA * z, -sinA * y + cosA * z );
		}
		
		public static Vector3 RotateY( float x, float y, float z, float cosA, float sinA ) {
			return new Vector3( cosA * x - sinA * z, y, sinA * x + cosA * z );
		}
		
		public static Vector3 RotateZ( float x, float y, float z, float cosA, float sinA ) {
			return new Vector3( cosA * x + sinA * y, -sinA * x + cosA * y, z );
		}
		
		/// <summary> Returns the square of the euclidean distance between two points. </summary>
		public static float DistanceSquared( Vector3 p1, Vector3 p2 ) {
			float dx = p2.X - p1.X;
			float dy = p2.Y - p1.Y;
			float dz = p2.Z - p1.Z;
			return dx * dx + dy * dy + dz * dz;
		}
		
		/// <summary> Returns the square of the euclidean distance between two points. </summary>
		public static float DistanceSquared( float x1, float y1, float z1, float x2, float y2, float z2 ) {
			float dx = x2 - x1;
			float dy = y2 - y1;
			float dz = z2 - z1;
			return dx * dx + dy * dy + dz * dz;
		}
		
		/// <summary> Returns the square of the euclidean distance between two points. </summary>
		public static int DistanceSquared( int x1, int y1, int z1, int x2, int y2, int z2 ) {
			int dx = x2 - x1;
			int dy = y2 - y1;
			int dz = z2 - z1;
			return dx * dx + dy * dy + dz * dz;
		}
		
		public static Vector3 GetDirVector( double yawRad, double pitchRad ) {
			double x = -Math.Cos( pitchRad ) * -Math.Sin( yawRad );
			double y = -Math.Sin( pitchRad );
			double z = -Math.Cos( pitchRad ) * Math.Cos( yawRad );
			return new Vector3( (float)x, (float)y, (float)z );
		}
		
		public static void Log( string text ) {
			Console.WriteLine( text );
		}
		
		public static void Log( string text, params object[] args ) {
			Log( String.Format( text, args ) );
		}
		
		public static void LogWarning( string text ) {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine( text );
			Console.ResetColor();
		}
		
		public static void LogWarning( string text, params object[] args ) {
			LogWarning( String.Format( text, args ) );
		}
		
		public static void LogError( string text ) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine( text );
			Console.ResetColor();
		}
		
		public static void LogError( string text, params object[] args ) {
			LogError( String.Format( text, args ) );
		}
		
		public static void LogDebug( string text ) {
			#if DEBUG
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine( text );
			Console.ResetColor();
			#endif
		}
		
		public static void LogDebug( string text, params object[] args ) {
			#if DEBUG
			LogDebug( String.Format( text, args ) );
			#endif
		}
		
		public static int Floor( float value ) {
			return value >= 0 ? (int)value : (int)value - 1;
		}
		
		/// <summary> Returns the number of vertices needed to subdivide a quad. </summary>
		internal static int CountVertices( int axis1Len, int axis2Len, int axisSize ) {
			return CeilDiv( axis1Len, axisSize ) * CeilDiv( axis2Len, axisSize ) * 4;
		}
		
		/// <summary> Performs rounding upwards integer division. </summary>
		public static int CeilDiv( int a, int b ) {
			return a / b + (a % b != 0 ? 1 : 0);
		}
		
		/// <summary> Performs linear interpolation between two values. </summary>
		public static float Lerp( float a, float b, float t ) {
			return a + (b - a) * t;
		}
		
		/// <summary> Linearly interpolates between a given angle range, adjusting if necessary. </summary>
		public static float LerpAngle( float leftAngle, float rightAngle, float t ) {
			// we have to cheat a bit for angles here.
			// Consider 350* --> 0*, we only want to travel 10*,
			// but without adjusting for this case, we would interpolate back the whole 350* degrees.
			bool invertLeft = leftAngle > 270 && rightAngle < 90;
			bool invertRight = rightAngle > 270 && leftAngle < 90;
			if( invertLeft ) leftAngle = leftAngle - 360;
			if( invertRight ) rightAngle = rightAngle - 360;
			
			return Lerp( leftAngle, rightAngle, t );
		}
		
		/// <summary> Determines the skin type of the specified bitmap. </summary>
		public static SkinType GetSkinType( Bitmap bmp ) {
			if( bmp.Width == bmp.Height * 2 ) {
				return SkinType.Type64x32;
			} else if( bmp.Width == bmp.Height ) {
				// Minecraft alex skins have this particular pixel with alpha of 0.
				if( bmp.Width >= 64 ) {
					bool isNormal = bmp.GetPixel( 54, 20 ).A >= 127;
					return isNormal ? SkinType.Type64x64 : SkinType.Type64x64Slim;
				} else {
					return SkinType.Type64x64;
				}
			} else {
				throw new NotSupportedException( "unsupported skin dimensions: " + bmp.Width + ", " + bmp.Height );
			}
		}
		
		public static bool IsUrl( string value ) {
			return value.StartsWith( "http://" ) || value.StartsWith( "https://" );
		}
	}
}