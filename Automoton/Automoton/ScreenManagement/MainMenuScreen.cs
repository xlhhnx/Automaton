using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automoton.ScreenManagement
{
    class MainMenuScreen
    {
        private List<IControl> Controls { get; set; }
        private Task<RenderTarget2D> RenderTask { get; set; }
        private GraphicsDevice GraphicsDevice { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        private Background Background { get; set; }

        public MainMenuScreen()
        {
            Controls = new List<IControl>();
            this.GetControls();
            this.GetBackground();
            this.ResetGraphicsDevice();
        }

        private void GetBackground()
        {
            Background = Automaton.Instance.GraphicsManager.GetBackground( Configuration.MainMenu.BackgroundAssetId );
        }

        public void ResetGraphicsDevice()
        {
            GraphicsDevice = new GraphicsDevice( Automaton.Instance.View.GraphicsAdapter , GraphicsProfile.HiDef , Automaton.Instance.View.PresentationParameters.Clone() );
            SpriteBatch = new SpriteBatch( GraphicsDevice );
        }

        private void GetControls()
        {
            foreach ( var c in Configuration.MainMenu.ControlParameters )
                Controls.Add( ControlFactory.Create( c ) );
        }

        public void Update( GameTime gameTime )
        {
            Background.Update( gameTime );
            foreach ( var c in Controls )
                c.Update( gameTime );
        }

        public void Add( IControl control )
        {
            Controls.Add( control );
        }

        public void Remove( IControl control )
        {
            Controls.Remove( control );
        }

        public RenderTarget2D Render()
        {
            RenderTarget2D renderTarget = new RenderTarget2D( GraphicsDevice ,
                GraphicsDevice.PresentationParameters.BackBufferWidth ,
                GraphicsDevice.PresentationParameters.BackBufferHeight ,
                false ,
                GraphicsDevice.PresentationParameters.BackBufferFormat ,
                DepthFormat.Depth24
                );
            GraphicsDevice.SetRenderTarget( renderTarget );
            GraphicsDevice.Clear( Color.Black );
            Background.Render( SpriteBatch , renderTarget );
            foreach ( var c in Controls )
                c.Render( SpriteBatch , renderTarget );
            GraphicsDevice.SetRenderTarget( null );
            return renderTarget;
        }

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
