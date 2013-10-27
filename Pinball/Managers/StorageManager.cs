using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;

namespace Pinball
{
    public class StorageManager : GameComponent
    {
        private static StorageDevice device;

        private static StorageContainer container;

        private static FileStream gameDataStream;

        private static XmlSerializer serializer;

        public static GameData GameDataInstance;

        public StorageManager(Game game)
            : base(game)
        {
            IAsyncResult result = Guide.BeginShowStorageDeviceSelector(null, null);
            while (!result.IsCompleted) { }
            device = Guide.EndShowStorageDeviceSelector(result);
          
        }

        public static void SaveGameData()
        {
            if (device != null)
            {
                container = device.OpenContainer("Pinball");

                gameDataStream = File.Open(Path.Combine(container.Path, "gamedata.sav"), FileMode.Create, FileAccess.ReadWrite);

                serializer = new XmlSerializer(typeof(GameData));

                serializer.Serialize(gameDataStream, GameDataInstance);

                gameDataStream.Close();

                container.Dispose();
            }

            
        }

        public static void LoadGameData()
        {
            if (device != null)
            {
                container = device.OpenContainer("Pinball");

                //File.Delete(Path.Combine(container.Path, "highscores.sav"));

                gameDataStream = File.Open(Path.Combine(container.Path, "gamedata.sav"), FileMode.OpenOrCreate, FileAccess.ReadWrite);

                try
                {
                    serializer = new XmlSerializer(typeof(GameData));
                    GameDataInstance = (GameData)serializer.Deserialize(gameDataStream);
                }
                catch (Exception)
                {
                    GameDataInstance.ControlOption = FlipperStyles.DPad;
                    GameDataInstance.TouchOption = (ZunePadState.TouchablePad ? true : false);
                    GameDataInstance.MusicVolumeOption = 1.0f;
                    GameDataInstance.SoundEffectVolumeOption = 0.5f;
                    GameDataInstance.HighScores = new List<Score>(5);
                    GameDataInstance.JackpotAmount = 1000L;

                    Score zero = new Score();
                    zero.Intials = "___";
                    zero.Number = 0;

                    for (int i = 0; i < 5; i++)
                    {
                        GameDataInstance.HighScores.Add(zero);
                    }
                }

                gameDataStream.Close();

                container.Dispose();

            }
            else
            {
                GameDataInstance.ControlOption = FlipperStyles.DPad;
                GameDataInstance.TouchOption = (ZunePadState.TouchablePad ? true : false);
                GameDataInstance.MusicVolumeOption = 1.0f;
                GameDataInstance.SoundEffectVolumeOption = 0.5f;
                GameDataInstance.HighScores = new List<Score>(5);
                GameDataInstance.JackpotAmount = 1000L;

                Score zero = new Score();
                zero.Intials = "___";
                zero.Number = 0;

                for (int i = 0; i < 5; i++)
                {
                    GameDataInstance.HighScores.Add(zero);
                }
            }

            PlayerInput.CurrentStyle = GameDataInstance.ControlOption;
            InputState.Touch = StorageManager.GameDataInstance.TouchOption;
            MediaPlayer.Volume = GameDataInstance.MusicVolumeOption;
            SoundManager.Volume = GameDataInstance.SoundEffectVolumeOption;
        }
    }

    [Serializable]
    public struct GameData
    {
        public List<Score> HighScores;
        public long JackpotAmount;
        public FlipperStyles ControlOption;
        public bool TouchOption;
        public float MusicVolumeOption;
        public float SoundEffectVolumeOption;
    }
    

}