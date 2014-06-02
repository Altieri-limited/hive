using UnityEngine;
using System.Collections;

public class HexGridLogic : MonoBehaviour
{
	private HexGridView _hexGridView;
	private string[] _alphabet;
	private int _rows;
	private int _cols;

	public void InitializeGrid(HexGridView hexGridView, int rows, int cols)
	{
		_hexGridView = hexGridView;
		GenerateRandomLetters(hexGridView, rows, cols);
	}

	private void GenerateRandomLetters(HexGridView hexGridView, int rows, int cols)
	{
		const int ALPHABET_LENGTH = 26;
		_rows = rows;
		_cols = cols;
		_alphabet = new string[ALPHABET_LENGTH];
		char c = 'A';
		for(int i = 0; i < _alphabet.Length; i++)
		{
			_alphabet[i] = new string(new char[]{c});
			c++;
		}
		for(int y = 0; y < _rows; y++)
		{
			for(int x = 0; x < _cols; x++)
			{
				hexGridView.SetLetter(x, y, _alphabet[Random.Range(0,_alphabet.Length)]);
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
