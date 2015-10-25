﻿using System;
using ClassicalSharp;

namespace Launcher2 {

	internal sealed class Program {
		
		public const string AppName = "ClassicalSharp Launcher 0.95";
		
		[STAThread]
		static void Main( string[] args ) {
			ErrorHandler.InstallHandler( "launcher.log" );		
			LauncherWindow window = new LauncherWindow();
			window.Run();
		}
		
		internal static bool LogException( Exception ex ) {
			return ErrorHandler.LogHandledException( ex );
		}
	}
}
