using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;

namespace AWGP3Squared.Interfaces
{
    interface IGameCommand : IModule
    {
        void LoadGame();
        void GameLoop();
        void Close();
        void SendData(Constant.enumMessage msgTypeID, Object[] data = null);
        void SendDataForObject<T>(Constant.enumMessage msgTypeID, String objectKey, T[] data);
        void SendDataForObject<T>(Constant.enumMessage msgTypeID, String objectKey, T data );
        void RequestData<T>(Constant.enumMessage msgTypeID, Constant.enumMessage recieveMsgTypeID, Object[] data = null);
        T[] RequestData<T>(Constant.enumMessage msgTypeID, Object[] data = null);
    }
}
