using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CG_3
{
    public partial class View
    {
        public static int BasicProgramID;
        public static int BasicVertexShader;
        public static int BasicFragmentShader;

        
        public static bool Init() // инициализация основных параметров OpenGL
        {
            GL.Enable(EnableCap.ColorMaterial);     //включает управление свойством материала с помощью текущего цвета
            GL.ShadeModel(ShadingModel.Smooth);     //включает сглаживание

            GL.Enable(EnableCap.DepthTest);         //включает тест глубины
            GL.Enable(EnableCap.CullFace);          //отключает все нелицевые грани

            GL.Enable(EnableCap.Lighting);          //включаем освещение
            GL.Enable(EnableCap.Light0);            //включает нулевой источник света

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
                                                                            
            return true;
        }

        public static void loadShader(String filename, ShaderType type, int program, out int address)  // загрузка и компиляция отдельного шейдера
        {
            address = GL.CreateShader(type); // создаем шейдер
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd()); // загружаем исходный код
            }
            GL.CompileShader(address); 
            GL.AttachShader(program, address); // прикрепляем к программе
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public static void InitShaders() // инициализация всей шейдерной программы
        {
            int status = 0;

            BasicProgramID = GL.CreateProgram(); // создаем шейдерную программу
            // загрузка и компилящия шейдеров
            loadShader("..\\..\\raytracing.vert.txt", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader("..\\..\\raytracing.frag.txt", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }
    }
}
