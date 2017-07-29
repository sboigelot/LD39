namespace Assets.Scripts.Models
{
    public class Level
    {
        public Mermaid Mermaid;
        public Tile[,] Tiles;

        public Level()
        {
            Tiles = new Tile[9, 14];
        }
    }
}