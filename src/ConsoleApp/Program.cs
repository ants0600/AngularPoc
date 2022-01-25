using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleApp
{
	internal class Program
	{

		static readonly testing _testing = new testing();

		private static void Main(string[] args)
		{
			string s1 = "super";
			string s2 = "tower";

			//dcecccbd
			s1 = "dce";
			s2 = "cccbd";
			Console.WriteLine("xxxx test:" + _testing.solution(s1, s2));
			Console.ReadKey();
		}



	}



}


class testing
{
	public string solution(string s1, string s2)
	{
		var a = s1.ToList();
		var b = s2.ToList();

		string value = string.Empty;
		while (a.Count > 0 || b.Count > 0)
		{
			if (a.Count <= 0)
			{
				while (b.Count > 0)
				{
					value += b[0];
					b.RemoveAt(0);
				}
				break;
			}
			if (b.Count <= 0)
			{
				while (a.Count > 0)
				{
					value += a[0];
					a.RemoveAt(0);
				}
				break;
			}

			char firstA = a[0];
			char firstB = b[0];
			int lexA = this.lex(firstA, a);
			int lexB = this.lex(firstB, b);
			if (lexA < lexB)
			{
				//remove a
				value += firstA;
				a.RemoveAt(0);
			}
			else if (lexA > lexB)
			{
				//remove b
				value += firstB;
				b.RemoveAt(0);
			}
			else if (firstA < firstB)
			{
				//remove a
				value += firstA;
				a.RemoveAt(0);
			}
			else
			{
				//remove b
				value += firstB;
				b.RemoveAt(0);
			}
		}

		return value;
	}

	private int lex(char firstA, List<char> list)
	{
		return list.Count(a => a.Equals(firstA));
	}
}