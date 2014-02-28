using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Core
{
    class Observer : IObserver
    {
        private List<IModule> m_Observers { get; set; }
        private static IObserver m_Instance  { get; set; }

        protected Observer()
        {
            m_Observers = new List<IModule>();
        }
        public static IObserver Instance()
        {
            if (m_Instance == null)
            {
                m_Instance = new Observer();
            }

            return m_Instance;
        }

        public bool Attach(IModule module)
        {
            if (!m_Observers.Contains(module))
            {
                m_Observers.Add(module);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Dettach(IModule module)
        {
            if (m_Observers.Contains(module))
            {
                m_Observers.Remove(module);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Notify(Boolean firstPass)
        {
            foreach(IModule module in m_Observers)
            {
                module.Notify(firstPass);
            }

        }
    }
}
