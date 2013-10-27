using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;

namespace Pinball
{
    public class StorageManager : GameComponent
    {
        private static FileStream gameDataStream;

        private static XmlSerializer serializer;

        public static GameData GameDataInstance;

        public StorageManager(Game game)
            : base(game)
        {          
        }

        public static void SaveGameData()
        {

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {

                gameDataStream = store.OpenFile("gamedata.sav", FileMode.OpenOrCreate, FileAccess.Write);

                serializer = new XmlSerializer(typeof(GameData));

                serializer.Serialize(gameDataStream, GameDataInstance);

                gameDataStream.Close();
            }

            
        }

        public static void LoadGameData()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists("gamedata.sav"))
                {
                    gameDataStream = store.OpenFile("gamedata.sav", FileMode.Open, FileAccess.Read);

                    try
                    {
                        serializer = new XmlSerializer(typeof(GameData));
                        GameDataInstance = (GameData)serializer.Deserialize(gameDataStream);
                    }
                    catch (Exception)
                    {
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

                }
                else
                {
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
            }

            MediaPlayer.Volume = GameDataInstance.MusicVolumeOption;
            SoundManager.Volume = GameDataInstance.SoundEffectVolumeOption;
        }
    }

    public struct GameData
    {
        public List<Score> HighScores;
        public long JackpotAmount;
        public float MusicVolumeOption;
        public float SoundEffectVolumeOption;
    }
    

}