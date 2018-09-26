using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_POE//Thee0
{
    public partial class Form1 : Form
    {
        Map GameMap;
        bool meleeAlive = true;
        bool rangedAlive = true;
        GameEngine TheGame;
        MyButton[,] buttonarray = new MyButton[20, 20];
        RichTextBox info = new RichTextBox();
        Button pause= new Button();
        Label GText = new Label();
        int GameCount = 0;
        bool timer = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            GameMap = new Map();
            GameMap.NewBattlefield();
            TheGame = new GameEngine();
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    MyButton map = new MyButton();
                    Map UnitPos = new Map();

                    Point NewPoint = new Point(100 + (x * 25), 100 + (y * 25));
                    Size NewSize = new Size(20, 20);
                    string say = GameMap.Update(x, y);
                    map.Location = NewPoint;
                    map.Size = NewSize;
                    map.Text = say;
                    map.x = x;
                    map.y = y;

                    foreach (MeleeUnit u in GameMap.MeleeList)
                    {
                        if (x == u.X && y == u.Y)
                        {

                            switch (u.team)
                            {
                                case "Blue":
                                    u.team = "Blue";
                                    map.BackColor = System.Drawing.Color.Blue;
                                    
                                    break;

                                case "Green":
                                    u.team = "Green";
                                    map.BackColor = System.Drawing.Color.Green;
                                   
                                    break;

                                case "Yellow":
                                    u.team = "Yellow";
                                    map.BackColor = System.Drawing.Color.Yellow;
                                   
                                    break;

                                case "Red":
                                    u.team = "Red";
                                    map.BackColor = System.Drawing.Color.Red;
                                  
                                    break;

                            }
                        }
                    }

                    foreach (RangedUnit u in GameMap.RangedList)
                    {
                        if (x == u.X && y == u.Y)
                        {

                            switch (u.team)
                            {
                                case "Blue":
                                    u.team = "Blue";
                                    map.BackColor = System.Drawing.Color.Blue;
                                   
                                    break;

                                case "Green":
                                    u.team = "Green";
                                    map.BackColor = System.Drawing.Color.Green;
                                   
                                    break;

                                case "Yellow":
                                    u.team = "Yellow";
                                    map.BackColor = System.Drawing.Color.Yellow;
                                   
                                    break;

                                case "Red":
                                    u.team = "Red";
                                    map.BackColor = System.Drawing.Color.Red;
                                   
                                    break;

                            }
                        }
                    }
                    this.Controls.Add(map);
                    buttonarray[x, y] = map;

                    map.Click += new EventHandler(button_Click);

                }
            } // Make the buttons
            Point newpoint = new Point(100, 600);
            Size newsize = new Size(400, 200);
            info.Location = newpoint;
            info.Size = newsize;
            this.Controls.Add(info);

            newpoint = new Point(500, 700);
            newsize = new Size(100, 50);

            pause.Location = newpoint;
            pause.Size = newsize;
            pause.Text = "> / ||";
            pause.Click += new EventHandler(Pause_Click);
            this.Controls.Add(pause);

            newpoint = new Point(500, 600);
            newsize = new Size(100, 50);

            GText.Location = newpoint;
            GText.Size = newsize;
            GText.Text = GameCount.ToString();

            this.Controls.Add(GText);

        }// placing all buttons and declaring everything

        private void Pause_Click(object sender, EventArgs e)
        {
            if (timer)
            {
                tmrGame.Enabled = false;
                timer = false;
            }
            else
            {
                tmrGame.Enabled = true;
                timer = true;
            }

        }

        private void updatebuttons()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    buttonarray[x, y].Text = "";
                    buttonarray[x, y].BackColor = System.Drawing.Color.LightGray;
                }
            }

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {

                    foreach (MeleeUnit u in GameMap.MeleeList )
                    {
                        if (u.Alive )
                        {
                            if (x == u.X && y == u.Y)
                            {
                                buttonarray[x, y].Text = "S";
                                switch (u.team)
                                {
                                    case "Blue":
                                        u.team = "Blue";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Blue;

                                        break;

                                    case "Green":
                                        u.team = "Green";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Green;

                                        break;

                                    case "Yellow":
                                        u.team = "Yellow";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Yellow;
                                        
                                        break;

                                    case "Red":
                                        u.team = "Red";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Red;
                                      
                                        break;

                                }
                            }
                        }
                        
                    }// update the map melee

                    foreach (RangedUnit u in GameMap.RangedList)
                    {
                        if (u.Alive)
                        {
                            if (x == u.X && y == u.Y)
                            {
                                buttonarray[x, y].Text = "A";
                                switch (u.team)
                                {
                                    case "Blue":
                                        u.team = "Blue";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Blue;
                                       
                                        break;

                                    case "Green":
                                        u.team = "Green";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Green;
                                       
                                        break;

                                    case "Yellow":
                                        u.team = "Yellow";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Yellow;
                                        
                                        break;

                                    case "Red":
                                        u.team = "Red";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Red;
                                       
                                        break;

                                }
                            }
                        }                 
                    }// update the map ranged
                }
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            
            if (((MyButton)sender).Text == "S" || ((MyButton)sender).Text == "A")
            {

                foreach (MeleeUnit u in GameMap.MeleeList)
                {

                    if (u!= null)
                    {
                        if (((MyButton)sender).x == u.X && ((MyButton)sender).y == u.Y)
                        {
                            MeleeUnit newunit = new MeleeUnit();
                            newunit.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                            info.Text = u.ToString();

                        }
                    }
                    
                }

                foreach (RangedUnit u in GameMap.RangedList)
                {

                    if (u != null)
                    {
                        if (((MyButton)sender).x == u.X && ((MyButton)sender).y == u.Y)
                        {
                            RangedUnit newunit = new RangedUnit();
                            newunit.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                            info.Text = u.ToString();

                        }
                    }

                }


            }



        }// dlisplay unit details

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            int count = 0;

            foreach (MeleeUnit u in GameMap.MeleeList)
            {
                if (u.Alive)
                {
                    count++;
                }

            }

            if (count == 0)
            {
                meleeAlive = false;
            }

            if (meleeAlive)
            {
                foreach (MeleeUnit u in GameMap.MeleeList)
                {
                    if (u.Alive)
                    {
                        if (count > 0)
                        {
                            CombatEngine(u);
                        }
                    }
                   
                }
            }
           
            count = 0;
           
            foreach (RangedUnit u in GameMap.RangedList)
            {
                if (u.Alive)
                {
                    count++;
                }

            }

            if (count == 0)
            {
                rangedAlive = false;
            }
        
            if (rangedAlive)
            {
                foreach (RangedUnit u in GameMap.RangedList)
                {
                    if (u.Alive)
                    {
                        if (count > 0)
                        {
                            CombatEngine(u);
                        }                      
                    }
                }
            }
            else
            {
                MessageBox.Show("Ranged units dead");
            }

            GameCount++;
            GText.Text = GameCount.ToString();
            updatebuttons();
            
        }// activates the game engine
       
        public void CombatEngine(Unit u)
        {
           
                if (u.attackRange < 3)
                {

                     MeleeUnit u2 = (MeleeUnit)u;
                if ((u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList)) != null)
                {
                    int distance = u2.FindUnit(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));
                  
                    if (distance <= u2.attackRange)
                    {
                        u2.Combat(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));

                    } //combat
                    else
                    {
                        if (u2.Health > 2)
                        {
                            if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange < 3)
                            {
                                MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                if (Position.X > u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X < u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }
                            else
                            {
                                RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                if (Position.X > u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X < u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }

                        }// movement
                        else
                        {
                            if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange < 3)
                            {
                                MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                
                                if (Position.X < u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X > u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }
                            else
                            {
                                RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);

                                if (Position.X < u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X > u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }

                        }// running away / needs work

                    }

                    if (u.Death())
                    {
                        u2.Alive = false;

                    } // handels death in the game
                }// melee

                }
                    
                else
                {
                RangedUnit u2 = (RangedUnit)u;
                int counti = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (GameMap.MeleeList[i] != null && u.ReturnPosition(GameMap.MeleeList, GameMap.RangedList) != null)
                        {
                            if (u.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).team != u.team)
                            {
                                counti++;
                            }

                        }
                    }
                    if (counti > 0) // see number of units in array
                    {
                       
                        int distance = u2.FindUnit(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));

                        if (distance <= u2.attackRange)
                        {
                            u2.Combat(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));

                        } //combat
                        else
                        {
                            if (u2.Health > 2)
                            {
                                if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange > 3)
                                {
                                    RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X > u2.X)
                                    {
                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }
                                    if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }
                                else
                                {
                                    MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X > u2.X)
                                    {
                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }
                                    if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }

                            }// movement
                            else
                            {
                                if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange > 3)
                                {
                                    RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X > u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }

                                    if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }
                                else
                                {
                                    MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X > u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }

                                    if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }                                                               
                            }// running away / needs work
                        }

                    }

                    if (u.Death())
                    {
                        
                        u2.Alive = false;
                       
                    } // handels death in the game
                }
            
            

           

        }// the working of the game
        

    }

}
