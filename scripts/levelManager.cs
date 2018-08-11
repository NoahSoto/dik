using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//allows you to work with text documents
public class levelManager : MonoBehaviour {
    [SerializeField]//allows you to place tiles
    private GameObject[] tilePrefabs;//An array of tiles.  ATM i have grass rock, and a start/stop.  Spoonce would be making the sprites for these and all we'd have to do is put them in an array.
    public float TileLength//This finds the tile length so that you can equally space out the tiles using the for loops.
    {
        get { return tilePrefabs[1].GetComponent<SpriteRenderer>().sprite.bounds.size.x; } //The basis for the tile length.  Takes the first element of the array and finds the size of that element.  They're all the same so you could choose which ever.
    }


    // Use this for initialization
    void Start() {
        levelCreator();//Very first thing to be run upon starting the script.  Tells Unity to go to the level creator method and run that.
    }

    // Update is called once per frame
    void Update() {

    }
    public void levelCreator() { //The level creator method is responsible for taking a text document, placing the contents of the document as strings inside of a string array, and then using that information to create the map.

        string[] mapData = readMap(); //A string array is being set equal to the map created ina text document.
      
               
        
        int mapXSize = mapData[0].ToCharArray().Length; //sets the x size. Becuase each element within the text document is the same length in order to keep it proportional it doesnt matter which one you choose.  I chose 0
        int mapYSize = mapData.Length; // The total amount of elements in the array sets the y size.

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); // This line tells the program where the start of the camera view is.  top left makes the most sense. top left corner of camera view
            
        for (int y = 0; y < mapYSize; y++)             //used x and y used for coordinates rather than i and j
        {

            char[] newTiles = mapData[y].ToCharArray(); // takes the element at the y position and converts it to a character array so that it can be traversed in the next for loop

            for (int x = 0; x < mapXSize; x++)
            {
                placeTile(x, y, worldStart, newTiles[x].ToString()); // performs the placeTile method, using the paramters of x,y,the position, and what block to use
            }
        }
    }





    private void placeTile(int x, int y, Vector3 worldStart, string tileType)//Responsible for placing the blocks in the world
    {
        int tileIndex = int.Parse(tileType); // converts the tiletype from a character into an integer


        GameObject newTile = Instantiate(tilePrefabs[tileIndex]); //instantiate basically means place in the world.  creates a new tile and goes through the array of possible tiles and uses the integer here to go to that element to determine the sprite.
        newTile.transform.position = new Vector3(worldStart.x + (TileLength * x),worldStart.y - (TileLength * y), 0); // moves the tile so they dont all go ontop of eachother but space out
     }
    //Go back over this:::confusing
    private string[] readMap() // 
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);  //replacing every new line witha  empty string?

        return data.Split('-');//returns the characters before the -'s.
    }

}
