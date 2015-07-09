﻿using Nest.Litterateur.Documentation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.Litterateur
{
	class Program
	{
		static void Main(string[] args) =>
			LitUp.Go();

		public static class LitUp
		{
			private readonly static string TestFolder = @"..\..\..\..\..\src\Tests";
			private readonly static string[] SkipFolders = { "Nest.Tests.Literate", "Debug", "Release" };

			public static IEnumerable<DocumentationFile> FindAll(string extension) =>
				from f in Directory.GetFiles(TestFolder, $"*.{extension}", SearchOption.AllDirectories)
				let dir = new DirectoryInfo(f)
				where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
				select DocumentationFile.Load(new FileInfo(f));

			public static void Go()
			{
				var files = FindAll("cs").Concat(FindAll("asciidoc"));
				foreach (var file in files)
					file.SaveToDocumentationFolder();

			}


		}
	}
}