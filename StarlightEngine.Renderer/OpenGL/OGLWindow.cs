using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Windowing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing.Glfw;
using Silk.NET.SDL;
using Silk.NET.GLFW;
using System.Runtime.InteropServices;

namespace StarlightEngine.Renderer.OpenGL
{
    public unsafe class OGLWindow : Window
    {
        public Action<double> r_OnUpdate { get; set; }
        public Action<double> r_OnRender { get; set; }
        public Action r_OnLoad { get; set; }

        public string Name = "Undefined Window";
        public Vector2 Size = new Vector2(800,600);
        public WindowHandle* _Handle { get; set; }
        public GlfwNativeWindow _natHandle { get; set; }
        public GlfwContext _glContext { get; set; }
        public bool CurrentlyRunning = false;

        public Glfw GlfwAPI;

        public unsafe override void Run()
        {
            CurrentlyRunning = true;
            GlfwAPI.MakeContextCurrent(_Handle);
        }

        public unsafe override void Exit()
        {
            GlfwAPI.DestroyWindow(_Handle);
            GlfwAPI.Terminate();
        }

        public unsafe override bool WindowShouldClose()
        {
            return GlfwAPI.WindowShouldClose(_Handle);
        }

        public unsafe override void ModifyName(string name)
        {
            Name = name;
            GlfwAPI.SetWindowTitle(_Handle, name);
        }

        public unsafe override void ModifySize(Vector2 size)
        {
            Size = size;

            GlfwAPI.SetWindowSize(_Handle, (int)Size.X, (int)Size.Y);
        }

        internal void OnKeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            // PROCESS TO INPUT CHANNELS.

            // ALWAYS CLOSE WINDOW ON ESCAPE.

            if (arg2 == Key.Escape)
                this.Exit();
        }

        // Internal Load, user load function is run inside.
        private unsafe void _Handle_Load()
        {
            r_OnLoad();

            /*IInputContext input = _Handle.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += OnKeyDown;
            }*/
        }

        public unsafe OGLWindow(Action<double> r_OnUpdate, Action<double> r_OnRender, Action r_OnLoad, string name, Vector2 size)
        {
            this.r_OnUpdate = r_OnUpdate ?? throw new ArgumentNullException(nameof(r_OnUpdate));
            this.r_OnRender = r_OnRender ?? throw new ArgumentNullException(nameof(r_OnRender));
            this.r_OnLoad = r_OnLoad ?? throw new ArgumentNullException(nameof(r_OnLoad));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Size = size;

            var Opts = WindowOptions.Default;
            Opts.Size = Size.ToSilkV2();
            Opts.Title = Name;

        }

        private void _Handle_Render(double obj)
        {
            Console.WriteLine("RF");
            this.r_OnRender(obj);
        }

        public unsafe OGLWindow(string name, Vector2 size)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Size = size;

            var Opts = WindowOptions.Default;
            Opts.Size = Size.ToSilkV2();
            Opts.Title = Name;

            GlfwAPI = Glfw.GetApi();

            if(!GlfwAPI.Init())
            {
                GlfwAPI.Terminate();
                byte* desc = (byte*)IntPtr.Zero;
                var errCode = GlfwAPI.GetError(out desc);
                throw new Exception("GLFW API could not be initialized. ERRCODE: "+errCode);
            }

            GlfwAPI.WindowHint(WindowHintBool.Resizable, true);
            GlfwAPI.WindowHint(WindowHintInt.ContextVersionMajor, 4);
            GlfwAPI.WindowHint(WindowHintInt.ContextVersionMinor, 5);
            GlfwAPI.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);

            _Handle = GlfwAPI.CreateWindow((int)size.X, (int)size.Y, Name, null, null);

            if(_Handle == null)
            {
                byte* desc = (byte*)IntPtr.Zero;
                var errCode = GlfwAPI.GetError(out desc);
                throw new Exception("Error while making GLFW window. WHND is Null. Error Code: "+errCode+" DESC: "+Marshal.PtrToStringAuto((IntPtr)desc));
            }

            _natHandle = new GlfwNativeWindow(GlfwAPI, _Handle);

            _glContext = new GlfwContext(GlfwAPI, _Handle);

            GlfwAPI.MakeContextCurrent(_Handle);
        }
    }
}
