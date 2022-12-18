using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace letter_sh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.3f, 0.3f, 0.3f, 1);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 m;

            m =
                Matrix4.LookAt(0, 1, 3, 0, 0, 0, 0, 1, 0) *
                Matrix4.CreatePerspectiveFieldOfView((float)(60 * Math.PI / 180.0), 1, 0.1f, 10);
            GL.LoadMatrix(ref m);
        }

        Vector3[] vert = new Vector3[] {       
            // Размер 9,1171875 на 8,58984375

            // Start point
            new Vector3( 0f, 0f, 0 ),
            // PolyLineSegment
            new Vector3( 1.13671875f, 0f, 0 ),
            new Vector3( 1.13671875f, 7.576171875f, 0 ),
            new Vector3( 3.990234375f, 7.576171875f, 0 ),
            new Vector3( 3.990234375f, 0f, 0 ),
            new Vector3( 5.126953125f, 0f, 0 ),
            new Vector3( 5.126953125f, 7.576171875f, 0 ),
            new Vector3( 7.98046875f, 7.576171875f, 0 ),
            new Vector3( 7.98046875f, 0f, 0 ),
            new Vector3( 9.1171875f, 0f, 0 ),
            new Vector3( 9.1171875f, 8.58984375f, 0 ),
            new Vector3( 0f, 8.58984375f, 0 ),
            new Vector3( 0f, 0f, 0 ),
        };

        void drawAxes()
        {
            // Рисуем оси
            GL.Begin(PrimitiveType.Lines);

            GL.Color3(1.0f, 0, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(15, 0, 0);

            GL.Color3(0, 1.0f, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 15, 0);

            GL.Color3(0, 0, 1.0f);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 15); // */

            GL.End();
        }

        float ra = 0, rb = 135;

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);

            Matrix4 m = 
                Matrix4.CreateTranslation( -4.5f, -4.4f, 0 ) *
                Matrix4.CreateRotationX(rb) *
                Matrix4.CreateRotationY(ra) * 
                Matrix4.CreateScale( 0.2f, 0.2f, 0.2f );

            GL.LoadMatrix(ref m);

            drawAxes();

            GL.Color3(0, 1.0f, 0);

            GL.Begin(PrimitiveType.Lines);

            for (int i = 0; i < vert.Length - 1; i++)
            {
                GL.Vertex3(vert[i].X, vert[i].Y, 0);
                GL.Vertex3(vert[i+1].X, vert[i+1].Y, 0);

                GL.Vertex3(vert[i].X, vert[i].Y, 3);
                GL.Vertex3(vert[i + 1].X, vert[i + 1].Y, 3);

                GL.Vertex3(vert[i].X, vert[i].Y, 0);
                GL.Vertex3(vert[i].X, vert[i].Y, 3);
            }

            GL.End();

            glControl1.SwapBuffers();
        }

        int oldmx, oldmy;

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ra += (e.X - oldmx) / 100.0f;
                rb += (e.Y - oldmy) / 100.0f;

                glControl1.Invalidate();
            }

            oldmx = e.X;
            oldmy = e.Y;
        }
    }
}
