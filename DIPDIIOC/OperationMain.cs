using System.Configuration;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace DIPDIIOC
{
    public class OperationMain
    {
        private IMediaFile iMediaFile;
        private IPlayer iPlayer;
        public void PlayMedia()
        {
            //IMediaFile mf = new MediaFile() { FileName = "a.mp3" };
            //IPlayer player = new Player();
            //player.Play(mf);

            //
            //IMediaFile _mtype = (IMediaFile)Assembly.Load("DIPDIIOC").CreateInstance("DIPDIIOC.MediaFile");
            //_mtype.FileName = "1";
            //IPlayer _player = (IPlayer)Assembly.Load("DIPDIIOC").CreateInstance("DIPDIIOC.Player");
            //_player.Play(_mtype);

            //
            iMediaFile.FileName = "unity";
            iPlayer.Play(iMediaFile);



        }

        public OperationMain(IMediaFile _IMediaFile, IPlayer _IPlayer )
        {
            iMediaFile = _IMediaFile;
            iPlayer = _IPlayer;
        }

    }
}