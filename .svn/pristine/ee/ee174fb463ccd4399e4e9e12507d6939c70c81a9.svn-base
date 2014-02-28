using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWGP3Squared.Helper;

namespace AWGP3Squared.Interfaces
{
    interface IScriptController
    {
        List<IScript> scriptList { get; }
        void AddScript(int gameObjectID, IScript[] scriptArray);
        IGameObject[] RequestGameObject(String[] data);
        IGameObject RequestGameObject(String data);


        void SendData(Constant.enumMessage msgTypeID, Object[] data = null);
        void SendDataForObject<T>(Constant.enumMessage msgTypeID, String objectKey, T[] data);
        void SendDataForObject<T>(Constant.enumMessage msgTypeID, String objectKey, T data);
        T[] RequestData<T>(Constant.enumMessage msgTypeID, Object[] data = null);

    }
}
