# Snake - Проект по Визуелно програмирање
## Ангел Ристевски, број на индекс 171012
### Краток опис на играта

- Играта започнува со табла на која има една жолта точка којашто претставува глава на змија која може да ја движиме со притискање на копчињата горе, долу, лево, десно и една црвена точка која претставува храна и таа се генерира на случајна позиција на таблата. На следната слика е прикажана една почетна состојба во играта:

![Pochetna sostojba](https://user-images.githubusercontent.com/48767427/176994848-82c7fb2d-6036-4441-8c08-c7369a07a1b7.png)

- Кога позицијата на змијата ќе стане еднаква со позицијата на храната змијата се зголемува за едно квадратче и на случајна позиција се создава нова храна. На следната слика е прикажана состојба во која змијата тукушто изела црвена точка и се зголемила за едно делче повеќе и се генерирала нова црвена точка на таблата:

![Zgolemena zmija](https://user-images.githubusercontent.com/48767427/176994851-6d5d480a-435f-48f5-a303-cf6d7c49d87b.png)

- Играта по желба на корисникот може и да биде паузирана со пристискање на копчето Пауза. Откако ќе биде притиснато копчето Пауза се менува текстот на тоа копче во Продолжи. Корисникот потоа кога ќе се реши да продолжи со играта повторно го притиска тоа копче. На сликата е прикажана паузирана состојба од играта:

![Pauza](https://user-images.githubusercontent.com/48767427/176994842-54825499-12e6-4287-bf0b-66587c74c343.png)

- Во долниот лев агол од формата има променлива Големина која динамички се менува во текот на играта соодветно на зголемувањето на змијата.

- Играта се смета за победена ако змијата изеде 20 точки. На сликата е прикажана состојба на победа во играта:

![Pobeda](https://user-images.githubusercontent.com/48767427/176994845-eab14000-49c6-4342-bcc2-bfbf7f79e3c9.png)
 
- Играта се смета за загубена ако главата на змијата се судри со некој од ѕидовите или со дел од своето тело. На сликата е прикажана состојба на загуба во играта:

![Zaguba](https://user-images.githubusercontent.com/48767427/176994849-8d7df6c3-926b-4529-8c86-11d09e365172.png)


### Имплементација
- Во windows form се креираат PictureBox компоненти за мапата на играта (game_map), главата на змијата (zmija), храната (hrana) и листа од PictureBox компоненти за телото на змијата (zmija_delovi).
```C#
        PictureBox game_map = new PictureBox();
        PictureBox zmija = new PictureBox();
        PictureBox hrana = new PictureBox();
        PictureBox[] zmija_delovi = new PictureBox[20];
```
- Се креираат и променливи cord_x и cord_y за насоката на движење на змијата, променлива index во која се чува должината на опашката на змијата и променлива golemina во која се чува целосната големина на змијата.
```C#
int cord_x = 1, cord_y = 0, index = 0;
public int golemina { get; set; } = 1;
```

- Се додава timer.

- Кога се стартува програмата во Form1_Load() функцијата се поставуваат почетните вредности на променливите и се стартува timer-от.

```C#
private void Form1_Load(object sender, EventArgs e)
        {
            game_map.Width = 500;
            game_map.Height = 500;
            game_map.BackColor = Color.Cyan;
            game_map.Location = new Point(40, 30);
            this.Location = new Point(50, 50);
            this.Width = this.Height = 600;
            this.Controls.Add(game_map);
            
            zmija.Width = zmija.Height = 18;
            zmija.BackColor = Color.Yellow;
            zmija.Location = new Point(100, 100);

            hrana.Width = hrana.Height = 18;
            hrana.BackColor = Color.Red;
            hrana.Location = new Point(300, 300);

            game_map.Controls.Add(zmija);
            game_map.Controls.Add(hrana);
            timer1.Start();
        }
```

- Додаваме KeyDown event со кој ја менуваме насоката на движење на змијата.
```C#
private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                cord_x = -1;
                cord_y = 0;
            }
            else if (e.KeyCode == Keys.Right)
            {
                cord_x = 1;
                cord_y = 0;
            }
            else if (e.KeyCode == Keys.Up)
            {
                cord_x = 0;
                cord_y = -1;
            }
            else if (e.KeyCode == Keys.Down)
            {
                cord_x = 0;
                cord_y = 1;
            }
        }
```


- Креираме функција iscrtaj_zmija() која се користи за да се движи змијата, односно се повикува при секое отчукување на timer-от и ја движи змијата за едно поле нанапред во назначената насока.
```C#
        public void iscrtaj_zmija()
        {
            for(int i = index; i >=2; i--)
            {
                zmija_delovi[i].Location = zmija_delovi[i - 1].Location;
            }
            if (index > 0)
            {
                zmija_delovi[1].Location = zmija.Location;
            }
        }
```
- Со помош на функцијата зголеми змија ја зголемуваме големината на змијата за секое изедено црвено квадратче. Доколку при соодветното зголемување на змијата играчот ја довел змијата до големина 20 тогаш се печати пораката за победа во играта "ЧЕСТИТКИ! ПОБЕДИВТЕ!".
```C#
        public void zgolemi_zmija()
        {
            if (golemina == 20)
            {
                timer1.Stop();
                MessageBox.Show("ЧЕСТИТКИ! ПОБЕДИВТЕ!");
                this.Close();
            }

            if(index <= 19)
            {
                zmija_delovi[index] = new PictureBox();
                zmija_delovi[index].BackColor = Color.White;
                zmija_delovi[index].Location = zmija.Location;
                zmija_delovi[index].Width = zmija_delovi[index].Height = 18;
                game_map.Controls.Add(zmija_delovi[index]);
            }      
        }
```

- Креираме функција nova_hrana() со помош на која исцртуваме нова храна на случајна локација на таблата доколку претходната храна е изедена од змијата.
```C#
public void nova_hrana()
        {
            Random r = new Random();
            int x = r.Next(25);
            int y = r.Next(25);
            hrana.Location = new Point(x * 20, y * 20);
            for (int i = 1; i < index; i++)
            {
                if (hrana.Location == zmija_delovi[i].Location)
                {
                    nova_hrana();
                }
            }
        }
```

- Со помош на функцијата proveri() се проверува дали играчот прекршил некое од правилата во играта односно дали змијата се удрила во ѕидовите од таблата или змијата се изела себеси. Доколку змијaта направила една од овие две работи, се печати пораката "ИЗГУБИВТЕ!".
```C#
        public void proveri()
        {
            if (zmija.Location.X < 0 || zmija.Location.X >= 500 || zmija.Location.Y < 0 || zmija.Location.Y >= 500)
            {
                timer1.Stop();
                MessageBox.Show("ИЗГУБИВТЕ!");
                this.Close();
            }
            for(int i = 1; i <index; i++)
            {
                if (zmija.Location == zmija_delovi[i].Location)
                {
                    timer1.Stop();
                    MessageBox.Show("ИЗГУБИВТЕ!");
                    this.Close();
                }
            }
        }
```


- Со секое отчукување на timer-от ја движиме змијата во соодветната насока, проверуваме дали змијата е на иста позиција со храната, ја зголемуваме големината на змијата доколку изела храна и проверуваме дали играта е завршена.
```C#
        private void timer1_Tick(object sender, EventArgs e)
        {
            iscrtaj_zmija();
            zmija.Location = new Point(zmija.Location.X + cord_x * 20, zmija.Location.Y + cord_y * 20);
            if (zmija.Location == hrana.Location)
            {
                index = index + 1;
                golemina += 1;
                goleminaStripStatusLabel1.Text = string.Format("Големина: {0}", golemina);
                zgolemi_zmija();
                nova_hrana();
            }
            proveri();
        }
```
