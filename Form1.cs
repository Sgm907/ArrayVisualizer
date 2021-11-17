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
        private int[] drawingArray;
        private bool updateStarted;

        private Pen pen;
        private Brush brush;
        private Random rng;

        private float visualWidth;
        private float rectWidth;
        private float rectMaxHeight;
        public int arraySize = 2048;
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
            updateStarted = true;
            timer.Stop();
            #endregion
            #region initialize ComboBox

            algorithms.Add("Unoptomized", Algorithms.Unoptomized);
            AlgorithmSelect.ValueMember = "Value";
            AlgorithmSelect.DisplayMember = "Key";
            AlgorithmSelect.DataSource = new BindingSource(algorithms, null);

            #endregion

        }

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

        private void FollowSort()
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                (int, int) step = ((int, int))instructions[i];
                int temp = drawingArray[step.Item1];
                drawingArray[step.Item1] = drawingArray[step.Item2];
                drawingArray[step.Item2] = temp;
                Thread.Sleep(10);
            }
        }
        #endregion

        private void SortButton_Click(object sender, EventArgs e)
        {
            if (!sort.IsAlive)
            {
                instructions = sorter.Sort(drawingArray);
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
                float rectHeight = (rectMaxHeight/arraySize)* drawingArray[i];
                e.Graphics.FillRectangle(brush, 10+(rectWidth*i), Height-rectHeight- buffer, rectWidth, rectHeight);
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
            if (!sort.IsAlive)
            {
                sorter.Scramble(ref drawingArray);
                Invalidate();
            }
        }

    }
}
