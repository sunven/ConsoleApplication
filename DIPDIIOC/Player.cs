using System;

namespace DIPDIIOC
{
    public class Player: IPlayer
    {
        public void Play(IMediaFile imf)
        {
            Console.WriteLine(imf.FileName);
            Console.ReadKey();
        }
    }
}