using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class HexGridLogic : MonoBehaviour
{
	private const int MIN_WORD_LENGTH = 2;
	private const string DICTIONARY_RESOURCES_PATH = "italian_lemma-openoffice";
	private HexGridView _hexGridView;
	private string[] _alphabet;
	private List<Int2> _currentWordCells = new List<Int2>();
	private string[,] _letterGrid;
	private HashSet<string> _dictionary;

	public void TouchedGridElement(Int2 indices)
	{
		if(_currentWordCells.Count > 0)
		{
			if(indices != _currentWordCells[_currentWordCells.Count - 1])
			{
//				if(!_hexGridView.GetAdjacentCells(indices).Contains(currentWordCells[currentWordCells.Count - 1]))
				if(!_hexGridView.AreAdjacent(indices, _currentWordCells[_currentWordCells.Count - 1]))
				{
					foreach(Int2 cell in _currentWordCells)
					{
						_hexGridView.ClearTouch(cell.x, cell.y);
					}
					_currentWordCells.Clear();
				}
				_currentWordCells.Add(indices);
				//TODO optimize
				if(_currentWordCells.Count >= MIN_WORD_LENGTH)
				{
					StringBuilder sb = new StringBuilder();
					for(int i = 0; i < _currentWordCells.Count; i++)
					{
						sb.Append(_letterGrid[_currentWordCells[i].x, _currentWordCells[i].y]);
					}
					if(_dictionary.Contains(sb.ToString()))
					{
						_hexGridView.SetSolved(_currentWordCells);
					}
				}
			}
		}
		else
		{
			_currentWordCells.Add(indices);
		}
	}

	public void InitializeGrid(HexGridView hexGridView, int rows, int cols)
	{
		if(LoadDictionary())
		{
			_hexGridView = hexGridView;
			GenerateRandomLetters(hexGridView, rows, cols);
		}
	}

	public bool LoadDictionary()
	{
		_dictionary = new HashSet<string>();
		TextAsset dictionaryAsset = Resources.Load<TextAsset>(DICTIONARY_RESOURCES_PATH);
		if(dictionaryAsset != null)
		{
			//TODO optimize
			string[] words = dictionaryAsset.text.Split(new string[]{"\r\n"}, System.StringSplitOptions.RemoveEmptyEntries);
			foreach(string word in words)
			{
				_dictionary.Add(word);
			}
		}
		return dictionaryAsset != null;
	}

	private void GenerateRandomLetters(HexGridView hexGridView, int rows, int cols)
	{
		const int ALPHABET_LENGTH = 26;
		_alphabet = new string[ALPHABET_LENGTH];
		char c = 'A';
		for(int i = 0; i < _alphabet.Length; i++)
		{
			_alphabet[i] = new string(new char[]{c});
			c++;
		}
		_letterGrid = new string[cols,rows];
		for(int y = 0; y < rows; y++)
		{
			for(int x = 0; x < cols; x++)
			{
				string letter = _alphabet[Random.Range(0,_alphabet.Length)];
				_letterGrid[x, y] = letter.ToLower();
				hexGridView.SetLetter(x, y, letter);
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
