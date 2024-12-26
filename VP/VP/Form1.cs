using System;
using System.Drawing;
using System.Windows.Forms;
using Vlc.DotNet.Core;

namespace VP
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();

            KeyDown += VideoSpeed_Up_or_Down;
            KeyDown += VideoPause;
            KeyDown += VideoSound_Up_or_Down;
            labelSpeed.Tick += LabelTimerSpeed;
            trackBar.Scroll += Scroll_TrackBar;
            vlcControl.PositionChanged += PositionChanged_TrackBar;
        }
        #region Реализация выбора и воспроизведения видеофайла
        private void open_video(object sender, EventArgs e)
        {
            #region Обработка папки видеофайлов (для будущей реализации проекта)

            //if (openFolder.ShowDialog() == DialogResult.OK)
            //{
            //    string[] listFolder = Directory.GetDirectories(openFolder.SelectedPath);
            //    foreach (var items in listFolder)
            //    {
            //        string[] listItems = Directory.GetFiles(items);
            //        foreach (string item in listItems)
            //        {
            //            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
            //            //playList.Items.Add(fileNameWithoutExtension);
            //            dictVideos.Add(fileNameWithoutExtension, item);
            //        }
            //    }
            //}
            #endregion

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                vlcControl.Play(new Uri(openFile.FileName));
                button_Play_Pause.Image = ImagePause;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Загрузка label которое показывает скорость на видео и придает прозрачность
            this.label1.Parent = vlcControl;
            this.label1.BackColor = Color.Transparent;
            // Начальное значение видимости label (показывает скорость воспроизведения видео)
            this.label1.Visible = false;

            this.label2.Parent = vlcControl;
            this.label2.BackColor = Color.Transparent;
            this.label2.Visible = false;

            // Воспроизведение видео без звука, сделать звук
            this.vlcControl.Audio.Volume = 50;
            this.button_Play_Pause.Image = ImagePlay;
        }
        #endregion

        #region Изменение/Работа TrackBar (Необходимо доработать плавность перемещения ползунка при проигрывании)
        // Изменяется позиция ползунка TrackBar при воспроизведения видео
        private void PositionChanged_TrackBar(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            if (trackBar.InvokeRequired)
            {
                trackBar.Invoke(new Action(() =>
                {
                    trackBar.Value = (int)(e.NewPosition * trackBar.Maximum);

                    timeSpaninitial = TimeSpan.FromSeconds(trackBar.Value);
                    initialValueVideo.Text = $"{timeSpaninitial.Hours:D2}:{timeSpaninitial.Minutes:D2}:{timeSpaninitial.Seconds:D2}";

                    timeSpanMax = TimeSpan.FromSeconds(trackBar.Maximum);
                    maxValueVideo.Text = $"{timeSpanMax.Hours:D2}:{timeSpanMax.Minutes:D2}:{timeSpanMax.Seconds:D2}";

                }));
            }
            else
            {

                trackBar.Value = (int)(e.NewPosition * trackBar.Maximum);
                timeSpaninitial = TimeSpan.FromSeconds(trackBar.Value);
                initialValueVideo.Text = $"{timeSpaninitial.Hours:D2}:{timeSpaninitial.Minutes:D2}:{timeSpaninitial.Seconds:D2}";

                timeSpanMax = TimeSpan.FromSeconds(trackBar.Maximum);
                maxValueVideo.Text = $"{timeSpanMax.Hours:D2}:{timeSpanMax.Minutes:D2}:{timeSpanMax.Seconds:D2}";
            }
        }
        // Изменение значения продолжительности видео через TrackBar под видео
        private void Scroll_TrackBar(object sender, EventArgs e)
        {
            vlcControl.Position = (float)trackBar.Value / trackBar.Maximum;
        }
        #endregion


        #region Измнение скорости воспроизведения и визуализация информации для пользователя (label на видео)

        private void VideoPause(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && vlcControl.IsPlaying)
            {
                button_Play_Pause.Image = ImagePlay;
                vlcControl.Pause();
            }
            else
            {
                button_Play_Pause.Image = ImagePause;
                vlcControl.Play();
            }

        }

        private void LabelTimerSpeed(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            labelSpeed.Stop();
        }
        private void VideoSpeed_Up_or_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemCloseBrackets)
            {
                vlcControl.Rate += 0.1f;
                this.label1.Text = "Speed: " + Math.Round(vlcControl.Rate, 1).ToString();
                this.label1.Visible = true;
                labelSpeed.Start();
            }

            if (e.KeyCode == Keys.OemOpenBrackets)
            {

                vlcControl.Rate -= 0.1f;
                this.label1.Text = "Speed: " + Math.Round(vlcControl.Rate, 1).ToString();
                this.label1.Visible = true;
                labelSpeed.Start();

            }
        }
        #endregion
        #region Измнение громкости воспроизведения и визуализация информации для пользователя
        private void VideoSound_Up_or_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && this.vlcControl.Audio.Volume < 100)
            {
                this.vlcControl.Audio.Volume += 10;
                this.label2.Text = $"Volume: {this.vlcControl.Audio.Volume}";
                this.label2.Visible = true;
                labelSpeed.Start();
            }
            if (e.KeyCode == Keys.O && this.vlcControl.Audio.Volume >= 0)
            {
                this.vlcControl.Audio.Volume -= 10;
                this.label2.Text = $"Volume: {this.vlcControl.Audio.Volume}";
                this.label2.Visible = true;
                labelSpeed.Start();
            }
        }
        #endregion

    }

}

