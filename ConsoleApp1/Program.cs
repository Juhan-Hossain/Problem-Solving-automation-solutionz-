// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.IO;

Console.WriteLine("Hello, World!");
var solution = new Solution();
var test = "aabbccz";


//Optimized one for charstream
// streamtext file will be uploaded
var finder = new StreamCharacterFinder();

using (var stream = new StreamReader("streamtext.txt"))
{
    int nextChar;
	while ((nextChar = stream.Read())!=-1)
	{
		char c = (char)nextChar;
		finder.processNextChar(c);

		char? currentnonrepeating = finder.GetFirstNonRepeating();
		if (currentnonrepeating.HasValue)
			Console.WriteLine($"Current first non repeating: {currentnonrepeating}");
	}

}