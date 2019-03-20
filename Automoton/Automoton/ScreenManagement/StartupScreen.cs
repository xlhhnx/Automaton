using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.ScreenManagement
{
    class StartupScreen
    {
        private Task LoadingTask { get; set; }
        private Task<RenderTarget2D> RenderTask { get; set; }

        public StartupScreen()
        {
            Automaton.Instance.View.Hide();
            LoadingTask = Task.Run( () => Automaton.Instance.AssetManager.LoadBatch( Configuration.Assets.MainMenuBatchId ) );
        }

        public void Update( GameTime gameTime )
        {
            var status = LoadingTask.Status;
            if ( status == TaskStatus.Faulted || status == TaskStatus.Canceled )
                Automaton.Instance.Exit();

            if ( status == TaskStatus.RanToCompletion )
                Automaton.Instance.Screen = new MainMenuScreen();
        }

        public RenderTarget2D Render() =>
            default( RenderTarget2D );

        public void BeginRender()
        {
            RenderTask = Task.Run( () => Render() );
        }

        public async Task<RenderTarget2D> AwaitRender()
        {
            if ( RenderTask is null )
                BeginRender();

            return await RenderTask;
        }
    }
}
