using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pacman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly Map Map = new();
        private Player Player;
        private List<Enemy> Enemies = new();
        private List<Position> Food = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 760;
            _graphics.PreferredBackBufferHeight = 840;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureHandler.LoadTextures(Content);

            Map.LoadFromFile("Content\\Level 1.map", ref Enemies, ref Player, ref Food);

            Player.Animation.Play = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Player.Update(Map, Food, Enemies);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(Player, Map);
            }

            Window.Title = "PACMAN   |   Lives: " + Player.Lives;

            if (Food.Count == 0 || Player.Lives <= 0)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Map.Draw(_spriteBatch);

            Player.Draw(_spriteBatch);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(_spriteBatch);
            }

            foreach (Position fruit in Food)
            {
                _spriteBatch.Draw(TextureHandler.Food, fruit.Raw, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}