using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Core;
using AWGP3Squared.Helper;


namespace AWGP3Squared.Interfaces
{
    interface IPostOffice
    {

        //private  List<IMessage> m_POBox;

        //private static PostOffice instance;
        // Protected PostOffice(); 
        // static Postoffice Instance();
        // these must be implemented into PostOffice for singleton

        void init();

        IObserver m_Observer{get;}
        Dictionary<Constant.enumModuleID, List<IMessage>> m_LetterBoxes { get; }
        void AddModule(IModule module);
        
        void LockMessageType(Constant.enumMessage msgTypeIDArray);
        void UnLockMessageType(Constant.enumMessage msgTypeIDArray);
        void SendMessages(IMessage[] msg);
        void SendMessage(Constant.enumMessage msgTypeID, Object[] data = null, Boolean command = true);
        //void SendMessage(Constant.enumMessage msgTypeID, Boolean command = true, Object[] data = null,Boolean split = false);
        //void SendMessage(Constant.enumMessage msgTypeID, Boolean command, Object data);

        //void SendMessages(IMessage[] msg);
        //void SendMessage(Constant.enumMessage msgTypeID, Boolean command = false, Object[] data = null);
        //void RemoveMessage(Constant.enumMessage msgTypeID, Constant.enumModuleID ModuleID);
        void RemoveMessage(Constant.enumMessage msgTypeID, Constant.enumModuleID ModuleID, Boolean command = true);
        
        void CheckMessages(Constant.enumModuleID ModuleID, Constant.enumMessage[] msgIDList);

        IMessage[] GetMessages(Constant.enumModuleID letterBoxID, Constant.enumMessage[] msgIDList, Boolean? restirct = null);


        Object[] GetMessageObjects(Constant.enumModuleID letterBoxID, Constant.enumMessage msgID);
        Object[] GetMessageObjects(Constant.enumModuleID letterBoxID, Constant.enumMessage[] msgIDArray);

        Constant.enumMessage[] getMessageTypeIDs(Constant.enumModuleID letterBoxID);

    }
}