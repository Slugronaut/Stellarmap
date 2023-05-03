using System;
using System.Collections.Generic;
using System.Text;
using NHunspell;

namespace Stellarmap.Spellcheck
	{
	public class SpellCheckRange
		{
		public int Start;
		public int End;
		public string Word;
		
		public bool IsWithinRange(int index)
			{
			if(index >= Start && index < Start + (End-Start)) return true;
			
			return false;
			}
		}
	
	public class TypingSpellChecker : IDisposable
		{
		private const string NonWordCharacters = " .,;:@\"!£$%^&*()-+-={}[]~#@'<>?/\\\n\r0123456789";
		private readonly string AffFile;
		private readonly string DicFile;
		private Hunspell Hunspell;
		private bool Disposed = false;
		
		
		~TypingSpellChecker()
			{
			Dispose(false);
			}
		
		public void Dispose()
			{
			Dispose(true);
			GC.SuppressFinalize(this);
			}
		
		protected virtual void Dispose(bool disposing)
			{
			if(!Disposed)
				{
				if(disposing)
					{
					//unmanaged
					if(Hunspell != null)
						{
						Hunspell.Dispose();
						}
					}
				//managed
				}		
			Disposed = true;
			}
		
		public bool DataDisposed
			{
			get {return Disposed;}
			}
		
		public TypingSpellChecker(string affFile,string dicFile)
			{
			if(affFile == null || affFile.Length < 1 ||
				dicFile == null || dicFile.Length < 1)
				{throw new ArgumentException("An invalid dictionary was passed to the spell checker.");}
			AffFile = affFile;
			DicFile = dicFile;
			Hunspell = new Hunspell(AffFile,DicFile);
			}
		
		public List<SpellCheckRange> ProofRead(string text)
			{
			if(Disposed) return null;
			
			List<SpellCheckRange> spellingErrorRanges = new List<SpellCheckRange>();
			List<SpellCheckRange> words = GetWordsAsRanges(text);

			foreach(SpellCheckRange word in words)
				{
				string wordText = text.Substring(word.Start,word.End - word.Start);
				if(IsWordSpelledCorrectly(wordText) == false)
					{
					spellingErrorRanges.Add(word);
					}
				}

			return spellingErrorRanges;
			}

		private List<SpellCheckRange> GetWordsAsRanges(string text)
			{
			List<SpellCheckRange> ranges = new List<SpellCheckRange>();

			SpellCheckRange currentRange = null;

			int index = 0;
			foreach(Char c in text.ToCharArray())
				{
				if(NonWordCharacters.IndexOf(c) != -1)
					{
					if(currentRange != null)
						{
						// End a word
						currentRange.End = index;
						currentRange.Word = text.Substring(currentRange.Start,currentRange.End - currentRange.Start);
						ranges.Add(currentRange);
						currentRange = null;
						}
					}
				else
					{
					if(currentRange == null)
						{
						// Start a word
						currentRange = new SpellCheckRange();
						currentRange.Start = index;
						}
					}

				index++;
				}

			if(currentRange != null)
				{
				// End a word
				currentRange.End = index;
				currentRange.Word = text.Substring(currentRange.Start,currentRange.End - currentRange.Start);
				ranges.Add(currentRange);
				currentRange = null;
				}

			return ranges;
			}
		
		private bool IsWordSpelledCorrectly(String wordToCheck)
			{
			if(Hunspell != null)	return Hunspell.Spell(wordToCheck);
			
			return false;
			}
		
		public List<String> Recommended(List<SpellCheckRange> ranges,int index)
			{
			if(index < 0 || index > ranges.Count)	return null;
			
			return Hunspell.Suggest(ranges[index].Word);
			}
		}
	}
