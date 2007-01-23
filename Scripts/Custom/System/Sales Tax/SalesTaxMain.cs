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
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Regions;

namespace Joeku
{
	public class SalesTaxMain
	{
		[CallPriority(99)]
		public static void Initialize()
		{
			Console.WriteLine();
			Console.WriteLine( "Taxes loading..." );
			if ( !System.IO.File.Exists( "Data/Taxes.xml" ) )
			{
				Console.WriteLine( "Error: Data/Taxes.xml does not exist" );
				Console.WriteLine();
				return;
			}

			XmlDocument doc = new XmlDocument();
			doc.Load( System.IO.Path.Combine( Core.BaseDirectory, "Data/Taxes.xml" ) );

			XmlElement root = doc["Taxes"];

			if ( root == null )
			{
				Console.WriteLine( "Could not find root element 'Taxes' in Taxes.xml" );
				Console.WriteLine();
				return;
			}
			else
			{
				foreach ( XmlElement facet in root.SelectNodes( "Facet" ) )
				{
					Map map = null;
					if ( Region.ReadMap( facet, "name", ref map ) )
					{
						if ( map == Map.Internal )
							Console.WriteLine( "   Invalid internal map in a facet element" );
						else
						{
							Console.WriteLine( "   {0} towns:", map.Name );
							LoadTaxes( facet, map );
						}
					}
				}
			}
			Console.WriteLine( "Taxes loading done." );
			Console.WriteLine();
		}

		public static void LoadTaxes( XmlElement xml, Map map )
		{
			Dictionary<string, int> towns = new Dictionary<string, int>();
			foreach ( XmlElement town in xml.SelectNodes( "Town" ) )
			{
				string name = null;
				int tax = 0;

				Region.ReadString( town, "name", ref name, false );
				Region.ReadInt32( town, "tax", ref tax, false );

				if( name != null && tax > 0 )
				{
					try{ towns.Add( name, tax ); }
					catch( Exception e ){ Console.WriteLine("Taxes error: {0}", e.Message); }
				}
			}

			List<Region> regions = Server.Region.Regions;
			int tempInt = 0;
			for( int i = 0; i < regions.Count; i++ )
			{
				if( !(regions[i] is TownRegion) || regions[i].Map != map )
					continue;
				if( towns.TryGetValue( regions[i].Name, out tempInt ) )
				{
					((TownRegion)regions[i]).Tax = tempInt;
					Console.WriteLine( "      '{0}' has been added, with a tax of {1}%.", regions[i].Name, tempInt );
				}
			}
		}
	}
}