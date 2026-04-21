using igra.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace igra
{
    internal class Load
    {
        //int pos = 400;

        Bitmap[] texture = { Resources.komadic };

        int tileSize = 32;
        int ColumnsToRender = 27;
        int RowsToRender = 20;

        int maxX = 8000;
        int maxY = 3200;

        public int[,] mapa = new int[8000 / 32, 3200 / 32];

        //Dictionary<Bitmap, int> mapa = new Dictionary<Bitmap, int>();

        public void begin()
        {
            for (int i = 0; i < maxX / tileSize; i++)
            {
                for (int j = 0; j < maxY / tileSize; j++)
                {
                    if (j == 0 || j == 1) mapa[i, j] = 1;
                    else mapa[i, j] = 0;
                }
            }
            mapa[36, 2] = 1;
            mapa[36, 3] = 1;
            mapa[36, 4] = 1;

            mapa[32, 6] = 1;
            mapa[30, 7] = 1;
        }


        public void render(Graphics g, int pos)
        {
            Rectangle sourceRectangle;
            for (int x = 0; x < ColumnsToRender; x++)
            {
                for (int y = 1; y <= RowsToRender; y++)
                {
                    //Console.WriteLine(x + " " + y + " " + pos);
                    if (mapa[x + pos / 32, y - 1] != 0)
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
        }

        //Tile[,] tiles;


//        private void HandleYCollisions()
//        {
//            playerGrounded = false; // pretpostavljamo da igrac nije na zemlji

//            foreach (Rectangle rect in currentLevel.obstacleHitboxes)
//            {

//                if (player.Bounds.IntersectsWith(rect))
//                {
//                    if (playerYVel > 0) // ako igrac pada
//                    {
//                        player.Top = rect.Top - player.Height; // ogranici igraca da ne pada ispod prepreke
//                        playerYVel = 0;
//                        playerGrounded = true;
//                    }
//                    else if (playerYVel < 0) // ako igrac skace (njegova Y brzina je negativna)
//                    {
//                        player.Top = rect.Bottom; // ogranici igraca da ne skoci kroz prepreku
//                        playerYVel = 0; // ako udari prepreku krene da pada
//                    }
//                }

//                /* ukoliko igrac stoji na predmetu, ovo sluzi da sprijeci da igrac skakuce gore-dolje po jedan piksel
//                * tako sto osigurava da je playerGrounded true i konstantno neutralise gravitaciju svaki frame */
//                if (player.Bottom == rect.Top && player.Left < rect.Right && player.Right > rect.Left)
//                {
//                    playerGrounded = true;
//                    playerYVel = 0;
//                }
//            }

//            if (player.Top < -player.Height) player.Top = 520;
//        }

//        // Gore iznad ima jos koda al bitno je da deklarises ovo negdje gore
//        public List<Rectangle> collisionRects;

//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);

//            List<Label> walls = new List<Label>();
//            collisionRects.Clear();

//            foreach (Label wall in this.Controls)
//            {
//                if (wall.Tag.ToString().ToLower() == "wall")
//                {
//                    int startX = (int)(Math.Round(wall.Left / 20.0));
//                    int endX = (int)(Math.Round(wall.Right / 20.0));
//                    int startY = (int)(Math.Round(wall.Top / 20.0));
//                    int endY = (int)(Math.Round(wall.Bottom / 20.0));

//                    walls.Add(wall);
//                    for (int i = startX; i < endX; i++)
//                    {
//                        for (int j = startY; j < endY; j++)
//                        {
//                            if (i >= 40 || j >= 30 || i < 0 || j < 0) continue;

//                            map[i, j] = 1;
//                            collisionRects.Add(new Rectangle(20 * i, 20 * j, 20, 20));
//                        }
//                    }

//                }
//            }

//            foreach (Label wall in walls)
//            {
//                wall.Hide();
//                this.Controls.Remove(wall);
//                this.Invalidate();
//            }
//        }
//    }

//// Gore iznad ima jos koda al bitno je da deklarises ovo negdje gore
//// mapa mi je 2D niz dimenzija 40x30
//    public List<Rectangle> collisionRects;

//        // ovo je isto sto i Form1_Load(object sender, EventArgs e) samo sto je meni ovako ljepse i lakse radit da ne moram
//        // koristit designer za kod nego samo za dizajn levela i takve stvari jer mi je sve preko grafike
//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);

//            List<Label> walls = new List<Label>(); // ovako sam ja labele koristio kao zidove
//            collisionRects.Clear(); // svaki put kad ucita novi level ocisti se lista collisiona

//            foreach (Label wall in this.Controls) // idem pojedinacno za svaki label
//            {
//                if (wall.Tag.ToString().ToLower() == "wall") // ako label ima tag "wall"
//                {
//                    // ovdje sam racunao koordinate da mi snappuje na grid ono automatski iz designera
//                    // dijelim sa 20 jer mi je tile size 20 pixela pa zaokruzujem da bude podjednako s obje strane
//                    int startX = (int)(Math.Round(wall.Left / 20.0));
//                    int endX = (int)(Math.Round(wall.Right / 20.0));
//                    int startY = (int)(Math.Round(wall.Top / 20.0));
//                    int endY = (int)(Math.Round(wall.Bottom / 20.0));

//                    // dodajem trenutni label wall u ovu listu da ih poslije mogu maknut jer mi nece trebat
//                    walls.Add(wall);
//                    for (int i = startX; i < endX; i++) // idem prvo po x osi posto hocu da upisem u matricu
//                    {
//                        for (int j = startY; j < endY; j++) // onda idem po y osi (mozda sam obrnuto ovo al radi opet sigurno)
//                        {
//                            // ako je izracunata vrijednost out of bounds od niza map, onda preskoci trenutnu iteraciju i nastavi
//                            if (i >= 40 || j >= 30 || i < 0 || j < 0) continue;

//                            // u slucaju da je sve u redu upise jedinicu u matrici na izracunatim pozicijama gdje je zid
//                            map[i, j] = 1;

//                            // TEBI TREBA OVO
//                            // na listu collisionRects dodaje se novi rectangle dimenzija sto su translated
//                            // tako da se iz koordinata matrice prikazu na ekran
//                            // tile size je 20, tako da ide od koordinata 20*i, 20*j, i velicine 20 piksela u obje dimenzije
//                            collisionRects.Add(new Rectangle(20 * i, 20 * j, 20, 20));
//                        }
//                    }

//                }
//            }

//            // makne labele da se ne moraju dodatno renderat i zauzimat memoriju u programu jer su oni bili samo placeholderi
//            // na osnovu kojih se kreira level (oni ti u sustini trebaju samo da ih postavih u designeru i na osnovu njih ide level)
//            foreach (Label wall in walls)
//            {
//                wall.Hide();
//                this.Controls.Remove(wall);
//                this.Invalidate();
//            }
//        }
    }
}
