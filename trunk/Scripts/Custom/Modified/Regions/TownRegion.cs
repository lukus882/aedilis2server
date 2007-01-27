/**************************************
*Script Name: NPC Sales Tax           *
*Author: Joeku AKA Demortris          *
*For use with RunUO 2.0               *
*Client Tested with: 5.0.7.1          *
*Version: 1.02                        *
*Initial Release: 01/06/07            *
*Revision Date: 01/07/07              *
**************************************/

using System;
using System.Xml;
using Server;

namespace Server.Regions
{
	public class TownRegion : GuardedRegion
	{
		private int m_Tax;
		public int Tax{ get{ return m_Tax; } set{ m_Tax = value; } }

		public TownRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
		}
	}
}