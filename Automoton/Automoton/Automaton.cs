using Automoton.AssetManagement;
using Automoton.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Automoton
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Automaton : Game
    {
        public static Automaton Instance
        {
            get
            {
                if ( ReferenceEquals( null , _instance ) )
                    _instance = new Automaton();
                return _instance;
            }
        }

        private static Automaton _instance;

        public AssetLoader AssetLoader { get; }
        public IAssetManager AssetManager { get; }
        public IGraphicsManager GraphicsManager { get; }
        public IInputManager InputManager { get; }
        public IEventManager EventManager { get; }
        public IAudioManager AudioManager { get; }
        public IScreen Screen { get; }
        public IView View { get; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        private Automaton()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Configuration.LoadDefaults();
            AssetLoader = new AssetLoader( AssetLoaderMapFactory.GetBasicContentsMap() );
            AssetManager = new SimpleAssetManager(
                Path.Combine( Directory.GetCurrentDirectory() , "Content/AssetManifest" ),
                Services
                );
            GraphicsManager = new SimpleGraphicsManager();
            InputManager = new SimpleInputManager();
            EventManager = new SimpleEventManager();
            AudioManager = new SimpleAudioManager();
            Screen = new StartupScreen();
            View = new SimpleView( graphics );
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            AssetManager.LoadBatch( Configuration.Assets.SplashBatch );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            AssetManager.UnloadAll();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.PollInput( gameTime );
            EventManager.SendEvents( gameTime );
            Screen.Update( gameTime );
            Screen.BeginRender();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var task = Screen.AwaitRender();
            task.Wait();
            var renderTarget = task.Result;
            View.Draw( renderTarget );

            base.Draw(gameTime);
        }
    }
}
