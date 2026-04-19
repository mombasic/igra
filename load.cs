using igra.Properties;
using System;
using System.Drawing;

namespace igra
{
	internal class Load
	{
        //int pos = 400;

        int tileSize = 32;
        int ColumnsToRender = 27;
        int RowsToRender = 2;

        int maxX = 8000;
        int maxY = 3200;

        int[,] mapa = new int[8000 / 32, 3200 / 32];

        //public void begin() 
        //{
        //    for(int i = 0; i < maxX / tileSize; i++) 
        //    {
        //        for(int j = 0; j < maxY / tileSize; j++) 
        //        {
        //            if (j == 0 || j == 1) mapa[i, j] = 1;
        //            mapa[i, j] = 0;
        //        }
        //    }
        //}


        public void render(Graphics g, int pos)
        {
            Rectangle sourceRectangle;
            for (int x = 0; x < ColumnsToRender; x++)
            {
                for (int y = 1; y <= RowsToRender; y++)
                {
                    //Tile tileToDraw = world.getTile(TopLeftTile.X + x, TopLeftTile.Y + y);
                    sourceRectangle = new Rectangle(0, 0, tileSize, 32);
                    //if (x == 0 || x == ColumnsToRender - 1) sourceRectangle = new Rectangle(0, 0, pos % tileSize, 32);

                    Rectangle destinationRectangle = new Rectangle(x * tileSize - pos % 32, 640 - y * tileSize, tileSize, tileSize);
                    //Rectangle sourceRectangle = tilesetTerrainPositions[tileToDraw.Terrain];
                    g.DrawImage(Resources.komadic, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
            }
        }

        //Tile[,] tiles;
    }
}
