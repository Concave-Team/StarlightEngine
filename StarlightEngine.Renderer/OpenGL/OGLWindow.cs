using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Windowing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.SDL;

namespace StarlightEngine.Renderer.OpenGL
{
    public class OGLWindow : Window
    {
        public Action<double> r_OnUpdate { get; set; }
        public Action<double> r_OnRender { get; set; }
        public Action r_OnLoad { get; set; }

        public string Name = "Undefined Window";
        public Vector2 Size = new Vector2(800,600);
        public IWindow _Handle { get; set; }
        public bool CurrentlyRunning = false;

        public override void Run()
        {
            CurrentlyRunning = true;
            _Handle.Run();
        }

        public override void Exit()
        {
            _Handle.Close();
        }

        public override void ModifyName(string name)
        {
            _Handle.Title = name;
        }

        public override void ModifySize(Vector2 size)
        {
            _Handle.Size = size.ToSilkV2();
        }

        internal void OnKeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            // PROCESS TO INPUT CHANNELS.

            // ALWAYS CLOSE WINDOW ON ESCAPE.

            if (arg2 == Key.Escape)
                this.Exit();
        }

        // Internal Load, user load function is run inside.
        private void _Handle_Load()
        {
            r_OnLoad();

            IInputContext input = _Handle.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += OnKeyDown;
            }
        }

        public OGLWindow(Action<double> r_OnUpdate, Action<double> r_OnRender, Action r_OnLoad, string name, Vector2 size)
        {
            this.r_OnUpdate = r_OnUpdate ?? throw new ArgumentNullException(nameof(r_OnUpdate));
            this.r_OnRender = r_OnRender ?? throw new ArgumentNullException(nameof(r_OnRender));
            this.r_OnLoad = r_OnLoad ?? throw new ArgumentNullException(nameof(r_OnLoad));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Size = size;

            var Opts = WindowOptions.Default;
            Opts.Size = Size.ToSilkV2();
            Opts.Title = Name;
            _Handle = Silk.NET.Windowing.Window.Create(Opts);

            _Handle.Load += _Handle_Load;
            _Handle.Render += _Handle_Render;
            _Handle.Update += this.r_OnUpdate;
        }

        private void _Handle_Render(double obj)
        {
            Console.WriteLine("RF");
            this.r_OnRender(obj);
        }

        public OGLWindow(string name, Vector2 size)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Size = size;

            var Opts = WindowOptions.Default;
            Opts.Size = Size.ToSilkV2();
            Opts.Title = Name;
            _Handle = Silk.NET.Windowing.Window.Create(Opts);
        }
    }
}
