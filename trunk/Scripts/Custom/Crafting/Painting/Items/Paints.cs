/*
 created by:Bad Karma aka Broadside

  			        BBBBB        A      DDDDD  
  			       BB  BB      AAA     DDDDDD
  			      BB  BB     AA AA    DD   DD
 			     BBBBBB	   AA   AA   DD   DD
  		    	BBBBB     AAAAAAA   DD   DD
   		       BB  BB    AAAAAAA   DD   DD
   		      BB  BB    AA   AA   DDDDDD
		     BBBBBB    AA   AA   DDDDD

			        KK   KK       A      RRRRRR    MM   MM      A
			       KK  KK       AAA     RR   RR   MMM MMM     AAA
			      KK KK       AA AA    RR   RR   MMMMMMM    AA AA
			     KKKK       AA   AA   RRRRRR    MM M MM   AA   AA
			    KKKK       AAAAAAA   RRRR      MM   MM   AAAAAAA
			   KK KK      AAAAAAA   RR RR     MM   MM   AAAAAAA
			  KK   KK    AA   AA   RR  RR    MM   MM   AA   AA
			 KK     KK  AA   AA   RR   RR   MM   MM   AA   AA
 */
using System;
using Server;
using Server.Items; 

namespace Server.Items
{
	public class RedPaint : Item
	{

		[Constructable]
		public RedPaint() : this( 5 )
		{
		}

		[Constructable]
		public RedPaint( int amount ) : base( 0x1006 )
		{
			Name = "Red Paint";
			Stackable = true;
			Hue = 0x21;
			Weight = 1.0;
			Amount = amount;
		}

		public RedPaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class YellowPaint : Item
	{

		[Constructable]
		public YellowPaint() : this( 5 )
		{
		}

		[Constructable]
		public YellowPaint( int amount ) : base( 0x1006 )
		{
			Name = "Yellow Paint";
			Stackable = true;
			Hue = 0x38;
			Weight = 1.0;
			Amount = amount;
		}

		public YellowPaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class WhitePaint : Item
	{

		[Constructable]
		public WhitePaint() : this( 5 )
		{
		}

		[Constructable]
		public WhitePaint( int amount ) : base( 0x1006 )
		{
			Name = "White Paint";
			Stackable = true;
			Hue = 0x481;
			Weight = 1.0;
			Amount = amount;
		}

		public WhitePaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class BluePaint : Item
	{

		[Constructable]
		public BluePaint() : this( 5 )
		{
		}

		[Constructable]
		public BluePaint( int amount ) : base( 0x1006 )
		{
			Name = "Blue Paint";
			Stackable = true;
			Hue = 0x5;
			Weight = 1.0;
			Amount = amount;
		}

		public BluePaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class BlackPaint : Item
	{

		[Constructable]
		public BlackPaint() : this( 5 )
		{
		}

		[Constructable]
		public BlackPaint( int amount ) : base( 0x1006 )
		{
			Name = "Black Paint";
			Stackable = true;
			Hue = 0x455;
			Weight = 1.0;
			Amount = amount;
		}

		public BlackPaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class GreenPaint : Item
	{

		[Constructable]
		public GreenPaint() : this( 5 )
		{
		}

		[Constructable]
		public GreenPaint( int amount ) : base( 0x1006 )
		{
			Name = "Green Paint";
			Stackable = true;
			Hue = 0x42;
			Weight = 1.0;
			Amount = amount;
		}

		public GreenPaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class BrownPaint : Item
	{

		[Constructable]
		public BrownPaint() : this( 5 )
		{
		}

		[Constructable]
		public BrownPaint( int amount ) : base( 0x1006 )
		{
			Name = "Brown Paint";
			Stackable = true;
			Hue = 0x21E;
			Weight = 1.0;
			Amount = amount;
		}

		public BrownPaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class PurplePaint : Item
	{

		[Constructable]
		public PurplePaint() : this( 5 )
		{
		}

		[Constructable]
		public PurplePaint( int amount ) : base( 0x1006 )
		{
			Name = "Purple Paint";
			Stackable = true;
			Hue = 0x10;
			Weight = 1.0;
			Amount = amount;
		}

		public PurplePaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class PinkPaint : Item
	{

		[Constructable]
		public PinkPaint() : this( 5 )
		{
		}

		[Constructable]
		public PinkPaint( int amount ) : base( 0x1006 )
		{
			Name = "Pink Paint";
			Stackable = true;
			Hue = 0x483;
			Weight = 1.0;
			Amount = amount;
		}

		public PinkPaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	public class OrangePaint : Item
	{

		[Constructable]
		public OrangePaint() : this( 5 )
		{
		}

		[Constructable]
		public OrangePaint( int amount ) : base( 0x1006 )
		{
			Name = "Orange Paint";
			Stackable = true;
			Hue = 0x2B;
			Weight = 1.0;
			Amount = amount;
		}

		public OrangePaint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		}
	
	}
