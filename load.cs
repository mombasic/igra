using System;

public class Class1
{
	public Class1()
	{
        TileMap { offset, tileSize, mapSize, tiles[][] }
        Camera { left, right, top, bottom }

        int sx = max(0, floor((camera.left - map.offset.x) / map.tileSize.x));
        int ex = min(map.mapSize.x, floor((camera.right - map.offset.x) / map.tileSize.x) + 1);
        int sy = max(0, floor((camera.top - map.offset.y) / map.tileSize.y));
        int ey = min(map.mapSize.y, floor((camera.bottom - map.offset.y) / map.tileSize.y) + 1);

        for (int y = sy; y < ey; y++)
        {
            for (int x = sx; x < ex; x++)
            {
                draw map.tiles[y][x];
            }
        }
    }
}
