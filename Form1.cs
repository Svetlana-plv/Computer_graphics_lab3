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

namespace CG_3
{
    public partial class Form1 : Form
    {
        public OpenTK.Vector3 CameraPosition;

        //public float Depth;


        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            View.Init();
            View.InitShaders();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            glControl1.Invalidate();
        }

        private void SetUniformVec3(string name, OpenTK.Vector3 value)
        {
            GL.Uniform3(GL.GetUniformLocation(View.BasicProgramID, name), value);
        }

        private void SetUniform1f(string name, float value)
        {
            GL.Uniform1(GL.GetUniformLocation(View.BasicProgramID, name), value);
        }

        private void Draw()
        {
            GL.ClearColor(Color.White);                 // устанавливает цвет, которым будет очищен цветовой буфер
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(View.BasicProgramID);          // активирует шейдерную программу для использования

            // передача параметров камеры в шейдеры
            SetUniformVec3("camera_position", CameraPosition);

            // отрисовка фона
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, -1);

            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1);

            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1);

            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, 1);

            GL.End();

            glControl1.SwapBuffers(); //копируем содержимое буфера вне экрана в буфер на экране
            GL.UseProgram(0);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            CameraPosition.X = trackBar4.Value;
            glControl1.Invalidate();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            CameraPosition.Y = trackBar5.Value;
            glControl1.Invalidate();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            CameraPosition.Z = trackBar6.Value;
            glControl1.Invalidate();
        }

    }
}
