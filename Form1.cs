using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ArrayVisualizer
{
    public partial class Form1 : Form
    {
        private Sorter sorter;
        private Thread sort;
        private Dictionary<string, Func<int[], ArrayList>> algorithms;

        private ArrayList instructions;
        private (int, int) lastStep;
        private int[] drawingArray;
        private bool updateStarted;

        private bool verify;
        private int verifyIndex;
        private bool sorted;

        private Pen pen;
        private Brush brush;
        private Random rng;

        private float visualWidth;
        private float rectWidth;
        private float rectMaxHeight;
        public int arraySize = 256;
        int buffer;

        //control variables;
        private static float desiredScreenPercent = 0.85f;//% of window width the visual should take up

        public Form1()
        {
            InitializeComponent();
            InitializeObjects();

        }

        #region Non-Event Methods

        private void InitializeObjects()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            #region initialize Form Objects
            rng = new Random();
            drawingArray = new int[arraySize];
            drawingArray = Enumerable.Range(1, arraySize).OrderBy(x => rng.Next()).ToArray();

            sorter = new Sorter(drawingArray);
            sort = new Thread(new ThreadStart(FollowSort));
            pen = new Pen(Color.White);
            brush = new SolidBrush(Color.White);

            visualWidth = Width * Helper.desiredScreenWPercent;
            rectWidth = visualWidth / arraySize;
            rectMaxHeight = Height * Helper.desiredScreenHPercent;

            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            buffer = screenRectangle.Top - this.Top + 10;

            algorithms = new Dictionary<string, Func<int[], ArrayList>>();

            instructions = new ArrayList();
            lastStep = (-1, -1);
            updateStarted = true;

            verify = false;
            verifyIndex = 0;
            sorted = true;

            timer.Stop();
            #endregion

            //Creates a dictionary full of the various sorting algorithms, and then populates the selector box with them
            #region initialize ComboBox

            algorithms.Add("Selection", Algorithms.SelectionSort);
            algorithms.Add("Insert", Algorithms.InsertSort);
            algorithms.Add("Bubble", Algorithms.BubbleSort);
            algorithms.Add("Gnome", Algorithms.GnomeSort);
            AlgorithmSelect.ValueMember = "Value";
            AlgorithmSelect.DisplayMember = "Key";
            AlgorithmSelect.DataSource = new BindingSource(algorithms, null);

            #endregion

        }

        /// <summary>
        /// Creates a new thread to sort and draw the array while the program runs
        /// </summary>
        private void Update(Object sender, EventArgs e)
        {
            if (!updateStarted)
            {
                if (!sort.IsAlive)
                    sort = new Thread(new ThreadStart(FollowSort));
                sort.Start();
                updateStarted = true;
            }
            Invalidate();
        }

        /// <summary>
        /// iterates through the steps provided to the form by the sorter
        /// </summary>
        private void FollowSort()
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                (int, int) step = ((int, int))instructions[i];
                lastStep = step;
                int temp = drawingArray[step.Item1];
                drawingArray[step.Item1] = drawingArray[step.Item2];
                drawingArray[step.Item2] = temp;
                Helper.BlockThread((double)visualDelay.Value / 1000);
            }
            Verify();
        }

        private void Verify()
        {
            verify = true;
            for (int i = 0; i < drawingArray.Length - 1; i++)
            {
                if (drawingArray[i] > drawingArray[i + 1])
                {
                    sorted = false;
                    break;
                }
                verifyIndex = i+1;
                Helper.BlockThread(0.005);
            }
        }
        #endregion

        private void SortButton_Click(object sender, EventArgs e)
        {
            verify = false;
            if (!sort.IsAlive)
            {
                double tempDuration = 0;
                instructions = sorter.Sort(drawingArray, ref tempDuration);
                sortDurationLabel.Text = tempDuration.ToString() + " ms";
                numIndLabel.Text = instructions.Count.ToString();
                timer.Start();
                updateStarted = false;
                Invalidate();
            }
        }

        private void AlgorithmSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func<int[], ArrayList> selected = (Func<int[], ArrayList>)AlgorithmSelect.SelectedValue;
            sorter.SetAlgorithm(selected);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < arraySize; i++)
            {
                if (!verify)
                {
                    if (i == lastStep.Item1)
                        pen.Color = Color.Red;
                    else if (i == lastStep.Item2)
                        pen.Color = Color.Green;
                    else
                        pen.Color = Color.White;
                }
                else
                {
                    if (sorted)
                    {
                        if (i <= verifyIndex)
                            pen.Color = Color.Green;
                        else
                            pen.Color = Color.White;
                    }
                    else
                    {
                        pen.Color = Color.Red;
                    }
                }
                float rectHeight = (rectMaxHeight / arraySize) * drawingArray[i];
                e.Graphics.DrawRectangle(pen, 10 + (rectWidth * i), Height - rectHeight - buffer, rectWidth, rectHeight);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            visualWidth = Width * Helper.desiredScreenWPercent;
            rectWidth = visualWidth / arraySize;
            rectMaxHeight = Height * Helper.desiredScreenHPercent;
            Invalidate();
        }

        private void ScrambleButton_Click(object sender, EventArgs e)
        {
            verify = false;
            if (!sort.IsAlive)
            {
                sorter.Scramble(ref drawingArray);
                Invalidate();
                numIndLabel.Text = "0";
                sortDurationLabel.Text = "0" + " ms";
            }
        }
    }
}
