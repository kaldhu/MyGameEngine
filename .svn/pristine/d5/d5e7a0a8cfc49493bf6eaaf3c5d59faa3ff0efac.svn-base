using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Components
{
    class ComponentType :IComponentType
    {
        //List of components Constant;

        private static List<ComponentType> m_InstanceList{ get; set; }
        public Constant.enumComponent m_ComponentTypeID { get; private set; }

        protected ComponentType(Constant.enumComponent iD)
        {
            m_ComponentTypeID = iD;
        }
        
        public static ComponentType Instance(Constant.enumComponent iD)
        {
            if (m_InstanceList == null)
            {
                m_InstanceList = new List<ComponentType>();
            }
            if (!m_InstanceList.Any(x=>x.m_ComponentTypeID ==iD))
            {
                m_InstanceList.Add(new ComponentType(iD));
            }

            return m_InstanceList.FirstOrDefault(x=>x.m_ComponentTypeID==iD);
        }
      
    }
}
